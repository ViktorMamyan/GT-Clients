using System.Runtime.Serialization;

namespace GTPriceImporterService
{
    [DataContract(Namespace = "")]
    public class DefaultReturnData
    {
        [DataMember(Order = 0)]
        public int StatusCode { get; set; }

        [DataMember(Order = 1)]
        public string ErrorMsg { get; set; }
    }
}