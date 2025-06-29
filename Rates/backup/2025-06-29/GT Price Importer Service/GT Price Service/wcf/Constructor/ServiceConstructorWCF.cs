using System;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Threading;

namespace GTPriceImporterService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any,
        InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class GTPriceImporterWCF : GTPriceImporterServiceWCF
    {
        internal OperationContext context;
        internal string ReqUrl = string.Empty;

        internal static readonly string AuthKey = "0b98231834ff51099ffccdb8377a1fa9";
        
        //internal static readonly string FirstEncriptionKey = "16d5fcfa48100a2cdef0e8a4acfd2900";
        //internal static readonly string EncriptionKey = "ef3c82e0a5b3997690123d2f345a0d2f";

        internal string RequestIPAddress = string.Empty;

        public GTPriceImporterWCF()
        {
            try
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("hy-AM");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                context = OperationContext.Current;
                ReqUrl = context.IncomingMessageHeaders.To.OriginalString;

                RequestIPAddress = ClientIPAddress.GetIP(OperationContext.Current);
            }
            catch (Exception)
            {
            }
        }

        internal void CheckIfAuthorized()
        {
            string Token = GetToken();
            if (Token != null && Token != AuthKey)
            {
                throw new SecurityAccessDeniedException("Not Authorized!");
            }
        }

        internal string GetToken()
        {
            var properties = OperationContext.Current.IncomingMessageProperties;
            if (properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                var httpRequest = (HttpRequestMessageProperty)properties[HttpRequestMessageProperty.Name];
                foreach (var headerKey in httpRequest.Headers.AllKeys)
                {
                    if (headerKey.ToLower() == "Token".ToLower())
                    {
                        var headerValue = httpRequest.Headers[headerKey];

                        return headerValue;
                    }
                }
            }

            return null;
        }
    }
}