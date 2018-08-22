using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT_Clients.Forms
{
    public partial class ClientAdd : DevExpress.XtraEditors.XtraForm
    {
        public ClientAdd()
        {
            InitializeComponent();
        }

        internal MainForm RefForm = new MainForm();

        private List<Clients> CL = new List<Clients>();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtName.Text)) throw new Exception("Անունը գրված չէ");
                if (String.IsNullOrEmpty(txtLastName.Text)) throw new Exception("Ազգանունը գրված չէ");
                if (String.IsNullOrEmpty(txtContact.Text)) throw new Exception("Կոնտակտային տվյալը գրված չէ");

                if (System.IO.File.Exists("Clients.db"))
                {
                    string Content = System.IO.File.ReadAllText("Clients.db", Encoding.UTF8);
                    if (!String.IsNullOrEmpty(Content))
                    {
                        CL = JsonConvert.DeserializeObject<List<Clients>>(Content);
                    }                    
                }

                int MaxID = 1;

                if (CL.Count > 0)
                {
                    MaxID = CL.Max(t => t.ID);
                    MaxID++;
                }

                Clients C = new Clients() { ID = MaxID,
                                            Name = txtName.Text.Trim(),
                                            LastName=txtLastName.Text.Trim(),
                                            BirdDate = ((bDate.Text == String.Empty) ? (DateTime?)null : bDate.DateTime),
                                            City=txtCity.Text.Trim(),
                                            Contacts = txtContact.Text.Trim(),
                                            Destination=txtDestination.Text.Trim(),
                                            TravelDate = ((TravelDate.Text == String.Empty) ? (DateTime?)null : TravelDate.DateTime),
                                            PersonsNumber=(String.IsNullOrEmpty(txtNumber.Text) ? 0 : Convert.ToInt32(txtNumber.Text)),
                                            Childs = txtChild.Text.Trim(),
                                            Comments=txtComment.Text.Trim()
                                        };

                CL.Add(C);

                string json = JsonConvert.SerializeObject(CL, Formatting.Indented);

                System.IO.File.WriteAllText(@"Clients.db", json, Encoding.UTF8);

                Cleanup();

                RefForm.RefreshGrid();

                //int rowHandle = RefForm.GridView1.LocateByValue("ID", MaxID);
                //if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                //{
                //    RefForm.GridView1.FocusedRowHandle = rowHandle;
                //}

                txtName.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void Cleanup()
        {
            //cleanup
            txtName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            bDate.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtDestination.Text = string.Empty;
            TravelDate.Text = string.Empty;
            txtNumber.Text = "2";
            txtChild.Text = string.Empty;
            txtComment.Text = string.Empty;
        }

        private void txtNumber_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (Convert.ToDecimal(e.NewValue) < 1)
	            {
		            e.Cancel=true;
	            }
        }
        private void txtName_Enter(object sender, EventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            BeginInvoke(new Action(() => edit.SelectAll()));
        }
        private void txtLastName_Enter(object sender, EventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            BeginInvoke(new Action(() => edit.SelectAll()));
        }
        private void txtCity_Enter(object sender, EventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            BeginInvoke(new Action(() => edit.SelectAll()));
        }
        private void txtContact_Enter(object sender, EventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            BeginInvoke(new Action(() => edit.SelectAll()));
        }
        private void txtDestination_Enter(object sender, EventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            BeginInvoke(new Action(() => edit.SelectAll()));
        }
        private void txtNumber_Enter(object sender, EventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            BeginInvoke(new Action(() => edit.SelectAll()));
        }
        private void txtChild_Enter(object sender, EventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            BeginInvoke(new Action(() => edit.SelectAll()));
        }
        private void txtComment_Enter(object sender, EventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            BeginInvoke(new Action(() => edit.SelectAll()));
        }

    }
}