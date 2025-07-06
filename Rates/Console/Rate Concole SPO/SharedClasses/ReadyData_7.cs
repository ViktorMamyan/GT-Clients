using System.Collections.Generic;

namespace ConsoleApp1
{
    public class ReadyData_7 : ReadyData_5
    {
        public ReadyData_7()
        {
            ObligatoryService = new List<obligatoryService>();
        }

        public List<obligatoryService> ObligatoryService { get; set; }
    }
}