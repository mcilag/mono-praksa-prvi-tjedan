using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModel.Common
{
    public interface IStudentModel
    {
        int Student_id { get; set; }
        string First_name { get; set; }
        string Last_name { get; set; }
        int Adress_id { get; set; }
    }
}
