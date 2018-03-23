using System.ComponentModel;
using DevExpress.XtraEditors;

namespace Insight.Utils.MainForm.Login.Views
{
    public partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.labUser = new DevExpress.XtraEditors.LabelControl();
            this.labPassword = new DevExpress.XtraEditors.LabelControl();
            this.UserNameInput = new DevExpress.XtraEditors.TextEdit();
            this.PassWordInput = new DevExpress.XtraEditors.TextEdit();
            this.CloseButton = new DevExpress.XtraEditors.SimpleButton();
            this.SetButton = new DevExpress.XtraEditors.SimpleButton();
            this.LoginButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lueDept = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeListLookUpEdit2TreeList = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.OrgTreeNode = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.UserNameInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PassWordInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit2TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).BeginInit();
            this.SuspendLayout();
            // 
            // labUser
            // 
            this.labUser.Appearance.Options.UseTextOptions = true;
            this.labUser.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labUser.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labUser.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labUser.Location = new System.Drawing.Point(130, 130);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(80, 21);
            this.labUser.TabIndex = 0;
            this.labUser.Text = "用 户 名：";
            // 
            // labPassword
            // 
            this.labPassword.Appearance.Options.UseTextOptions = true;
            this.labPassword.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labPassword.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labPassword.CausesValidation = false;
            this.labPassword.Location = new System.Drawing.Point(130, 165);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(80, 21);
            this.labPassword.TabIndex = 0;
            this.labPassword.Text = "密 码：";
            // 
            // UserNameInput
            // 
            this.UserNameInput.EnterMoveNextControl = true;
            this.UserNameInput.Location = new System.Drawing.Point(210, 130);
            this.UserNameInput.Name = "UserNameInput";
            this.UserNameInput.Properties.AutoHeight = false;
            this.UserNameInput.Size = new System.Drawing.Size(160, 21);
            this.UserNameInput.TabIndex = 0;
            // 
            // PassWordInput
            // 
            this.PassWordInput.EditValue = "";
            this.PassWordInput.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PassWordInput.Location = new System.Drawing.Point(210, 165);
            this.PassWordInput.Name = "PassWordInput";
            this.PassWordInput.Properties.AutoHeight = false;
            this.PassWordInput.Properties.PasswordChar = '○';
            this.PassWordInput.Size = new System.Drawing.Size(160, 21);
            this.PassWordInput.TabIndex = 1;
            // 
            // CloseButton
            // 
            this.CloseButton.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseButton.Appearance.Options.UseFont = true;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(130, 245);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(80, 23);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "取  消";
            // 
            // SetButton
            // 
            this.SetButton.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SetButton.Appearance.Options.UseFont = true;
            this.SetButton.Location = new System.Drawing.Point(220, 245);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(80, 23);
            this.SetButton.TabIndex = 4;
            this.SetButton.Text = "设  置";
            // 
            // LoginButton
            // 
            this.LoginButton.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LoginButton.Appearance.Options.UseFont = true;
            this.LoginButton.Location = new System.Drawing.Point(310, 245);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(80, 23);
            this.LoginButton.TabIndex = 3;
            this.LoginButton.Text = "登  录";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.CausesValidation = false;
            this.labelControl1.Location = new System.Drawing.Point(130, 199);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 21);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "机 构：";
            // 
            // lueDept
            // 
            this.lueDept.Location = new System.Drawing.Point(210, 200);
            this.lueDept.Name = "lueDept";
            this.lueDept.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDept.Properties.NullText = "请选择……";
            this.lueDept.Properties.PopupFormMinSize = new System.Drawing.Size(160, 80);
            this.lueDept.Properties.PopupFormSize = new System.Drawing.Size(320, 200);
            this.lueDept.Properties.TreeList = this.treeListLookUpEdit2TreeList;
            this.lueDept.Size = new System.Drawing.Size(160, 20);
            this.lueDept.TabIndex = 2;
            // 
            // treeListLookUpEdit2TreeList
            // 
            this.treeListLookUpEdit2TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.treeListLookUpEdit2TreeList.Location = new System.Drawing.Point(0, 0);
            this.treeListLookUpEdit2TreeList.Name = "treeListLookUpEdit2TreeList";
            this.treeListLookUpEdit2TreeList.OptionsView.ShowIndentAsRowStyle = true;
            this.treeListLookUpEdit2TreeList.SelectImageList = this.OrgTreeNode;
            this.treeListLookUpEdit2TreeList.Size = new System.Drawing.Size(400, 200);
            this.treeListLookUpEdit2TreeList.TabIndex = 0;
            // 
            // colName
            // 
            this.colName.Caption = "名称";
            this.colName.FieldName = "name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // OrgTreeNode
            // 
            this.OrgTreeNode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("OrgTreeNode.ImageStream")));
            this.OrgTreeNode.Images.SetKeyName(0, "NodeOrg.png");
            this.OrgTreeNode.Images.SetKeyName(1, "NodeDept.png");
            this.OrgTreeNode.Images.SetKeyName(2, "NodePost.png");
            // 
            // Login
            // 
            this.AcceptButton = this.LoginButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Zoom;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(520, 320);
            this.Controls.Add(this.lueDept);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.labUser);
            this.Controls.Add(this.SetButton);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PassWordInput);
            this.Controls.Add(this.UserNameInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insight SRM Client";
            ((System.ComponentModel.ISupportInitialize)(this.UserNameInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PassWordInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit2TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private LabelControl labUser;
        private LabelControl labPassword;
        internal TextEdit UserNameInput;
        internal TextEdit PassWordInput;
        internal SimpleButton CloseButton;
        internal SimpleButton SetButton;
        internal SimpleButton LoginButton;
        private LabelControl labelControl1;
        internal TreeListLookUpEdit lueDept;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit2TreeList;
        protected DevExpress.Utils.ImageCollection OrgTreeNode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
    }
}