using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;

using CarShop.Data;
using CarShop.Services;

namespace CarShop
{
    public class StartUp
    {
        public static async Task Main()
            => await HttpServer.WithRoutes(routes => routes
                .MapStaticFiles()
                .MapControllers())
            .WithServices(services => services
                .Add<IViewEngine, CompilationViewEngine>()
                .Add<IValidator,Validator>()
                .Add<IPasswordHasher,PasswordHasher>()
                .Add<CarShopDbContex>())
            .WithConfiguration<CarShopDbContex>(context=> context.Database.Migrate())
            .Start();
    }
}
