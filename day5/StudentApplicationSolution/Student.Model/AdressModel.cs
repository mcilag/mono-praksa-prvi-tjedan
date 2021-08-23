using System;

namespace Student.Model
{
    public class Adress: StudentModel.Common.IAdressModel
    {

        public Adress() { }

        public Adress(int _adress_id, string _street, int _number, string _city, string _zipcode)
        {
            Adress_id = _adress_id;
            Street = _street;
            Number = _number;
            City = _city;
            Zipcode = _zipcode;
        }

        public int Adress_id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
    }
}
