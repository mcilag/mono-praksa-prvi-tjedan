using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using Student.Repository;


namespace Student.Service
{
    public class StudentService
    {
        StudentRepository studentRepository = new StudentRepository();
        public List<Student> GetStudents()
        {
            return studentRepository.GetStudents();

        }

        public Student GetStudentById(int id)
        {
            return studentRepository.GetStudentById(id);
        }

        public void CreateStudent(Student student)
        {
            studentRepository.CreateStudent(student);
        }

        public bool UpdateStudent(int id, Student student)
        {
            return studentRepository.UpdateStudent(id, student);
        }

        public bool DeleteStudent(int id)
        {
            return studentRepository.DeleteStudent(id);
        }
    }
}