using GT_Price_Importer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Price_Importer
{
    public class MergeAcrossMethods
    {
        public async Task MergeAcross6(List<CountyItem> ListCountry, List<OperatorItem> ListOperator, string Country, string Operator, string ExcelFile, string Sheet)
        {
            var query_country = ListCountry.Where(x => x.Country == Country).FirstOrDefault();
            var query_operator = ListOperator.Where(x => x.Operator == Operator).FirstOrDefault();

            if (query_country == null || query_country.ID < 1) throw new Exception("Անորոշ Երկիր");
            if (query_operator == null || query_operator.ID < 1) throw new Exception("Անորոշ օպերատոր");

            if (ExcelFile == string.Empty || System.IO.File.Exists(ExcelFile) == false) throw new Exception("Անորոշ excel-ի ֆայլ");

            List<gt_excelReader_lib.ReadyData_6> RData_6 = new List<gt_excelReader_lib.ReadyData_6>();

            RData_6 = await new ExcelReader().ReadExcelXML_6(ExcelFile, Sheet, GT_Price_Importer.Classes.ExcelReader.FileType.Contract);

            if (RData_6 == null || RData_6.Count == 0) throw new Exception("Excel-ից տվյալներ չեն ստացվել");

            //Checking Regions
            var query_unique_regions = RData_6.Select(x => x.Region).Distinct().ToList();
            if (query_unique_regions == null || query_unique_regions.Count == 0) throw new Exception("Error with regions");
            List<OperatorWithRegion> RegionData = new List<OperatorWithRegion>();
            foreach (var item in query_unique_regions)
            {
                RegionData.Add(new OperatorWithRegion() { CountryID = query_country.ID, OperatorID = query_operator.ID, Region = item.Trim() });
            }
            await new SetData().HttpsDataDefault("Region", "CheckRegionAsync", "", RegionData);

            //Add Hotels Only
            var query_hotels_only = RData_6.Distinct().ToList();
            if (query_hotels_only == null || query_hotels_only.Count == 0) throw new Exception("Error with hotels");
            List<HotelsOnly> hotelsOnly = new List<HotelsOnly>();
            foreach (var item in query_hotels_only)
            {
                hotelsOnly.Add(new HotelsOnly() { CountryID = query_country.ID, OperatorID = query_operator.ID, Region = item.Region, Hotel = item.HotelName });
            }
            await new SetData().HttpsDataDefault("Hotel", "CheckHotelAsync", "", hotelsOnly);

            bool AllowInfSearch = RData_6.Any(x => x.CHDStart1 == 0);

            //Add Hotels
            List<NewSerachHotel> HotelData = new List<NewSerachHotel>();
            foreach (gt_excelReader_lib.ReadyData_6 hotel in RData_6)
            {
                NewSerachHotel searchHotel = new NewSerachHotel();
                searchHotel.HotelName = hotel.HotelName;
                searchHotel.Board = hotel.Board;
                searchHotel.Category = hotel.Category;
                searchHotel.Currency = hotel.Currency;
                searchHotel.NightsFrom = hotel.NightsFrom;
                searchHotel.NightsTill = hotel.NightsTill;
                searchHotel.ReservationStart = hotel.ReservationStart;
                searchHotel.ReservationEnd = hotel.ReservationEnd;
                searchHotel.PeriodsStart = hotel.PeriodsStart;
                searchHotel.PeriodsEnd = hotel.PeriodsEnd;
                searchHotel.Room = hotel.Room;
                searchHotel.Accommodation = hotel.Accommodation;
                searchHotel.B2BPrice = hotel.Price;
                searchHotel.B2CPrice = hotel.Price;
                searchHotel.RealPrice = hotel.Price;
                searchHotel.PriceAddedByPercent = true;
                
                searchHotel.B2BPriceAddValue = 0;
                searchHotel.B2CPriceAddValue = 0;

                searchHotel.ADL = hotel.ADL;
                searchHotel.CHD = hotel.CHD;

                searchHotel.CHDStart1 = hotel.CHDStart1;
                searchHotel.CHDStart2 = hotel.CHDStart2;

                searchHotel.CHDEnd1 = hotel.CHDEnd1;
                searchHotel.CHDEnd2 = hotel.CHDEnd2;

                searchHotel.CHDStart3 = hotel.CHDStart3;
                searchHotel.CHDEnd3 = hotel.CHDEnd3;

                searchHotel.INFStart = hotel.INFStart;
                searchHotel.INFEnd = hotel.INFEnd;

                if (hotel.CHD == 0)
                {
                    searchHotel.AllowInfSearch = false;
                }
                else
                {
                    searchHotel.AllowInfSearch = !AllowInfSearch;
                }
                
                //searchHotel.Cancellation = hotel.Cancellation;
                //searchHotel.ObligatoryService = hotel.ObligatoryService;

                HotelData.Add(searchHotel);
            }

            HotelData.RemoveAll(x => x.RealPrice == null);

            bool IsDataChanged = false;

            FrmPriceCorrecter_6 frm = new FrmPriceCorrecter_6();
            frm.IsContractPrice = true;
            frm.IsSpoPrice = false;
            frm.HotelData = HotelData;

            frm.ShowDialog();

            IsDataChanged = frm.IsDataChanged;

            frm.Dispose();

            if (IsDataChanged == false) return;
        }

        public async Task MergeAcross6Spo(List<CountyItem> ListCountry, List<OperatorItem> ListOperator, string Country, string Operator, string ExcelFile, string Sheet)
        {
            var query_country = ListCountry.Where(x => x.Country == Country).FirstOrDefault();
            var query_operator = ListOperator.Where(x => x.Operator == Operator).FirstOrDefault();

            if (query_country == null || query_country.ID < 1) throw new Exception("Անորոշ Երկիր");
            if (query_operator == null || query_operator.ID < 1) throw new Exception("Անորոշ օպերատոր");

            if (ExcelFile == string.Empty || System.IO.File.Exists(ExcelFile) == false) throw new Exception("Անորոշ excel-ի ֆայլ");

            List<gt_excelReader_lib.ReadyData_6> RData_6 = new List<gt_excelReader_lib.ReadyData_6>();

            RData_6 = await new ExcelReader().ReadExcelXML_6(ExcelFile, Sheet, GT_Price_Importer.Classes.ExcelReader.FileType.SPO);

            if (RData_6 == null || RData_6.Count == 0) throw new Exception("Excel-ից տվյալներ չեն ստացվել");

            //Checking Regions
            var query_unique_regions = RData_6.Select(x => x.Region).Distinct().ToList();
            if (query_unique_regions == null || query_unique_regions.Count == 0) throw new Exception("Error with regions");
            List<OperatorWithRegion> RegionData = new List<OperatorWithRegion>();
            foreach (var item in query_unique_regions)
            {
                RegionData.Add(new OperatorWithRegion() { CountryID = query_country.ID, OperatorID = query_operator.ID, Region = item.Trim() });
            }
            await new SetData().HttpsDataDefault("Region", "CheckRegionAsync", "", RegionData);

            //Add Hotels Only
            var query_hotels_only = RData_6.Distinct().ToList();
            if (query_hotels_only == null || query_hotels_only.Count == 0) throw new Exception("Error with hotels");
            List<HotelsOnly> hotelsOnly = new List<HotelsOnly>();
            foreach (var item in query_hotels_only)
            {
                hotelsOnly.Add(new HotelsOnly() { CountryID = query_country.ID, OperatorID = query_operator.ID, Region = item.Region, Hotel = item.HotelName });
            }
            await new SetData().HttpsDataDefault("Hotel", "CheckHotelAsync", "", hotelsOnly);

            bool AllowInfSearch = RData_6.Any(x => x.CHDStart1 == 0);

            //Add Hotels
            List<NewSerachHotel> HotelData = new List<NewSerachHotel>();
            foreach (gt_excelReader_lib.ReadyData_6 hotel in RData_6)
            {
                NewSerachHotel searchHotel = new NewSerachHotel();
                searchHotel.HotelName = hotel.HotelName;
                searchHotel.Board = hotel.Board;
                searchHotel.Category = hotel.Category;
                searchHotel.Currency = hotel.Currency;
                searchHotel.NightsFrom = hotel.NightsFrom;
                searchHotel.NightsTill = hotel.NightsTill;
                searchHotel.ReservationStart = hotel.ReservationStart;
                searchHotel.ReservationEnd = hotel.ReservationEnd;
                searchHotel.PeriodsStart = hotel.PeriodsStart;
                searchHotel.PeriodsEnd = hotel.PeriodsEnd;
                searchHotel.Room = hotel.Room;
                searchHotel.Accommodation = hotel.Accommodation;
                searchHotel.B2BPrice = hotel.Price;
                searchHotel.B2CPrice = hotel.Price;
                searchHotel.RealPrice = hotel.Price;
                searchHotel.PriceAddedByPercent = true;

                searchHotel.B2BPriceAddValue = 0;
                searchHotel.B2CPriceAddValue = 0;

                searchHotel.ADL = hotel.ADL;
                searchHotel.CHD = hotel.CHD;

                searchHotel.CHDStart1 = hotel.CHDStart1;
                searchHotel.CHDStart2 = hotel.CHDStart2;

                searchHotel.CHDEnd1 = hotel.CHDEnd1;
                searchHotel.CHDEnd2 = hotel.CHDEnd2;

                searchHotel.CHDStart3 = hotel.CHDStart3;
                searchHotel.CHDEnd3 = hotel.CHDEnd3;

                searchHotel.INFStart = hotel.INFStart;
                searchHotel.INFEnd = hotel.INFEnd;

                if (hotel.CHD == 0)
                {
                    searchHotel.AllowInfSearch = false;
                }
                else
                {
                    searchHotel.AllowInfSearch = !AllowInfSearch;
                }

                //searchHotel.Cancellation = hotel.Cancellation;
                //searchHotel.ObligatoryService = hotel.ObligatoryService;

                searchHotel.SPO_No = hotel.SPO_No;

                HotelData.Add(searchHotel);
            }

            HotelData.RemoveAll(x => x.RealPrice == null);

            bool IsDataChanged = false;

            FrmPriceCorrecter_6 frm = new FrmPriceCorrecter_6();
            frm.IsContractPrice = false;
            frm.IsSpoPrice = true;
            frm.HotelData = HotelData;

            frm.ShowDialog();

            IsDataChanged = frm.IsDataChanged;

            frm.Dispose();

            if (IsDataChanged == false) return;
        }

        public void MergeAcross5() { }

        public void MergeAcross7() { }
    }
}