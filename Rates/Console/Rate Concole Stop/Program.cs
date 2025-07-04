using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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

            //XDocument doc = XDocument.Load(@"C:\Users\Vitya\Desktop\C.xls");
            XDocument doc = XDocument.Load(@"C:\Users\Vitya\Desktop\STOP1.xls");

            XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

            var worksheet = doc.Descendants(ss + "Worksheet")
                         .FirstOrDefault(w => (string)w.Attribute(ss + "Name") == "Page1");

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

                List<StopInfo> stopList = new List<StopInfo>();

                StopInfo StopItem = new StopInfo();
                DateTime HotelStopDate = new DateTime();

                int CurrentRow = 0;

                foreach (var rowData in ListData)
                {
                    for (int DD = 0; DD < rowData.Count; DD++)
                    {
                        if (rowData[DD].MergeAcross == 10 && rowData[DD].Value.Trim() != "STOP-SALE LIST")
                        {
                            HotelStopDate = DateTime.ParseExact(rowData[DD].Value.Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            StopItem.HotelStopDate = HotelStopDate;

                            continue;
                        }

                        if (rowData[DD].StyleID.Replace("\"", "").ToLower() == "s65".ToLower() || rowData[DD].StyleID.Replace("\"", "").ToLower() == "s66".ToLower())
                        {
                            switch (CurrentRow)
                            {
                                case 0:
                                    StopItem.Hotel = rowData[DD].Value.Trim();
                                    break;
                                case 1:
                                    StopItem.Touroperator = rowData[DD].Value.Trim();
                                    break;
                                case 2:
                                    StopItem.Market = rowData[DD].Value.Trim();
                                    break;
                                case 3:
                                    StopItem.Region = rowData[DD].Value.Trim();
                                    break;
                                case 4:
                                    StopItem.Room = rowData[DD].Value.Trim();
                                    break;
                                case 5:
                                    StopItem.Accommodation = rowData[DD].Value.Trim();
                                    break;
                                case 6:
                                    StopItem.Meal = rowData[DD].Value.Trim();
                                    break;
                                case 7:
                                    StopItem.DateFrom = DateTime.ParseExact(rowData[DD].Value.Trim(), "dd.MM.yy", CultureInfo.InvariantCulture);
                                    break;
                                case 8:
                                    StopItem.DateTill = DateTime.ParseExact(rowData[DD].Value.Trim(), "dd.MM.yy", CultureInfo.InvariantCulture);
                                    break;
                                case 9:
                                    StopItem.IssueDate = DateTime.ParseExact(rowData[DD].Value.Trim(), "dd.MM.yy", CultureInfo.InvariantCulture);
                                    break;
                                case 10:
                                    StopItem.Note = rowData[DD].Value.Trim();

                                    stopList.Add(StopItem);
                                    StopItem = new StopInfo();
                                    StopItem.HotelStopDate = HotelStopDate;
                                    CurrentRow = 0;

                                    continue;
                                default:
                                    break;
                            }

                            CurrentRow++;

                            continue;
                        }
                    }
                }

                Console.WriteLine("Start Save");

                string json = JsonConvert.SerializeObject(stopList, Formatting.Indented);
                System.IO.File.WriteAllText(@"C:\Users\Vitya\Desktop\ready.json", json);

                //DataTable dt = ListToTable.ListToDataTable(stopList);
                //new ExcelSaver().SaveDataTableToExcel(dt, @"C:\Users\Vitya\Desktop\R.xlsx");

                Console.WriteLine("End Save");
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

    public class CellInfo
    {
        public string Value { get; set; }
        public int MergeAcross { get; set; }
        public string StyleID { get; set; }
    }

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