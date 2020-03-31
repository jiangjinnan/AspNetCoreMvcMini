using Microsoft.AspNetCore.Http;

namespace Mvc
{
    public abstract class Controller
    {
        public ActionContext  ActionContext { get; internal set; }
    }
}
