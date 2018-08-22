using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using DevExpress.XtraGrid;
using GT_Clients.Forms;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace GT_Clients
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
            GridView1.DoubleClick += GridView1_DoubleClick;
        }

        #region Variables

        internal const int SC_CLOSE = 0xF060;
        internal const int MF_GRAYED = 0x1;
        internal const int MF_ENABLED = 0x00000000;
        internal const int MF_DISABLED = 0x00000002;
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr HWNDValue, bool Revert);
        [DllImport("user32.dll")]
        private static extern int EnableMenuItem(IntPtr tMenu, int targetItem, int targetStatus);

        List<Clients> CL = new List<Clients>();

        #endregion

        #region Custom Methods

        void DisableControls()
        {
            btnLoadData.Enabled = false;
            btnEdit.Enabled = false;
            btnAdd.Enabled = false;

            GridControl1.Enabled = false;

            EnableMenuItem(GetSystemMenu(this.Handle, false), SC_CLOSE, MF_GRAYED);

            this.Refresh();
        }

        void EnableControls()
        {
            btnLoadData.Enabled = true;
            btnEdit.Enabled = true;
            btnAdd.Enabled = true;

            GridControl1.Enabled = true;

            EnableMenuItem(GetSystemMenu(this.Handle, false), SC_CLOSE, MF_ENABLED);

            this.Refresh();
        }

        internal void RefreshGrid()
        {
            btnLoadData_Click(btnLoadData, null);
        }

        #endregion

        #region Events

            private void MainForm_Shown(object sender, EventArgs e)
            {
                this.WindowState = FormWindowState.Maximized;
                RefreshGrid();
            }

            private async void btnLoadData_Click(object sender, EventArgs e)
                {
                    DataLoaderX.DataLoader dl = new DataLoaderX.DataLoader();
                    try
                    {
                        dl.progressPanel1.Description = "Բազայի բեռնում ...";
                        dl.Show();

                        DisableControls();

                        CL.Clear();

                        if (System.IO.File.Exists("Clients.db"))
                        {
                            await LoadFile();
                        }

                        await SetDataTable(CL);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        dl.Close();
                        dl.Dispose();
                        dl = null;

                        EnableControls();

                        roClient.Text = String.Empty;
                        roBDate.Text = String.Empty;
                        roCity.Text = String.Empty;
                        roContact.Text = String.Empty;
                        roPlace.Text = String.Empty;
                        roTravelDate.Text = String.Empty;
                        roNumber.Text = String.Empty;
                        roChild.Text = String.Empty;
                        roComment.Text = String.Empty;
                    }
                }

            private void btnEdit_Click(object sender, EventArgs e)
            {

            }

            private void btnAdd_Click(object sender, EventArgs e)
            {
                ClientAdd f = new ClientAdd() { RefForm = (MainForm)this };
                f.ShowDialog();
                f.Dispose();
            }

            private void btnDelete_Click(object sender, EventArgs e)
            {
                try
                {
                    if(GridView1.SelectedRowsCount == 0) throw new Exception("Նշված տողեր չկան");

                    if (MessageBox.Show("Ցանկանու՞մ եք ջնջել գրանցումը", "Հարցում", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No) return;

                    DataRow rowData;
                    int[] listRowList = GridView1.GetSelectedRows();
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = GridView1.GetDataRow(listRowList[i]);
                        CL.RemoveAll((x) => x.ID == Convert.ToInt32(rowData["ID"].ToString()));
                    }

                    string json = JsonConvert.SerializeObject(CL, Formatting.Indented);
                    System.IO.File.WriteAllText(@"Clients.db", json, Encoding.UTF8);

                    RefreshGrid();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            void GridView1_DoubleClick(object sender, EventArgs e)
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);
                DoRowDoubleClick(view, pt);
            }

            private void DoRowDoubleClick(GridView view, Point pt)
            {
                try
                {
                    GridHitInfo info = view.CalcHitInfo(pt);
                    if (info.InRow || info.InRowCell)
                    {
                        if (info.Column == null)
                        {
                        }
                        else
                        {
                            if ((view.GetRowCellValue(info.RowHandle, "ID")) == DBNull.Value || Convert.ToInt32(view.GetRowCellValue(info.RowHandle, "ID")) <= 0)
                                return;

                            GridView1.ClearSelection();
                            GridView1.SelectRows(info.RowHandle, info.RowHandle);

                            roClient.Text = String.Concat(view.GetRowCellValue(info.RowHandle, "Name").ToString()," ", view.GetRowCellValue(info.RowHandle, "LastName").ToString());
                            roBDate.Text = String.IsNullOrEmpty(view.GetRowCellValue(info.RowHandle, "BirdDate").ToString()) ? String.Empty : Convert.ToDateTime(view.GetRowCellValue(info.RowHandle, "BirdDate")).ToShortDateString();
                            roCity.Text = view.GetRowCellValue(info.RowHandle, "City").ToString();
                            roContact.Text = view.GetRowCellValue(info.RowHandle, "Contacts").ToString();
                            roPlace.Text = view.GetRowCellValue(info.RowHandle, "Destination").ToString();
                            roTravelDate.Text = String.IsNullOrEmpty(view.GetRowCellValue(info.RowHandle, "TravelDate").ToString()) ? String.Empty : Convert.ToDateTime(view.GetRowCellValue(info.RowHandle, "TravelDate")).ToShortDateString();
                            roNumber.Text = view.GetRowCellValue(info.RowHandle, "PersonsNumber").ToString();
                            roChild.Text = view.GetRowCellValue(info.RowHandle, "Childs").ToString();
                            roComment.Text = view.GetRowCellValue(info.RowHandle, "Comments").ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        #endregion 

        #region Async Task

            async Task SetDataTable(List<Clients> SD)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        GridControl1.Invoke(new Action(() => GridControl1.BeginUpdate()));
                        GridControl1.Invoke(new Action(() => GridControl1.DataSource = null));
                        GridControl1.Invoke(new Action(() => GridView1.Columns.Clear()));

                        DataTable dt = Ext.ToDataTable(SD);

                        GridControl1.Invoke(new Action(() => GridControl1.DataSource = dt));

                        for (int i = 0; i < GridView1.Columns.Count; i++)
                        {
                            GridView1.Columns[i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                        }

                        GridView1.Columns["ID"].Visible = false;
                        GridView1.Columns["Name"].Caption = "Անուն";
                        GridView1.Columns["LastName"].Caption = "Ազգանուն";
                        GridView1.Columns["BirdDate"].Caption = "Ծննդյան Ամս.";
                        GridView1.Columns["City"].Caption = "Բնակավայր";
                        GridView1.Columns["Contacts"].Caption = "Կոնտակտային Տվ.";
                        GridView1.Columns["Destination"].Caption = "Մեկնման Վայր";
                        GridView1.Columns["TravelDate"].Caption = "Մեկնման Ամս.";
                        GridView1.Columns["PersonsNumber"].Caption = "Անձերի Քանակ";
                        GridView1.Columns["Childs"].Caption = "Երեխաներ";
                        GridView1.Columns["Comments"].Caption = "Մեկնաբանություն";

                        if (GridView1.RowCount > 0)
                        {
                            if (GridView1.Columns["Name"].Summary.ActiveCount == 0)
                            {
                                GridView1.Columns["Name"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Name", "Count` {0}"));
                            }
                        }

                        GridView1.ClearSelection();
                        GridControl1.Invoke(new Action(() => GridControl1.EndUpdate()));

                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

            async Task LoadFile()
            {
                try
                {
                    await Task.Run(() =>
                    {
                        string Content = System.IO.File.ReadAllText("Clients.db", Encoding.UTF8);
                        if (!String.IsNullOrEmpty(Content))
                        {
                            CL = JsonConvert.DeserializeObject<List<Clients>>(Content);    
                        }
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        #endregion

    }
}

#region Custom Classes

public class Clients
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime? BirdDate { get; set; }
    public string City { get; set; }
    public string Contacts { get; set; }
    public string Destination { get; set; }
    public DateTime? TravelDate { get; set; }
    public int PersonsNumber { get; set; }
    public string Childs { get; set; }
    public string Comments { get; set; }
}

public static class Ext
{

    internal static DataTable ToDataTable<T>(this IList<T> data)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable dt = new DataTable();
        for (int i = 0; i <= properties.Count - 1; i++)
        {
            PropertyDescriptor property = properties[i];
            //dt.Columns.Add(property.Name, property.PropertyType);
            dt.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
        }
        object[] values = new object[properties.Count - 1 + 1];
        foreach (T item in data)
        {
            for (int i = 0; i <= values.Length - 1; i++)
                values[i] = properties[i].GetValue(item);
            dt.Rows.Add(values);
        }
        return dt;
    }

}

#endregion