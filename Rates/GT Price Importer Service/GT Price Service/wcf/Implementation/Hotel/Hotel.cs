using GTPriceImporterService.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    public partial class GTPriceImporterWCF
    {
        public async Task<DefaultReturnData> CheckHotelAsync(List<HotelsOnly> data)
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
                        new SqlParameter("@Region", data[i].Region),
                        new SqlParameter("@Hotel", data[i].Hotel)
                    };

                    await new SqlHelper().ExecuteAsync("Operator.CheckHotelOnly", CommandType.StoredProcedure, Parameters.ToArray());
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

        public async Task<DefaultReturnData> NewSerachHotelAsync(NewSerachHotel data)
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
                
                DateTime PeriodsStart = data.PeriodsStart.Value;
                DateTime PeriodsEnd = data.PeriodsEnd.Value;

                for (; PeriodsStart <= PeriodsEnd; PeriodsStart = PeriodsStart.AddDays(1))
                {
                    DateTime SearchDay = PeriodsStart;
                    
                    Console.WriteLine(data.HotelName + " -> " + data.Room + " -> " + data.Accommodation);

                    string RowDataHash = string.Empty;
                    RowDataHash = data.HotelName;
                    RowDataHash += data.Board;
                    RowDataHash += data.Category;
                    RowDataHash += data.Currency;
                    RowDataHash += data.Room;
                    RowDataHash += data.Accommodation;
                    RowDataHash += SearchDay.ToString();

                    RowDataHash = Security.MD5Hash(RowDataHash);

                    List<SqlParameter> Parameters = new List<SqlParameter>
                            {
                                new SqlParameter("@HotelName", data.HotelName),
                                new SqlParameter("@Board", data.Board),
                                new SqlParameter("@Category", data.Category),
                                new SqlParameter("@Currency", data.Currency),
                                new SqlParameter("@NightsFrom", data.NightsFrom),
                                new SqlParameter("@NightsTill", data.NightsTill),

                                new SqlParameter("@ReservationStart", (data.ReservationStart.HasValue) ?
                                                                        DateTime.SpecifyKind(data.ReservationStart.Value, DateTimeKind.Utc) :
                                                                        (object)DBNull.Value),

                                new SqlParameter("@ReservationEnd", (data.ReservationEnd.HasValue) ?
                                                                        DateTime.SpecifyKind(data.ReservationEnd.Value, DateTimeKind.Utc) :
                                                                        (object)DBNull.Value),

                                new SqlParameter("@PeriodsStart", (data.PeriodsStart.HasValue) ?
                                                                        DateTime.SpecifyKind(data.PeriodsStart.Value, DateTimeKind.Utc) :
                                                                        (object)DBNull.Value),

                                new SqlParameter("@PeriodsEnd", (data.PeriodsEnd.HasValue) ?
                                                                        DateTime.SpecifyKind(data.PeriodsEnd.Value, DateTimeKind.Utc) :
                                                                        (object)DBNull.Value),

                                new SqlParameter("@Room", data.Room),
                                new SqlParameter("@Accommodation", data.Accommodation),

                                new SqlParameter("@Price", (data.Price.HasValue) ? data.Price.Value : (object)DBNull.Value),
                                new SqlParameter("@RealPrice", (data.RealPrice.HasValue) ? data.RealPrice.Value : (object)DBNull.Value),

                                new SqlParameter("@SearchDay", SearchDay),
                                new SqlParameter("@RowDataHash", RowDataHash),

                                new SqlParameter("@PriceAddedByPercent", data.PriceAddedByPercent),
                                new SqlParameter("@PriceAddValue", data.PriceAddValue)
                            };

                    await new SqlHelper().ExecuteAsync("Search.AddHotelForSearch", CommandType.StoredProcedure, Parameters.ToArray());
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
                
                Console.WriteLine(ex.Message);
                
                return returnData;

                #endregion
            }
        }

        public async Task<DefaultReturnData> NewSpoSerachHotelAsync(NewSerachHotel data)
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

                DateTime PeriodsStart = data.PeriodsStart.Value;
                DateTime PeriodsEnd = data.PeriodsEnd.Value;

                for (; PeriodsStart <= PeriodsEnd; PeriodsStart = PeriodsStart.AddDays(1))
                {
                    DateTime SearchDay = PeriodsStart;

                    Console.WriteLine(data.HotelName + " -> " + data.Room + " -> " + data.Accommodation);

                    string RowDataHash = string.Empty;
                    RowDataHash = data.HotelName;
                    RowDataHash += data.Board;
                    RowDataHash += data.Category;
                    RowDataHash += data.Currency;
                    RowDataHash += data.Room;
                    RowDataHash += data.Accommodation;
                    RowDataHash += data.SPO_No;
                    RowDataHash += SearchDay.ToString();

                    RowDataHash = Security.MD5Hash(RowDataHash);

                    List<SqlParameter> Parameters = new List<SqlParameter>
                            {
                                new SqlParameter("@HotelName", data.HotelName),
                                new SqlParameter("@Board", data.Board),
                                new SqlParameter("@Category", data.Category),
                                new SqlParameter("@Currency", data.Currency),
                                new SqlParameter("@NightsFrom", data.NightsFrom),
                                new SqlParameter("@NightsTill", data.NightsTill),

                                new SqlParameter("@ReservationStart", (data.ReservationStart.HasValue) ?
                                                                        DateTime.SpecifyKind(data.ReservationStart.Value, DateTimeKind.Utc) :
                                                                        (object)DBNull.Value),

                                new SqlParameter("@ReservationEnd", (data.ReservationEnd.HasValue) ?
                                                                        DateTime.SpecifyKind(data.ReservationEnd.Value, DateTimeKind.Utc) :
                                                                        (object)DBNull.Value),

                                new SqlParameter("@PeriodsStart", (data.PeriodsStart.HasValue) ?
                                                                        DateTime.SpecifyKind(data.PeriodsStart.Value, DateTimeKind.Utc) :
                                                                        (object)DBNull.Value),

                                new SqlParameter("@PeriodsEnd", (data.PeriodsEnd.HasValue) ?
                                                                        DateTime.SpecifyKind(data.PeriodsEnd.Value, DateTimeKind.Utc) :
                                                                        (object)DBNull.Value),

                                new SqlParameter("@Room", data.Room),
                                new SqlParameter("@Accommodation", data.Accommodation),

                                new SqlParameter("@Price", (data.Price.HasValue) ? data.Price.Value : (object)DBNull.Value),
                                new SqlParameter("@RealPrice", (data.RealPrice.HasValue) ? data.RealPrice.Value : (object)DBNull.Value),

                                new SqlParameter("@SearchDay", SearchDay),
                                new SqlParameter("@RowDataHash", RowDataHash),

                                new SqlParameter("@PriceAddedByPercent", data.PriceAddedByPercent),
                                new SqlParameter("@SPO_No", data.SPO_No),

                                new SqlParameter("@PriceAddValue", data.PriceAddValue)
                            };

                    await new SqlHelper().ExecuteAsync("Search.AddHotelForSearchSPO", CommandType.StoredProcedure, Parameters.ToArray());
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

                Console.WriteLine(ex.Message);

                return returnData;

                #endregion
            }
        }

        public async Task<DefaultReturnData> NewStopHotelAsync(StopInfo data)
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

                DateTime PeriodsStart = data.DateFrom;
                DateTime PeriodsEnd = data.DateTill;

                for (; PeriodsStart <= PeriodsEnd; PeriodsStart = PeriodsStart.AddDays(1))
                {
                    DateTime SearchDay = PeriodsStart;

                    Console.WriteLine(data.Hotel + " -> " + data.Room + " -> " + data.Accommodation);

                    string RowDataHash = string.Empty;
                    RowDataHash = data.Hotel;
                    RowDataHash += data.Room;
                    RowDataHash += data.Accommodation;
                    RowDataHash += data.Meal;
                    RowDataHash += SearchDay.ToString();

                    RowDataHash = Security.MD5Hash(RowDataHash);

                    List<SqlParameter> Parameters = new List<SqlParameter>
                            {
                                new SqlParameter("@HotelStopDate", data.HotelStopDate),
                                new SqlParameter("@Hotel", data.Hotel),
                                new SqlParameter("@Touroperator", data.Touroperator),
                                new SqlParameter("@Market", data.Market),
                                new SqlParameter("@Region", data.Region),
                                new SqlParameter("@Room", data.Room),
                                new SqlParameter("@Accommodation", data.Accommodation),
                                new SqlParameter("@Meal", data.Meal),
                                new SqlParameter("@DateFrom", data.DateFrom),
                                new SqlParameter("@DateTill", data.DateTill),
                                new SqlParameter("@IssueDate", data.IssueDate),
                                new SqlParameter("@Note", data.Note),
                                new SqlParameter("@SearchDay", SearchDay),
                                new SqlParameter("@RowDataHash", RowDataHash)
                            };

                    await new SqlHelper().ExecuteAsync("Search.AddStopHotel", CommandType.StoredProcedure, Parameters.ToArray());
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

                Console.WriteLine(ex.Message);

                return returnData;

                #endregion
            }
        }
    }
}