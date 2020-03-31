namespace Mvc
{
public class HttpHeaderValueProviderFactory : IValueProviderFactory
{
    public IValueProvider CreateValueProvider(ActionContext actionContext) 
        => new ValueProvider(actionContext.HttpContext.Request.Headers);
}
}
