using System;
using System.Reflection;

namespace Mvc
{
public class ControllerActionDescriptor : ActionDescriptor
{
    public Type ControllerType { get; set; }
    public MethodInfo Method { get; set; }
}
}
