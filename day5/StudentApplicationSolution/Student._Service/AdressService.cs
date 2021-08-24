using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using Student._Repository;
using StudentRepository.Common;
using StudentService.Common;


namespace Student._Service
{
    public class AdressService: IAdressService
    {
        IAdressRepository _adressRepository;

        public AdressService(IAdressRepository adressRepository)
        {
            _adressRepository = adressRepository;
        }

        public async Task<List<Adress>> GetAdressesAsync()
        {
            return await _adressRepository.GetAdressesAsync();

        }

        public async Task<Adress> GetAdressByIdAsync(int id)
        {
            return await _adressRepository.GetAdressByIdAsync(id);
        }

        public async Task<bool> CreateAdressAsync(Adress adress)
        {
            return await _adressRepository.CreateAdressAsync(adress);
        }

        public async Task<bool> UpdateAdressAsync(int id, Adress adress)
        {
            return await _adressRepository.UpdateAdressAsync(id, adress);
        }

        public async Task<bool> DeleteAdressAsync(int id)
        {
            return await _adressRepository.DeleteAdressAsync(id);
        }
    }
}