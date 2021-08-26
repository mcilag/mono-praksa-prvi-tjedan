using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplication.Common
{
    public class AdressFilter : IAdressFilter
    {
        public string Filter { get; set; }

        public string FilterLike(string Filter)
        {
            if (Filter != "")
            {
                return String.Format(" WHERE City LIKE '%{0}%' ", Filter);
            }
            return "";
        }
    }

}
