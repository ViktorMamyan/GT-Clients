using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    [ServiceContract]
    public partial interface GTPriceImporterServiceWCF
    {
        //[OperationContract(AsyncPattern = true, Name = "GET_TokenAsync", ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        //[WebGet(UriTemplate = "Token/GET_TokenAsync",
        //   RequestFormat = WebMessageFormat.Json,
        //   ResponseFormat = WebMessageFormat.Json,
        //   BodyStyle = WebMessageBodyStyle.Bare
        //)]
        //Task<DefaultReturnData> GET_TokenAsync();
    }
}