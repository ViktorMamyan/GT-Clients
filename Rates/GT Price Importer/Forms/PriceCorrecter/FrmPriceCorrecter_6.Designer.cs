namespace GT_Price_Importer
{
    partial class FrmPriceCorrecter_6
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtB2CPriceValue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnAddToDB = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetPrice = new DevExpress.XtraEditors.SimpleButton();
            this.txtB2BPriceValue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chPercent = new DevExpress.XtraEditors.CheckEdit();
            this.GridControl1 = new DevExpress.XtraGrid.GridControl();
            this.GridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtB2CPriceValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtB2BPriceValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chPercent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtB2CPriceValue);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.btnAddToDB);
            this.groupControl1.Controls.Add(this.btnSetPrice);
            this.groupControl1.Controls.Add(this.txtB2BPriceValue);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.chPercent);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(662, 117);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Պարամետրեր";
            // 
            // txtB2CPriceValue
            // 
            this.txtB2CPriceValue.EditValue = "0";
            this.txtB2CPriceValue.Location = new System.Drawing.Point(92, 80);
            this.txtB2CPriceValue.Name = "txtB2CPriceValue";
            this.txtB2CPriceValue.Properties.Appearance.Options.UseTextOptions = true;
            this.txtB2CPriceValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtB2CPriceValue.Properties.Mask.EditMask = "n2";
            this.txtB2CPriceValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtB2CPriceValue.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtB2CPriceValue.Size = new System.Drawing.Size(111, 20);
            this.txtB2CPriceValue.TabIndex = 2;
            this.txtB2CPriceValue.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtB2CPriceValue_EditValueChanging);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 83);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "B2C Միավոր";
            // 
            // btnAddToDB
            // 
            this.btnAddToDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToDB.Location = new System.Drawing.Point(511, 54);
            this.btnAddToDB.Name = "btnAddToDB";
            this.btnAddToDB.Size = new System.Drawing.Size(139, 23);
            this.btnAddToDB.TabIndex = 4;
            this.btnAddToDB.Text = "Ավելացնել Բազա";
            this.btnAddToDB.Click += new System.EventHandler(this.btnAddToDB_Click);
            // 
            // btnSetPrice
            // 
            this.btnSetPrice.Location = new System.Drawing.Point(235, 55);
            this.btnSetPrice.Name = "btnSetPrice";
            this.btnSetPrice.Size = new System.Drawing.Size(139, 23);
            this.btnSetPrice.TabIndex = 3;
            this.btnSetPrice.Text = "Կիրառել Հավելավճար";
            this.btnSetPrice.Click += new System.EventHandler(this.btnSetPrice_Click);
            // 
            // txtB2BPriceValue
            // 
            this.txtB2BPriceValue.EditValue = "0";
            this.txtB2BPriceValue.Location = new System.Drawing.Point(92, 57);
            this.txtB2BPriceValue.Name = "txtB2BPriceValue";
            this.txtB2BPriceValue.Properties.Appearance.Options.UseTextOptions = true;
            this.txtB2BPriceValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtB2BPriceValue.Properties.Mask.EditMask = "n2";
            this.txtB2BPriceValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtB2BPriceValue.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtB2BPriceValue.Size = new System.Drawing.Size(111, 20);
            this.txtB2BPriceValue.TabIndex = 1;
            this.txtB2BPriceValue.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtPriceValue_EditValueChanging);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 60);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "B2B Միավոր";
            // 
            // chPercent
            // 
            this.chPercent.EditValue = true;
            this.chPercent.Location = new System.Drawing.Point(15, 25);
            this.chPercent.Name = "chPercent";
            this.chPercent.Properties.Caption = "Տոկոսային Հավելավճար";
            this.chPercent.Size = new System.Drawing.Size(157, 19);
            this.chPercent.TabIndex = 0;
            // 
            // GridControl1
            // 
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.Location = new System.Drawing.Point(0, 117);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.Size = new System.Drawing.Size(662, 306);
            this.GridControl1.TabIndex = 1;
            this.GridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView1});
            // 
            // GridView1
            // 
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsBehavior.Editable = false;
            this.GridView1.OptionsBehavior.ReadOnly = true;
            this.GridView1.OptionsCustomization.AllowColumnMoving = false;
            this.GridView1.OptionsCustomization.AllowGroup = false;
            this.GridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.GridView1.OptionsView.ShowAutoFilterRow = true;
            this.GridView1.OptionsView.ShowFooter = true;
            this.GridView1.OptionsView.ShowGroupPanel = false;
            this.GridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.GridView1_CustomDrawCell);
            // 
            // FrmPriceCorrecter_6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 423);
            this.Controls.Add(this.GridControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmPriceCorrecter_6";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price Creater";
            this.Shown += new System.EventHandler(this.FrmPriceCorrecter_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtB2CPriceValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtB2BPriceValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chPercent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        internal DevExpress.XtraGrid.GridControl GridControl1;
        internal DevExpress.XtraGrid.Views.Grid.GridView GridView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chPercent;
        internal DevExpress.XtraEditors.TextEdit txtB2BPriceValue;
        private DevExpress.XtraEditors.SimpleButton btnAddToDB;
        private DevExpress.XtraEditors.SimpleButton btnSetPrice;
        internal DevExpress.XtraEditors.TextEdit txtB2CPriceValue;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}