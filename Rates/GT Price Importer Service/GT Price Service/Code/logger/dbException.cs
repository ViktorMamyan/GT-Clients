namespace GTPriceImporterService
{
    internal static class dbException
    {
        internal static string GetSQLExeption(int ErrorNumber, string ErrorMessage)
        {
            switch (ErrorNumber)
            {
                case 245:
                    return "Տվյալների փոխակերպման սխալ";
                case 313:
                    return "Մուտքային պարամետրերի սխալ քանակ";
                case 547:
                    return "Տվյալները չեն կարող ջնջվել կամ ավելացվել, քանի որ առկա են հղումներ այլ տվյալների";
                case 2601:
                    return "Փորձ է արվում մուտքագրել կրկնվող տվյալներ";
                case 50001:
                    return "Տվյալների բազայի սխալ";
                case 50002:
                    return ErrorMessage;        //Կազմակերպությունը գրանցված է
                case 50003:
                    return ErrorMessage;        //Կազմակերպությունը չի գտնվել
                case 50004:
                    return ErrorMessage;        //Համակարգիչը չի գտնվել
                case 50005:
                    return ErrorMessage;        //Համապատասխան ծրագիրը չի գտնվել
                case 50006:
                    return ErrorMessage;        //Անորոշ ծրագիր կամ ծրագիրը դեակտիվացված է
                case 50007:
                    return ErrorMessage;        //Անորոշ կազմակերպություն կամ կազմակերպությունը դեակտիվացված է
                case 50008:
                    return ErrorMessage;        //Անորոշ համակարգիչ
                case 50009:
                    return ErrorMessage;        //Համակարգչի անորոշ ծրագիր
                default:
                    return "Տվյալների բազայի անորոշ սխալ";
            }
        }
    }
}