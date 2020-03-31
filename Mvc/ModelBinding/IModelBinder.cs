using System.Threading.Tasks;

namespace Mvc
{
public interface IModelBinder
{
    public Task BindAsync(ModelBindingContext context);
}
}
