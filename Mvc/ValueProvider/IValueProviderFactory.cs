using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc
{
public interface IValueProviderFactory
{
    IValueProvider CreateValueProvider(ActionContext actionContext);
}
}
