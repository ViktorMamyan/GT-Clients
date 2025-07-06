using System;

namespace ConsoleApp1
{
    public class ReadyData
    {
        public string HotelName { get; set; }
        public string Region { get; set; }
        public string Board { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
        public int NightsFrom { get; set; }
        public int NightsTill { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public DateTime PeriodsStart { get; set; }
        public DateTime PeriodsEnd { get; set; }
        public string Room { get; set; }
        public string Accommodation { get; set; }
        public decimal? Price { get; set; }

        public int ADL { get; set; }
        public int CHD { get; set; }

        public int? CHDStart1 { get; set; }
        public int? CHDEnd1 { get; set; }

        public int? CHDStart2 { get; set; }
        public int? CHDEnd2 { get; set; }

        public int? CHDStart3 { get; set; }
        public int? CHDEnd3 { get; set; }

        public int? INFStart { get; set; }
        public int? INFEnd { get; set; }
    }
}