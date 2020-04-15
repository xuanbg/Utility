namespace Insight.Utils.Controls.Nim
{
    partial class MessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBox));
            this.pceMessage = new DevExpress.XtraEditors.PanelControl();
            this.pbcSend = new DevExpress.XtraEditors.ProgressBarControl();
            this.picTarget = new DevExpress.XtraEditors.PictureEdit();
            this.pceText = new DevExpress.XtraEditors.PanelControl();
            this.labMessage = new DevExpress.XtraEditors.LabelControl();
            this.picImage = new DevExpress.XtraEditors.PictureEdit();
            this.picMe = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pceMessage)).BeginInit();
            this.pceMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbcSend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTarget.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceText)).BeginInit();
            this.pceText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMe.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pceMessage
            // 
            this.pceMessage.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceMessage.Controls.Add(this.pbcSend);
            this.pceMessage.Controls.Add(this.picTarget);
            this.pceMessage.Controls.Add(this.pceText);
            this.pceMessage.Controls.Add(this.picImage);
            this.pceMessage.Controls.Add(this.picMe);
            this.pceMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pceMessage.Location = new System.Drawing.Point(0, 0);
            this.pceMessage.Name = "pceMessage";
            this.pceMessage.Size = new System.Drawing.Size(300, 70);
            this.pceMessage.TabIndex = 0;
            // 
            // pbcSend
            // 
            this.pbcSend.EditValue = 100;
            this.pbcSend.Location = new System.Drawing.Point(70, 32);
            this.pbcSend.Name = "pbcSend";
            this.pbcSend.Properties.Step = 1;
            this.pbcSend.Size = new System.Drawing.Size(100, 5);
            this.pbcSend.TabIndex = 0;
            this.pbcSend.Visible = false;
            // 
            // picTarget
            // 
            this.picTarget.EditValue = ((object)(resources.GetObject("picTarget.EditValue")));
            this.picTarget.Location = new System.Drawing.Point(5, 5);
            this.picTarget.Name = "picTarget";
            this.picTarget.Properties.AllowFocused = false;
            this.picTarget.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTarget.Properties.Appearance.Options.UseBackColor = true;
            this.picTarget.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTarget.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTarget.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picTarget.Size = new System.Drawing.Size(60, 60);
            this.picTarget.TabIndex = 0;
            this.picTarget.Visible = false;
            // 
            // pceText
            // 
            this.pceText.Controls.Add(this.labMessage);
            this.pceText.Location = new System.Drawing.Point(70, 5);
            this.pceText.Name = "pceText";
            this.pceText.Size = new System.Drawing.Size(100, 24);
            this.pceText.TabIndex = 0;
            this.pceText.Visible = false;
            // 
            // labMessage
            // 
            this.labMessage.AllowHtmlString = true;
            this.labMessage.Appearance.Options.UseFont = true;
            this.labMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labMessage.Location = new System.Drawing.Point(5, 5);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(80, 0);
            this.labMessage.TabIndex = 0;
            // 
            // picImage
            // 
            this.picImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picImage.Location = new System.Drawing.Point(70, 5);
            this.picImage.Name = "picImage";
            this.picImage.Properties.AllowFocused = false;
            this.picImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picImage.Properties.Appearance.Options.UseBackColor = true;
            this.picImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picImage.Size = new System.Drawing.Size(60, 60);
            this.picImage.TabIndex = 0;
            this.picImage.Visible = false;
            // 
            // picMe
            // 
            this.picMe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picMe.EditValue = ((object)(resources.GetObject("picMe.EditValue")));
            this.picMe.Location = new System.Drawing.Point(235, 5);
            this.picMe.Name = "picMe";
            this.picMe.Properties.AllowFocused = false;
            this.picMe.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picMe.Properties.Appearance.Options.UseBackColor = true;
            this.picMe.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picMe.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picMe.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picMe.Size = new System.Drawing.Size(60, 60);
            this.picMe.TabIndex = 0;
            this.picMe.Visible = false;
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pceMessage);
            this.Name = "MessageBox";
            this.Size = new System.Drawing.Size(300, 70);
            ((System.ComponentModel.ISupportInitialize)(this.pceMessage)).EndInit();
            this.pceMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbcSend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTarget.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceText)).EndInit();
            this.pceText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMe.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pceMessage;
        public DevExpress.XtraEditors.PictureEdit picImage;
        private DevExpress.XtraEditors.PictureEdit picMe;
        private DevExpress.XtraEditors.PictureEdit picTarget;
        private DevExpress.XtraEditors.PanelControl pceText;
        private DevExpress.XtraEditors.LabelControl labMessage;
        private DevExpress.XtraEditors.ProgressBarControl pbcSend;
    }
}
