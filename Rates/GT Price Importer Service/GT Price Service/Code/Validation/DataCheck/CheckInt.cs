namespace GTPriceImporterService
{
    internal static class CheckInt
    {
        internal static bool IsValidInt(string data)
        {
            bool success = int.TryParse(data, out int number);

            if (success == false)
            {
                return false;
            }

            return true;
        }
    }
}