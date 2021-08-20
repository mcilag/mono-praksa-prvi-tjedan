using System;

namespace Student.Model
{
    public class Adress
    {

        public Adress() { }

        public Adress(int adress_id, string street, int number, string city, string zipcode)
        {
            adress_id = adress_id;
            street = street;
            number = number;
            city = city;
            zipcode = zipcode;
        }

        public int adress_id { get; set; }
        public string street { get; set; }
        public int number { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
    }
}
