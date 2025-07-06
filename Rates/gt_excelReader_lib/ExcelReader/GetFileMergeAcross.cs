using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace gt_excelReader_lib
{
    public partial class DataReader
    {
        public int GetFileMergeAcross(string File, string Sheet = "UnnamedPage_0")
        {
            int MergeAcross = 0;

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
                        MergeAcross = rowData[DD].MergeAcross;
                        ShouldBrake = true;
                    }
                }
            }

            return MergeAcross;
        }
    }
}