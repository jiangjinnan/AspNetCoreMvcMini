using System.Collections.Generic;

namespace Mvc
{
public interface IActionDescriptorCollectionProvider
{
    IReadOnlyList<ActionDescriptor> ActionDescriptors { get; }
}
}
