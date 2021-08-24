using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StudentApplication.Controllers;
using Autofac;
using StudentModel.Common;
using Student.Model;
using StudentRepository.Common;
using Student._Repository;
using StudentService.Common;
using Student._Service;
using Autofac.Integration.WebApi;

namespace StudentApplication
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterType<Students>().As<IStudent>();
            builder.RegisterType<Adress>().As<IAdress>();

            builder.RegisterType<Student_Repository>().As<IStudentRepository>();
            builder.RegisterType<AdressRepository>().As<IAdressRepository>();

            builder.RegisterType<Student_Service>().As<IStudentService>();
            builder.RegisterType<AdressService>().As<IAdressService>();

            builder.RegisterModule<RepositoryDIModule>();
            builder.RegisterModule<ServiceDIModule>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
