namespace Insight.Utils.NetEaseIM.Controls
{
    partial class HistoryDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sceHistory = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pceHistory = new DevExpress.XtraEditors.PanelControl();
            this.sbeNext = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            this.sceHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.sceHistory);
            this.panel.Location = new System.Drawing.Point(7, 35);
            this.panel.Size = new System.Drawing.Size(570, 472);
            // 
            // cancel
            // 
            this.cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cancel.Appearance.Options.UseFont = true;
            this.cancel.Location = new System.Drawing.Point(400, 524);
            this.cancel.Visible = false;
            // 
            // confirm
            // 
            this.confirm.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.Appearance.Options.UseFont = true;
            this.confirm.Location = new System.Drawing.Point(490, 524);
            this.confirm.Visible = false;
            // 
            // close
            // 
            this.close.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.close.Appearance.Options.UseFont = true;
            this.close.Location = new System.Drawing.Point(490, 524);
            // 
            // sceHistory
            // 
            this.sceHistory.Controls.Add(this.pceHistory);
            this.sceHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceHistory.Location = new System.Drawing.Point(2, 2);
            this.sceHistory.Name = "sceHistory";
            this.sceHistory.Size = new System.Drawing.Size(566, 468);
            this.sceHistory.TabIndex = 0;
            // 
            // pceHistory
            // 
            this.pceHistory.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pceHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.pceHistory.Location = new System.Drawing.Point(0, 0);
            this.pceHistory.Name = "pceHistory";
            this.pceHistory.Size = new System.Drawing.Size(566, 468);
            this.pceHistory.TabIndex = 0;
            // 
            // sbeNext
            // 
            this.sbeNext.Location = new System.Drawing.Point(7, 7);
            this.sbeNext.Name = "sbeNext";
            this.sbeNext.Size = new System.Drawing.Size(570, 23);
            this.sbeNext.TabIndex = 0;
            this.sbeNext.Text = "更多消息……";
            // 
            // HistoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.sbeNext);
            this.Name = "HistoryDialog";
            this.Text = "历史消息";
            this.Controls.SetChildIndex(this.confirm, 0);
            this.Controls.SetChildIndex(this.close, 0);
            this.Controls.SetChildIndex(this.cancel, 0);
            this.Controls.SetChildIndex(this.panel, 0);
            this.Controls.SetChildIndex(this.sbeNext, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            this.sceHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pceHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl sceHistory;
        private DevExpress.XtraEditors.PanelControl pceHistory;
        public DevExpress.XtraEditors.SimpleButton sbeNext;
    }
}