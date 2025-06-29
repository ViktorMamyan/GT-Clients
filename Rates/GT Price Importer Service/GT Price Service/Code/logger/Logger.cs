using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    internal static class Logger
    {
        //internal static async Task RequestError(string IP_Address, string ErrorMessage, string RquestURL)
        //{
        //    try
        //    {
        //        await Task.Run(async () =>
        //        {
        //            List<SqlParameter> Parameters = new List<SqlParameter>
        //            {
        //                new SqlParameter("@IP_Address", IP_Address),
        //                new SqlParameter("@ErrorMessage", ErrorMessage),
        //                new SqlParameter("@RquestURL", RquestURL)
        //            };

        //            await new SqlHelper().ExecuteAsync("dbo.CreateErrorLog", CommandType.StoredProcedure, Parameters.ToArray());
        //        });
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
    }
}