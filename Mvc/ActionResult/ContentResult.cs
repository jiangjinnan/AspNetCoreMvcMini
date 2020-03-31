using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Mvc
{
public class ContentResult : IActionResult
{
    private readonly string _content;
    private readonly string _contentType;
    public ContentResult(string content, string contentType)
    {
        _content = content;
        _contentType = contentType;
    }
    public Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = _contentType;
        return response.WriteAsync(_content);
    }
}
}
