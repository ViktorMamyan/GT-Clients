using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Price_Importer.Classes
{
    public class CountryList : DefaultReturnData<CountyItem>
    {
    }

    public class CountyItem
    {
        public int ID { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }
}