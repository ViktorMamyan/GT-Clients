using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GTPriceImporterService
{
    [DataContract(Namespace = "")]
    public class OperatorList : DefaultReturnData
    {
        public OperatorList()
        {
            Response = new List<OperatorItem>();
        }

        [DataMember(Order = 0)]
        public List<OperatorItem> Response { get; set; }
    }

    [DataContract(Namespace = "")]
    public class OperatorItem
    {
        [DataMember(Order = 0)]
        public int ID { get; set; }

        [DataMember(Order = 1)]
        public string Operator { get; set; }

        [DataMember(Order = 2)]
        public bool IsActive { get; set; }
    }
}