using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Mvc
{
public class ActionContext
{
    public ActionDescriptor ActionDescriptor {  get; set; }
    public HttpContext HttpContext { get; set; }
} 
}