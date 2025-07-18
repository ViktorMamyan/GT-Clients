﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GT_Price_Importer.Classes
{
    internal static class AppData
    {
        internal static string url = "https://localhost:55555/V1/";

        internal static readonly string AuthKey = "0b98231834ff51099ffccdb8377a1fa9";


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