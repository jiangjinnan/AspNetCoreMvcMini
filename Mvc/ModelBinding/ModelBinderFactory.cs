using System.Collections.Generic;
using System.Linq;

namespace Mvc
{
public class ModelBinderFactory : IModelBinderFactory
{
    private readonly IEnumerable<IModelBinderProvider> _providers;
    public ModelBinderFactory(IEnumerable<IModelBinderProvider> providers)
    => _providers = providers;

    public IModelBinder CreateBinder(ModelMetadata metadata)
    {
        foreach (var provider in _providers)
        {
            var binder = provider.GetBinder(metadata);
            if (binder != null)
            {
                return binder;
            }
        }
        return null;
    }
}
}
