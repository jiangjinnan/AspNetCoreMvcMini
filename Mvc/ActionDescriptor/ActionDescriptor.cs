using System.Collections.Generic;

namespace Mvc
{
    public class ActionDescriptor
    {
        public AttributeRouteInfo AttributeRouteInfo { get; set; }
        public IDictionary<string, string> RouteValues { get; set; }
    }
}
