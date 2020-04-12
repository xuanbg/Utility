namespace Insight.Utils.Controls.Nim
{
    partial class NimList
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
            this.sceMessage = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pceList = new DevExpress.XtraEditors.PanelControl();
            this.sceMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceList)).BeginInit();
            this.SuspendLayout();
            // 
            // sceMessage
            // 
            this.sceMessage.Controls.Add(this.pceList);
            this.sceMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceMessage.Location = new System.Drawing.Point(0, 0);
            this.sceMessage.Name = "sceMessage";
            this.sceMessage.Size = new System.Drawing.Size(400, 400);
            this.sceMessage.TabIndex = 0;
            // 
            // pceList
            // 
            this.pceList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pceList.Location = new System.Drawing.Point(0, 0);
            this.pceList.Name = "pceList";
            this.pceList.Size = new System.Drawing.Size(400, 100);
            this.pceList.TabIndex = 0;
            // 
            // MessageList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sceMessage);
            this.Name = "MessageList";
            this.Size = new System.Drawing.Size(400, 400);
            this.sceMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pceList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl sceMessage;
        private DevExpress.XtraEditors.PanelControl pceList;
    }
}
