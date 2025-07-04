using DevExpress.Utils;
using DevExpress.XtraGrid;
using gt_excelReader_lib;
using GT_Price_Importer.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT_Price_Importer
{
    public partial class FrmStopList : DevExpress.XtraEditors.XtraForm
    {
        internal bool IsDataChanged = false;

        internal List<StopInfo> StopData = new List<StopInfo>();
        DataTable Table;

        public FrmStopList()
        {
            InitializeComponent();
        }

        private void FrmPriceCorrecter_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Table = StopData.ToDataTable();
            LoadData(Table);
        }

        void LoadData(DataTable dt)
        {
            try
            {
                GridControl1.BeginUpdate();
                GridControl1.DataSource = dt;

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    GridView1.Columns[i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                }

                if (GridView1.Columns["Hotel"].Summary.ActiveCount == 0)
                {
                    GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Hotel", "{0}");
                    GridView1.Columns["Hotel"].Summary.Add(item);
                }

                GridView1.ClearSelection();
                GridControl1.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPriceValue_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == string.Empty) e.NewValue = 0;
            if (e.NewValue.ToString().Contains("-")) e.Cancel = true;
        }

        private void btnAddToDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridView1.SelectedRowsCount == 0) throw new Exception("Նշված տողեր չկան");

                if (MessageBox.Show("Ցանկանու՞մ եք թարմացնել բազան", "Հարցում", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes) return;

                AddStopList();

                IsDataChanged = true;

                MessageBox.Show("Գործողությունը կատարվեց", "Հարցում", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        async void AddStopList()
        {
            int[] SelRows = GridView1.GetSelectedRows();

                for (int rowHandle = SelRows.Length - 1; rowHandle >= 0; rowHandle--)
                {
                    if (rowHandle > -1)
                    {
                        DataRowView rowView = (DataRowView)GridView1.GetRow(rowHandle);
                        DataRow row = rowView.Row;

                        StopInfo stopHotel = new StopInfo();
                        stopHotel.HotelStopDate = (DateTime)row["HotelStopDate"];
                        stopHotel.Hotel = (string)row["Hotel"];
                        stopHotel.Touroperator = (string)row["Touroperator"];
                        stopHotel.Market = (string)row["Market"];
                        stopHotel.Region = (string)row["Region"];
                        stopHotel.Room = (string)row["Room"];
                        stopHotel.Accommodation = (string)row["Accommodation"];
                        stopHotel.Meal = (string)row["Meal"];
                        stopHotel.DateFrom = (DateTime)row["DateFrom"];
                        stopHotel.DateTill = (DateTime)row["DateTill"];
                        stopHotel.IssueDate = (DateTime)row["IssueDate"];
                        stopHotel.Note = (string)row["Note"];

                        await new SetData().HttpsDataDefault("Hotel", "NewStopHotelAsync", "", stopHotel);

                        row.Delete();
                    }
                }
        }
    }
}