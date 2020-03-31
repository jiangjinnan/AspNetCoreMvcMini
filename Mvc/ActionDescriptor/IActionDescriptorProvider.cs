using System.Collections.Generic;

namespace Mvc
{
public interface IActionDescriptorProvider
{
    IEnumerable<ActionDescriptor> ActionDescriptors { get; }
}
}
