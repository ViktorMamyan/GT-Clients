using System.Collections.Generic;

namespace ConsoleApp1
{
    public class hotel_5 : hotel_6
    {
        public hotel_5()
        {
            Periods = new List<periods>();
            ReservationDates = new List<reservationdates>();
            Rooms = new List<rooms>();
            Cancellation = new List<CancelPolicy>();
        }

        public List<CancelPolicy> Cancellation { get; set; }
    }
}