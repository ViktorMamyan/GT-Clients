using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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

            XDocument doc = XDocument.Load(@"C:\Users\Vitya\Desktop\GT\Rates\files\new\6 Sharm Contract.xls");

            XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

            var worksheet = doc.Descendants(ss + "Worksheet")
                         .FirstOrDefault(w => (string)w.Attribute(ss + "Name") == "Page1");
            
            //UnnamedPage_0

            if (worksheet != null)
            {
                var rows = worksheet.Descendants(ss + "Row");

                List<object> readyData = new List<object>();
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

                bool ShouldBrake = false;

                foreach (var rowData in ListData)
                {
                    if (ShouldBrake == true) break;
                    for (int DD = 0; DD < rowData.Count; DD++)
                    {
                        if (ShouldBrake == true) break;
                        if (rowData[DD].StyleID.Replace("\"", "").ToLower() == "s63".ToLower())
                        {
                            if (ShouldBrake == true) break;
                            if (rowData[DD].MergeAcross == 6)
                            {
                                var tempData = new MergeAcross6_Contract().Read6_Contract(ListData);
                                readyData = tempData.Cast<object>().ToList();

                                if (readyData == null || readyData.Count == 0)
                                {
                                    throw new Exception("There is no data from excel");
                                }
                                else
                                {
                                    foreach (ReadyData_6 item in readyData.Cast<ReadyData_6>().ToList())
                                    {
                                        ParseObject parseObject = new ParseObject();

                                        parseObject = Parser6.ParseX(item.Accommodation);

                                        if (parseObject != null && parseObject.ADL > 0)
                                        {
                                            item.ADL = parseObject.ADL;
                                            item.CHD = parseObject.CHD;
                                            
                                            item.CHDStart1 = parseObject.CHDStart1;
                                            item.CHDEnd1 = parseObject.CHDEnd1;

                                            item.CHDStart2 = parseObject.CHDStart2;
                                            item.CHDEnd2 = parseObject.CHDEnd2;

                                            item.CHDStart3 = parseObject.CHDStart3;
                                            item.CHDEnd3 = parseObject.CHDEnd3;

                                            item.INFStart = parseObject.INFStart;
                                            item.INFEnd = parseObject.INFEnd;
                                        }
                                    }
                                }

                                ShouldBrake = true;
                                break;
                            }
                            else if (rowData[DD].MergeAcross == 5)
                            {
                                var tempData = new MergeAcross5_Contract().Read5_Contract(ListData);
                                readyData = tempData.Cast<object>().ToList();

                                if (readyData == null || readyData.Count == 0)
                                {
                                    throw new Exception("There is no data from excel");
                                }







                                ShouldBrake = true;
                                break;
                            }
                            else if (rowData[DD].MergeAcross == 7)
                            {
                                var tempData = new MergeAcross7_Contract().Read7_Contract(ListData);
                                readyData = tempData.Cast<object>().ToList();

                                if (readyData == null || readyData.Count == 0)
                                {
                                    throw new Exception("There is no data from excel");
                                }







                                ShouldBrake = true;
                                break;
                            }
                        }
                    }
                }

                Console.WriteLine("Start Save");

                //string json = JsonConvert.SerializeObject(readyData, Formatting.Indented);
                //System.IO.File.WriteAllText(@"C:\Users\Vitya\Desktop\ready.json", json);

                //DataTable dt = ListToTable.ListToDataTable(readyData);
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
}