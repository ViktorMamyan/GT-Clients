using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    public partial interface GTPriceImporterServiceWCF
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Hotel/CheckHotelAsync")]
        Task<DefaultReturnData> CheckHotelAsync(List<HotelsOnly> data);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Hotel/NewSerachHotelAsync")]
        Task<DefaultReturnData> NewSerachHotelAsync(NewSerachHotel data);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Hotel/NewSpoSerachHotelAsync")]
        Task<DefaultReturnData> NewSpoSerachHotelAsync(NewSerachHotel data);

    }
}