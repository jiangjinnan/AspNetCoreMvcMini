using System.Collections.Generic;
using System.Linq;

namespace Mvc
{
public class CompositeValueProvider : IValueProvider
{
    private readonly IEnumerable<IValueProvider> _providers;
    public CompositeValueProvider(IEnumerable<IValueProvider> providers) => _providers = providers;
    public bool ContainsPrefix(string prefix) => _providers.Any(it => it.ContainsPrefix(prefix));
    public bool TryGetValues(string name, out string[] value)
    {
        foreach (var provider in _providers)
        {
            if (provider.TryGetValues(name, out value))
            {
                return true;
            }
        }
        return (value = null) != null;
    }
}
}
