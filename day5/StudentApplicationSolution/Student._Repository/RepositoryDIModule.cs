using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRepository.Common;


namespace Student._Repository
{
    public class RepositoryDIModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Student_Repository>().As<IStudentRepository>();
            builder.RegisterType<AdressRepository>().As<IAdressRepository>();
        }
    }
}
