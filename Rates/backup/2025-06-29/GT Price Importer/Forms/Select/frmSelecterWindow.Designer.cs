namespace GT_Price_Importer
{
    partial class frmSelecterWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            this.GridControl1 = new DevExpress.XtraGrid.GridControl();
            this.GridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // GridControl1
            // 
            this.GridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GridControl1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.GridControl1.Location = new System.Drawing.Point(0, 0);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.Size = new System.Drawing.Size(518, 263);
            this.GridControl1.TabIndex = 13;
            this.GridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView1});
            this.GridControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridControl1_KeyDown);
            // 
            // GridView1
            // 
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.Editable = false;
            this.GridView1.OptionsBehavior.ReadOnly = true;
            this.GridView1.OptionsCustomization.AllowColumnMoving = false;
            this.GridView1.OptionsCustomization.AllowColumnResizing = false;
            this.GridView1.OptionsCustomization.AllowFilter = false;
            this.GridView1.OptionsCustomization.AllowGroup = false;
            this.GridView1.OptionsCustomization.AllowSort = false;
            this.GridView1.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.GridView1.OptionsFind.ShowClearButton = false;
            this.GridView1.OptionsFind.ShowFindButton = false;
            this.GridView1.OptionsMenu.EnableColumnMenu = false;
            this.GridView1.OptionsView.ShowAutoFilterRow = true;
            this.GridView1.OptionsView.ShowGroupPanel = false;
            this.GridView1.DoubleClick += new System.EventHandler(this.GridView1_DoubleClick);
            // 
            // frmSelecterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 263);
            this.Controls.Add(this.GridControl1);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmSelecterWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Select";
            this.Shown += new System.EventHandler(this.frmSelecterWindow_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSelecterWindow_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.FormAssistant formAssistant1;
        internal DevExpress.XtraGrid.GridControl GridControl1;
        internal DevExpress.XtraGrid.Views.Grid.GridView GridView1;
    }
}