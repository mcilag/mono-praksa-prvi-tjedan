﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using StudentModel.Common;

namespace StudentRepository.Common
{
    public interface IStudentRepository
    {
        Task<List<Students>> GetStudentsAsync();
        Task<Students> GetStudentByIdAsync(int id);
        Task<bool> CreateStudentAsync(Students student);
        Task<bool> UpdateStudentAsync(int id, Students student);
        Task<bool> DeleteStudentAsync(int id);
        
    }

    public interface IAdressRepository
    {
        Task<List<Adress>> GetAdressesAsync();
        Task<Adress> GetAdressByIdAsync(int id);
        Task<bool> CreateAdressAsync(Adress adress);
        Task<bool> UpdateAdressAsync(int id, Adress adress);
        Task<bool> DeleteAdressAsync(int id);
    }
}
