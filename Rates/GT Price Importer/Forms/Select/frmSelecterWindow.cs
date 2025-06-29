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
    public partial class frmSelecterWindow : DevExpress.XtraEditors.XtraForm
    {
        public frmSelecterWindow()
        {
            InitializeComponent();

            DataID = -1;
            DataName = string.Empty;

            table = new List<DataPairs>();
        }

        internal int DataID { get; set; }
        internal string DataName { get; set; }

        internal List<DataPairs> table { get; set; }

        private void frmSelecterWindow_Shown(object sender, EventArgs e)
        {
            SetDataToGrid();
        }

        private void frmSelecterWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GridView1_DoubleClick(GridView1, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void SetDataToGrid()
        {
            try
            {
                GridControl1.BeginUpdate();
                GridControl1.DataSource = null;

                GridView1.Columns.Clear();

                GridControl1.DataSource = ListToTable.ListToDataTable(table);

                GridView1.OptionsCustomization.AllowColumnMoving = false;
                GridView1.OptionsCustomization.AllowGroup = false;
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
                GridView1.OptionsSelection.EnableAppearanceFocusedRow = false;

                GridView1.Columns["ID"].Visible = false;

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    GridView1.Columns[i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                }

                GridView1.ClearSelection();
                GridControl1.EndUpdate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (GridView1.GetFocusedDataRow() == null) return;
                if (GridView1.SelectedRowsCount == 0) return;
                if (GridView1.FocusedRowHandle < 0) return;

                DataID = Convert.ToInt32(GridView1.GetFocusedRowCellValue("ID"));
                DataName = Convert.ToString(GridView1.GetFocusedRowCellValue("Name"));

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(GridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }
    }
}