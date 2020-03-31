using System;

namespace Mvc
{
public class ActionResultTypeMapper : IActionResultTypeMapper
{
    public IActionResult Convert(object value, Type returnType)
    {
        return value is IActionResult actionResult
            ? actionResult
            : new ContentResult(value.ToString(), "text/plain");
    }
}
}
