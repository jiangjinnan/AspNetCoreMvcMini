namespace Mvc
{
public interface IActionInvokerFactory
{
    IActionInvoker CreateInvoker(ActionContext actionContext);
}
}
