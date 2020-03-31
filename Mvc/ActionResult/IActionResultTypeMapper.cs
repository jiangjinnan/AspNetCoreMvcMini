using System;

namespace Mvc
{
public interface IActionResultTypeMapper
{
    IActionResult Convert(object value, Type returnType);
}
}