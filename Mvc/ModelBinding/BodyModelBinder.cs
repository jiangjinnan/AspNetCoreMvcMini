using System.Text.Json;
using System.Threading.Tasks;

namespace Mvc
{
public class BodyModelBinder : IModelBinder
{
    public async Task BindAsync(ModelBindingContext context)
    {
        var input = context.ActionContext.HttpContext.Request.Body;
        var result = await JsonSerializer.DeserializeAsync(input, context.ModelMetadata.ModelType);
        context.Bind(result);
    }
}
}
