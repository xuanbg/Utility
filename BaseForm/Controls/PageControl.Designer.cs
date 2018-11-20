namespace Insight.Utils.Controls
{
    partial class PageControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageControl));
            this.labRows = new DevExpress.XtraEditors.LabelControl();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.cbeRows = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnJump = new DevExpress.XtraEditors.SimpleButton();
            this.txtPage = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeRows.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labRows
            // 
            this.labRows.Location = new System.Drawing.Point(45, 4);
            this.labRows.Name = "labRows";
            this.labRows.Size = new System.Drawing.Size(133, 14);
            this.labRows.TabIndex = 0;
            this.labRows.Text = "行/页 | 共 0 行 | 分 1 页";
            // 
            // btnFirst
            // 
            this.btnFirst.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnFirst.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFirst.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.ImageOptions.Image")));
            this.btnFirst.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnFirst.Location = new System.Drawing.Point(214, 0);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(22, 22);
            this.btnFirst.TabIndex = 0;
            this.btnFirst.ToolTip = "首页";
            // 
            // btnPrev
            // 
            this.btnPrev.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnPrev.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrev.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.ImageOptions.Image")));
            this.btnPrev.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPrev.Location = new System.Drawing.Point(236, 0);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(22, 22);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.ToolTipTitle = "上一页";
            // 
            // btnNext
            // 
            this.btnNext.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.ImageOptions.Image")));
            this.btnNext.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNext.Location = new System.Drawing.Point(276, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(22, 22);
            this.btnNext.TabIndex = 0;
            this.btnNext.ToolTipTitle = "下一页";
            // 
            // btnLast
            // 
            this.btnLast.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnLast.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLast.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.ImageOptions.Image")));
            this.btnLast.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLast.Location = new System.Drawing.Point(298, 0);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(22, 22);
            this.btnLast.TabIndex = 0;
            this.btnLast.ToolTipTitle = "末页";
            // 
            // cbeRows
            // 
            this.cbeRows.EditValue = "";
            this.cbeRows.Location = new System.Drawing.Point(0, 2);
            this.cbeRows.Name = "cbeRows";
            this.cbeRows.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.cbeRows.Properties.Appearance.Options.UseBackColor = true;
            this.cbeRows.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.cbeRows.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeRows.Properties.DropDownRows = 5;
            this.cbeRows.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbeRows.Size = new System.Drawing.Size(45, 20);
            this.cbeRows.TabIndex = 0;
            // 
            // btnJump
            // 
            this.btnJump.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnJump.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnJump.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnJump.Location = new System.Drawing.Point(258, 0);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(18, 22);
            this.btnJump.TabIndex = 0;
            this.btnJump.ToolTipTitle = "指定页";
            // 
            // txtPage
            // 
            this.txtPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPage.Location = new System.Drawing.Point(236, 2);
            this.txtPage.Name = "txtPage";
            this.txtPage.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.txtPage.Properties.Mask.EditMask = "d";
            this.txtPage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPage.Size = new System.Drawing.Size(62, 20);
            this.txtPage.TabIndex = 1;
            this.txtPage.Visible = false;
            // 
            // PageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.txtPage);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnJump);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.labRows);
            this.Controls.Add(this.cbeRows);
            this.MaximumSize = new System.Drawing.Size(0, 22);
            this.MinimumSize = new System.Drawing.Size(320, 22);
            this.Name = "PageControl";
            this.Size = new System.Drawing.Size(320, 22);
            ((System.ComponentModel.ISupportInitialize)(this.cbeRows.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPage.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cbeRows;
        private DevExpress.XtraEditors.LabelControl labRows;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private DevExpress.XtraEditors.SimpleButton btnJump;
        private DevExpress.XtraEditors.TextEdit txtPage;
    }
}
