using System.Security.Cryptography;
using System.Text;

namespace GTPriceImporterService.Code
{
    internal class Security
    {
        internal static string MD5Hash(string OriginalData)
        {
            MD5 MD5Hasher = MD5.Create();
            StringBuilder ResultData = new StringBuilder();

            byte[] data = MD5Hasher.ComputeHash(Encoding.Default.GetBytes(OriginalData));

            for (int i = 0; i < data.Length; i++)
            {
                ResultData.Append(data[i].ToString("x2"));
            }

            return ResultData.ToString();
        }
    }
}