using System.ServiceModel.Web;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GTPriceImporterService
{
    public partial interface GTPriceImporterServiceWCF
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Region/CheckRegionAsync")]
        Task<DefaultReturnData> CheckRegionAsync(List<OperatorWithRegion> data);

    }
}