using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT_Price_Importer.Classes
{
    internal class SetData
    {
        internal async Task<bool> HttpsData<T>(DefaultReturnData<T> obj, string directory, string MethodName, string Parameters, object cls)
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

                    string Json_result = JsonDownloader.PostAsync(link, cls).Result;

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

                if (ret_Data.StatusCode == 200)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Nothing returned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
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

        internal async Task<bool> HttpsDataDefault(string directory, string MethodName, string Parameters, object cls)
        {
            //DataLoader dl = new DataLoader();
            try
            {
                //dl.progressPanel1.Description = "Working...";
                //dl.Show();

                DefaultReturnData ret_Data = new DefaultReturnData();

                bool isError = false;
                string ErrorData = string.Empty;

                await Task.Run(() =>
                {
                    string link = string.Format(AppData.url + "{0}/{1}?{2}", directory, MethodName, Parameters);

                    string Json_result = JsonDownloader.PostAsync(link, cls).Result;

                    ret_Data = JsonConvert.DeserializeObject<DefaultReturnData>(Json_result);

                    if (ret_Data.StatusCode != 200)
                    {
                        //dl.Invoke(new MethodInvoker(delegate { dl.Close(); }));

                        isError = true;
                        ErrorData = ret_Data.ErrorMsg;
                    }
                });

                if (isError == true)
                {
                    throw new Exception(ErrorData);
                }

                if (ret_Data.StatusCode == 200)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Nothing returned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            //finally
            //{
            //    //close loader
            //    if (dl != null)
            //    {
            //        dl.Close();
            //        dl.Dispose();
            //        dl = null;
            //    }
            //}
        }
    }
}