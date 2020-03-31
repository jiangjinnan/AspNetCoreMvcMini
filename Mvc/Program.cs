using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mvc
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                .ConfigureServices(services => services
                        .AddRouting()
                        .AddMvcControllers())
                .Configure(app => app
                .UseDeveloperExceptionPage()
                    .UseRouting()
                    .UseEndpoints(endpoints => endpoints.MapMvcControllerRoute("default", "{controller}/{action}"))))
                .Build()
                .Run();
        }

    }

public class HomeController
{
    private static readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

    public string Action1(string foo, int bar, double baz)
    => JsonSerializer.Serialize(new { Foo = foo, Bar = bar, Baz = baz }, _options);

    public string Action2(Foobarbaz value1, Foobarbaz value2)
    => JsonSerializer.Serialize(new { Value1 = value1, Value2 = value2 }, _options);

    public string Action3(Foobarbaz value1, [FromBody]Foobarbaz value2)
    => JsonSerializer.Serialize(new { Value1 = value1, Value2 = value2 }, _options);
}

public class Foobarbaz
{
    public Foobar Foobar { get; set; }
    public double Baz { get; set; }
}

public class Foobar
{
    public string Foo { get; set; }
    public int Bar { get; set; }
}
}
