using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using Student._Repository;
using StudentService.Common;
using StudentRepository.Common;




namespace Student._Service 
{
    public class Student_Service : IStudentService
    {
        IStudentRepository _studentRepository;

        public Student_Service(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Students>> GetStudentsAsync()
        {
            return await _studentRepository.GetStudentsAsync();

        }

        public async Task<Students> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<bool> CreateStudentAsync(Students student)
        {
            return await _studentRepository.CreateStudentAsync(student);
        }

        public async Task<bool> UpdateStudentAsync(int id, Students student)
        {
            return await _studentRepository.UpdateStudentAsync(id, student);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteStudentAsync(id);
        }
    }
}