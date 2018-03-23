namespace Insight.Utils.MainForm.Views
{
    public partial class Locked
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Locked));
            this.Password = new DevExpress.XtraEditors.TextEdit();
            this.labUnlockPw = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FolderNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Password.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.Password);
            this.panel.Controls.Add(this.labUnlockPw);
            // 
            // FolderNode
            // 
            this.FolderNode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("FolderNode.ImageStream")));
            this.FolderNode.Images.SetKeyName(0, "Item.png");
            this.FolderNode.Images.SetKeyName(1, "Folder.png");
            this.FolderNode.Images.SetKeyName(2, "FolderOpen.png");
            // 
            // CategoryNode
            // 
            this.CategoryNode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("CategoryNode.ImageStream")));
            this.CategoryNode.Images.SetKeyName(0, "Doc.png");
            this.CategoryNode.Images.SetKeyName(1, "Folder.png");
            this.CategoryNode.Images.SetKeyName(2, "FolderOpen.png");
            // 
            // OrgTreeNode
            // 
            this.OrgTreeNode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("OrgTreeNode.ImageStream")));
            this.OrgTreeNode.Images.SetKeyName(0, "NodeOrg.png");
            this.OrgTreeNode.Images.SetKeyName(1, "NodeDept.png");
            this.OrgTreeNode.Images.SetKeyName(2, "NodePost.png");
            // 
            // Cancel
            // 
            this.Cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cancel.Appearance.Options.UseFont = true;
            this.Cancel.Visible = false;
            // 
            // Confirm
            // 
            this.Confirm.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Confirm.Appearance.Options.UseFont = true;
            this.Confirm.Text = "解  锁";
            // 
            // Password
            // 
            this.Password.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Password.Location = new System.Drawing.Point(145, 65);
            this.Password.Name = "Password";
            this.Password.Properties.PasswordChar = '○';
            this.Password.Size = new System.Drawing.Size(160, 20);
            this.Password.TabIndex = 2;
            // 
            // labUnlockPw
            // 
            this.labUnlockPw.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labUnlockPw.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labUnlockPw.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labUnlockPw.Location = new System.Drawing.Point(65, 65);
            this.labUnlockPw.Name = "labUnlockPw";
            this.labUnlockPw.Size = new System.Drawing.Size(80, 21);
            this.labUnlockPw.TabIndex = 3;
            this.labUnlockPw.Text = "输入密码：";
            // 
            // Locked
            // 
            this.ClientSize = new System.Drawing.Size(384, 212);
            this.ControlBox = false;
            this.Name = "Locked";
            this.Text = "锁定";
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FolderNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Password.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.TextEdit Password;
        private DevExpress.XtraEditors.LabelControl labUnlockPw;
    }
}