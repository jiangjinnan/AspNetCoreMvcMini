using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using System;
using System.Collections.Generic;

namespace Mvc
{
internal struct ConventionalRouteEntry
{
    public RoutePattern Pattern { get; }
    public string RouteName;
    public RouteValueDictionary DataTokens { get; }
    public int Order { get; }
    public IReadOnlyList<Action<EndpointBuilder>> Conventions { get; }
    public ConventionalRouteEntry(string routeName, string pattern, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, int order, List<Action<EndpointBuilder>> conventions)
    {
        RouteName = routeName;
        DataTokens = dataTokens;
        Order = order;
        Conventions = conventions;
        Pattern = RoutePatternFactory.Parse(pattern, defaults, constraints);
    }
}
}
