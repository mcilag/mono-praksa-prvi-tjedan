using System;
using StudentModel.Common;

namespace Student.Model
{
    public class Students : IStudent
    {

        public int Student_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public int Adress_id { get; set; }

    }
}
