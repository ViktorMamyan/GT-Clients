using System.Collections.Generic;

namespace gt_excelReader_lib
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