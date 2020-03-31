using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Mvc
{
public class ComplexTypeModelBinder : IModelBinder
{
    public async Task BindAsync(ModelBindingContext context)
    {
        var metadata = context.ModelMetadata;
        var model = Activator.CreateInstance(metadata.ModelType);
        foreach (var property in metadata.ModelType.GetProperties().Where(it => it.SetMethod != null))
        {
            var propertyMetadata = ModelMetadata.CreateByProperty(property);
            var binderFactory = context.ActionContext.HttpContext.RequestServices.GetRequiredService<IModelBinderFactory>();
            var binder = binderFactory.CreateBinder(propertyMetadata);
            var modelName = string.IsNullOrWhiteSpace(context.ModelName)
                ? property.Name
                : $"{context.ModelName}.{property.Name}";
            var propertyContext = new ModelBindingContext(context.ActionContext, modelName, propertyMetadata, context.ValueProvider);
            await binder.BindAsync(propertyContext);
            if (propertyContext.IsModelSet)
            {
                property.SetValue(model, propertyContext.Model);
            }
        }
        context.Bind(model);
    }
}
}
