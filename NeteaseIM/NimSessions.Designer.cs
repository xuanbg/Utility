namespace Insight.Utils.NetEaseIM
{
    partial class NimSessions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NimSessions));
            this.sceMain = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pceMain0 = new DevExpress.XtraEditors.PanelControl();
            this.pceMain1 = new DevExpress.XtraEditors.PanelControl();
            this.ppcSession = new Insight.Base.BaseForm.Controls.PageControl();
            this.sceMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceMain0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceMain1)).BeginInit();
            this.SuspendLayout();
            // 
            // sceMain
            // 
            this.sceMain.Controls.Add(this.pceMain0);
            this.sceMain.Controls.Add(this.pceMain1);
            this.sceMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceMain.Location = new System.Drawing.Point(0, 0);
            this.sceMain.Name = "sceMain";
            this.sceMain.Size = new System.Drawing.Size(320, 518);
            this.sceMain.TabIndex = 0;
            // 
            // pceMain0
            // 
            this.pceMain0.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceMain0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pceMain0.Location = new System.Drawing.Point(0, 0);
            this.pceMain0.Name = "pceMain0";
            this.pceMain0.Size = new System.Drawing.Size(320, 518);
            this.pceMain0.TabIndex = 0;
            // 
            // pceMain1
            // 
            this.pceMain1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pceMain1.Location = new System.Drawing.Point(0, 0);
            this.pceMain1.Name = "pceMain1";
            this.pceMain1.Size = new System.Drawing.Size(320, 518);
            this.pceMain1.TabIndex = 0;
            this.pceMain1.Visible = false;
            // 
            // ppcSession
            // 
            this.ppcSession.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ppcSession.Location = new System.Drawing.Point(0, 518);
            this.ppcSession.MaximumSize = new System.Drawing.Size(0, 22);
            this.ppcSession.MinimumSize = new System.Drawing.Size(300, 22);
            this.ppcSession.Name = "ppcSession";
            this.ppcSession.orderBy = Insight.Base.BaseForm.Controls.OrderBy.POSITIVE;
            this.ppcSession.pageSizeItems = ((System.Collections.ObjectModel.Collection<string>)(resources.GetObject("ppcSession.pageSizeItems")));
            this.ppcSession.Size = new System.Drawing.Size(320, 22);
            this.ppcSession.TabIndex = 0;
            // 
            // NimSessions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sceMain);
            this.Controls.Add(this.ppcSession);
            this.MinimumSize = new System.Drawing.Size(320, 540);
            this.Name = "NimSessions";
            this.Size = new System.Drawing.Size(320, 540);
            this.sceMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pceMain0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceMain1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl sceMain;
        private DevExpress.XtraEditors.PanelControl pceMain0;
        private DevExpress.XtraEditors.PanelControl pceMain1;
        public Base.BaseForm.Controls.PageControl ppcSession;
    }
}
