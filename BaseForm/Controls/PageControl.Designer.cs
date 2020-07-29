namespace Insight.Base.BaseForm.Controls
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
            this.pceTop = new DevExpress.XtraEditors.PanelControl();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.txtPage = new DevExpress.XtraEditors.TextEdit();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnJump = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.labRows = new DevExpress.XtraEditors.LabelControl();
            this.cbeRows = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pceTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeRows.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pceTop
            // 
            this.pceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pceTop.Location = new System.Drawing.Point(0, 0);
            this.pceTop.Name = "pceTop";
            this.pceTop.Size = new System.Drawing.Size(300, 2);
            this.pceTop.TabIndex = 2;
            // 
            // btnFirst
            // 
            this.btnFirst.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnFirst.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFirst.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.ImageOptions.Image")));
            this.btnFirst.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnFirst.Location = new System.Drawing.Point(194, 2);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(22, 22);
            this.btnFirst.TabIndex = 10;
            this.btnFirst.ToolTip = "首页";
            // 
            // txtPage
            // 
            this.txtPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPage.Location = new System.Drawing.Point(216, 4);
            this.txtPage.Name = "txtPage";
            this.txtPage.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.txtPage.Properties.Mask.EditMask = "d";
            this.txtPage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPage.Size = new System.Drawing.Size(62, 20);
            this.txtPage.TabIndex = 17;
            this.txtPage.Visible = false;
            // 
            // btnPrev
            // 
            this.btnPrev.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnPrev.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrev.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.ImageOptions.Image")));
            this.btnPrev.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPrev.Location = new System.Drawing.Point(216, 2);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(22, 22);
            this.btnPrev.TabIndex = 11;
            this.btnPrev.ToolTipTitle = "上一页";
            // 
            // btnJump
            // 
            this.btnJump.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnJump.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnJump.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnJump.Location = new System.Drawing.Point(238, 2);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(18, 22);
            this.btnJump.TabIndex = 12;
            this.btnJump.ToolTipTitle = "指定页";
            // 
            // btnNext
            // 
            this.btnNext.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.ImageOptions.Image")));
            this.btnNext.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNext.Location = new System.Drawing.Point(256, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(22, 22);
            this.btnNext.TabIndex = 13;
            this.btnNext.ToolTipTitle = "下一页";
            // 
            // btnLast
            // 
            this.btnLast.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnLast.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLast.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.ImageOptions.Image")));
            this.btnLast.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLast.Location = new System.Drawing.Point(278, 2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(22, 22);
            this.btnLast.TabIndex = 14;
            this.btnLast.ToolTipTitle = "末页";
            // 
            // labRows
            // 
            this.labRows.Location = new System.Drawing.Point(45, 4);
            this.labRows.Name = "labRows";
            this.labRows.Size = new System.Drawing.Size(133, 14);
            this.labRows.TabIndex = 15;
            this.labRows.Text = "行/页 | 共 0 行 | 分 1 页";
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
            this.cbeRows.TabIndex = 16;
            // 
            // PageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.labRows);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.txtPage);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnJump);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.cbeRows);
            this.Controls.Add(this.pceTop);
            this.MaximumSize = new System.Drawing.Size(0, 24);
            this.MinimumSize = new System.Drawing.Size(300, 24);
            this.Name = "PageControl";
            this.Size = new System.Drawing.Size(300, 24);
            ((System.ComponentModel.ISupportInitialize)(this.pceTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeRows.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pceTop;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.TextEdit txtPage;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnJump;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private DevExpress.XtraEditors.LabelControl labRows;
        private DevExpress.XtraEditors.ComboBoxEdit cbeRows;
    }
}
