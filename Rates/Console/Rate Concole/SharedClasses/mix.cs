using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class periods
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class reservationdates
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class rooms
    {
        public rooms()
        {
            Accommodation = new List<accommodation>();
        }

        public string Room { get; set; }

        public List<accommodation> Accommodation { get; set; }
    }

    public class accommodation
    {
        public accommodation()
        {
            Price = new List<price>();
        }

        public string Accommodation { get; set; }

        public List<price> Price { get; set; }
    }

    public class price
    {
        public decimal? Price { get; set; }

        public DateTime ReservationStart;
        public DateTime ReservationEnd;

        public DateTime PeriodsStart { get; set; }
        public DateTime PeriodsEnd { get; set; }
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