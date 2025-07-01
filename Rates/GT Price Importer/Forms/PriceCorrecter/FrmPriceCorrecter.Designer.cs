namespace GT_Price_Importer
{
    partial class FrmPriceCorrecter
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
            this.GridControl1 = new DevExpress.XtraGrid.GridControl();
            this.GridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chPercent = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPriceValue = new DevExpress.XtraEditors.TextEdit();
            this.btnSetPrice = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddToDB = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chPercent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPriceValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnAddToDB);
            this.groupControl1.Controls.Add(this.btnSetPrice);
            this.groupControl1.Controls.Add(this.txtPriceValue);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.chPercent);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(583, 117);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Պարամետրեր";
            // 
            // GridControl1
            // 
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.Location = new System.Drawing.Point(0, 117);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.Size = new System.Drawing.Size(583, 223);
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
            // 
            // chPercent
            // 
            this.chPercent.EditValue = true;
            this.chPercent.Location = new System.Drawing.Point(22, 36);
            this.chPercent.Name = "chPercent";
            this.chPercent.Properties.Caption = "Տոկոսային Հավելավճար";
            this.chPercent.Size = new System.Drawing.Size(157, 19);
            this.chPercent.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 71);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Միավոր";
            // 
            // txtPriceValue
            // 
            this.txtPriceValue.EditValue = "0";
            this.txtPriceValue.Location = new System.Drawing.Point(68, 68);
            this.txtPriceValue.Name = "txtPriceValue";
            this.txtPriceValue.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPriceValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPriceValue.Properties.Mask.EditMask = "n2";
            this.txtPriceValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPriceValue.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPriceValue.Size = new System.Drawing.Size(111, 20);
            this.txtPriceValue.TabIndex = 1;
            this.txtPriceValue.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtPriceValue_EditValueChanging);
            // 
            // btnSetPrice
            // 
            this.btnSetPrice.Location = new System.Drawing.Point(211, 66);
            this.btnSetPrice.Name = "btnSetPrice";
            this.btnSetPrice.Size = new System.Drawing.Size(139, 23);
            this.btnSetPrice.TabIndex = 2;
            this.btnSetPrice.Text = "Կիրառել Հավելավճար";
            this.btnSetPrice.Click += new System.EventHandler(this.btnSetPrice_Click);
            // 
            // btnAddToDB
            // 
            this.btnAddToDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToDB.Location = new System.Drawing.Point(432, 66);
            this.btnAddToDB.Name = "btnAddToDB";
            this.btnAddToDB.Size = new System.Drawing.Size(139, 23);
            this.btnAddToDB.TabIndex = 3;
            this.btnAddToDB.Text = "Ավելացնել Բազա";
            this.btnAddToDB.Click += new System.EventHandler(this.btnAddToDB_Click);
            // 
            // FrmPriceCorrecter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 340);
            this.Controls.Add(this.GridControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmPriceCorrecter";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price Creater";
            this.Shown += new System.EventHandler(this.FrmPriceCorrecter_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chPercent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPriceValue.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        internal DevExpress.XtraGrid.GridControl GridControl1;
        internal DevExpress.XtraGrid.Views.Grid.GridView GridView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chPercent;
        internal DevExpress.XtraEditors.TextEdit txtPriceValue;
        private DevExpress.XtraEditors.SimpleButton btnAddToDB;
        private DevExpress.XtraEditors.SimpleButton btnSetPrice;
    }
}