using System.ComponentModel;
using DevExpress.XtraEditors;

namespace Insight.Base.MainForm.Views
{
    public partial class LoginDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginDialog));
            this.OrgTreeNode = new DevExpress.Utils.ImageCollection(this.components);
            this.panMain = new DevExpress.XtraEditors.PanelControl();
            this.peeDept = new DevExpress.XtraEditors.PictureEdit();
            this.peePassword = new DevExpress.XtraEditors.PictureEdit();
            this.peeAccount = new DevExpress.XtraEditors.PictureEdit();
            this.sbeLogin = new DevExpress.XtraEditors.SimpleButton();
            this.sbeSet = new DevExpress.XtraEditors.SimpleButton();
            this.sbeCacel = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassWord = new DevExpress.XtraEditors.TextEdit();
            this.txtAccount = new DevExpress.XtraEditors.TextEdit();
            this.lueTenant = new DevExpress.XtraEditors.LookUpEdit();
            this.labLoading = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peeDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peeAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueTenant.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // OrgTreeNode
            // 
            this.OrgTreeNode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("OrgTreeNode.ImageStream")));
            this.OrgTreeNode.Images.SetKeyName(0, "NodeOrg.png");
            this.OrgTreeNode.Images.SetKeyName(1, "NodeDept.png");
            this.OrgTreeNode.Images.SetKeyName(2, "NodePost.png");
            // 
            // panMain
            // 
            this.panMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panMain.Appearance.Options.UseBackColor = true;
            this.panMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panMain.Controls.Add(this.peeDept);
            this.panMain.Controls.Add(this.peePassword);
            this.panMain.Controls.Add(this.peeAccount);
            this.panMain.Controls.Add(this.sbeLogin);
            this.panMain.Controls.Add(this.sbeSet);
            this.panMain.Controls.Add(this.sbeCacel);
            this.panMain.Controls.Add(this.txtPassWord);
            this.panMain.Controls.Add(this.txtAccount);
            this.panMain.Controls.Add(this.lueTenant);
            this.panMain.Location = new System.Drawing.Point(130, 128);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(260, 140);
            this.panMain.TabIndex = 0;
            this.panMain.Visible = false;
            // 
            // peeDept
            // 
            this.peeDept.Cursor = System.Windows.Forms.Cursors.Default;
            this.peeDept.EditValue = ((object)(resources.GetObject("peeDept.EditValue")));
            this.peeDept.Location = new System.Drawing.Point(30, 70);
            this.peeDept.Name = "peeDept";
            this.peeDept.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peeDept.Properties.Appearance.Options.UseBackColor = true;
            this.peeDept.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peeDept.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peeDept.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peeDept.Size = new System.Drawing.Size(24, 24);
            this.peeDept.TabIndex = 0;
            this.peeDept.Visible = false;
            // 
            // peePassword
            // 
            this.peePassword.Cursor = System.Windows.Forms.Cursors.Default;
            this.peePassword.EditValue = ((object)(resources.GetObject("peePassword.EditValue")));
            this.peePassword.Location = new System.Drawing.Point(30, 35);
            this.peePassword.Name = "peePassword";
            this.peePassword.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peePassword.Properties.Appearance.Options.UseBackColor = true;
            this.peePassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peePassword.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peePassword.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peePassword.Size = new System.Drawing.Size(24, 24);
            this.peePassword.TabIndex = 0;
            // 
            // peeAccount
            // 
            this.peeAccount.Cursor = System.Windows.Forms.Cursors.Default;
            this.peeAccount.EditValue = ((object)(resources.GetObject("peeAccount.EditValue")));
            this.peeAccount.Location = new System.Drawing.Point(30, 0);
            this.peeAccount.Name = "peeAccount";
            this.peeAccount.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peeAccount.Properties.Appearance.Options.UseBackColor = true;
            this.peeAccount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peeAccount.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peeAccount.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peeAccount.Size = new System.Drawing.Size(24, 24);
            this.peeAccount.TabIndex = 0;
            // 
            // sbeLogin
            // 
            this.sbeLogin.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sbeLogin.Appearance.Options.UseFont = true;
            this.sbeLogin.Location = new System.Drawing.Point(180, 117);
            this.sbeLogin.Name = "sbeLogin";
            this.sbeLogin.Size = new System.Drawing.Size(80, 23);
            this.sbeLogin.TabIndex = 0;
            this.sbeLogin.Text = "登  录";
            // 
            // sbeSet
            // 
            this.sbeSet.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sbeSet.Appearance.Options.UseFont = true;
            this.sbeSet.Location = new System.Drawing.Point(90, 117);
            this.sbeSet.Name = "sbeSet";
            this.sbeSet.Size = new System.Drawing.Size(80, 23);
            this.sbeSet.TabIndex = 0;
            this.sbeSet.Text = "设  置";
            // 
            // sbeCacel
            // 
            this.sbeCacel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sbeCacel.Appearance.Options.UseFont = true;
            this.sbeCacel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbeCacel.Location = new System.Drawing.Point(0, 117);
            this.sbeCacel.Name = "sbeCacel";
            this.sbeCacel.Size = new System.Drawing.Size(80, 23);
            this.sbeCacel.TabIndex = 0;
            this.sbeCacel.Text = "取  消";
            // 
            // txtPassWord
            // 
            this.txtPassWord.EditValue = "";
            this.txtPassWord.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPassWord.Location = new System.Drawing.Point(60, 37);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Properties.AutoHeight = false;
            this.txtPassWord.Properties.PasswordChar = '○';
            this.txtPassWord.Size = new System.Drawing.Size(160, 21);
            this.txtPassWord.TabIndex = 2;
            // 
            // txtAccount
            // 
            this.txtAccount.EnterMoveNextControl = true;
            this.txtAccount.Location = new System.Drawing.Point(60, 2);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Properties.AutoHeight = false;
            this.txtAccount.Size = new System.Drawing.Size(160, 21);
            this.txtAccount.TabIndex = 1;
            // 
            // lueTenant
            // 
            this.lueTenant.Location = new System.Drawing.Point(60, 72);
            this.lueTenant.Name = "lueTenant";
            this.lueTenant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueTenant.Properties.NullText = "请选择……";
            this.lueTenant.Size = new System.Drawing.Size(160, 44);
            this.lueTenant.TabIndex = 3;
            // 
            // labLoading
            // 
            this.labLoading.Appearance.ForeColor = System.Drawing.Color.White;
            this.labLoading.Appearance.Options.UseForeColor = true;
            this.labLoading.Location = new System.Drawing.Point(183, 153);
            this.labLoading.Name = "labLoading";
            this.labLoading.Size = new System.Drawing.Size(308, 29);
            this.labLoading.TabIndex = 0;
            this.labLoading.Text = "正在加载应用程序，请稍候…";
            this.labLoading.Visible = false;
            // 
            // LoginDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(520, 320);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.labLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).EndInit();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peeDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peeAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueTenant.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected DevExpress.Utils.ImageCollection OrgTreeNode;
        public LabelControl labLoading;
        public PictureEdit peeDept;
        private PictureEdit peePassword;
        private PictureEdit peeAccount;
        public PanelControl panMain;
        public SimpleButton sbeLogin;
        public SimpleButton sbeSet;
        public SimpleButton sbeCacel;
        public TextEdit txtPassWord;
        public TextEdit txtAccount;
        public LookUpEdit lueTenant;
    }
}