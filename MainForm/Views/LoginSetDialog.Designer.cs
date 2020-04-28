using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Insight.Base.MainForm.Views
{
    public partial class LoginSetDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.SaveUserCheckBox = new System.Windows.Forms.CheckBox();
            this.BaseInupt = new DevExpress.XtraEditors.TextEdit();
            this.labBase = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BaseInupt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.SaveUserCheckBox);
            this.panel.Controls.Add(this.labBase);
            this.panel.Controls.Add(this.BaseInupt);
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
            // SaveUserCheckBox
            // 
            this.SaveUserCheckBox.AutoSize = true;
            this.SaveUserCheckBox.Location = new System.Drawing.Point(260, 95);
            this.SaveUserCheckBox.Name = "SaveUserCheckBox";
            this.SaveUserCheckBox.Size = new System.Drawing.Size(86, 18);
            this.SaveUserCheckBox.TabIndex = 5;
            this.SaveUserCheckBox.Text = "保存用户名";
            this.SaveUserCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveUserCheckBox.UseVisualStyleBackColor = true;
            // 
            // BaseInupt
            // 
            this.BaseInupt.EnterMoveNextControl = true;
            this.BaseInupt.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BaseInupt.Location = new System.Drawing.Point(80, 61);
            this.BaseInupt.Name = "BaseInupt";
            this.BaseInupt.Size = new System.Drawing.Size(280, 20);
            this.BaseInupt.TabIndex = 3;
            // 
            // labBase
            // 
            this.labBase.Appearance.Options.UseTextOptions = true;
            this.labBase.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labBase.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labBase.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labBase.Location = new System.Drawing.Point(0, 60);
            this.labBase.Name = "labBase";
            this.labBase.Size = new System.Drawing.Size(80, 21);
            this.labBase.TabIndex = 0;
            this.labBase.Text = "网关地址：";
            // 
            // LoginSetDialog
            // 
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Name = "LoginSetDialog";
            this.Text = "设置";
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BaseInupt.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal CheckBox SaveUserCheckBox;
        internal TextEdit BaseInupt;
        private LabelControl labBase;
    }
}