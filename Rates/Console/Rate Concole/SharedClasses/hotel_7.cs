using System.Collections.Generic;

namespace ConsoleApp1
{
    public class hotel_7 : hotel_5
    {
        public hotel_7()
        {
            ObligatoryService = new List<obligatoryService>();
        }

        public List<obligatoryService> ObligatoryService { get; set; }
    }
}