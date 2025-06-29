using System.ServiceModel.Web;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    public partial interface GTPriceImporterServiceWCF
    {
        [OperationContract(AsyncPattern = true, Name = "GetOperatorAsync", ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign)]
        [WebGet(UriTemplate = "Operator/GetOperatorAsync",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare
        )]
        Task<OperatorList> GetOperatorAsync();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Operator/NewOperatorAsync")]
        Task<DefaultReturnData> NewOperatorAsync(OperatorItem data);
    }
}