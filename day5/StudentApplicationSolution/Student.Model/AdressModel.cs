using System;
using StudentModel.Common;

namespace Student.Model
{
    public class Adress: IAdress
    {

        public int Adress_id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
    }
}
