using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mvc
{
public class ControllerActionInvoker : IActionInvoker
{
    private static readonly MethodInfo _taskConvertMethod;

    private static readonly MethodInfo _valueTaskConvertMethod;
    public ActionContext ActionContext { get; }
    static ControllerActionInvoker()
    {
        var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
        _taskConvertMethod = typeof(ControllerActionInvoker).GetMethod(nameof(ConvertFromTaskAsync), bindingFlags);
        _valueTaskConvertMethod = typeof(ControllerActionInvoker).GetMethod(nameof(ConvertFromValueTaskAsync), bindingFlags);
    }
    public ControllerActionInvoker(ActionContext actionContext)
    {
        ActionContext = actionContext;
    }
    public async Task InvokeAsync()
    {
        var actionDescriptor = (ControllerActionDescriptor)ActionContext.ActionDescriptor;
        var controllerType = actionDescriptor.ControllerType;
        var requestServies = ActionContext.HttpContext.RequestServices;
        var controllerInstance = ActivatorUtilities.CreateInstance(requestServies, controllerType);
        if (controllerInstance is Controller controller)
        {
            controller.ActionContext = ActionContext;
        }
        var actionMethod = actionDescriptor.Method;
        var arguments = await BindArgumentsAsync(actionMethod);
        var returnValue = actionMethod.Invoke(controllerInstance, arguments);
        var mapper = requestServies.GetRequiredService<IActionResultTypeMapper>();
        var actionResult = await ToActionResultAsync(returnValue, actionMethod.ReturnType, mapper);
        await actionResult.ExecuteResultAsync(ActionContext);
    }

    private async Task<object[]> BindArgumentsAsync(MethodInfo methodInfo)
    {
        var parameters = methodInfo.GetParameters();
        if (parameters.Length == 0)
        {
            return new object[0];
        }
        var arguments = new object[parameters.Length];
        for (int index = 0; index < arguments.Length; index++)
        {
            var parameter = parameters[index];
            var metadata = ModelMetadata.CreateByParameter(parameter);
            var requestServices = ActionContext.HttpContext.RequestServices;
            var valueProviderFactories = requestServices.GetServices<IValueProviderFactory>();
            var modelBinderFactory = requestServices.GetRequiredService<IModelBinderFactory>();
            var valueProvider = new CompositeValueProvider(valueProviderFactories.Select(it => it.CreateValueProvider(ActionContext)));
            var context = valueProvider.ContainsPrefix(parameter.Name)
                ? new ModelBindingContext(ActionContext, parameter.Name, metadata, valueProvider)
                : new ModelBindingContext(ActionContext, "", metadata, valueProvider);
            var binder = modelBinderFactory.CreateBinder(metadata);
            await binder.BindAsync(context);
            arguments[index] = context.Model;
        }
        return arguments;
    }

    private Task<IActionResult> ToActionResultAsync(object returnValue, Type returnType, IActionResultTypeMapper mapper)
    {
        //Null
        if (returnValue == null || returnType == typeof(Task) || returnType == typeof(ValueTask))
        {
            return Task.FromResult<IActionResult>(NullActionResult.Instance);
        }

        //IActionResult
        if (returnValue is IActionResult actionResult)
        {
            return Task.FromResult(actionResult);
        }

        //Task<TResult>
        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            var declaredType = returnType.GenericTypeArguments.Single();
            var taskOfResult = _taskConvertMethod.MakeGenericMethod(declaredType).Invoke(null, new object[] { returnValue, mapper });
            return (Task<IActionResult>)taskOfResult;
        }

        //ValueTask<TResult>
        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ValueTask<>))
        {
            var declaredType = returnType.GenericTypeArguments.Single();
            var valueTaskOfResult = _valueTaskConvertMethod.MakeGenericMethod(declaredType).Invoke(null, new object[] { returnValue, mapper });
            return (Task<IActionResult>)valueTaskOfResult;
        }

        return Task.FromResult(mapper.Convert(returnValue, returnType));
    }

    private static async Task<IActionResult> ConvertFromTaskAsync<TValue>(Task<TValue> returnValue, IActionResultTypeMapper mapper)
    {
        var result = await returnValue;
        return result is IActionResult actionResult
            ? actionResult
            : mapper.Convert(result, typeof(TValue));
    }

    private static async Task<IActionResult> ConvertFromValueTaskAsync<TValue>(ValueTask<TValue> returnValue, IActionResultTypeMapper mapper)
    {
        var result = await returnValue;
        return result is IActionResult actionResult
            ? actionResult
            : mapper.Convert(result, typeof(TValue));
    }
}
}