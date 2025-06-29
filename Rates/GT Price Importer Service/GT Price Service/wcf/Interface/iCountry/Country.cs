using System.ServiceModel.Web;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    public partial interface GTPriceImporterServiceWCF
    {
        [OperationContract(AsyncPattern = true, Name = "GetCountryAsync", ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        [WebGet(UriTemplate = "Country/GetCountryAsync",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare
        )]
        Task<CountryList> GetCountryAsync();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Country/NewCountryAsync")]
        Task<DefaultReturnData> NewCountryAsync(CountyItem data);
    }
}