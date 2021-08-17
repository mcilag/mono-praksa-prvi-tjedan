using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StudentApplication.Controllers;

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


            DictionaryofStudents.students.Add(1, "James Jackson");
            DictionaryofStudents.students.Add(2, "Marco Black");
            DictionaryofStudents.students.Add(3, "Steve James");
            DictionaryofStudents.students.Add(4, "Jane Doe");
            DictionaryofStudents.students.Add(5, "Josh Peterson");
            DictionaryofStudents.students.Add(6, "Mark Mark");
            DictionaryofStudents.students.Add(7, "Jason Jon");
            DictionaryofStudents.students.Add(8, "Martha South");
            DictionaryofStudents.students.Add(9, "Nick North");
            DictionaryofStudents.students.Add(10, "Amanda Nicholson");
            
        }
    }
}
