using System;
using System.Collections.Generic;

namespace GTPriceImporterService
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
        public decimal? B2BPrice { get; set; }
        public decimal? B2CPrice { get; set; }
        public decimal? RealPrice { get; set; }
        public bool PriceAddedByPercent { get; set; }
        public decimal B2BPriceAddValue { get; set; }
        public decimal B2CPriceAddValue { get; set; }
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
        public bool AllowInfSearch { get; set; }
        public List<CancelPolicy> Cancellation { get; set; }
        public List<obligatoryService> ObligatoryService { get; set; }
        public string SPO_No { get; set; }
    }

    public class CancelPolicy
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StayingNights { get; set; }
        public string NoShow { get; set; }

        public string Penalty { get; set; }
    }

    public class obligatoryService
    {
        public string ServiceName { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public string Meal { get; set; }
        public decimal AdultPrice { get; set; }

        public decimal? ChildOldPrice { get; set; }
        public int? ChildOldFrom { get; set; }
        public int? ChildOldTo { get; set; }

        public decimal? ChildMiddlePrice { get; set; }
        public int? ChildMiddleFrom { get; set; }
        public int? ChildMiddleTo { get; set; }

        public decimal? ChildYoungPrice { get; set; }
        public int? ChildYoungFrom { get; set; }
        public int? ChildYoungTo { get; set; }

        public decimal Inf_w_o_seat { get; set; }
        public decimal PerService { get; set; }
    }
}