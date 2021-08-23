using System;

namespace Student.Model
{
    public class Students : StudentModel.Common.IStudentModel
    {

        public Students() { }

        public Students(int _student_id, string _first_name, string _last_name, int _adress_id)
        {
            Student_id = _student_id;
            First_name = _first_name;
            Last_name = _last_name;
            Adress_id = _adress_id;
        }

        public int Student_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public int Adress_id { get; set; }

    }
}
