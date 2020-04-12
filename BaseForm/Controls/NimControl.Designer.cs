namespace Insight.Utils.Controls
{
    partial class NimControl
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
            this.sccMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.nccChat = new Insight.Utils.Controls.NimChatControl();
            ((System.ComponentModel.ISupportInitialize)(this.sccMain)).BeginInit();
            this.sccMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // sccMain
            // 
            this.sccMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sccMain.Location = new System.Drawing.Point(0, 0);
            this.sccMain.Name = "sccMain";
            this.sccMain.Panel1.MinSize = 300;
            this.sccMain.Panel2.Controls.Add(this.nccChat);
            this.sccMain.Size = new System.Drawing.Size(1060, 640);
            this.sccMain.SplitterPosition = 300;
            this.sccMain.TabIndex = 0;
            this.sccMain.Text = "splitContainerControl1";
            // 
            // nccChat
            // 
            this.nccChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nccChat.Location = new System.Drawing.Point(0, 0);
            this.nccChat.Name = "nccChat";
            this.nccChat.Padding = new System.Windows.Forms.Padding(5);
            this.nccChat.Size = new System.Drawing.Size(755, 640);
            this.nccChat.TabIndex = 0;
            // 
            // NimControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sccMain);
            this.Name = "NimControl";
            this.Size = new System.Drawing.Size(1060, 640);
            ((System.ComponentModel.ISupportInitialize)(this.sccMain)).EndInit();
            this.sccMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl sccMain;
        private NimChatControl nccChat;
    }
}
