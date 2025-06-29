using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    public partial class GTPriceImporterWCF
    {
        //public async Task<DefaultReturnData> GET_TokenAsync()
        //{
        //    DefaultReturnData returnData = new DefaultReturnData
        //    {
        //        ErrorMsg = string.Empty,
        //        StatusCode = -1
        //    };

        //    try
        //    {
        //        CheckIfAuthorized();

        //        #region Return

        //        Validator.OK(200, returnData);

        //        return await Task.FromResult(returnData);

        //        #endregion
        //    }
        //    catch (SqlException ex)
        //    {
        //        #region Exception

        //        Validator.SetCustomException(new Exception(dbException.GetSQLExeption(ex.Number, ex.Message)), returnData);

        //        //Logger.RequestError(RequestIPAddress, "SQL Error: " + ex.Number + " | " + ex.Message, ReqUrl);

        //        return await Task.FromResult(returnData);

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Exception

        //        Validator.SetCustomException(ex, returnData);

        //        //Logger.RequestError(RequestIPAddress, ex.Message, ReqUrl);

        //        return await Task.FromResult(returnData);

        //        #endregion
        //    }
        //}
    }
}