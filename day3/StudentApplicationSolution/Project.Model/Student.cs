using System;

namespace Student.Model
{
    public class Student
    {

    public Student() { }

    public Student(int student_id, string first_name, string last_name, int adress_id)
        {
            student_id = student_id;
            first_name = first_name;
            last_name = last_name;
            adress_id = adress_id;
        }

        public int student_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int adress_id { get; set; }

    }
}
