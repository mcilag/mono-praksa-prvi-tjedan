using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using Student._Repository;

namespace Student._Service
{
    public class AdressService
    {
        AdressRepository adressRepository = new AdressRepository();
        public List<Adress> GetAdresses()
        {
            return adressRepository.GetAdresses();

        }

        public Adress GetAdressById(int id)
        {
            return adressRepository.GetAdressById(id);
        }

        public bool CreateAdress(Adress adress)
        {
            return adressRepository.CreateAdress(adress);
        }

        public bool UpdateAdress(int id, Adress adress)
        {
            return adressRepository.UpdateAdress(id, adress);
        }

        public bool DeleteAdress(int id)
        {
            return adressRepository.DeleteAdress(id);
        }
    }
}