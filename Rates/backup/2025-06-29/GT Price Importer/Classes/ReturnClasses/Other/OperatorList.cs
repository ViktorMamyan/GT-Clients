using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Price_Importer.Classes
{
    public class OperatorList : DefaultReturnData<OperatorItem>
    {
    }

    public class OperatorItem
    {
        public int ID { get; set; }
        public string Operator { get; set; }
        public bool IsActive { get; set; }
    }
}