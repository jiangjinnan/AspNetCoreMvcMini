using System.Threading.Tasks;

namespace Mvc
{
public interface IActionResult
{
    Task ExecuteResultAsync(ActionContext context);
}
}
