using System.Collections.Generic;

namespace gt_excelReader_lib
{
    public class ReadyData_5 : ReadyData_6
    {
        public ReadyData_5()
        {
            Cancellation = new List<CancelPolicy>();
        }

        public List<CancelPolicy> Cancellation { get; set; }
    }
}