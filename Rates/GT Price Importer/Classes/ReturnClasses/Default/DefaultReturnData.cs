using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Price_Importer.Classes
{
    public class DefaultReturnData<T>
    {
        public DefaultReturnData()
        {
            Response = new List<T>();
        }

        public int StatusCode { get; set; }
        public string ErrorMsg { get; set; }

        public virtual List<T> Response { get; set; }
    }

    public class DefaultReturnData
    {
        public int StatusCode { get; set; }
        public string ErrorMsg { get; set; }
    }
}