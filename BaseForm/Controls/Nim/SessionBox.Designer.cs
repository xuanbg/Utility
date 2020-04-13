namespace Insight.Utils.Controls.Nim
{
    partial class SessionBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionBox));
            this.pceSession = new DevExpress.XtraEditors.PanelControl();
            this.peeUnread = new DevExpress.XtraEditors.PictureEdit();
            this.labTime = new DevExpress.XtraEditors.LabelControl();
            this.labMessage = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.picTarget = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pceSession)).BeginInit();
            this.pceSession.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peeUnread.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTarget.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pceSession
            // 
            this.pceSession.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceSession.Controls.Add(this.peeUnread);
            this.pceSession.Controls.Add(this.labTime);
            this.pceSession.Controls.Add(this.labMessage);
            this.pceSession.Controls.Add(this.labName);
            this.pceSession.Controls.Add(this.picTarget);
            this.pceSession.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pceSession.Location = new System.Drawing.Point(0, 0);
            this.pceSession.Name = "pceSession";
            this.pceSession.Size = new System.Drawing.Size(300, 70);
            this.pceSession.TabIndex = 0;
            // 
            // peeUnread
            // 
            this.peeUnread.EditValue = ((object)(resources.GetObject("peeUnread.EditValue")));
            this.peeUnread.Location = new System.Drawing.Point(275, 39);
            this.peeUnread.Name = "peeUnread";
            this.peeUnread.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peeUnread.Properties.Appearance.Options.UseBackColor = true;
            this.peeUnread.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peeUnread.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peeUnread.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peeUnread.Size = new System.Drawing.Size(16, 16);
            this.peeUnread.TabIndex = 0;
            this.peeUnread.Visible = false;
            // 
            // labTime
            // 
            this.labTime.Location = new System.Drawing.Point(240, 15);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(0, 14);
            this.labTime.TabIndex = 0;
            // 
            // labMessage
            // 
            this.labMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labMessage.Location = new System.Drawing.Point(70, 40);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(200, 14);
            this.labMessage.TabIndex = 0;
            // 
            // labName
            // 
            this.labName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labName.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.labName.Location = new System.Drawing.Point(70, 15);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(160, 14);
            this.labName.TabIndex = 0;
            // 
            // picTarget
            // 
            this.picTarget.EditValue = ((object)(resources.GetObject("picTarget.EditValue")));
            this.picTarget.Location = new System.Drawing.Point(10, 10);
            this.picTarget.Name = "picTarget";
            this.picTarget.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTarget.Properties.Appearance.Options.UseBackColor = true;
            this.picTarget.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTarget.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTarget.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picTarget.Size = new System.Drawing.Size(50, 50);
            this.picTarget.TabIndex = 0;
            // 
            // SessionBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pceSession);
            this.MinimumSize = new System.Drawing.Size(300, 70);
            this.Name = "SessionBox";
            this.Size = new System.Drawing.Size(300, 70);
            ((System.ComponentModel.ISupportInitialize)(this.pceSession)).EndInit();
            this.pceSession.ResumeLayout(false);
            this.pceSession.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peeUnread.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTarget.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pceSession;
        private DevExpress.XtraEditors.PictureEdit picTarget;
        private DevExpress.XtraEditors.PictureEdit peeUnread;
        private DevExpress.XtraEditors.LabelControl labTime;
        private DevExpress.XtraEditors.LabelControl labMessage;
        private DevExpress.XtraEditors.LabelControl labName;
    }
}
