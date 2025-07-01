using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Price_Importer.Classes
{
    public class NewSerachHotel
    {
        public string HotelName { get; set; }
        public string Board { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
        public int NightsFrom { get; set; }
        public int NightsTill { get; set; }
        public DateTime? ReservationStart { get; set; }
        public DateTime? ReservationEnd { get; set; }
        public DateTime? PeriodsStart { get; set; }
        public DateTime? PeriodsEnd { get; set; }
        public string Room { get; set; }
        public string Accommodation { get; set; }
        public decimal? Price { get; set; }
        public decimal? RealPrice { get; set; }
        public bool PriceAddedByPercent { get; set; }
        public decimal PriceAddValue { get; set; }
    }
}