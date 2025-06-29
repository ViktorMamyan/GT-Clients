using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GT_Price_Importer.Classes
{
    internal class JsonDownloader
    {
        internal static async Task<string> GetAsync(string url, int Timeout)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = Timeout;
            request.Headers["Token"] = AppData.AuthKey;

            WebResponse response = await request.GetResponseAsync();
            HttpWebResponse httpResponse = (HttpWebResponse)response;
            string result;

            using (Stream responseStream = httpResponse.GetResponseStream())
            {
                result = new StreamReader(responseStream).ReadToEnd();
            }

            return result;
        }

        internal static async Task<string> PostAsync(string url, object ReqData, string ContentType = "application/json")
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(ReqData.GetType());

            MemoryStream mem = new MemoryStream();

            ser.WriteObject(mem, ReqData);

            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = ContentType;
            webClient.Headers["Token"] = AppData.AuthKey;
            webClient.Encoding = Encoding.UTF8;

            string Result = string.Empty;

            Uri uri = new Uri(url, UriKind.Absolute);

            Result = await webClient.UploadStringTaskAsync(uri, "POST", data);

            return Result;
        }

    }
}