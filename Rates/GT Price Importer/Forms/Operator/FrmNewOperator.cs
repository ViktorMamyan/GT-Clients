using GT_Price_Importer.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT_Price_Importer
{
    public partial class FrmNewOperator : DevExpress.XtraEditors.XtraForm
    {
        public FrmNewOperator()
        {
            InitializeComponent();
        }

        internal string DataName = string.Empty;
        internal bool IsDataChanged = false;

        private async void btnDO_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Trim() == string.Empty) throw new Exception("Անվանումը բացակայում է");

                OperatorItem operators = new OperatorItem() { Operator = txtName.Text.Trim(), ID = 0, IsActive = true };

                bool IsOK = await new SetData().HttpsDataDefault("Operator", "NewOperatorAsync", "", operators);

                if (IsOK == true)
                {
                    DataName = txtName.Text.Trim();
                    IsDataChanged = true;

                    this.Close();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}