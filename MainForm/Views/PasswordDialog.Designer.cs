namespace Insight.Base.MainForm.Views
{
    public partial class PasswordDialog
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
            this.txtConfirm = new DevExpress.XtraEditors.TextEdit();
            this.labConfirmPw = new DevExpress.XtraEditors.LabelControl();
            this.txtOld = new DevExpress.XtraEditors.TextEdit();
            this.labOldPw = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.labNewPw = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOld.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.txtConfirm);
            this.panel.Controls.Add(this.labConfirmPw);
            this.panel.Controls.Add(this.txtOld);
            this.panel.Controls.Add(this.labOldPw);
            this.panel.Controls.Add(this.txtPassword);
            this.panel.Controls.Add(this.labNewPw);
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
            // 
            // close
            // 
            this.close.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.close.Appearance.Options.UseFont = true;
            // 
            // txtConfirm
            // 
            this.txtConfirm.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtConfirm.Location = new System.Drawing.Point(145, 100);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Properties.PasswordChar = '○';
            this.txtConfirm.Size = new System.Drawing.Size(160, 20);
            this.txtConfirm.TabIndex = 3;
            // 
            // labConfirmPw
            // 
            this.labConfirmPw.Appearance.Options.UseTextOptions = true;
            this.labConfirmPw.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labConfirmPw.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labConfirmPw.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labConfirmPw.Location = new System.Drawing.Point(65, 100);
            this.labConfirmPw.Name = "labConfirmPw";
            this.labConfirmPw.Size = new System.Drawing.Size(80, 21);
            this.labConfirmPw.TabIndex = 9;
            this.labConfirmPw.Text = "确认密码：";
            // 
            // txtOld
            // 
            this.txtOld.EnterMoveNextControl = true;
            this.txtOld.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtOld.Location = new System.Drawing.Point(145, 30);
            this.txtOld.Name = "txtOld";
            this.txtOld.Size = new System.Drawing.Size(160, 20);
            this.txtOld.TabIndex = 1;
            // 
            // labOldPw
            // 
            this.labOldPw.Appearance.Options.UseTextOptions = true;
            this.labOldPw.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labOldPw.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labOldPw.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labOldPw.Location = new System.Drawing.Point(65, 30);
            this.labOldPw.Name = "labOldPw";
            this.labOldPw.Size = new System.Drawing.Size(80, 21);
            this.labOldPw.TabIndex = 5;
            this.labOldPw.Text = "原 密 码：";
            // 
            // txtPassword
            // 
            this.txtPassword.EnterMoveNextControl = true;
            this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPassword.Location = new System.Drawing.Point(145, 65);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '○';
            this.txtPassword.Size = new System.Drawing.Size(160, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // labNewPw
            // 
            this.labNewPw.Appearance.Options.UseTextOptions = true;
            this.labNewPw.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labNewPw.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labNewPw.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labNewPw.Location = new System.Drawing.Point(65, 65);
            this.labNewPw.Name = "labNewPw";
            this.labNewPw.Size = new System.Drawing.Size(80, 21);
            this.labNewPw.TabIndex = 7;
            this.labNewPw.Text = "新 密 码：";
            // 
            // ChangePw
            // 
            this.ClientSize = new System.Drawing.Size(384, 212);
            this.Name = "ChangePw";
            this.Text = "更换密码";
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOld.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labConfirmPw;
        private DevExpress.XtraEditors.LabelControl labOldPw;
        private DevExpress.XtraEditors.LabelControl labNewPw;
        public DevExpress.XtraEditors.TextEdit txtOld;
        public DevExpress.XtraEditors.TextEdit txtPassword;
        public DevExpress.XtraEditors.TextEdit txtConfirm;
    }
}