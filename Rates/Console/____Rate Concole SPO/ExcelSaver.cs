using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System;

namespace ConsoleApp1
{
    internal class ExcelSaver
    {
        public void SaveDataTableToExcel(DataTable dt, string filePath)
        {
            var excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel._Worksheet worksheet = workbook.Sheets[1];
            worksheet = workbook.ActiveSheet;

            // Add column headers
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
            }

            // Add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dt.Rows[i][j]?.ToString();
                }

                Console.WriteLine("Row " + i);
            }

            // Save and close
            workbook.SaveAs(filePath);
            workbook.Close();
            excelApp.Quit();

            // Clean up
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

    }
}