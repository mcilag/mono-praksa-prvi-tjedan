using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using Student._Repository;
using System.Threading.Tasks;


namespace Student._Service
{
    public class AdressService
    {
        AdressRepository adressRepository = new AdressRepository();
        public async Task<List<Adress>> GetAdressesAsync()
        {
            return await adressRepository.GetAdressesAsync();

        }

        public async Task<Adress> GetAdressByIdAsync(int id)
        {
            return await adressRepository.GetAdressByIdAsync(id);
        }

        public async Task<bool> CreateAdressAsync(Adress adress)
        {
            return await adressRepository.CreateAdressAsync(adress);
        }

        public async Task<bool> UpdateAdressAsync(int id, Adress adress)
        {
            return await adressRepository.UpdateAdressAsync(id, adress);
        }

        public async Task<bool> DeleteAdressAsync(int id)
        {
            return await adressRepository.DeleteAdressAsync(id);
        }
    }
}