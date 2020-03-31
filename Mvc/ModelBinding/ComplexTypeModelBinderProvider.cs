using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Mvc
{
public class ComplexTypeModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelMetadata metadata)
    {
        if (metadata.CanConvertFromString)
        {
            return null;
        }
        return metadata.Parameter?.GetCustomAttribute<FromBodyAttribute>() == null
            ? new ComplexTypeModelBinder()
            : null;
    }
}
}
