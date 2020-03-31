namespace Mvc
{
public class QueryStringValueProviderFactory : IValueProviderFactory
{
    public IValueProvider CreateValueProvider(ActionContext actionContext) => new ValueProvider(actionContext.HttpContext.Request.Query);
}
}
