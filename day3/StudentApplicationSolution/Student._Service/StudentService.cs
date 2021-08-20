using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using Student._Repository;



namespace Student._Service
{
    public class StudentService
    {
        StudentRepository studentRepository = new StudentRepository();
        public List<Students> GetStudents()
        {
            return studentRepository.GetStudents();

        }

        public Students GetStudentById(int id)
        {
            return studentRepository.GetStudentById(id);
        }

        public bool CreateStudent(Students student)
        {
            return studentRepository.CreateStudent(student);
        }

        public bool UpdateStudent(int id, Students student)
        {
            return studentRepository.UpdateStudent(id, student);
        }

        public bool DeleteStudent(int id)
        {
            return studentRepository.DeleteStudent(id);
        }
    }
}