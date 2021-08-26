using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using StudentApplication.Common;


namespace StudentService.Common
{
    public interface IStudentService
    {
        Task<List<Students>> GetStudentsAsync(Sorter sorter, Pager pager, StudentFilter studentFilter);
        Task<Students> GetStudentByIdAsync(int id);
        Task<bool> CreateStudentAsync(Students student);
        Task<bool> UpdateStudentAsync(int id, Students student);
        Task<bool> DeleteStudentAsync(int id);
    }
    public interface IAdressService
    { 
        Task<List<Adress>> GetAdressesAsync(Sorter sorter, Pager pager, AdressFilter adressFilter);
        Task<Adress> GetAdressByIdAsync(int id);
        Task<bool> CreateAdressAsync(Adress adress);
        Task<bool> UpdateAdressAsync(int id, Adress adress);
        Task<bool> DeleteAdressAsync(int id);
    }
}
