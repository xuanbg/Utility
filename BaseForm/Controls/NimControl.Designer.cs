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
            this.pceList = new DevExpress.XtraEditors.PanelControl();
            this.sceMain = new DevExpress.XtraEditors.SplitterControl();
            this.pceChat = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceChat)).BeginInit();
            this.SuspendLayout();
            // 
            // pceList
            // 
            this.pceList.Dock = System.Windows.Forms.DockStyle.Left;
            this.pceList.Location = new System.Drawing.Point(0, 0);
            this.pceList.Name = "pceList";
            this.pceList.Size = new System.Drawing.Size(300, 640);
            this.pceList.TabIndex = 0;
            // 
            // sceMain
            // 
            this.sceMain.Location = new System.Drawing.Point(300, 0);
            this.sceMain.Name = "sceMain";
            this.sceMain.Size = new System.Drawing.Size(5, 640);
            this.sceMain.TabIndex = 0;
            this.sceMain.TabStop = false;
            // 
            // pceChat
            // 
            this.pceChat.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pceChat.Location = new System.Drawing.Point(305, 0);
            this.pceChat.Name = "pceChat";
            this.pceChat.Size = new System.Drawing.Size(755, 640);
            this.pceChat.TabIndex = 0;
            // 
            // NimControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pceChat);
            this.Controls.Add(this.sceMain);
            this.Controls.Add(this.pceList);
            this.Name = "NimControl";
            this.Size = new System.Drawing.Size(1060, 640);
            ((System.ComponentModel.ISupportInitialize)(this.pceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceChat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pceList;
        private DevExpress.XtraEditors.SplitterControl sceMain;
        private DevExpress.XtraEditors.PanelControl pceChat;
    }
}
