using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ActiveCharts.Controllers;
using ActiveCharts.Services;
using ActiveCharts.Services.Interfaces;
using Autofac;
using Autofac.Integration.Mvc;

namespace ActiveCharts
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterFilterProvider();

            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<ObserveService>().As<IObserveService>().InstancePerRequest();
            builder.RegisterType<RenderService>().As<IRenderService>().InstancePerRequest();

            builder.RegisterControllers(typeof(BaseController).Assembly).PropertiesAutowired();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
