using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Mvc
{
public class BodyModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelMetadata metadata)
    {
        return metadata.Parameter?.GetCustomAttribute<FromBodyAttribute>() == null
            ? null
            : new BodyModelBinder();                
    }
}
}
