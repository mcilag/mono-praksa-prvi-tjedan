using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student._Service;
using StudentService.Common;


namespace Student._Service
{
    public class ServiceDIModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Student_Service>().As<IStudentService>();
            builder.RegisterType<AdressService>().As<IAdressService>();
        }

    }
}
