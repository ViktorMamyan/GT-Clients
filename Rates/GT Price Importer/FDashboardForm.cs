﻿using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GT_Price_Importer.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT_Price_Importer
{
    public partial class FDashboardForm : DevExpress.XtraEditors.XtraForm
    {
        public FDashboardForm()
        {
            InitializeComponent();

            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("hy-AM");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        #region Variables

        string ExcelFile = string.Empty;

        List<CountyItem> ListCountry = new List<CountyItem>();
        List<OperatorItem> ListOperator = new List<OperatorItem>();

        #endregion

        #region Load

        async void LoadCountryList()
        {
            DataTable CountryTable = await new GetData().HttpsData(new CountryList(), "Country", "GetCountryAsync", "");
            ListCountry.Clear();
            ListCountry = ListToTable.DataTableToList<CountyItem>(CountryTable);
        }

        async void LoadOperatorList()
        {
            DataTable OperatorTable = await new GetData().HttpsData(new OperatorList(), "Operator", "GetOperatorAsync", "");
            ListOperator.Clear();
            ListOperator = ListToTable.DataTableToList<OperatorItem>(OperatorTable);
        }

        #endregion

        #region Form Events

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoadCountryList();
            LoadOperatorList();
        }

        #endregion

        #region Country

        private void txtCountry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Down)
                {
                    SearchCountry(txtCountry.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtCountry_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                ButtonEdit Editor = (ButtonEdit)sender;
                EditorButton Button = e.Button;

                txtCountry.Tag = null;

                string search = txtCountry.Text.Trim();

                switch (Editor.Properties.Buttons.IndexOf(e.Button))
                {
                    case 0:
                        {
                            if (search == string.Empty)
                            {
                                SearchCountry(null);
                            }
                            else
                            {
                                SearchCountry(search);
                            }

                            break;
                        }
                    case 1:
                        SearchCountry(null);

                        break;
                    case 2:
                        {
                            NewCountryForm();

                            break;
                        }
                    case 3:
                        {
                            txtCountry.Text = string.Empty;
                            txtCountry.Tag = null;

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void SearchCountry(string search)
        {
            List<CountyItem> query = new List<CountyItem>(); ;
            
            if (!string.IsNullOrEmpty(search))
            {
                query = ListCountry.Where(x => x.IsActive = true && x.Country != null && x.Country.StartsWith(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (query != null && query.Count == 1)
            {
                txtCountry.Text = query[0].Country;
                txtCountry.Tag = query[0].ID;
            }
            else
            {
                List<DataPairs> table = new List<DataPairs>();

                if (query != null && query.Count > 1)
                {
                    foreach (CountyItem item in query)
                    {
                        table.Add(new DataPairs { ID = item.ID, Name = item.Country });
                    }
                }
                else
                {
                    foreach (CountyItem item in ListCountry.Where(x=> x.IsActive = true))
                    {
                        table.Add(new DataPairs{ ID = item.ID, Name = item.Country});
                    }
                }

                frmSelecterWindow f = new frmSelecterWindow();

                f.StartPosition = FormStartPosition.Manual;
                f.Location = this.PointToScreen(new Point(groupControl1.Left + txtCountry.Left, groupControl1.Top + txtCountry.Top + txtCountry.Height));

                f.table = table;
                f.ShowDialog();

                if (f.DataID > 0)
                {
                    txtCountry.Tag = f.DataID;
                    txtCountry.Text = f.DataName;
                }

                f.Dispose();
            }
        }
        void NewCountryForm()
        {
            try
            {
                FrmNewCountry f = new FrmNewCountry();

                f.StartPosition = FormStartPosition.Manual;
                f.Location = this.PointToScreen(new Point(groupControl1.Left + txtCountry.Left, groupControl1.Top + txtCountry.Top + txtCountry.Height));

                f.ShowDialog();

                if (f.IsDataChanged == true)
                {
                    LoadCountryList();

                    txtCountry.Text = f.DataName;

                    SearchCountry(f.DataName);
                }

                f.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Operator

        private void txtOperator_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Down)
                {
                    SearchOperator(txtOperator.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtOperator_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                ButtonEdit Editor = (ButtonEdit)sender;
                EditorButton Button = e.Button;

                txtOperator.Tag = null;

                string search = txtOperator.Text.Trim();

                switch (Editor.Properties.Buttons.IndexOf(e.Button))
                {
                    case 0:
                        {
                            if (search == string.Empty)
                            {
                                SearchOperator(null);
                            }
                            else
                            {
                                SearchOperator(search);
                            }

                            break;
                        }
                    case 1:
                        SearchOperator(null);

                        break;
                    case 2:
                        {
                            NewOperatorForm();

                            break;
                        }
                    case 3:
                        {
                            txtOperator.Text = string.Empty;
                            txtOperator.Tag = null;

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void SearchOperator(string search)
        {
            List<OperatorItem> query = new List<OperatorItem>(); ;

            if (!string.IsNullOrEmpty(search))
            {
                query = ListOperator.Where(x => x.IsActive = true && x.Operator != null && x.Operator.StartsWith(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (query != null && query.Count == 1)
            {
                txtOperator.Text = query[0].Operator;
                txtOperator.Tag = query[0].ID;
            }
            else
            {
                List<DataPairs> table = new List<DataPairs>();

                if (query != null && query.Count > 1)
                {
                    foreach (OperatorItem item in query)
                    {
                        table.Add(new DataPairs { ID = item.ID, Name = item.Operator });
                    }
                }
                else
                {
                    foreach (OperatorItem item in ListOperator.Where(x => x.IsActive = true))
                    {
                        table.Add(new DataPairs { ID = item.ID, Name = item.Operator });
                    }
                }

                frmSelecterWindow f = new frmSelecterWindow();

                f.StartPosition = FormStartPosition.Manual;
                f.Location = this.PointToScreen(new Point(groupControl1.Left + txtOperator.Left, groupControl1.Top + txtOperator.Top + txtOperator.Height));

                f.table = table;
                f.ShowDialog();

                if (f.DataID > 0)
                {
                    txtOperator.Tag = f.DataID;
                    txtOperator.Text = f.DataName;
                }

                f.Dispose();
            }
        }
        void NewOperatorForm()
        {
            try
            {
                FrmNewOperator f = new FrmNewOperator();

                f.StartPosition = FormStartPosition.Manual;
                f.Location = this.PointToScreen(new Point(groupControl1.Left + txtOperator.Left, groupControl1.Top + txtOperator.Top + txtOperator.Height));

                f.ShowDialog();

                if (f.IsDataChanged == true)
                {
                    LoadOperatorList();

                    txtOperator.Text = f.DataName;

                    SearchOperator(f.DataName);
                }

                f.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Excel Selecter

        private void btnSelectExcelFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                ButtonEdit Editor = (ButtonEdit)sender;
                EditorButton Button = e.Button;

                switch ( (Editor.Properties.Buttons.IndexOf(e.Button)))
                {
                    case 0:
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.Filter = "Excel (.xls)|*.xls";
                        dialog.FilterIndex = 1;
                        dialog.RestoreDirectory = true;

                        if (dialog.ShowDialog() == DialogResult.Cancel) btnSelectExcelFile.Text = string.Empty;

                        btnSelectExcelFile.Text= dialog.FileName;

                        ExcelFile = btnSelectExcelFile.Text;

                        break;
                    default:
                        btnSelectExcelFile.Text = string.Empty;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Check Settings

        private async void btnCheckData_Click(object sender, EventArgs e)
        {
            try
            {
                int MergeAcross = new gt_excelReader_lib.DataReader().GetFileMergeAcross(btnSelectExcelFile.Text.Trim(), txtSheet.Text.Trim());

                if (MergeAcross == 5)
                {

                }
                else if (MergeAcross == 6)
                {
                    if (chContract.Checked == true)
                    {
                       await new MergeAcrossMethods().MergeAcross6(ListCountry, ListOperator, txtCountry.Text.Trim(), txtOperator.Text.Trim(), btnSelectExcelFile.Text.Trim(), txtSheet.Text.Trim());
                    }
                    else if (chSPO.Checked == true)
                    {
                        await new MergeAcrossMethods().MergeAcross6Spo(ListCountry, ListOperator, txtCountry.Text.Trim(), txtOperator.Text.Trim(), btnSelectExcelFile.Text.Trim(), txtSheet.Text.Trim());
                    }

                    MessageBox.Show("Գործողությունը կատարվեց:\nՀարկավոր է ստուգել արդյոք բոլոր տարածաշրջանները և հյուրանոցներն են կցված փնտրման համակարգին", "Հաղորդագրություն", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (MergeAcross == 7)
                {

                }

                //else    //STOP
                //{
                //    List<gt_excelReader_lib.StopInfo> RData = await new ExcelReader().ReadExcelXMLStop(btnSelectExcelFile.Text.Trim(), txtSheet.Text.Trim());

                //    if (RData == null) return;

                //    var query_unique_regions = RData.Select(x => x.Region).Distinct().ToList();
                //    if (query_unique_regions == null || query_unique_regions.Count == 0) throw new Exception("Error with regions");
                //    List<OperatorWithRegion> RegionData = new List<OperatorWithRegion>();
                //    foreach (var item in query_unique_regions)
                //    {
                //        RegionData.Add(new OperatorWithRegion() { CountryID = query_country.ID, OperatorID = query_operator.ID, Region = item.Trim() });
                //    }
                //    await new SetData().HttpsDataDefault("Region", "CheckRegionAsync", "", RegionData);

                //    bool IsDataChanged = false;

                //    FrmStopList frm = new FrmStopList();
                //    frm.StopData = RData;
                //    frm.ShowDialog();
                //    IsDataChanged = frm.IsDataChanged;
                //    frm.Dispose();

                //    if (IsDataChanged == false) return;

                //    MessageBox.Show("Գործողությունը կատարվեց:\nՀարկավոր է ստուգել արդյոք բոլոր տարածաշրջանները և հյուրանոցներն են կցված փնտրման համակարգին", "Հաղորդագրություն", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}


                //check regions

                //check hotels

                //check rooms
                
                //
     

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void chContract_CheckedChanged(object sender, EventArgs e)
        {
            txtSheet.Text = "UnnamedPage_0";
        }

        private void chSPO_CheckedChanged(object sender, EventArgs e)
        {
            txtSheet.Text = "UnnamedPage_0";
        }

        private void ckStop_CheckedChanged(object sender, EventArgs e)
        {
            txtSheet.Text = "Page1";
        }

    }
}