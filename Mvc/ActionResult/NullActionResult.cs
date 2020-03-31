using System.Threading.Tasks;

namespace Mvc
{
public sealed class NullActionResult : IActionResult
{
    private NullActionResult() { }
    public static NullActionResult Instance { get; } = new NullActionResult();
    public Task ExecuteResultAsync(ActionContext context) => Task.CompletedTask;
}
}
