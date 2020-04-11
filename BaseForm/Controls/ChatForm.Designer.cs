namespace Insight.Utils.Controls
{
    partial class ChatForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.pceInfo = new DevExpress.XtraEditors.PanelControl();
            this.labSign = new DevExpress.XtraEditors.LabelControl();
            this.sbeGender = new DevExpress.XtraEditors.SimpleButton();
            this.iceGender = new DevExpress.Utils.ImageCollection(this.components);
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.picHeadImg = new DevExpress.XtraEditors.PictureEdit();
            this.iceStatus = new DevExpress.Utils.ImageCollection(this.components);
            this.pceSpit = new DevExpress.XtraEditors.PanelControl();
            this.pceInput = new DevExpress.XtraEditors.PanelControl();
            this.mmeInput = new DevExpress.XtraEditors.MemoEdit();
            this.pceButtons = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.sbeSend = new DevExpress.XtraEditors.SimpleButton();
            this.pceTools = new DevExpress.XtraEditors.PanelControl();
            this.sbeEmoji = new DevExpress.XtraEditors.SimpleButton();
            this.sbeScreenshot = new DevExpress.XtraEditors.SimpleButton();
            this.sbeFile = new DevExpress.XtraEditors.SimpleButton();
            this.sbeHistroy = new DevExpress.XtraEditors.SimpleButton();
            this.sbeImage = new DevExpress.XtraEditors.SimpleButton();
            this.sceMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pceMessage = new DevExpress.XtraEditors.PanelControl();
            this.mlcMessage = new Insight.Utils.Controls.MessageList();
            ((System.ComponentModel.ISupportInitialize)(this.pceInfo)).BeginInit();
            this.pceInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iceGender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeadImg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iceStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceSpit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceInput)).BeginInit();
            this.pceInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmeInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceButtons)).BeginInit();
            this.pceButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceTools)).BeginInit();
            this.pceTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sceMain)).BeginInit();
            this.sceMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceMessage)).BeginInit();
            this.pceMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pceInfo
            // 
            this.pceInfo.Appearance.Options.UseBackColor = true;
            this.pceInfo.Controls.Add(this.labSign);
            this.pceInfo.Controls.Add(this.sbeGender);
            this.pceInfo.Controls.Add(this.labName);
            this.pceInfo.Controls.Add(this.picHeadImg);
            this.pceInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pceInfo.Location = new System.Drawing.Point(6, 6);
            this.pceInfo.Name = "pceInfo";
            this.pceInfo.Size = new System.Drawing.Size(921, 79);
            this.pceInfo.TabIndex = 0;
            // 
            // labSign
            // 
            this.labSign.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSign.Appearance.Options.UseFont = true;
            this.labSign.Location = new System.Drawing.Point(83, 48);
            this.labSign.Name = "labSign";
            this.labSign.Size = new System.Drawing.Size(48, 14);
            this.labSign.TabIndex = 0;
            this.labSign.Text = "个性签名";
            // 
            // sbeGender
            // 
            this.sbeGender.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeGender.ImageOptions.ImageIndex = 0;
            this.sbeGender.ImageOptions.ImageList = this.iceGender;
            this.sbeGender.Location = new System.Drawing.Point(82, 16);
            this.sbeGender.Name = "sbeGender";
            this.sbeGender.Size = new System.Drawing.Size(26, 26);
            this.sbeGender.TabIndex = 0;
            // 
            // iceGender
            // 
            this.iceGender.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("iceGender.ImageStream")));
            this.iceGender.Images.SetKeyName(0, "role_16x16.png");
            this.iceGender.Images.SetKeyName(1, "user_16x16.png");
            this.iceGender.Images.SetKeyName(2, "female_16x16.png");
            // 
            // labName
            // 
            this.labName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labName.Appearance.Options.UseFont = true;
            this.labName.Location = new System.Drawing.Point(111, 21);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(24, 14);
            this.labName.TabIndex = 0;
            this.labName.Text = "昵称";
            // 
            // picHeadImg
            // 
            this.picHeadImg.EditValue = ((object)(resources.GetObject("picHeadImg.EditValue")));
            this.picHeadImg.Location = new System.Drawing.Point(6, 5);
            this.picHeadImg.Name = "picHeadImg";
            this.picHeadImg.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picHeadImg.Properties.Appearance.Options.UseBackColor = true;
            this.picHeadImg.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picHeadImg.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picHeadImg.Size = new System.Drawing.Size(70, 70);
            this.picHeadImg.TabIndex = 0;
            // 
            // iceStatus
            // 
            this.iceStatus.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("iceStatus.ImageStream")));
            this.iceStatus.Images.SetKeyName(0, "cancel_16x16.png");
            this.iceStatus.Images.SetKeyName(1, "apply_16x16.png");
            // 
            // pceSpit
            // 
            this.pceSpit.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pceSpit.Appearance.Options.UseBackColor = true;
            this.pceSpit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceSpit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pceSpit.Location = new System.Drawing.Point(6, 85);
            this.pceSpit.Name = "pceSpit";
            this.pceSpit.Size = new System.Drawing.Size(921, 6);
            this.pceSpit.TabIndex = 0;
            // 
            // pceInput
            // 
            this.pceInput.Controls.Add(this.mmeInput);
            this.pceInput.Controls.Add(this.pceButtons);
            this.pceInput.Controls.Add(this.pceTools);
            this.pceInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pceInput.Location = new System.Drawing.Point(0, 0);
            this.pceInput.Name = "pceInput";
            this.pceInput.Size = new System.Drawing.Size(921, 187);
            this.pceInput.TabIndex = 0;
            // 
            // mmeInput
            // 
            this.mmeInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mmeInput.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mmeInput.Location = new System.Drawing.Point(2, 44);
            this.mmeInput.Name = "mmeInput";
            this.mmeInput.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmeInput.Properties.Appearance.Options.UseFont = true;
            this.mmeInput.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mmeInput.Size = new System.Drawing.Size(917, 99);
            this.mmeInput.TabIndex = 1;
            // 
            // pceButtons
            // 
            this.pceButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceButtons.Controls.Add(this.labelControl1);
            this.pceButtons.Controls.Add(this.sbeSend);
            this.pceButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pceButtons.Location = new System.Drawing.Point(2, 143);
            this.pceButtons.Name = "pceButtons";
            this.pceButtons.Size = new System.Drawing.Size(917, 42);
            this.pceButtons.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(238, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "提示：按 Enter 换行，Ctrl-Enter 发送消息。";
            // 
            // sbeSend
            // 
            this.sbeSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sbeSend.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbeSend.Appearance.Options.UseFont = true;
            this.sbeSend.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeSend.ImageOptions.Image")));
            this.sbeSend.Location = new System.Drawing.Point(793, 6);
            this.sbeSend.Name = "sbeSend";
            this.sbeSend.Size = new System.Drawing.Size(117, 31);
            this.sbeSend.TabIndex = 2;
            this.sbeSend.Text = "发  送";
            // 
            // pceTools
            // 
            this.pceTools.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceTools.Controls.Add(this.sbeEmoji);
            this.pceTools.Controls.Add(this.sbeScreenshot);
            this.pceTools.Controls.Add(this.sbeFile);
            this.pceTools.Controls.Add(this.sbeHistroy);
            this.pceTools.Controls.Add(this.sbeImage);
            this.pceTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pceTools.Location = new System.Drawing.Point(2, 2);
            this.pceTools.Name = "pceTools";
            this.pceTools.Size = new System.Drawing.Size(917, 42);
            this.pceTools.TabIndex = 0;
            // 
            // sbeEmoji
            // 
            this.sbeEmoji.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeEmoji.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeEmoji.ImageOptions.Image")));
            this.sbeEmoji.Location = new System.Drawing.Point(292, 2);
            this.sbeEmoji.Name = "sbeEmoji";
            this.sbeEmoji.Size = new System.Drawing.Size(37, 37);
            this.sbeEmoji.TabIndex = 0;
            // 
            // sbeScreenshot
            // 
            this.sbeScreenshot.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbeScreenshot.Appearance.Options.UseFont = true;
            this.sbeScreenshot.Appearance.Options.UseTextOptions = true;
            this.sbeScreenshot.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.sbeScreenshot.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeScreenshot.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeScreenshot.ImageOptions.Image")));
            this.sbeScreenshot.Location = new System.Drawing.Point(216, 2);
            this.sbeScreenshot.Name = "sbeScreenshot";
            this.sbeScreenshot.Size = new System.Drawing.Size(76, 37);
            this.sbeScreenshot.TabIndex = 0;
            this.sbeScreenshot.Text = "截屏";
            // 
            // sbeFile
            // 
            this.sbeFile.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbeFile.Appearance.Options.UseFont = true;
            this.sbeFile.Appearance.Options.UseTextOptions = true;
            this.sbeFile.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.sbeFile.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeFile.ImageOptions.Image")));
            this.sbeFile.Location = new System.Drawing.Point(111, 2);
            this.sbeFile.Name = "sbeFile";
            this.sbeFile.Size = new System.Drawing.Size(105, 37);
            this.sbeFile.TabIndex = 0;
            this.sbeFile.Text = "发送文件";
            // 
            // sbeHistroy
            // 
            this.sbeHistroy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbeHistroy.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbeHistroy.Appearance.Options.UseFont = true;
            this.sbeHistroy.Appearance.Options.UseTextOptions = true;
            this.sbeHistroy.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.sbeHistroy.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeHistroy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeHistroy.ImageOptions.Image")));
            this.sbeHistroy.Location = new System.Drawing.Point(811, 2);
            this.sbeHistroy.Name = "sbeHistroy";
            this.sbeHistroy.Size = new System.Drawing.Size(105, 37);
            this.sbeHistroy.TabIndex = 0;
            this.sbeHistroy.Text = "历史消息";
            // 
            // sbeImage
            // 
            this.sbeImage.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbeImage.Appearance.Options.UseFont = true;
            this.sbeImage.Appearance.Options.UseTextOptions = true;
            this.sbeImage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.sbeImage.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeImage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeImage.ImageOptions.Image")));
            this.sbeImage.Location = new System.Drawing.Point(6, 2);
            this.sbeImage.Name = "sbeImage";
            this.sbeImage.Size = new System.Drawing.Size(105, 37);
            this.sbeImage.TabIndex = 0;
            this.sbeImage.Text = "发送图片";
            // 
            // sceMain
            // 
            this.sceMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.sceMain.Horizontal = false;
            this.sceMain.Location = new System.Drawing.Point(6, 91);
            this.sceMain.Name = "sceMain";
            this.sceMain.Panel1.Controls.Add(this.pceMessage);
            this.sceMain.Panel1.MinSize = 400;
            this.sceMain.Panel1.Text = "Panel1";
            this.sceMain.Panel2.Controls.Add(this.pceInput);
            this.sceMain.Panel2.MinSize = 160;
            this.sceMain.Panel2.Text = "Panel2";
            this.sceMain.Size = new System.Drawing.Size(921, 603);
            this.sceMain.SplitterPosition = 187;
            this.sceMain.TabIndex = 0;
            // 
            // pceMessage
            // 
            this.pceMessage.Controls.Add(this.mlcMessage);
            this.pceMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pceMessage.Location = new System.Drawing.Point(0, 0);
            this.pceMessage.Name = "pceMessage";
            this.pceMessage.Size = new System.Drawing.Size(921, 411);
            this.pceMessage.TabIndex = 0;
            // 
            // mlcMessage
            // 
            this.mlcMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlcMessage.Location = new System.Drawing.Point(2, 2);
            this.mlcMessage.Name = "mlcMessage";
            this.mlcMessage.Size = new System.Drawing.Size(917, 407);
            this.mlcMessage.TabIndex = 0;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sceMain);
            this.Controls.Add(this.pceSpit);
            this.Controls.Add(this.pceInfo);
            this.Name = "ChatForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(933, 700);
            ((System.ComponentModel.ISupportInitialize)(this.pceInfo)).EndInit();
            this.pceInfo.ResumeLayout(false);
            this.pceInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iceGender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeadImg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iceStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceSpit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceInput)).EndInit();
            this.pceInput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mmeInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceButtons)).EndInit();
            this.pceButtons.ResumeLayout(false);
            this.pceButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceTools)).EndInit();
            this.pceTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sceMain)).EndInit();
            this.sceMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pceMessage)).EndInit();
            this.pceMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pceInfo;
        private DevExpress.XtraEditors.PictureEdit picHeadImg;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.Utils.ImageCollection iceStatus;
        private DevExpress.XtraEditors.SimpleButton sbeGender;
        private DevExpress.Utils.ImageCollection iceGender;
        private DevExpress.XtraEditors.LabelControl labSign;
        private DevExpress.XtraEditors.PanelControl pceSpit;
        private DevExpress.XtraEditors.PanelControl pceInput;
        private DevExpress.XtraEditors.MemoEdit mmeInput;
        private DevExpress.XtraEditors.PanelControl pceButtons;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton sbeSend;
        private DevExpress.XtraEditors.PanelControl pceTools;
        private DevExpress.XtraEditors.SimpleButton sbeEmoji;
        private DevExpress.XtraEditors.SimpleButton sbeScreenshot;
        private DevExpress.XtraEditors.SimpleButton sbeFile;
        private DevExpress.XtraEditors.SimpleButton sbeHistroy;
        private DevExpress.XtraEditors.SimpleButton sbeImage;
        private DevExpress.XtraEditors.SplitContainerControl sceMain;
        private DevExpress.XtraEditors.PanelControl pceMessage;
        private MessageList mlcMessage;
    }
}
