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
    public partial class FrmPriceCorrecter_6 : DevExpress.XtraEditors.XtraForm
    {
        internal bool IsDataChanged = false;

        internal bool IsContractPrice = false;
        internal bool IsSpoPrice = false;

        internal List<NewSerachHotel> HotelData = new List<NewSerachHotel>();
        DataTable Table;

        public FrmPriceCorrecter_6()
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

                GridView1.Columns["B2BPrice"].DisplayFormat.FormatType = FormatType.Numeric;
                GridView1.Columns["B2BPrice"].DisplayFormat.FormatString = "{0:n2}";

                GridView1.Columns["B2CPrice"].DisplayFormat.FormatType = FormatType.Numeric;
                GridView1.Columns["B2CPrice"].DisplayFormat.FormatString = "{0:n2}";

                GridView1.Columns["RealPrice"].DisplayFormat.FormatType = FormatType.Numeric;
                GridView1.Columns["RealPrice"].DisplayFormat.FormatString = "{0:n2}";

                GridView1.Columns["B2BPriceAddValue"].DisplayFormat.FormatType = FormatType.Numeric;
                GridView1.Columns["B2BPriceAddValue"].DisplayFormat.FormatString = "{0:n2}";

                GridView1.Columns["B2CPriceAddValue"].DisplayFormat.FormatType = FormatType.Numeric;
                GridView1.Columns["B2CPriceAddValue"].DisplayFormat.FormatString = "{0:n2}";

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

        private void txtB2CPriceValue_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString() == string.Empty) e.NewValue = 0;
            if (e.NewValue.ToString().Contains("-")) e.Cancel = true;
        }

        private void btnSetPrice_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridView1.SelectedRowsCount == 0) throw new Exception("Նշված տողեր չկան");

                if (Convert.ToDecimal(txtB2BPriceValue.Text) == 0) throw new Exception("B2B հավելավճարը նշված չէ");
                if (Convert.ToDecimal(txtB2CPriceValue.Text) == 0) throw new Exception("B2C հավելավճարը նշված չէ");

                decimal B2BChangeValue = Convert.ToDecimal(txtB2BPriceValue.Text);
                decimal B2CChangeValue = Convert.ToDecimal(txtB2CPriceValue.Text);

                for (int i = 0; i < GridView1.RowCount; i++)
                {
                    int rowHandle = GridView1.GetVisibleRowHandle(i);

                    if (GridView1.IsRowSelected(rowHandle))
                    {
                        DataRowView rowView = (DataRowView)GridView1.GetRow(rowHandle);
                        DataRow row = rowView.Row;

                        if (chPercent.Checked == true)
                        {
                            //row["PriceAddedByPercent"] = true;
                            //row["B2BPrice"] = (decimal)row["RealPrice"] + ((decimal)row["RealPrice"] * B2BChangeValue / 100);
                            //row["B2CPrice"] = (decimal)row["RealPrice"] + ((decimal)row["RealPrice"] * B2CChangeValue / 100);

                            GridView1.SetRowCellValue(rowHandle, "PriceAddedByPercent", true);
                            GridView1.SetRowCellValue(rowHandle, "B2BPrice", (decimal)row["RealPrice"] + ((decimal)row["RealPrice"] * B2BChangeValue / 100));
                            GridView1.SetRowCellValue(rowHandle, "B2CPrice", (decimal)row["RealPrice"] + ((decimal)row["RealPrice"] * B2CChangeValue / 100));

                        }
                        else
                        {
                            //row["PriceAddedByPercent"] = false;
                            //row["B2BPrice"] = (decimal)row["RealPrice"] + B2BChangeValue;
                            //row["B2CPrice"] = (decimal)row["RealPrice"] + B2CChangeValue;

                            GridView1.SetRowCellValue(rowHandle, "PriceAddedByPercent", false);
                            GridView1.SetRowCellValue(rowHandle, "B2BPrice", (decimal)row["RealPrice"] + B2BChangeValue);
                            GridView1.SetRowCellValue(rowHandle, "B2CPrice", (decimal)row["RealPrice"] + B2CChangeValue);
                        }


                        //row["B2BPriceAddValue"] = B2BChangeValue;
                        //row["B2CPriceAddValue"] = B2CChangeValue;

                        GridView1.SetRowCellValue(rowHandle, "B2BPriceAddValue", B2BChangeValue);
                        GridView1.SetRowCellValue(rowHandle, "B2CPriceAddValue", B2CChangeValue);

                        // Force commit to the DataTable
                        GridView1.PostEditor();        // Applies current editor value
                        GridView1.UpdateCurrentRow();  // Pushes all edited values to DataTable
                    }
                }


                //foreach (int rowHandle in GridView1.GetSelectedRows())
                //{
                //    if (rowHandle > -1)
                //    {
                //        DataRowView rowView = (DataRowView)GridView1.GetRow(rowHandle);
                //        DataRow row = rowView.Row;

                //        if (chPercent.Checked == true)
                //        {
                //            row["PriceAddedByPercent"] = true;
                //            row["B2BPrice"] = (decimal)row["RealPrice"] + ((decimal)row["RealPrice"] * B2BChangeValue / 100);
                //            row["B2CPrice"] = (decimal)row["RealPrice"] + ((decimal)row["RealPrice"] * B2CChangeValue / 100);
                //        }
                //        else
                //        {
                //            row["PriceAddedByPercent"] = false;
                //            row["B2BPrice"] = (decimal)row["RealPrice"] + B2BChangeValue;
                //            row["B2CPrice"] = (decimal)row["RealPrice"] + B2CChangeValue;
                //        }

                //        row["B2BPriceAddValue"] = B2BChangeValue;
                //        row["B2CPriceAddValue"] = B2CChangeValue;
                //    }
                //}

                MessageBox.Show("Վաճառքի գները տեղադրված են", "Հաղորդագրություն", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAddToDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ցանկանու՞մ եք թարմացնել բազան", "Հարցում", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes) return;

            DataLoader dl = new DataLoader();

            try
            {
                dl.progressPanel1.Description = "Working...";
                dl.Show();

                if (GridView1.SelectedRowsCount == 0) throw new Exception("Նշված տողեր չկան");

                if (IsContractPrice == true)
                {
                    await AddContractPrice();
                }
                else if (IsSpoPrice == true)
                {
                    await AddSpoPrice();
                }

                IsDataChanged = true;

                dl.Invoke(new MethodInvoker(delegate { dl.Close(); }));

                MessageBox.Show("Գործողությունը կատարվեց", "Հարցում", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        async Task AddContractPrice()
        {
            for (int rowHandle = GridView1.RowCount; rowHandle >= 0; rowHandle--)
            {
                if (GridView1.IsRowSelected(rowHandle))
                {
                    DataRowView rowView = (DataRowView)GridView1.GetRow(rowHandle);
                    DataRow row = rowView.Row;

                    if ((decimal)row["B2BPriceAddValue"] == 0M) throw new Exception("B2B հավելավճարը գրված չէ");
                    if ((decimal)row["B2CPriceAddValue"] == 0M) throw new Exception("B2B հավելավճարը գրված չէ");

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

                    searchHotelData.B2BPrice = !DBNull.Value.Equals(row["B2BPrice"]) ? (decimal)row["B2BPrice"] : (decimal?)null;
                    searchHotelData.B2CPrice = !DBNull.Value.Equals(row["B2CPrice"]) ? (decimal)row["B2CPrice"] : (decimal?)null;
                    searchHotelData.RealPrice = !DBNull.Value.Equals(row["RealPrice"]) ? (decimal)row["RealPrice"] : (decimal?)null;

                    searchHotelData.PriceAddedByPercent = (bool)row["PriceAddedByPercent"];
                    searchHotelData.B2BPriceAddValue = (decimal)row["B2BPriceAddValue"];
                    searchHotelData.B2CPriceAddValue = (decimal)row["B2CPriceAddValue"];

                    searchHotelData.ADL = (int)row["ADL"];
                    searchHotelData.CHD = (int)row["CHD"];

                    searchHotelData.CHDStart1 = (!DBNull.Value.Equals(row["CHDStart1"])) ? (int)row["CHDStart1"] : (int?)null;
                    searchHotelData.CHDEnd1 = (!DBNull.Value.Equals(row["CHDEnd1"])) ? (int)row["CHDEnd1"] : (int?)null;

                    searchHotelData.CHDStart2 = (!DBNull.Value.Equals(row["CHDStart2"])) ? (int)row["CHDStart2"] : (int?)null;
                    searchHotelData.CHDEnd2 = (!DBNull.Value.Equals(row["CHDEnd2"])) ? (int)row["CHDEnd2"] : (int?)null;

                    searchHotelData.CHDStart3 = (!DBNull.Value.Equals(row["CHDStart3"])) ? (int)row["CHDStart3"] : (int?)null;
                    searchHotelData.CHDEnd3 = (!DBNull.Value.Equals(row["CHDEnd3"])) ? (int)row["CHDEnd3"] : (int?)null;

                    searchHotelData.INFStart = (!DBNull.Value.Equals(row["INFStart"])) ? (int)row["INFStart"] : (int?)null;
                    searchHotelData.INFEnd = (!DBNull.Value.Equals(row["INFEnd"])) ? (int)row["INFEnd"] : (int?)null;

                    await new SetData().HttpsDataDefault("Hotel", "NewSerachHotelAsync", "", searchHotelData);

                    if (rowHandle >= 0 && !GridView1.IsNewItemRow(rowHandle))
                    {
                        DataRowView rowDelView = (DataRowView)GridView1.GetRow(rowHandle);
                        if (rowDelView != null)
                        {
                            rowDelView.Row.Delete();
                        }
                    }
                }
            }
        }

        async Task AddSpoPrice()
        {
            for (int rowHandle = GridView1.RowCount; rowHandle >= 0; rowHandle--)
            {
                if (GridView1.IsRowSelected(rowHandle))
                {
                    DataRowView rowView = (DataRowView)GridView1.GetRow(rowHandle);
                    DataRow row = rowView.Row;

                    if ((decimal)row["B2BPriceAddValue"] == 0M) throw new Exception("B2B հավելավճարը գրված չէ");
                    if ((decimal)row["B2CPriceAddValue"] == 0M) throw new Exception("B2B հավելավճարը գրված չէ");

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

                    searchHotelData.B2BPrice = !DBNull.Value.Equals(row["B2BPrice"]) ? (decimal)row["B2BPrice"] : (decimal?)null;
                    searchHotelData.B2CPrice = !DBNull.Value.Equals(row["B2CPrice"]) ? (decimal)row["B2CPrice"] : (decimal?)null;
                    searchHotelData.RealPrice = !DBNull.Value.Equals(row["RealPrice"]) ? (decimal)row["RealPrice"] : (decimal?)null;

                    searchHotelData.PriceAddedByPercent = (bool)row["PriceAddedByPercent"];
                    searchHotelData.B2BPriceAddValue = (decimal)row["B2BPriceAddValue"];
                    searchHotelData.B2CPriceAddValue = (decimal)row["B2CPriceAddValue"];

                    searchHotelData.SPO_No = (string)row["SPO_No"];

                    searchHotelData.ADL = (int)row["ADL"];
                    searchHotelData.CHD = (int)row["CHD"];

                    searchHotelData.CHDStart1 = (!DBNull.Value.Equals(row["CHDStart1"])) ? (int)row["CHDStart1"] : (int?)null;
                    searchHotelData.CHDEnd1 = (!DBNull.Value.Equals(row["CHDEnd1"])) ? (int)row["CHDEnd1"] : (int?)null;

                    searchHotelData.CHDStart2 = (!DBNull.Value.Equals(row["CHDStart2"])) ? (int)row["CHDStart2"] : (int?)null;
                    searchHotelData.CHDEnd2 = (!DBNull.Value.Equals(row["CHDEnd2"])) ? (int)row["CHDEnd2"] : (int?)null;

                    searchHotelData.CHDStart3 = (!DBNull.Value.Equals(row["CHDStart3"])) ? (int)row["CHDStart3"] : (int?)null;
                    searchHotelData.CHDEnd3 = (!DBNull.Value.Equals(row["CHDEnd3"])) ? (int)row["CHDEnd3"] : (int?)null;

                    searchHotelData.INFStart = (!DBNull.Value.Equals(row["INFStart"])) ? (int)row["INFStart"] : (int?)null;
                    searchHotelData.INFEnd = (!DBNull.Value.Equals(row["INFEnd"])) ? (int)row["INFEnd"] : (int?)null;

                    await new SetData().HttpsDataDefault("Hotel", "NewSpoSerachHotelAsync", "", searchHotelData);

                    if (rowHandle >= 0 && !GridView1.IsNewItemRow(rowHandle))
                    {
                        DataRowView rowDelView = (DataRowView)GridView1.GetRow(rowHandle);
                        if (rowDelView != null)
                        {
                            rowDelView.Row.Delete();
                        }
                    }
                }
            }
        }

        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                //Disable Child Gridview
                (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).CellButtonRect = Rectangle.Empty;
            }
            catch (Exception)
            {
            }
        }

    }
}