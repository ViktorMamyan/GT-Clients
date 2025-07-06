using System.Collections.Generic;

namespace ConsoleApp1
{
    public class hotel
    {
        public hotel()
        {
            Periods = new List<periods>();
            ReservationDates = new List<reservationdates>();
            Rooms = new List<rooms>();
        }

        public string HotelName { get; set; }
        public string Region { get; set; }
        public string Board { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
        public int NightsFrom { get; set; }
        public int NightsTill { get; set; }

        public List<periods> Periods { get; set; }
        public List<reservationdates> ReservationDates { get; set; }
        public List<rooms> Rooms { get; set; }
        public string SPO_No { get; set; }
    }
}