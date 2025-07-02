using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.Xml.Linq;

using gt_excelReader_lib;

namespace GT_Price_Importer.Classes
{
    internal class ExcelReader
    {
        internal enum FileType
        {
            Contract = 0,
            SPO = 1,
            STOP = 2
        }

        internal async Task<List<ReadyData>> ReadExcelXML(string File, string Sheet = "UnnamedPage_0", FileType fileType = FileType.Contract)
        {
            DataLoader dl = new DataLoader();
            try
            {
                dl.progressPanel1.Description = "Reading Excel File...";
                dl.Show();
                dl.Refresh();

                bool isError = false;
                string ErrorData = string.Empty;
                
                List<ReadyData> RData = new List<ReadyData>();

                await Task.Run(() =>
                {
                    if (fileType == FileType.Contract)
                    {
                        RData = new gt_excelReader_lib.ExcelReader().GetContractPrice(File, Sheet);    
                    }
                    else if (fileType == FileType.SPO)
                    {
                        RData = new gt_excelReader_lib.ExcelReader().GetSPOPrice(File, Sheet);    
                    }
                    else
                    {
                        //RData = new gt_excelReader_lib.ExcelReader().GetContractPrice(File, Sheet);    
                    }

                    if (RData == null)
                    {
                        isError = true;
                        ErrorData = "Worksheet is NULL";

                        dl.Invoke(new MethodInvoker(delegate { dl.Close(); }));
                    }
                });

                if (isError == true)
                {
                    throw new Exception(ErrorData);
                }

                return RData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
            finally
            {
                //close loader
                if (dl != null)
                {
                    dl.Close();
                    dl.Dispose();
                    dl = null;
                }
            }
        }
    }
}