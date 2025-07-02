using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace gt_excelReader_lib
{
    public class ExcelReader
    {
        public List<ReadyData> GetContractPrice(string File, string Sheet = "UnnamedPage_0")
        {
            XDocument doc = XDocument.Load(File);

            XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

            var worksheet = doc.Descendants(ss + "Worksheet")
                         .FirstOrDefault(w => (string)w.Attribute(ss + "Name") == Sheet);

            if (worksheet == null) return null;

            var rows = worksheet.Descendants(ss + "Row");

            List<List<CellInfo>> ListData = new List<List<CellInfo>>();

            foreach (var row in rows)
            {
                List<CellInfo> rowData = row.Elements(ss + "Cell")
                .Select(cell => new CellInfo
                {
                    Value = (string)cell.Element(ss + "Data") ?? "",
                    MergeAcross = int.TryParse((string)cell.Attribute(ss + "MergeAcross"), out int m) ? m : 0,
                    StyleID = (string)cell.Attribute(ss + "StyleID") ?? ""
                }).ToList();

                ListData.Add(rowData);
            }

            List<Hotel> hotelList = new List<Hotel>();
            Hotel hotel = new Hotel();

            string CurrentHeader = string.Empty;

            int priceID = 0;

            int pID = 0;
            int rID = 0;

            string LastCaption = string.Empty;

            foreach (var rowData in ListData)
            {
                for (int DD = 0; DD < rowData.Count; DD++)
                {
                    if (rowData[DD].MergeAcross == 6 && rowData[DD].StyleID.Replace("\"", "").ToLower() == "s63".ToLower())
                    {
                        CurrentHeader = string.Empty;

                        priceID = 0;
                        pID = 0;
                        rID = 0;

                        LastCaption = string.Empty;

                        if (hotel == null || hotel.HotelName == null)
                        {
                            hotel = new Hotel();
                            hotel.HotelName = rowData[DD].Value.Trim();
                            continue;
                        }

                        hotelList.Add(hotel);
                        hotel = new Hotel();
                        hotel.HotelName = rowData[DD].Value.Trim();

                        continue;
                    }
                    else
                    {
                        switch (rowData[DD].Value.Trim())
                        {
                            case "Region":
                                LastCaption = "Region"; continue;
                            case "Board":
                                LastCaption = "Board"; continue;
                            case "Category":
                                LastCaption = "Category"; continue;
                            case "Currency":
                                LastCaption = "Currency"; continue;
                            case "Nights from - till":
                                LastCaption = "Nights from - till"; continue;
                            case "Periods":
                                LastCaption = "Periods"; continue;
                            case "Reservation dates":
                                LastCaption = "Reservation dates"; continue;
                            //case "DBL Interconnecting":
                            //    LastCaption = "DBL Interconnecting";
                            //    hotel.Rooms.Add(new rooms() { Room = rowData[DD].Value.Trim() });
                            //    continue;
                            //case "Family (min 3 pax)":
                            //    LastCaption = "Family (min 3 pax)";
                            //    hotel.Rooms.Add(new rooms() { Room = rowData[DD].Value.Trim() });
                            //    continue;
                            default:
                                break;
                        }

                        if (LastCaption == "Region") { hotel.Region = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Board") { hotel.Board = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Category") { hotel.Category = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Currency") { hotel.Currency = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Nights from - till")
                        {
                            string ft = rowData[DD].Value.Trim();
                            string[] parts = ft.Split('-');
                            if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int start) && int.TryParse(parts[1].Trim(), out int end))
                            {
                                hotel.NightsFrom = start;
                                hotel.NightsTill = end;
                            }
                            LastCaption = string.Empty;
                            continue;
                        }
                        if (LastCaption == "Periods")
                        {
                            string period = rowData[DD].Value.Trim();
                            string[] parts = period.Split(new[] { "\n" }, StringSplitOptions.None);

                            if (parts.Length == 2 && DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date1) && DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date2))
                            {
                                periods Local_period = new periods();

                                Local_period.Start = date1;
                                Local_period.End = date2;

                                hotel.Periods.Add(Local_period);
                            }
                            //LastCaption = string.Empty;
                            pID++;
                            continue;
                        }
                        if (LastCaption == "Reservation dates")
                        {
                            string resdate = rowData[DD].Value.Trim();
                            string[] parts = resdate.Split(new[] { "\n" }, StringSplitOptions.None);

                            if (parts.Length == 2 && DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rdate1) && DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rdate2))
                            {
                                reservationdates reservationDates = new reservationdates();

                                reservationDates.Start = rdate1;
                                reservationDates.End = rdate2;

                                hotel.ReservationDates.Add(reservationDates);
                            }
                            //LastCaption = string.Empty;
                            rID++;
                            if (rID == pID)
                            {
                                LastCaption = string.Empty;
                            }
                            continue;
                        }
                    }

                    if (rowData[DD].StyleID.Replace("\"", "").ToLower() == "s83".ToLower())
                    {
                        if (rowData[DD].MergeAcross == 1)
                        {
                            CurrentHeader = "Room";

                            rooms Rooms = new rooms();
                            Rooms.Room = rowData[DD].Value.Trim();

                            hotel.Rooms.Add(Rooms);
                        }
                    }
                    else if (hotel.Rooms != null && hotel.Rooms.Count > 0 && rowData[DD].StyleID.Contains("s80"))
                    {
                        if (rowData[DD].Value.Trim() != "Release periods")
                        {
                            int roomQTY = hotel.Rooms.Count - 1;

                            accommodation accommodation1 = new accommodation();
                            accommodation1.Accommodation = rowData[DD].Value.Trim();

                            hotel.Rooms[roomQTY].Accommodation.Add(accommodation1);

                            priceID = 0;
                        }
                    }
                    else if (rowData[DD].StyleID.Contains("s85"))
                    {
                        priceID++;

                        int roomQTY = hotel.Rooms.Count - 1;
                        int AccQTY = hotel.Rooms[roomQTY].Accommodation.Count - 1;

                        price price = new price();

                        price.PeriodsStart = hotel.Periods[priceID - 1].Start;
                        price.PeriodsEnd = hotel.Periods[priceID - 1].End;

                        price.ReservationStart = hotel.ReservationDates[priceID - 1].Start;
                        price.ReservationEnd = hotel.ReservationDates[priceID - 1].End;

                        if (decimal.TryParse(rowData[DD].Value.Trim(), out decimal result))
                        {
                            price.Price = Convert.ToDecimal(result);
                        }
                        else
                        {
                            price.Price = (decimal?)(null);
                        }

                        hotel.Rooms[roomQTY].Accommodation[AccQTY].Price.Add(price);
                    }
                    else
                    {
                        if (rowData[DD].Value.Trim() != string.Empty && rowData[DD].Value.Trim() != hotel.HotelName && hotel.HotelName != null)
                        {
                            if (rowData[DD].StyleID.Contains("s80") && rowData[DD].Value.Trim() == "Release periods")
                            {
                                continue;
                            }
                            else if (rowData[DD].StyleID.Contains("s86") && rowData[DD].Value.Trim() == "0")
                            {
                                continue;
                            }
                            else if (rowData[DD].StyleID.Contains("s88") && rowData[DD].Value.Trim() == "Notes")
                            {
                                continue;
                            }
                            else if (rowData[DD].StyleID.Contains("s90") && rowData[DD].Value.Trim() == "RELEASE DAYS")
                            {
                                continue;
                            }
                            else if (rowData[DD].StyleID.Contains("s90") && rowData[DD].Value.Trim() == "")
                            {
                                continue;
                            }
                            else
                            {
                                //Console.WriteLine(rowData[DD].Value.Trim());
                            }
                        }
                    }
                }
            }

            if (hotel != null || !string.IsNullOrEmpty(hotel.HotelName))
            {
                hotelList.Add(hotel);
                hotel = null;
            }

            List<ReadyData> readyData = new List<ReadyData>();

            foreach (var hotelItem in hotelList)
            {
                for (int j = 0; j < hotelItem.Rooms.Count; j++)
                {
                    for (int t = 0; t < hotelItem.Rooms[j].Accommodation.Count; t++)
                    {
                        for (int k = 0; k < hotelItem.Rooms[j].Accommodation[t].Price.Count; k++)
                        {
                            ReadyData data = new ReadyData();

                            data.HotelName = hotelItem.HotelName;
                            data.Region = hotelItem.Region;
                            data.Board = hotelItem.Board;
                            data.Category = hotelItem.Category;
                            data.Currency = hotelItem.Currency;
                            data.NightsFrom = hotelItem.NightsFrom;
                            data.NightsTill = hotelItem.NightsTill;

                            data.ReservationStart = hotelItem.Rooms[j].Accommodation[t].Price[k].ReservationStart;
                            data.ReservationEnd = hotelItem.Rooms[j].Accommodation[t].Price[k].ReservationEnd;


                            data.PeriodsStart = hotelItem.Rooms[j].Accommodation[t].Price[k].PeriodsStart;
                            data.PeriodsEnd = hotelItem.Rooms[j].Accommodation[t].Price[k].PeriodsEnd;

                            data.Room = hotelItem.Rooms[j].Room;
                            data.Accommodation = hotelItem.Rooms[j].Accommodation[t].Accommodation;

                            data.Price = hotelItem.Rooms[j].Accommodation[t].Price[k].Price;

                            readyData.Add(data);
                        }
                    }
                }
            }

            List<ReadyData> readyDataUnique = readyData.Distinct().ToList();

            return readyDataUnique;
        }

        public List<ReadyData> GetSPOPrice(string File, string Sheet = "UnnamedPage_0")
        {
            XDocument doc = XDocument.Load(File);

            XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

            var worksheet = doc.Descendants(ss + "Worksheet")
                         .FirstOrDefault(w => (string)w.Attribute(ss + "Name") == Sheet);

            if (worksheet == null) return null;

            var rows = worksheet.Descendants(ss + "Row");

            List<List<CellInfo>> ListData = new List<List<CellInfo>>();

            foreach (var row in rows)
            {
                List<CellInfo> rowData = row.Elements(ss + "Cell")
                .Select(cell => new CellInfo
                {
                    Value = (string)cell.Element(ss + "Data") ?? "",
                    MergeAcross = int.TryParse((string)cell.Attribute(ss + "MergeAcross"), out int m) ? m : 0,
                    StyleID = (string)cell.Attribute(ss + "StyleID") ?? ""
                }).ToList();

                ListData.Add(rowData);
            }

            List<Hotel> hotelList = new List<Hotel>();
            Hotel hotel = new Hotel();

            string CurrentHeader = string.Empty;

            int priceID = 0;

            int pID = 0;
            int rID = 0;

            string LastCaption = string.Empty;

            foreach (var rowData in ListData)
            {
                for (int DD = 0; DD < rowData.Count; DD++)
                {
                    if (rowData[DD].MergeAcross == 6 && rowData[DD].StyleID.Replace("\"", "").ToLower() == "s63".ToLower())
                    {
                        CurrentHeader = string.Empty;

                        priceID = 0;
                        pID = 0;
                        rID = 0;

                        LastCaption = string.Empty;

                        if (hotel == null || hotel.HotelName == null)
                        {
                            hotel = new Hotel();
                            hotel.HotelName = rowData[DD].Value.Trim();
                            continue;
                        }

                        hotelList.Add(hotel);
                        hotel = new Hotel();
                        hotel.HotelName = rowData[DD].Value.Trim();

                        continue;
                    }
                    else
                    {
                        switch (rowData[DD].Value.Trim())
                        {
                            case "Region":
                                LastCaption = "Region"; continue;
                            case "Board":
                                LastCaption = "Board"; continue;
                            case "Category":
                                LastCaption = "Category"; continue;
                            case "Currency":
                                LastCaption = "Currency"; continue;
                            case "Nights from - till":
                                LastCaption = "Nights from - till"; continue;
                            case "Periods":
                                LastCaption = "Periods"; continue;
                            case "Reservation dates":
                                LastCaption = "Reservation dates"; continue;
                            //case "DBL Interconnecting":
                            //    LastCaption = "DBL Interconnecting";
                            //    hotel.Rooms.Add(new rooms() { Room = rowData[DD].Value.Trim() });
                            //    continue;
                            //case "Family (min 3 pax)":
                            //    LastCaption = "Family (min 3 pax)";
                            //    hotel.Rooms.Add(new rooms() { Room = rowData[DD].Value.Trim() });
                            //    continue;
                            default:
                                break;
                        }

                        if (LastCaption == "Region") { hotel.Region = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Board") { hotel.Board = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Category") { hotel.Category = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Currency") { hotel.Currency = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Nights from - till")
                        {
                            string ft = rowData[DD].Value.Trim();
                            string[] parts = ft.Split('-');
                            if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int start) && int.TryParse(parts[1].Trim(), out int end))
                            {
                                hotel.NightsFrom = start;
                                hotel.NightsTill = end;
                            }
                            LastCaption = string.Empty;
                            continue;
                        }
                        if (LastCaption == "Periods")
                        {
                            string period = rowData[DD].Value.Trim();
                            string[] parts = period.Split(new[] { "\n" }, StringSplitOptions.None);

                            if (parts.Length == 2 && DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date1) && DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date2))
                            {
                                periods Local_period = new periods();

                                Local_period.Start = date1;
                                Local_period.End = date2;

                                hotel.Periods.Add(Local_period);
                            }
                            //LastCaption = string.Empty;
                            pID++;
                            continue;
                        }
                        if (LastCaption == "Reservation dates")
                        {
                            string resdate = rowData[DD].Value.Trim();
                            string[] parts = resdate.Split(new[] { "\n" }, StringSplitOptions.None);

                            if (parts.Length == 2 && DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rdate1) && DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rdate2))
                            {
                                reservationdates reservationDates = new reservationdates();

                                reservationDates.Start = rdate1;
                                reservationDates.End = rdate2;

                                hotel.ReservationDates.Add(reservationDates);
                            }
                            //LastCaption = string.Empty;
                            rID++;
                            if (rID == pID)
                            {
                                LastCaption = string.Empty;
                            }
                            continue;
                        }
                    }

                    if (rowData[DD].StyleID.Replace("\"", "").ToLower() == "s85".ToLower())
                    {
                        if (rowData[DD].MergeAcross == 1)
                        {
                            CurrentHeader = "Room";

                            rooms Rooms = new rooms();
                            Rooms.Room = rowData[DD].Value.Trim();

                            hotel.Rooms.Add(Rooms);
                        }
                    }
                    else if (hotel.Rooms != null && hotel.Rooms.Count > 0 && rowData[DD].StyleID.Contains("s82"))
                    {
                        if (rowData[DD].Value.Trim() != "Release periods")
                        {
                            int roomQTY = hotel.Rooms.Count - 1;

                            accommodation accommodation1 = new accommodation();
                            accommodation1.Accommodation = rowData[DD].Value.Trim();

                            hotel.Rooms[roomQTY].Accommodation.Add(accommodation1);

                            priceID = 0;
                        }
                    }
                    else if (rowData[DD].StyleID.Contains("s87"))
                    {
                        priceID++;

                        int roomQTY = hotel.Rooms.Count - 1;
                        int AccQTY = hotel.Rooms[roomQTY].Accommodation.Count - 1;

                        price price = new price();

                        price.PeriodsStart = hotel.Periods[priceID - 1].Start;
                        price.PeriodsEnd = hotel.Periods[priceID - 1].End;

                        price.ReservationStart = hotel.ReservationDates[priceID - 1].Start;
                        price.ReservationEnd = hotel.ReservationDates[priceID - 1].End;

                        if (decimal.TryParse(rowData[DD].Value.Trim(), out decimal result))
                        {
                            price.Price = Convert.ToDecimal(result);
                        }
                        else
                        {
                            price.Price = (decimal?)(null);
                        }

                        hotel.Rooms[roomQTY].Accommodation[AccQTY].Price.Add(price);
                    }
                    else
                    {
                        if (rowData[DD].Value.Trim() != string.Empty && rowData[DD].Value.Trim() != hotel.HotelName && hotel.HotelName != null)
                        {
                            if (rowData[DD].StyleID.Contains("s80") && rowData[DD].Value.Trim() == "Release periods")
                            {
                                continue;
                            }
                            else if (rowData[DD].StyleID.Contains("s86") && rowData[DD].Value.Trim() == "0")
                            {
                                continue;
                            }
                            else if (rowData[DD].StyleID.Contains("s88") && rowData[DD].Value.Trim() == "Notes")
                            {
                                continue;
                            }
                            else if (rowData[DD].StyleID.Contains("s90") && rowData[DD].Value.Trim() == "RELEASE DAYS")
                            {
                                continue;
                            }
                            else if (rowData[DD].StyleID.Contains("s90") && rowData[DD].Value.Trim() == "")
                            {
                                continue;
                            }
                            else
                            {
                                if (rowData[DD].Value.Trim().StartsWith("SPO No"))
                                {
                                    hotel.SPO_No = rowData[DD].Value.Trim();
                                }
                                else
                                {
                                    //Console.WriteLine(rowData[DD].Value.Trim());
                                }
                            }
                        }
                    }
                }
            }

            if (hotel != null || !string.IsNullOrEmpty(hotel.HotelName))
            {
                hotelList.Add(hotel);
                hotel = null;
            }

            List<ReadyData> readyData = new List<ReadyData>();

            foreach (var hotelItem in hotelList)
            {
                for (int j = 0; j < hotelItem.Rooms.Count; j++)
                {
                    for (int t = 0; t < hotelItem.Rooms[j].Accommodation.Count; t++)
                    {
                        for (int k = 0; k < hotelItem.Rooms[j].Accommodation[t].Price.Count; k++)
                        {
                            ReadyData data = new ReadyData();

                            data.HotelName = hotelItem.HotelName;
                            data.Region = hotelItem.Region;
                            data.Board = hotelItem.Board;
                            data.Category = hotelItem.Category;
                            data.Currency = hotelItem.Currency;
                            data.NightsFrom = hotelItem.NightsFrom;
                            data.NightsTill = hotelItem.NightsTill;

                            data.ReservationStart = hotelItem.Rooms[j].Accommodation[t].Price[k].ReservationStart;
                            data.ReservationEnd = hotelItem.Rooms[j].Accommodation[t].Price[k].ReservationEnd;


                            data.PeriodsStart = hotelItem.Rooms[j].Accommodation[t].Price[k].PeriodsStart;
                            data.PeriodsEnd = hotelItem.Rooms[j].Accommodation[t].Price[k].PeriodsEnd;

                            data.Room = hotelItem.Rooms[j].Room;
                            data.Accommodation = hotelItem.Rooms[j].Accommodation[t].Accommodation;

                            data.Price = hotelItem.Rooms[j].Accommodation[t].Price[k].Price;

                            data.SPO_No = hotelItem.SPO_No;

                            readyData.Add(data);
                        }
                    }
                }
            }

            List<ReadyData> readyDataUnique = readyData.Distinct().ToList();

            return readyDataUnique;
        }
    }

    public class CellInfo
    {
        public string Value { get; set; }
        public int MergeAcross { get; set; }
        public string StyleID { get; set; }
    }

    public class Hotel
    {
        public Hotel()
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
        public string SPO_No { get; set; }
    }
}