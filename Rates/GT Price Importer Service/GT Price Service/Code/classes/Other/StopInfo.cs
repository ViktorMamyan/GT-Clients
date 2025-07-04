using System;

namespace GTPriceImporterService
{
    public class StopInfo
    {
        public DateTime HotelStopDate { get; set; }
        public string Hotel { get; set; }
        public string Touroperator { get; set; }
        public string Market { get; set; }
        public string Region { get; set; }
        public string Room { get; set; }
        public string Accommodation { get; set; }
        public string Meal { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public DateTime IssueDate { get; set; }
        public string Note { get; set; }
    }
}