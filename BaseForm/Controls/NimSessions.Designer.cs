namespace Insight.Utils.Controls
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
            this.sceMain = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pceMain = new DevExpress.XtraEditors.PanelControl();
            this.sceMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceMain)).BeginInit();
            this.SuspendLayout();
            // 
            // sceMain
            // 
            this.sceMain.Controls.Add(this.pceMain);
            this.sceMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceMain.Location = new System.Drawing.Point(0, 0);
            this.sceMain.Name = "sceMain";
            this.sceMain.Size = new System.Drawing.Size(320, 540);
            this.sceMain.TabIndex = 0;
            // 
            // pceMain
            // 
            this.pceMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pceMain.Location = new System.Drawing.Point(0, 0);
            this.pceMain.Name = "pceMain";
            this.pceMain.Size = new System.Drawing.Size(320, 70);
            this.pceMain.TabIndex = 0;
            // 
            // NimSessions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sceMain);
            this.MinimumSize = new System.Drawing.Size(320, 540);
            this.Name = "NimSessions";
            this.Size = new System.Drawing.Size(320, 540);
            this.sceMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pceMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl sceMain;
        private DevExpress.XtraEditors.PanelControl pceMain;
    }
}
