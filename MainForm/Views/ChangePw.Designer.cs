namespace Insight.Utils.MainForm.Views
{
    public partial class ChangePw
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePw));
            this.ConfirmPw = new DevExpress.XtraEditors.TextEdit();
            this.labConfirmPw = new DevExpress.XtraEditors.LabelControl();
            this.Password = new DevExpress.XtraEditors.TextEdit();
            this.labOldPw = new DevExpress.XtraEditors.LabelControl();
            this.NewPw = new DevExpress.XtraEditors.TextEdit();
            this.labNewPw = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfirmPw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPw.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.ConfirmPw);
            this.panel.Controls.Add(this.labConfirmPw);
            this.panel.Controls.Add(this.Password);
            this.panel.Controls.Add(this.labOldPw);
            this.panel.Controls.Add(this.NewPw);
            this.panel.Controls.Add(this.labNewPw);
            // 
            // Cancel
            // 
            this.cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cancel.Appearance.Options.UseFont = true;
            // 
            // Confirm
            // 
            this.confirm.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.Appearance.Options.UseFont = true;
            // 
            // ConfirmPw
            // 
            this.ConfirmPw.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ConfirmPw.Location = new System.Drawing.Point(145, 100);
            this.ConfirmPw.Name = "ConfirmPw";
            this.ConfirmPw.Properties.PasswordChar = '○';
            this.ConfirmPw.Size = new System.Drawing.Size(160, 20);
            this.ConfirmPw.TabIndex = 3;
            // 
            // labConfirmPw
            // 
            this.labConfirmPw.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labConfirmPw.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labConfirmPw.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labConfirmPw.Location = new System.Drawing.Point(65, 100);
            this.labConfirmPw.Name = "labConfirmPw";
            this.labConfirmPw.Size = new System.Drawing.Size(80, 21);
            this.labConfirmPw.TabIndex = 9;
            this.labConfirmPw.Text = "确认密码：";
            // 
            // Password
            // 
            this.Password.EnterMoveNextControl = true;
            this.Password.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Password.Location = new System.Drawing.Point(145, 30);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(160, 20);
            this.Password.TabIndex = 1;
            // 
            // labOldPw
            // 
            this.labOldPw.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labOldPw.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labOldPw.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labOldPw.Location = new System.Drawing.Point(65, 30);
            this.labOldPw.Name = "labOldPw";
            this.labOldPw.Size = new System.Drawing.Size(80, 21);
            this.labOldPw.TabIndex = 5;
            this.labOldPw.Text = "原 密 码：";
            // 
            // NewPw
            // 
            this.NewPw.EnterMoveNextControl = true;
            this.NewPw.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NewPw.Location = new System.Drawing.Point(145, 65);
            this.NewPw.Name = "NewPw";
            this.NewPw.Properties.PasswordChar = '○';
            this.NewPw.Size = new System.Drawing.Size(160, 20);
            this.NewPw.TabIndex = 2;
            // 
            // labNewPw
            // 
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
            ((System.ComponentModel.ISupportInitialize)(this.ConfirmPw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPw.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.TextEdit Password;
        internal DevExpress.XtraEditors.TextEdit NewPw;
        internal DevExpress.XtraEditors.TextEdit ConfirmPw;
        private DevExpress.XtraEditors.LabelControl labConfirmPw;
        private DevExpress.XtraEditors.LabelControl labOldPw;
        private DevExpress.XtraEditors.LabelControl labNewPw;

    }
}