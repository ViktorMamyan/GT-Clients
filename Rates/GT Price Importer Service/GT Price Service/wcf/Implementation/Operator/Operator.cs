using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    public partial class GTPriceImporterWCF
    {
        public async Task<OperatorList> GetOperatorAsync()
        {
            #region Parameters

            OperatorList returnData = new OperatorList
            {
                ErrorMsg = string.Empty,
                StatusCode = -1
            };

            #endregion

            try
            {
                CheckIfAuthorized();

                #region Check Input Data

                //Validator

                #endregion

                #region SQL

                SqlHelper helper = new SqlHelper();

                DataTable table = await helper.QueryAsync("SELECT * FROM Operator.GetOperatorList()", CommandType.Text);

                List<OperatorItem> ListOfData = new List<OperatorItem>();

                if (table != null && table.Rows != null && table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        OperatorItem DataRow = new OperatorItem();

                        DataRow.ID = (int)table.Rows[i]["ID"];
                        DataRow.Operator = (string)table.Rows[i]["Operator"];
                        DataRow.IsActive = (bool)table.Rows[i]["IsActive"];

                        ListOfData.Add(DataRow);
                    }
                }
                else
                {
                    returnData.Response = new List<OperatorItem>();
                }

                #endregion

                #region Return

                returnData.Response = ListOfData;

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

        public async Task<DefaultReturnData> NewOperatorAsync(OperatorItem data)
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

                List<SqlParameter> Parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Operator", data.Operator)
                };

                SqlHelper helper = new SqlHelper();

                await helper.ExecuteAsync("Operator.NewOperatorAsync", CommandType.StoredProcedure, Parameters.ToArray());

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