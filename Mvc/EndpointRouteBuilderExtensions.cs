using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Mvc
{
public static class EndpointRouteBuilderExtensions
{
    public static ControllerActionEndpointConventionBuilder MapMvcControllers(this IEndpointRouteBuilder endpointBuilder)
    {
        var endpointDatasource = endpointBuilder.ServiceProvider.GetRequiredService<ControllerActionEndpointDataSource>();
        endpointBuilder.DataSources.Add(endpointDatasource);
        return endpointDatasource.DefaultBuilder;
    }

    public static ControllerActionEndpointConventionBuilder MapMvcControllerRoute(this IEndpointRouteBuilder endpointBuilder, string name, string pattern, RouteValueDictionary defaults = null, RouteValueDictionary constraints = null, RouteValueDictionary dataTokens = null)
    {
        var endpointDatasource = endpointBuilder.ServiceProvider.GetRequiredService<ControllerActionEndpointDataSource>();
        endpointBuilder.DataSources.Add(endpointDatasource);
        return endpointDatasource.AddRoute(name, pattern, defaults, constraints, dataTokens);
    }
}
}
