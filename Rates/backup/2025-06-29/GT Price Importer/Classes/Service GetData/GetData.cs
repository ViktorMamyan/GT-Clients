using Newtonsoft.Json;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT_Price_Importer.Classes
{
    internal class GetData
    {
        internal async Task<DataTable> HttpsData<T>(DefaultReturnData<T> obj, string directory, string MethodName, string Parameters)
        {
            DataLoader dl = new DataLoader();
            try
            {
                dl.progressPanel1.Description = "Working...";
                dl.Show();

                DefaultReturnData<T> ret_Data = new DefaultReturnData<T>();

                bool isError = false;
                string ErrorData = string.Empty;

                await Task.Run(() =>
                {
                    string link = string.Format(AppData.url + "{0}/{1}?{2}", directory, MethodName, Parameters);

                    string Json_result = JsonDownloader.GetAsync(link, 25000).Result;

                    ret_Data = JsonConvert.DeserializeObject<DefaultReturnData<T>>(Json_result);

                    if (ret_Data.StatusCode != 200)
                    {
                        dl.Invoke(new MethodInvoker(delegate { dl.Close(); }));

                        isError = true;
                        ErrorData = ret_Data.ErrorMsg;
                    }  
                });

                if (isError == true)
                {
                    throw new Exception(ErrorData);
                }

                if (ret_Data.Response != null)
                {
                    //DataTable dt = ret_Data.Response.ListToDataTable();
                    DataTable dt = ListToTable.ListToDataTable(ret_Data.Response);

                    return dt;
                }
                else
                {
                    MessageBox.Show("Nothing returned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
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