namespace GTPriceImporterService
{
    internal static class CheckString
    {
        internal static bool IsValidString(string data)
        {
            if (string.IsNullOrEmpty(data) || data.Trim().Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}