using Pastelaria.Web.Application.Teste;
using SimpleInjector;

namespace Pastelaria.Web
{
    public static class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {

            var container = new Container();

            container.Register<TesteApplication>();

            container.Verify();

            return container;
        }
    }
}