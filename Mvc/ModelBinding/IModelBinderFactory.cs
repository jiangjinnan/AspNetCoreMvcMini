namespace Mvc
{
public interface IModelBinderFactory
{
    IModelBinder CreateBinder(ModelMetadata metadata);
}
}
