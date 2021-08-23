﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRepository.Common
{
    public interface IStudentRepository<Students>
    {
        Task<List<Students>> GetStudentsAsync();
        Task<Students> GetStudentByIdAsync(int id);
        Task<bool> CreateStudentAsync(Students student);
        Task<bool> UpdateStudentAsync(int id, Students student);
        Task<bool> DeleteStudentAsync(int id);
        
    }

    public interface IAdressRepository<Adress>
    {
        Task<List<Adress>> GetAdressesAsync();
        Task<Adress> GetAdressByIdAsync(int id);
        Task<bool> CreateAdressAsync(Adress adress);
        Task<bool> UpdateAdressAsync(int id, Adress adress);
        Task<bool> DeleteAdressAsync(int id);
    }
}