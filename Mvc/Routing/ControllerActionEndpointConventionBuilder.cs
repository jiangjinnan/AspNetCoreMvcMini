using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;

namespace Mvc
{
public class ControllerActionEndpointConventionBuilder : IEndpointConventionBuilder
{
    private readonly List<Action<EndpointBuilder>> _conventions;
    public ControllerActionEndpointConventionBuilder(List<Action<EndpointBuilder>> conventions)
    {
        _conventions = conventions;
    }
    public void Add(Action<EndpointBuilder> convention) => _conventions.Add(convention);
}
}
