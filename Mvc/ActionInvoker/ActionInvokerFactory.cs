namespace Mvc
{
    public class ActionInvokerFactory : IActionInvokerFactory
    {
        public IActionInvoker CreateInvoker(ActionContext actionContext) => new ControllerActionInvoker(actionContext);
    }
}
