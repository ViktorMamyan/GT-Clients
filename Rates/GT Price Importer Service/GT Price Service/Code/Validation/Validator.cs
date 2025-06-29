using System;

namespace GTPriceImporterService
{
    public static class Validator
    {
        internal static void OK(int StatusCode, DefaultReturnData returnData)
        {
            returnData.ErrorMsg = string.Empty;
            returnData.StatusCode = StatusCode;
        }

        internal static void ValidStringPlus(string Value, string ErrorMsg, int StatusCode, DefaultReturnData returnData)
        {
            if (CheckString.IsValidString(Value) == false)
            {
                returnData.ErrorMsg = ErrorMsg;
                returnData.StatusCode = StatusCode;

                throw new Exception(returnData.ErrorMsg);
            }
        }

        internal static void CheckCustomInt(string value, string ErrorMsg, DefaultReturnData returnData)
        {
            if (CheckString.IsValidString(value) == false || CheckInt.IsValidInt(value) == false)
            {
                returnData.ErrorMsg = ErrorMsg;
                returnData.StatusCode = 1001;

                throw new Exception(returnData.ErrorMsg);
            }
        }

        internal static void CheckDateString(string value, string ErrorMsg, DefaultReturnData returnData)
        {
            if (CheckString.IsValidString(value) == false || CheckDate.Is_Valid_Date(value) == false)
            {
                returnData.ErrorMsg = ErrorMsg;
                returnData.StatusCode = 1001;

                throw new Exception(returnData.ErrorMsg);
            }
        }

        internal static void CheckValidDate(DateTime value, string ErrorMsg, DefaultReturnData returnData)
        {
            if (CheckString.IsValidString(value.ToString("yyyy-MM-dd")) == false || CheckDate.Is_Valid_Date(value.ToString("yyyy-MM-dd")) == false)
            {
                returnData.ErrorMsg = ErrorMsg;
                returnData.StatusCode = 1001;

                throw new Exception(returnData.ErrorMsg);
            }
        }

        internal static void CheckDateTimeString(string value, string ErrorMsg, DefaultReturnData returnData)
        {
            if (CheckString.IsValidString(value) == false || CheckDate.Is_Valid_Date_Time(value) == false)
            {
                returnData.ErrorMsg = ErrorMsg;
                returnData.StatusCode = 1001;

                throw new Exception(returnData.ErrorMsg);
            }
        }

        internal static void CheckCustomIntInterval(int value, int min, int max, bool onlyUnSigned, string ErrorMsg, DefaultReturnData returnData)
        {
            if (onlyUnSigned == true)
            {
                if (value < 0)
                {
                    returnData.ErrorMsg = ErrorMsg;
                    returnData.StatusCode = 1001;

                    throw new Exception(returnData.ErrorMsg);
                }
            }

            if (value < min || value > max)
            {
                returnData.ErrorMsg = ErrorMsg;
                returnData.StatusCode = 1001;

                throw new Exception(returnData.ErrorMsg);
            }
        }

        internal static void ValidCheckBool(string Value, string ErrorMsg, int StatusCode, DefaultReturnData returnData)
        {
            if (CheckString.IsValidString(Value) == false || CheckBool.IsValidBool(Value) == false)
            {
                returnData.ErrorMsg = ErrorMsg;
                returnData.StatusCode = StatusCode;

                throw new Exception(returnData.ErrorMsg);
            }
        }


        internal static void SetCustomException(Exception ex, DefaultReturnData returnData)
        {
            returnData.ErrorMsg = ex.Message;
        }

        internal static void ThrowException(string Message, DefaultReturnData returnData, int statusCode = 1001)
        {
            returnData.ErrorMsg = Message;
            returnData.StatusCode = statusCode;

            throw new Exception(returnData.ErrorMsg);
        }


        internal static void DbNoData(DefaultReturnData returnData)
        {
            returnData.ErrorMsg = "There is no data";
            returnData.StatusCode = 1001;

            throw new Exception(returnData.ErrorMsg);
        }

    }
}