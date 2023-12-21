namespace Insight.Base.MainForm.Views
{
    public partial class UpdateDialog
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.sbeUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.pceUpdate = new DevExpress.XtraEditors.ProgressBarControl();
            this.LabFile = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pceUpdate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.LabFile);
            this.panel.Controls.Add(this.pceUpdate);
            // 
            // cancel
            // 
            this.cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cancel.Appearance.Options.UseFont = true;
            // 
            // confirm
            // 
            this.confirm.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.Appearance.Options.UseFont = true;
            this.confirm.Visible = false;
            // 
            // close
            // 
            this.close.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.close.Appearance.Options.UseFont = true;
            // 
            // sbeUpdate
            // 
            this.sbeUpdate.Location = new System.Drawing.Point(290, 174);
            this.sbeUpdate.Name = "sbeUpdate";
            this.sbeUpdate.Size = new System.Drawing.Size(80, 23);
            this.sbeUpdate.TabIndex = 1;
            this.sbeUpdate.Text = "更  新";
            // 
            // pceUpdate
            // 
            this.pceUpdate.EditValue = "0";
            this.pceUpdate.Location = new System.Drawing.Point(25, 65);
            this.pceUpdate.Name = "pceUpdate";
            this.pceUpdate.Properties.ShowTitle = true;
            this.pceUpdate.Size = new System.Drawing.Size(320, 20);
            this.pceUpdate.TabIndex = 0;
            // 
            // LabFile
            // 
            this.LabFile.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LabFile.Appearance.Options.UseFont = true;
            this.LabFile.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LabFile.Location = new System.Drawing.Point(25, 45);
            this.LabFile.Name = "LabFile";
            this.LabFile.Size = new System.Drawing.Size(320, 14);
            this.LabFile.TabIndex = 0;
            // 
            // UpdateDialog
            // 
            this.ClientSize = new System.Drawing.Size(384, 212);
            this.ControlBox = false;
            this.Controls.Add(this.sbeUpdate);
            this.Name = "UpdateDialog";
            this.Text = "更新客户端程序…";
            this.Controls.SetChildIndex(this.confirm, 0);
            this.Controls.SetChildIndex(this.close, 0);
            this.Controls.SetChildIndex(this.cancel, 0);
            this.Controls.SetChildIndex(this.panel, 0);
            this.Controls.SetChildIndex(this.sbeUpdate, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pceUpdate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraEditors.SimpleButton sbeUpdate;
        public DevExpress.XtraEditors.ProgressBarControl pceUpdate;
        public DevExpress.XtraEditors.LabelControl LabFile;
    }
}
