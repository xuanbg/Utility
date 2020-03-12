using System.ComponentModel;
using DevExpress.XtraEditors;

namespace Insight.Utils.MainForm.Views
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
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.lueDept = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeListLookUpEdit2TreeList = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.loginButton = new DevExpress.XtraEditors.SimpleButton();
            this.setButton = new DevExpress.XtraEditors.SimpleButton();
            this.closeButton = new DevExpress.XtraEditors.SimpleButton();
            this.passWord = new DevExpress.XtraEditors.TextEdit();
            this.account = new DevExpress.XtraEditors.TextEdit();
            this.labLoading = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peeDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit2TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.account.Properties)).BeginInit();
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
            this.panMain.Controls.Add(this.pictureEdit2);
            this.panMain.Controls.Add(this.pictureEdit1);
            this.panMain.Controls.Add(this.lueDept);
            this.panMain.Controls.Add(this.loginButton);
            this.panMain.Controls.Add(this.setButton);
            this.panMain.Controls.Add(this.closeButton);
            this.panMain.Controls.Add(this.passWord);
            this.panMain.Controls.Add(this.account);
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
            this.peeDept.TabIndex = 6;
            this.peeDept.Visible = false;
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(30, 35);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit2.Size = new System.Drawing.Size(24, 24);
            this.pictureEdit2.TabIndex = 7;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(30, 0);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(24, 24);
            this.pictureEdit1.TabIndex = 8;
            // 
            // lueDept
            // 
            this.lueDept.Location = new System.Drawing.Point(60, 72);
            this.lueDept.Name = "lueDept";
            this.lueDept.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDept.Properties.NullText = "请选择……";
            this.lueDept.Properties.PopupFormMinSize = new System.Drawing.Size(160, 80);
            this.lueDept.Properties.PopupFormSize = new System.Drawing.Size(320, 200);
            this.lueDept.Properties.TreeList = this.treeListLookUpEdit2TreeList;
            this.lueDept.Size = new System.Drawing.Size(160, 20);
            this.lueDept.TabIndex = 0;
            this.lueDept.Visible = false;
            // 
            // treeListLookUpEdit2TreeList
            // 
            this.treeListLookUpEdit2TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.treeListLookUpEdit2TreeList.Location = new System.Drawing.Point(-70, -64);
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
            // loginButton
            // 
            this.loginButton.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginButton.Appearance.Options.UseFont = true;
            this.loginButton.Location = new System.Drawing.Point(180, 117);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(80, 23);
            this.loginButton.TabIndex = 0;
            this.loginButton.Text = "登  录";
            // 
            // setButton
            // 
            this.setButton.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.setButton.Appearance.Options.UseFont = true;
            this.setButton.Location = new System.Drawing.Point(90, 117);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(80, 23);
            this.setButton.TabIndex = 0;
            this.setButton.Text = "设  置";
            // 
            // closeButton
            // 
            this.closeButton.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.closeButton.Appearance.Options.UseFont = true;
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(0, 117);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(80, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "取  消";
            // 
            // passWord
            // 
            this.passWord.EditValue = "";
            this.passWord.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.passWord.Location = new System.Drawing.Point(60, 37);
            this.passWord.Name = "passWord";
            this.passWord.Properties.AutoHeight = false;
            this.passWord.Properties.PasswordChar = '○';
            this.passWord.Size = new System.Drawing.Size(160, 21);
            this.passWord.TabIndex = 2;
            // 
            // account
            // 
            this.account.EnterMoveNextControl = true;
            this.account.Location = new System.Drawing.Point(60, 2);
            this.account.Name = "account";
            this.account.Properties.AutoHeight = false;
            this.account.Size = new System.Drawing.Size(160, 21);
            this.account.TabIndex = 1;
            // 
            // labLoading
            // 
            this.labLoading.Appearance.ForeColor = System.Drawing.Color.White;
            this.labLoading.Appearance.Options.UseForeColor = true;
            this.labLoading.Location = new System.Drawing.Point(183, 153);
            this.labLoading.Name = "labLoading";
            this.labLoading.Size = new System.Drawing.Size(154, 14);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit2TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.account.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected DevExpress.Utils.ImageCollection OrgTreeNode;
        public LabelControl labLoading;
        public PictureEdit peeDept;
        private PictureEdit pictureEdit2;
        private PictureEdit pictureEdit1;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit2TreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        public PanelControl panMain;
        public TreeListLookUpEdit lueDept;
        public SimpleButton loginButton;
        public SimpleButton setButton;
        public SimpleButton closeButton;
        public TextEdit passWord;
        public TextEdit account;
    }
}