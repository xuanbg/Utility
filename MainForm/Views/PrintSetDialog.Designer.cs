namespace Insight.Utils.MainForm.Views
{
    public partial class PrintSetDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintSetDialog));
            this.TagPrint = new DevExpress.XtraEditors.ComboBoxEdit();
            this.BilPrint = new DevExpress.XtraEditors.ComboBoxEdit();
            this.DocPrint = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labDocPrint = new DevExpress.XtraEditors.LabelControl();
            this.labelTagPrint = new DevExpress.XtraEditors.LabelControl();
            this.labBillPrint = new DevExpress.XtraEditors.LabelControl();
            this.MergerPrint = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TagPrint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BilPrint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocPrint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MergerPrint.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.MergerPrint);
            this.panel.Controls.Add(this.TagPrint);
            this.panel.Controls.Add(this.BilPrint);
            this.panel.Controls.Add(this.DocPrint);
            this.panel.Controls.Add(this.labDocPrint);
            this.panel.Controls.Add(this.labelTagPrint);
            this.panel.Controls.Add(this.labBillPrint);
            // 
            // Cancel
            // 
            this.cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cancel.Appearance.Options.UseFont = true;
            // 
            // Confirm
            // 
            this.confirm.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.Appearance.Options.UseFont = true;
            // 
            // TagPrint
            // 
            this.TagPrint.Location = new System.Drawing.Point(105, 55);
            this.TagPrint.Name = "TagPrint";
            this.TagPrint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TagPrint.Properties.NullText = "请选择默认标签打印机";
            this.TagPrint.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.TagPrint.Size = new System.Drawing.Size(240, 20);
            this.TagPrint.TabIndex = 2;
            this.TagPrint.Tag = "";
            // 
            // BilPrint
            // 
            this.BilPrint.Location = new System.Drawing.Point(105, 90);
            this.BilPrint.Name = "BilPrint";
            this.BilPrint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.BilPrint.Properties.NullText = "请选择默认票据打印机";
            this.BilPrint.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.BilPrint.Size = new System.Drawing.Size(240, 20);
            this.BilPrint.TabIndex = 3;
            this.BilPrint.Tag = "";
            // 
            // DocPrint
            // 
            this.DocPrint.Location = new System.Drawing.Point(105, 20);
            this.DocPrint.Name = "DocPrint";
            this.DocPrint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DocPrint.Properties.NullText = "请选择默认文档打印机";
            this.DocPrint.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.DocPrint.Size = new System.Drawing.Size(240, 20);
            this.DocPrint.TabIndex = 1;
            this.DocPrint.Tag = "";
            // 
            // labDocPrint
            // 
            this.labDocPrint.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labDocPrint.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labDocPrint.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labDocPrint.Location = new System.Drawing.Point(25, 20);
            this.labDocPrint.Name = "labDocPrint";
            this.labDocPrint.Size = new System.Drawing.Size(80, 21);
            this.labDocPrint.TabIndex = 5;
            this.labDocPrint.Text = "文档打印机：";
            // 
            // labelTagPrint
            // 
            this.labelTagPrint.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelTagPrint.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelTagPrint.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelTagPrint.Location = new System.Drawing.Point(25, 55);
            this.labelTagPrint.Name = "labelTagPrint";
            this.labelTagPrint.Size = new System.Drawing.Size(80, 21);
            this.labelTagPrint.TabIndex = 0;
            this.labelTagPrint.Text = "标签打印机：";
            // 
            // labBillPrint
            // 
            this.labBillPrint.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labBillPrint.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labBillPrint.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labBillPrint.Location = new System.Drawing.Point(25, 90);
            this.labBillPrint.Name = "labBillPrint";
            this.labBillPrint.Size = new System.Drawing.Size(80, 21);
            this.labBillPrint.TabIndex = 0;
            this.labBillPrint.Text = "票据打印机：";
            // 
            // MergerPrint
            // 
            this.MergerPrint.Location = new System.Drawing.Point(105, 115);
            this.MergerPrint.Name = "MergerPrint";
            this.MergerPrint.Properties.AutoHeight = false;
            this.MergerPrint.Properties.Caption = "合并打印三联票据";
            this.MergerPrint.Size = new System.Drawing.Size(240, 20);
            this.MergerPrint.TabIndex = 4;
            // 
            // PrintSet
            // 
            this.ClientSize = new System.Drawing.Size(384, 212);
            this.Name = "PrintSet";
            this.Text = "打印机设置";
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TagPrint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BilPrint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocPrint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MergerPrint.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.ComboBoxEdit TagPrint;
        internal DevExpress.XtraEditors.ComboBoxEdit BilPrint;
        internal DevExpress.XtraEditors.ComboBoxEdit DocPrint;
        internal DevExpress.XtraEditors.CheckEdit MergerPrint;
        private DevExpress.XtraEditors.LabelControl labDocPrint;
        private DevExpress.XtraEditors.LabelControl labelTagPrint;
        private DevExpress.XtraEditors.LabelControl labBillPrint;

    }
}