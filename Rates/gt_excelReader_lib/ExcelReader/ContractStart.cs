using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace gt_excelReader_lib
{
    public partial class DataReader
    {
        public List<ReadyData_6> GetData_6_Contract(string File, string Sheet = "UnnamedPage_0")
        {
            XDocument doc = XDocument.Load(File);

            XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

            var worksheet = doc.Descendants(ss + "Worksheet")
                         .FirstOrDefault(w => (string)w.Attribute(ss + "Name") == Sheet);

            if (worksheet == null) throw new Exception("Worksheet is not found");

            var rows = worksheet.Descendants(ss + "Row");

            List<ReadyData_6> readyData = new List<ReadyData_6>();
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
                            readyData = new ContractMethods().Read6_Contract(ListData);

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
                        //else if (rowData[DD].MergeAcross == 5)
                        //{
                        //    var tempData = new MergeAcross5_Contract().Read5_Contract(ListData);
                        //    readyData = tempData.Cast<object>().ToList();

                        //    if (readyData == null || readyData.Count == 0)
                        //    {
                        //        throw new Exception("There is no data from excel");
                        //    }







                        //    ShouldBrake = true;
                        //    break;
                        //}
                        //else if (rowData[DD].MergeAcross == 7)
                        //{
                        //    var tempData = new MergeAcross7_Contract().Read7_Contract(ListData);
                        //    readyData = tempData.Cast<object>().ToList();

                        //    if (readyData == null || readyData.Count == 0)
                        //    {
                        //        throw new Exception("There is no data from excel");
                        //    }







                        //    ShouldBrake = true;
                        //    break;
                        //}
                    }
                }
            }

            return readyData;
        }

        

      
        //public List<StopInfo> GetStopDates(string File, string Sheet = "Page1")
        //{
        //    XDocument doc = XDocument.Load(File);

        //    XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

        //    var worksheet = doc.Descendants(ss + "Worksheet")
        //                 .FirstOrDefault(w => (string)w.Attribute(ss + "Name") == Sheet);

        //    if (worksheet == null) return null;

        //    var rows = worksheet.Descendants(ss + "Row");

        //    List<List<CellInfo>> ListData = new List<List<CellInfo>>();

        //    foreach (var row in rows)
        //    {
        //        List<CellInfo> rowData = row.Elements(ss + "Cell")
        //        .Select(cell => new CellInfo
        //        {
        //            Value = (string)cell.Element(ss + "Data") ?? "",
        //            MergeAcross = int.TryParse((string)cell.Attribute(ss + "MergeAcross"), out int m) ? m : 0,
        //            StyleID = (string)cell.Attribute(ss + "StyleID") ?? ""
        //        }).ToList();

        //        ListData.Add(rowData);
        //    }

        //    List<StopInfo> stopList = new List<StopInfo>();

        //    StopInfo StopItem = new StopInfo();
        //    DateTime HotelStopDate = new DateTime();

        //    int CurrentRow = 0;

        //    foreach (var rowData in ListData)
        //    {
        //        for (int DD = 0; DD < rowData.Count; DD++)
        //        {
        //            if (rowData[DD].MergeAcross == 10 && rowData[DD].Value.Trim() != "STOP-SALE LIST")
        //            {
        //                HotelStopDate = DateTime.ParseExact(rowData[DD].Value.Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
        //                StopItem.HotelStopDate = HotelStopDate;

        //                continue;
        //            }

        //            if (rowData[DD].StyleID.Replace("\"", "").ToLower() == "s65".ToLower() || rowData[DD].StyleID.Replace("\"", "").ToLower() == "s66".ToLower())
        //            {
        //                switch (CurrentRow)
        //                {
        //                    case 0:
        //                        StopItem.Hotel = rowData[DD].Value.Trim();
        //                        break;
        //                    case 1:
        //                        StopItem.Touroperator = rowData[DD].Value.Trim();
        //                        break;
        //                    case 2:
        //                        StopItem.Market = rowData[DD].Value.Trim();
        //                        break;
        //                    case 3:
        //                        StopItem.Region = rowData[DD].Value.Trim();
        //                        break;
        //                    case 4:
        //                        StopItem.Room = rowData[DD].Value.Trim();
        //                        break;
        //                    case 5:
        //                        StopItem.Accommodation = rowData[DD].Value.Trim();
        //                        break;
        //                    case 6:
        //                        StopItem.Meal = rowData[DD].Value.Trim();
        //                        break;
        //                    case 7:
        //                        StopItem.DateFrom = DateTime.ParseExact(rowData[DD].Value.Trim(), "dd.MM.yy", CultureInfo.InvariantCulture);
        //                        break;
        //                    case 8:
        //                        StopItem.DateTill = DateTime.ParseExact(rowData[DD].Value.Trim(), "dd.MM.yy", CultureInfo.InvariantCulture);
        //                        break;
        //                    case 9:
        //                        StopItem.IssueDate = DateTime.ParseExact(rowData[DD].Value.Trim(), "dd.MM.yy", CultureInfo.InvariantCulture);
        //                        break;
        //                    case 10:
        //                        StopItem.Note = rowData[DD].Value.Trim();

        //                        stopList.Add(StopItem);
        //                        StopItem = new StopInfo();
        //                        StopItem.HotelStopDate = HotelStopDate;
        //                        CurrentRow = 0;

        //                        continue;
        //                    default:
        //                        break;
        //                }

        //                CurrentRow++;

        //                continue;
        //            }
        //        }
        //    }

        //    return stopList.Distinct().ToList();
        //}
    }



}