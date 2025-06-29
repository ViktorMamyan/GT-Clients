namespace GTPriceImporterService
{
    internal static class CheckBool
    {
        internal static bool IsValidBool(string data)
        {
            bool success = bool.TryParse(data, out bool number);

            if (success == false)
            {
                return false;
            }

            return true;
        }
    }
}