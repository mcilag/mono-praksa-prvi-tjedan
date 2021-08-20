using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using Student.Repository;

namespace Student.Service
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

        public void CreateAdress(Adress adress)
        {
            adressRepository.CreateAdress(adress);
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