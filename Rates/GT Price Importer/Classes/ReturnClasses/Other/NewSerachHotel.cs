using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GT_Price_Importer.Classes
{
    public class NewSerachHotel
    {
        [DataMember(Order = 0)]
        public string HotelName { get; set; }

        [DataMember(Order = 1)]
        public string Board { get; set; }

        [DataMember(Order = 2)]
        public string Category { get; set; }

        [DataMember(Order = 3)]
        public string Currency { get; set; }

        [DataMember(Order = 4)]
        public int NightsFrom { get; set; }

        [DataMember(Order = 5)]
        public int NightsTill { get; set; }

        [DataMember(Order = 6)]
        public DateTime? ReservationStart { get; set; }

        [DataMember(Order = 7)]
        public DateTime? ReservationEnd { get; set; }

        [DataMember(Order = 8)]
        public DateTime? PeriodsStart { get; set; }

        [DataMember(Order = 9)]
        public DateTime? PeriodsEnd { get; set; }

        [DataMember(Order = 10)]
        public string Room { get; set; }

        [DataMember(Order = 11)]
        public string Accommodation { get; set; }

        [DataMember(Order = 12)]
        public decimal? B2BPrice { get; set; }

        [DataMember(Order = 13)]
        public decimal? B2CPrice { get; set; }

        [DataMember(Order = 14)]
        public decimal? RealPrice { get; set; }

        [DataMember(Order = 15)]
        public bool PriceAddedByPercent { get; set; }

        [DataMember(Order = 16)]
        public decimal B2BPriceAddValue { get; set; }

        [DataMember(Order = 17)]
        public decimal B2CPriceAddValue { get; set; }

        [DataMember(Order = 18)]
        public int ADL { get; set; }

        [DataMember(Order = 19)]
        public int CHD { get; set; }

        [DataMember(Order = 20)]
        public int? CHDStart1 { get; set; }

        [DataMember(Order = 21)]
        public int? CHDEnd1 { get; set; }

        [DataMember(Order = 22)]
        public int? CHDStart2 { get; set; }

        [DataMember(Order = 23)]
        public int? CHDEnd2 { get; set; }

        [DataMember(Order = 24)]
        public int? CHDStart3 { get; set; }

        [DataMember(Order = 25)]
        public int? CHDEnd3 { get; set; }

        [DataMember(Order = 26)]
        public int? INFStart { get; set; }

        [DataMember(Order = 27)]
        public int? INFEnd { get; set; }

        [DataMember(Order = 28)]
        public bool AllowInfSearch { get; set; }
        
        //[Browsable(false)]
        //public string Region { get; set; }

        [Browsable(false)]
        public List<gt_excelReader_lib.CancelPolicy> Cancellation { get; set; }

        [Browsable(false)]
        public List<gt_excelReader_lib.obligatoryService> ObligatoryService { get; set; }

        [DataMember(Order = 29)]
        public string SPO_No { get; set; }
    }
}