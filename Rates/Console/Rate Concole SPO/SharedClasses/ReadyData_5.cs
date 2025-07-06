using System;
using System.Collections.Generic;

namespace ConsoleApp1
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