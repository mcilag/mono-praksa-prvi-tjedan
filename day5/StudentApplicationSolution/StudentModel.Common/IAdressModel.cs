using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModel.Common
{
    public interface IAdress
    {
        int Adress_id { get; set; }
        string Street { get; set; }
        int Number { get; set; }
        string City { get; set; }
        string Zipcode { get; set; }
    }
}
