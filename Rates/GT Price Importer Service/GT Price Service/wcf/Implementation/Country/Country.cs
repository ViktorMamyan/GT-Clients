using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    public partial class GTPriceImporterWCF
    {
        public async Task<CountryList> GetCountryAsync()
        {
            #region Parameters

            CountryList returnData = new CountryList
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

                DataTable table = await helper.QueryAsync("SELECT * FROM dbo.GetCountryList()", CommandType.Text);

                List<CountyItem> ListOfData = new List<CountyItem>();

                if (table != null && table.Rows != null && table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        CountyItem DataRow = new CountyItem();

                        DataRow.ID = (int)table.Rows[i]["ID"];
                        DataRow.Country = (string)table.Rows[i]["Country"];
                        DataRow.IsActive = (bool)table.Rows[i]["IsActive"];

                        ListOfData.Add(DataRow);
                    }
                }
                else
                {
                    returnData.Response = new List<CountyItem>();
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

        public async Task<DefaultReturnData> NewCountryAsync(CountyItem data)
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
                    new SqlParameter("@Country", data.Country)
                };

                SqlHelper helper = new SqlHelper();

                await helper.ExecuteAsync("dbo.NewCountryAsync", CommandType.StoredProcedure, Parameters.ToArray());

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