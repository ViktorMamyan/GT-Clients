using DevExpress.Utils;
using DevExpress.XtraGrid;
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
    public partial class FrmPriceCorrecter : DevExpress.XtraEditors.XtraForm
    {
        internal bool IsDataChanged = false;

        internal bool IsContractPrice = false;
        internal bool IsSpoPrice = false;

        internal List<NewSerachHotel> HotelData = new List<NewSerachHotel>();
        DataTable Table;

        public FrmPriceCorrecter()
        {
            InitializeComponent();
        }

        private void FrmPriceCorrecter_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Table = HotelData.ToDataTable();
            LoadData(Table);
        }

        void LoadData(DataTable dt)
        {
            try
            {
                GridControl1.BeginUpdate();
                GridControl1.DataSource = dt;

                GridView1.Columns["Price"].DisplayFormat.FormatType = FormatType.Numeric;
                GridView1.Columns["Price"].DisplayFormat.FormatString = "{0:n2}";

                GridView1.Columns["RealPrice"].DisplayFormat.FormatType = FormatType.Numeric;
                GridView1.Columns["RealPrice"].DisplayFormat.FormatString = "{0:n2}";

                GridView1.Columns["PriceAddValue"].DisplayFormat.FormatType = FormatType.Numeric;
                GridView1.Columns["PriceAddValue"].DisplayFormat.FormatString = "{0:n2}";

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    GridView1.Columns[i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                }

                if (GridView1.Columns["HotelName"].Summary.ActiveCount == 0)
                {
                    GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "HotelName", "{0}");
                    GridView1.Columns["HotelName"].Summary.Add(item);
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

        private void btnSetPrice_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridView1.SelectedRowsCount == 0) throw new Exception("Նշված տողեր չկան");

                if (Convert.ToDecimal(txtPriceValue.Text) == 0) throw new Exception("Հավելավճարը նշված չէ");

                decimal ChangeValue = Convert.ToDecimal(txtPriceValue.Text);

                foreach (int rowHandle in GridView1.GetSelectedRows())
                {
                    if (rowHandle > -1)
                    {
                        DataRowView rowView = (DataRowView)GridView1.GetRow(rowHandle);
                        DataRow row = rowView.Row;

                        decimal Price = (decimal)row["Price"];

                        if (chPercent.Checked == true)
                        {
                            row["PriceAddedByPercent"] = true;
                            row["Price"] = (decimal)row["RealPrice"] + ((decimal)row["RealPrice"] * ChangeValue / 100);
                        }
                        else
                        {
                            row["PriceAddedByPercent"] = false;
                            row["Price"] = (decimal)row["RealPrice"] + ChangeValue;
                        }

                        row["PriceAddValue"] = ChangeValue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddToDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridView1.SelectedRowsCount == 0) throw new Exception("Նշված տողեր չկան");

                if (MessageBox.Show("Ցանկանու՞մ եք թարմացնել բազան", "Հարցում", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes) return;

                if (IsContractPrice == true)
                {
                    AddContractPrice();
                }
                else if (IsSpoPrice == true)
                {
                    AddSpoPrice();
                }
                
                IsDataChanged = true;

                MessageBox.Show("Գործողությունը կատարվեց", "Հարցում", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        async void AddContractPrice()
        {
            int[] SelRows = GridView1.GetSelectedRows();

                for (int rowHandle = SelRows.Length - 1; rowHandle >= 0; rowHandle--)
                {
                    if (rowHandle > -1)
                    {
                        DataRowView rowView = (DataRowView)GridView1.GetRow(rowHandle);
                        DataRow row = rowView.Row;

                        NewSerachHotel searchHotelData = new NewSerachHotel();
                        searchHotelData.HotelName = (string)row["HotelName"];
                        searchHotelData.Board = (string)row["Board"];
                        searchHotelData.Category = (string)row["Category"];
                        searchHotelData.Currency = (string)row["Currency"];

                        searchHotelData.NightsFrom = (int)row["NightsFrom"];
                        searchHotelData.NightsTill = (int)row["NightsTill"];

                        searchHotelData.ReservationStart = !DBNull.Value.Equals(row["ReservationStart"]) ? (DateTime)row["ReservationStart"] : (DateTime?)null;
                        searchHotelData.ReservationEnd = !DBNull.Value.Equals(row["ReservationEnd"]) ? (DateTime)row["ReservationEnd"] : (DateTime?)null;
                        searchHotelData.PeriodsStart = !DBNull.Value.Equals(row["PeriodsStart"]) ? (DateTime)row["PeriodsStart"] : (DateTime?)null;
                        searchHotelData.PeriodsEnd = !DBNull.Value.Equals(row["PeriodsEnd"]) ? (DateTime)row["PeriodsEnd"] : (DateTime?)null;

                        searchHotelData.Room = (string)row["Room"];
                        searchHotelData.Accommodation = (string)row["Accommodation"];

                        searchHotelData.Price = !DBNull.Value.Equals(row["Price"]) ? (decimal)row["Price"] : (decimal?)null;
                        searchHotelData.RealPrice = !DBNull.Value.Equals(row["RealPrice"]) ? (decimal)row["RealPrice"] : (decimal?)null;
                        searchHotelData.PriceAddedByPercent = (bool)row["PriceAddedByPercent"];
                        searchHotelData.PriceAddValue = (decimal)row["PriceAddValue"];

                        await new SetData().HttpsDataDefault("Hotel", "NewSerachHotelAsync", "", searchHotelData);

                        row.Delete();
                    }
                }
        }

        async void AddSpoPrice()
        {
            int[] SelRows = GridView1.GetSelectedRows();

            for (int rowHandle = SelRows.Length - 1; rowHandle >= 0; rowHandle--)
            {
                if (rowHandle > -1)
                {
                    DataRowView rowView = (DataRowView)GridView1.GetRow(rowHandle);
                    DataRow row = rowView.Row;

                    NewSerachHotel searchHotelData = new NewSerachHotel();
                    searchHotelData.HotelName = (string)row["HotelName"];
                    searchHotelData.Board = (string)row["Board"];
                    searchHotelData.Category = (string)row["Category"];
                    searchHotelData.Currency = (string)row["Currency"];

                    searchHotelData.NightsFrom = (int)row["NightsFrom"];
                    searchHotelData.NightsTill = (int)row["NightsTill"];

                    searchHotelData.ReservationStart = !DBNull.Value.Equals(row["ReservationStart"]) ? (DateTime)row["ReservationStart"] : (DateTime?)null;
                    searchHotelData.ReservationEnd = !DBNull.Value.Equals(row["ReservationEnd"]) ? (DateTime)row["ReservationEnd"] : (DateTime?)null;
                    searchHotelData.PeriodsStart = !DBNull.Value.Equals(row["PeriodsStart"]) ? (DateTime)row["PeriodsStart"] : (DateTime?)null;
                    searchHotelData.PeriodsEnd = !DBNull.Value.Equals(row["PeriodsEnd"]) ? (DateTime)row["PeriodsEnd"] : (DateTime?)null;

                    searchHotelData.Room = (string)row["Room"];
                    searchHotelData.Accommodation = (string)row["Accommodation"];

                    searchHotelData.Price = !DBNull.Value.Equals(row["Price"]) ? (decimal)row["Price"] : (decimal?)null;
                    searchHotelData.RealPrice = !DBNull.Value.Equals(row["RealPrice"]) ? (decimal)row["RealPrice"] : (decimal?)null;
                    searchHotelData.PriceAddedByPercent = (bool)row["PriceAddedByPercent"];
                    searchHotelData.PriceAddValue = (decimal)row["PriceAddValue"];
                    searchHotelData.SPO_No = (string)row["SPO_No"];

                    await new SetData().HttpsDataDefault("Hotel", "NewSpoSerachHotelAsync", "", searchHotelData);

                    row.Delete();
                }
            }
        }

    }
}