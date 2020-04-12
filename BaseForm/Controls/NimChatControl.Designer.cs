using Insight.Utils.Controls.Nim;

namespace Insight.Utils.Controls
{
    partial class NimChatControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NimChatControl));
            this.pceInput = new DevExpress.XtraEditors.PanelControl();
            this.mmeInput = new DevExpress.XtraEditors.MemoEdit();
            this.labPrompt = new DevExpress.XtraEditors.LabelControl();
            this.sbeSend = new DevExpress.XtraEditors.SimpleButton();
            this.pceTools = new DevExpress.XtraEditors.PanelControl();
            this.sbeReply = new DevExpress.XtraEditors.SimpleButton();
            this.sbeEmoji = new DevExpress.XtraEditors.SimpleButton();
            this.sbeFile = new DevExpress.XtraEditors.SimpleButton();
            this.sbeHistroy = new DevExpress.XtraEditors.SimpleButton();
            this.sbeImage = new DevExpress.XtraEditors.SimpleButton();
            this.sceMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pceMessage = new DevExpress.XtraEditors.PanelControl();
            this.mlcMessage = new Insight.Utils.Controls.Nim.NimList();
            this.pceBotton = new DevExpress.XtraEditors.PanelControl();
            this.pceSpei = new DevExpress.XtraEditors.PanelControl();
            this.ofdMessage = new System.Windows.Forms.OpenFileDialog();
            this.labSp1 = new DevExpress.XtraEditors.LabelControl();
            this.labSp2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pceInput)).BeginInit();
            this.pceInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmeInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceTools)).BeginInit();
            this.pceTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sceMain)).BeginInit();
            this.sceMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceMessage)).BeginInit();
            this.pceMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceBotton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceSpei)).BeginInit();
            this.SuspendLayout();
            // 
            // pceInput
            // 
            this.pceInput.Controls.Add(this.mmeInput);
            this.pceInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pceInput.Location = new System.Drawing.Point(0, 45);
            this.pceInput.Name = "pceInput";
            this.pceInput.Padding = new System.Windows.Forms.Padding(2);
            this.pceInput.Size = new System.Drawing.Size(505, 115);
            this.pceInput.TabIndex = 0;
            // 
            // mmeInput
            // 
            this.mmeInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mmeInput.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mmeInput.Location = new System.Drawing.Point(4, 4);
            this.mmeInput.Name = "mmeInput";
            this.mmeInput.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmeInput.Properties.Appearance.Options.UseFont = true;
            this.mmeInput.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mmeInput.Size = new System.Drawing.Size(497, 107);
            this.mmeInput.TabIndex = 1;
            // 
            // labPrompt
            // 
            this.labPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labPrompt.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPrompt.Appearance.Options.UseFont = true;
            this.labPrompt.Location = new System.Drawing.Point(411, 13);
            this.labPrompt.Name = "labPrompt";
            this.labPrompt.Size = new System.Drawing.Size(174, 14);
            this.labPrompt.TabIndex = 0;
            this.labPrompt.Text = "Enter 换行，Ctrl-Enter 发送消息";
            // 
            // sbeSend
            // 
            this.sbeSend.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbeSend.Appearance.Options.UseFont = true;
            this.sbeSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.sbeSend.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeSend.ImageOptions.Image")));
            this.sbeSend.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.sbeSend.Location = new System.Drawing.Point(510, 45);
            this.sbeSend.Name = "sbeSend";
            this.sbeSend.Size = new System.Drawing.Size(80, 115);
            this.sbeSend.TabIndex = 2;
            this.sbeSend.Text = "发  送";
            // 
            // pceTools
            // 
            this.pceTools.Controls.Add(this.labSp2);
            this.pceTools.Controls.Add(this.labSp1);
            this.pceTools.Controls.Add(this.sbeReply);
            this.pceTools.Controls.Add(this.labPrompt);
            this.pceTools.Controls.Add(this.sbeEmoji);
            this.pceTools.Controls.Add(this.sbeFile);
            this.pceTools.Controls.Add(this.sbeHistroy);
            this.pceTools.Controls.Add(this.sbeImage);
            this.pceTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pceTools.Location = new System.Drawing.Point(0, 0);
            this.pceTools.Name = "pceTools";
            this.pceTools.Size = new System.Drawing.Size(590, 40);
            this.pceTools.TabIndex = 0;
            // 
            // sbeReply
            // 
            this.sbeReply.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeReply.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeReply.ImageOptions.Image")));
            this.sbeReply.Location = new System.Drawing.Point(285, 2);
            this.sbeReply.Name = "sbeReply";
            this.sbeReply.Size = new System.Drawing.Size(90, 37);
            this.sbeReply.TabIndex = 0;
            this.sbeReply.Text = "快速回复";
            // 
            // sbeEmoji
            // 
            this.sbeEmoji.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeEmoji.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeEmoji.ImageOptions.Image")));
            this.sbeEmoji.Location = new System.Drawing.Point(200, 2);
            this.sbeEmoji.Name = "sbeEmoji";
            this.sbeEmoji.Size = new System.Drawing.Size(65, 37);
            this.sbeEmoji.TabIndex = 0;
            this.sbeEmoji.Text = "表情";
            // 
            // sbeFile
            // 
            this.sbeFile.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbeFile.Appearance.Options.UseFont = true;
            this.sbeFile.Appearance.Options.UseTextOptions = true;
            this.sbeFile.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.sbeFile.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeFile.ImageOptions.Image")));
            this.sbeFile.Location = new System.Drawing.Point(115, 2);
            this.sbeFile.Name = "sbeFile";
            this.sbeFile.Size = new System.Drawing.Size(37, 37);
            this.sbeFile.TabIndex = 0;
            // 
            // sbeHistroy
            // 
            this.sbeHistroy.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbeHistroy.Appearance.Options.UseFont = true;
            this.sbeHistroy.Appearance.Options.UseTextOptions = true;
            this.sbeHistroy.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.sbeHistroy.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sbeHistroy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbeHistroy.ImageOptions.Image")));
            this.sbeHistroy.Location = new System.Drawing.Point(5, 2);
            this.sbeHistroy.Name = "sbeHistroy";
            this.sbeHistroy.Size = new System.Drawing.Size(90, 37);
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
            this.sbeImage.Location = new System.Drawing.Point(160, 2);
            this.sbeImage.Name = "sbeImage";
            this.sbeImage.Size = new System.Drawing.Size(37, 37);
            this.sbeImage.TabIndex = 0;
            // 
            // sceMain
            // 
            this.sceMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.sceMain.Horizontal = false;
            this.sceMain.Location = new System.Drawing.Point(5, 5);
            this.sceMain.Name = "sceMain";
            this.sceMain.Panel1.Controls.Add(this.pceMessage);
            this.sceMain.Panel1.MinSize = 400;
            this.sceMain.Panel1.Text = "Panel1";
            this.sceMain.Panel2.Controls.Add(this.pceInput);
            this.sceMain.Panel2.Controls.Add(this.pceBotton);
            this.sceMain.Panel2.Controls.Add(this.sbeSend);
            this.sceMain.Panel2.Controls.Add(this.pceSpei);
            this.sceMain.Panel2.Controls.Add(this.pceTools);
            this.sceMain.Panel2.MinSize = 160;
            this.sceMain.Panel2.Text = "Panel2";
            this.sceMain.Size = new System.Drawing.Size(590, 530);
            this.sceMain.SplitterPosition = 160;
            this.sceMain.TabIndex = 0;
            // 
            // pceMessage
            // 
            this.pceMessage.Controls.Add(this.mlcMessage);
            this.pceMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pceMessage.Location = new System.Drawing.Point(0, 0);
            this.pceMessage.Name = "pceMessage";
            this.pceMessage.Size = new System.Drawing.Size(590, 365);
            this.pceMessage.TabIndex = 0;
            // 
            // mlcMessage
            // 
            this.mlcMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlcMessage.Location = new System.Drawing.Point(2, 2);
            this.mlcMessage.Name = "mlcMessage";
            this.mlcMessage.Size = new System.Drawing.Size(586, 361);
            this.mlcMessage.TabIndex = 0;
            // 
            // pceBotton
            // 
            this.pceBotton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceBotton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pceBotton.Location = new System.Drawing.Point(505, 45);
            this.pceBotton.Name = "pceBotton";
            this.pceBotton.Size = new System.Drawing.Size(5, 115);
            this.pceBotton.TabIndex = 0;
            // 
            // pceSpei
            // 
            this.pceSpei.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceSpei.Dock = System.Windows.Forms.DockStyle.Top;
            this.pceSpei.Location = new System.Drawing.Point(0, 40);
            this.pceSpei.Name = "pceSpei";
            this.pceSpei.Size = new System.Drawing.Size(590, 5);
            this.pceSpei.TabIndex = 0;
            // 
            // labSp1
            // 
            this.labSp1.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.labSp1.Appearance.Options.UseFont = true;
            this.labSp1.Location = new System.Drawing.Point(100, 9);
            this.labSp1.Name = "labSp1";
            this.labSp1.Size = new System.Drawing.Size(7, 21);
            this.labSp1.TabIndex = 1;
            this.labSp1.Text = "|";
            // 
            // labSp2
            // 
            this.labSp2.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.labSp2.Appearance.Options.UseFont = true;
            this.labSp2.Location = new System.Drawing.Point(270, 9);
            this.labSp2.Name = "labSp2";
            this.labSp2.Size = new System.Drawing.Size(7, 21);
            this.labSp2.TabIndex = 0;
            this.labSp2.Text = "|";
            // 
            // NimChatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sceMain);
            this.MinimumSize = new System.Drawing.Size(600, 540);
            this.Name = "NimChatControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(600, 540);
            ((System.ComponentModel.ISupportInitialize)(this.pceInput)).EndInit();
            this.pceInput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mmeInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceTools)).EndInit();
            this.pceTools.ResumeLayout(false);
            this.pceTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sceMain)).EndInit();
            this.sceMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pceMessage)).EndInit();
            this.pceMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pceBotton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pceSpei)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pceInput;
        private DevExpress.XtraEditors.MemoEdit mmeInput;
        private DevExpress.XtraEditors.LabelControl labPrompt;
        private DevExpress.XtraEditors.SimpleButton sbeSend;
        private DevExpress.XtraEditors.PanelControl pceTools;
        private DevExpress.XtraEditors.SimpleButton sbeEmoji;
        private DevExpress.XtraEditors.SimpleButton sbeFile;
        private DevExpress.XtraEditors.SimpleButton sbeHistroy;
        private DevExpress.XtraEditors.SimpleButton sbeImage;
        private DevExpress.XtraEditors.SplitContainerControl sceMain;
        private DevExpress.XtraEditors.PanelControl pceMessage;
        private NimList mlcMessage;
        private DevExpress.XtraEditors.PanelControl pceBotton;
        private DevExpress.XtraEditors.PanelControl pceSpei;
        private DevExpress.XtraEditors.SimpleButton sbeReply;
        private System.Windows.Forms.OpenFileDialog ofdMessage;
        private DevExpress.XtraEditors.LabelControl labSp2;
        private DevExpress.XtraEditors.LabelControl labSp1;
    }
}
