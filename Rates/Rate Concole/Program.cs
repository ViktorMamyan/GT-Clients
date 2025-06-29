using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Console.ReadKey();

            XDocument doc = XDocument.Load(@"C:\Users\Vitya\Desktop\Contract1.xls");

            XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

            var worksheet = doc.Descendants(ss + "Worksheet")
                         .FirstOrDefault(w => (string)w.Attribute(ss + "Name") == "UnnamedPage_0");

            if (worksheet != null)
            {
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

                bool IsStart = true;

                string CurrentHeader = string.Empty;
                int HeadrRowID = 0;

                foreach (var rowData in ListData)
                {
                    foreach (var cell in rowData)
                    {
                        if (cell.MergeAcross == 6 && cell.StyleID.Replace("\"", "").ToLower() == "s63".ToLower())
                        {
                            IsStart = true;
                        }
                        else
                        {
                            IsStart = false;
                        }

                        if (IsStart == true)
                        {
                            if (hotel != null && hotel.HotelName != string.Empty)
                            {
                                hotelList.Add(hotel);
                            }

                            hotel = new Hotel();

                            if (cell.Value.Trim() != string.Empty)
                            {
                                hotel.HotelName = cell.Value;
                            }
                        }
                        else
                        {
                            if (hotel.HotelName != null && hotel.HotelName.Length > 1)
                            {
                                //Region Category
                                if (cell.StyleID.Replace("\"", "").ToLower() == "s64".ToLower())
                                {
                                    if (cell.Value.ToLower() == "Region".ToLower())
                                    {
                                        CurrentHeader = "Region";
                                    }
                                    else if (cell.Value.ToLower() == "Category".ToLower())
                                    {
                                        CurrentHeader = "Category";
                                    }
                                    else if (cell.Value.ToLower() == "Nights from - till".ToLower())
                                    {
                                        CurrentHeader = "Nights from - till";
                                    }
                                }

                                if (cell.MergeAcross == 1 && CurrentHeader == "Region")
                                {
                                    if (CurrentHeader == "Region" && HeadrRowID == 0)
                                    {
                                        hotel.Region = cell.Value;
                                        HeadrRowID = 1;
                                    }
                                    else if (CurrentHeader == "Region" && HeadrRowID == 1)
                                    {
                                        HeadrRowID++;
                                    }
                                    else if (CurrentHeader == "Region" && HeadrRowID == 2)
                                    {
                                        hotel.Board = cell.Value;
                                        CurrentHeader = string.Empty;
                                        HeadrRowID = 0;
                                    }
                                }

                                if (cell.MergeAcross == 1 && CurrentHeader == "Category")
                                {
                                    if (CurrentHeader == "Category" && HeadrRowID == 0)
                                    {
                                        hotel.Category = cell.Value;
                                        HeadrRowID = 1;
                                    }
                                    else if (CurrentHeader == "Category" && HeadrRowID == 1)
                                    {
                                        HeadrRowID++;
                                    }
                                    else if (CurrentHeader == "Category" && HeadrRowID == 2)
                                    {
                                        hotel.Currency = cell.Value;
                                        CurrentHeader = string.Empty;
                                        HeadrRowID = 0;
                                    }
                                }

                                if (cell.MergeAcross == 1 && CurrentHeader == "Nights from - till")
                                {
                                    if (CurrentHeader == "Nights from - till" && HeadrRowID == 0)
                                    {
                                        string ft = cell.Value.Trim();
                                        string[] parts = ft.Split('-');
                                        if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int start) && int.TryParse(parts[1].Trim(), out int end))
                                        {
                                            hotel.NightsFrom = start;
                                            hotel.NightsTill = end;
                                        }
                                    }
                                }


                                //Periods ReservationDates
                                if (cell.StyleID.Replace("\"", "").ToLower() == "s80".ToLower())
                                {
                                    if (cell.Value.ToLower() == "Periods".ToLower())
                                    {
                                        CurrentHeader = "Periods";
                                    }
                                    else if (cell.Value.ToLower() == "Reservation dates".ToLower())
                                    {
                                        CurrentHeader = "Reservation dates";
                                    }
                                }

                                if ((CurrentHeader == "Periods" || CurrentHeader == "Reservation dates") && (cell.StyleID.Replace("\"", "").ToLower() == "s81".ToLower()))
                                {
                                    if (CurrentHeader == "Periods" && HeadrRowID == 0)
                                    {
                                        string period = cell.Value.Trim();

                                        string[] parts = period.Split(new[] { "\n" }, StringSplitOptions.None);

                                        if (parts.Length == 2 && DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date1) && DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date2))
                                        {
                                            periods Local_period = new periods();

                                            Local_period.Start = date1;
                                            Local_period.End = date2;

                                            hotel.Periods.Add(Local_period);
                                        }
                                    }
                                    else if (CurrentHeader == "Reservation dates" && HeadrRowID == 0)
                                    {
                                        string resdate = cell.Value.Trim();

                                        string[] parts = resdate.Split(new[] { "\n" }, StringSplitOptions.None);

                                        if (parts.Length == 2 && DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rdate1) && DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rdate2))
                                        {
                                            reservationdates reservationDates = new reservationdates();

                                            reservationDates.Start = rdate1;
                                            reservationDates.End = rdate2;

                                            hotel.ReservationDates.Add(reservationDates);
                                        }
                                    }
                                }


                                //Rooms And Price
                                if (cell.MergeAcross == 1 && cell.StyleID.Replace("\"", "").ToLower() == "s83".ToLower())
                                {
                                    CurrentHeader = "Room";

                                    rooms Rooms = new rooms();
                                    Rooms.Room = cell.Value;

                                    hotel.Rooms.Add(Rooms);

                                    HeadrRowID = 1;
                                }

                                if (CurrentHeader == "Room" && cell.StyleID.Replace("\"", "").ToLower() == "s80".ToLower())
                                {
                                    int roomQTY = hotel.Rooms.Count - 1;

                                    accommodation accommodation1 = new accommodation();
                                    accommodation1.Accommodation = cell.Value;

                                    hotel.Rooms[roomQTY].Accommodation.Add(accommodation1);
                                }

                                if (CurrentHeader == "Room" && cell.StyleID.Replace("\"", "").ToLower() == "s85".ToLower() && HeadrRowID == 1)
                                {
                                    int roomQTY = hotel.Rooms.Count - 1;
                                    int AccQTY = hotel.Rooms[roomQTY].Accommodation.Count - 1;

                                    price price = new price();

                                    if (decimal.TryParse(cell.Value, out decimal result))
                                    {
                                        price.Price = Convert.ToDecimal(result);
                                    }
                                    else
                                    {
                                        price.Price = (decimal?)(null);
                                    }

                                    hotel.Rooms[roomQTY].Accommodation[AccQTY].Price.Add(price);
                                }



                            }
                        }
                    }
                }

                foreach (var item in hotelList)
                {
                    if (item.HotelName == null || item.HotelName.Trim() == string.Empty) continue;
                    Console.WriteLine(item.HotelName + ">" + item.Region + ">" + item.Board + ">" + item.Category + ">" + item.Currency + ">" + item.NightsFrom + ">" + item.NightsTill);
                    Console.WriteLine("Periuds >" + item.Periods.Count);
                    Console.WriteLine("Periuds >" + item.ReservationDates.Count);
                }

                //string json = JsonConvert.SerializeObject(hotelList, Formatting.Indented);

                //System.IO.File.WriteAllText(@"C:\Users\Vitya\Desktop\ready.json", json);


                //Notes

                //Relase Periuds
            }
            else
            {
                Console.WriteLine("Worksheet not found.");
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }

    class CellInfo
    {
        public string Value { get; set; }
        public int MergeAcross { get; set; }
        public string StyleID { get; set; }
    }


    class Hotel
    {
        public Hotel()
        {
            Periods = new List<periods>();
            ReservationDates = new List<reservationdates>();
            Rooms=new List<rooms>();
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
    }

}