using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    public partial class GTPriceImporterWCF
    {
        public async Task<DefaultReturnData> CheckRegionAsync(List<OperatorWithRegion> data)
        {
            #region Parameters

            DefaultReturnData returnData = new DefaultReturnData
            {
                ErrorMsg = string.Empty,
                StatusCode = -1
            };

            #endregion

            try
            {
                CheckIfAuthorized();

                #region SQL

                for (int i = 0; i < data.Count; i++)
                {
                    List<SqlParameter> Parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@CountryID", data[i].CountryID),
                        new SqlParameter("@OperatorID", data[i].OperatorID),
                        new SqlParameter("@Region", data[i].Region)
                    };

                    await new SqlHelper().ExecuteAsync("Operator.CheckOperatorWithRegion", CommandType.StoredProcedure, Parameters.ToArray());
                }

                #endregion

                #region Return

                Validator.OK(200, returnData);

                return returnData;

                #endregion
            }
            catch (Exception ex)
            {
                #region Exception

                Validator.SetCustomException(ex, returnData);

                return returnData;

                #endregion
            }
        }
    }
}