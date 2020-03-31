namespace Mvc
{
public class SimpleTypeModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelMetadata metadata)
        => metadata.CanConvertFromString ? new SimpleTypeModelBinder() : null;
}
}
