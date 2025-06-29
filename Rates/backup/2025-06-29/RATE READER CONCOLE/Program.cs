using System;
using System.Collections.Generic;
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
                                //other parameters




                            }
                        }
                    }
                }

                foreach (var item in hotelList)
                {
                    Console.WriteLine(item.HotelName);
                }
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
        public string HotelName { get; set; }
    }

}