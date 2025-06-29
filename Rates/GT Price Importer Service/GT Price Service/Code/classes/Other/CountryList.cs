using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GTPriceImporterService
{
    [DataContract(Namespace = "")]
    public class CountryList: DefaultReturnData
    {
        public CountryList()
        {
            Response = new List<CountyItem>();
        }
        
        [DataMember(Order = 0)]
        public List<CountyItem> Response { get; set; }
    }

    [DataContract(Namespace = "")]
    public class CountyItem
    {
        [DataMember(Order = 0)]
        public int ID { get; set; }

        [DataMember(Order = 1)]
        public string Country { get; set; }

        [DataMember(Order = 2)]
        public bool IsActive { get; set; }
    }
}