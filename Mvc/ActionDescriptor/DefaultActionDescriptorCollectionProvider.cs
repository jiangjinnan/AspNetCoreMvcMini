using System;
using System.Collections.Generic;
using System.Linq;

namespace Mvc
{
    public class DefaultActionDescriptorCollectionProvider : IActionDescriptorCollectionProvider
    {
        private readonly Lazy<IReadOnlyList<ActionDescriptor>> _accessor;
        public DefaultActionDescriptorCollectionProvider(IEnumerable<IActionDescriptorProvider> providers)
            => _accessor = new Lazy<IReadOnlyList<ActionDescriptor>>(() => providers.SelectMany(it => it.ActionDescriptors).ToList());
        public IReadOnlyList<ActionDescriptor> ActionDescriptors => _accessor.Value;
    }
}
