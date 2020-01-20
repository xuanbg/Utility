namespace Insight.Utils.MainForm.Views
{
    public partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.picAbout = new DevExpress.XtraEditors.PictureEdit();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labVersion = new DevExpress.XtraEditors.LabelControl();
            this.labDevelopers = new DevExpress.XtraEditors.LabelControl();
            this.labDev = new DevExpress.XtraEditors.LabelControl();
            this.labVer = new DevExpress.XtraEditors.LabelControl();
            this.labProduct = new DevExpress.XtraEditors.LabelControl();
            this.labProductName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FolderNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAbout.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.labVersion);
            this.panel.Controls.Add(this.labDevelopers);
            this.panel.Controls.Add(this.labDev);
            this.panel.Controls.Add(this.labVer);
            this.panel.Controls.Add(this.labProduct);
            this.panel.Controls.Add(this.labProductName);
            this.panel.Controls.Add(this.txtDescription);
            this.panel.Location = new System.Drawing.Point(177, 7);
            this.panel.Size = new System.Drawing.Size(300, 250);
            // 
            // Cancel
            // 
            this.cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cancel.Appearance.Options.UseFont = true;
            this.cancel.Location = new System.Drawing.Point(300, 275);
            this.cancel.Visible = false;
            // 
            // Confirm
            // 
            this.confirm.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.Appearance.Options.UseFont = true;
            this.confirm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.confirm.Location = new System.Drawing.Point(390, 275);
            this.confirm.TabIndex = 0;
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
            // Close
            // 
            this.close.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.close.Appearance.Options.UseFont = true;
            this.close.Location = new System.Drawing.Point(390, 275);
            // 
            // picAbout
            // 
            this.picAbout.EditValue = ((object)(resources.GetObject("picAbout.EditValue")));
            this.picAbout.Location = new System.Drawing.Point(7, 7);
            this.picAbout.Name = "picAbout";
            this.picAbout.Size = new System.Drawing.Size(164, 250);
            this.picAbout.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.EnterMoveNextControl = true;
            this.txtDescription.Location = new System.Drawing.Point(10, 100);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(280, 140);
            this.txtDescription.TabIndex = 1;
            // 
            // labVersion
            // 
            this.labVersion.Appearance.Options.UseTextOptions = true;
            this.labVersion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labVersion.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labVersion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labVersion.Location = new System.Drawing.Point(10, 42);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(70, 21);
            this.labVersion.TabIndex = 0;
            this.labVersion.Text = "版本号：";
            // 
            // labDevelopers
            // 
            this.labDevelopers.Appearance.Options.UseTextOptions = true;
            this.labDevelopers.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labDevelopers.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labDevelopers.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labDevelopers.Location = new System.Drawing.Point(10, 69);
            this.labDevelopers.Name = "labDevelopers";
            this.labDevelopers.Size = new System.Drawing.Size(70, 21);
            this.labDevelopers.TabIndex = 0;
            this.labDevelopers.Text = "开发商：";
            // 
            // labDev
            // 
            this.labDev.Appearance.Options.UseTextOptions = true;
            this.labDev.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labDev.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labDev.Location = new System.Drawing.Point(80, 69);
            this.labDev.Name = "labDev";
            this.labDev.Size = new System.Drawing.Size(210, 21);
            this.labDev.TabIndex = 0;
            // 
            // labVer
            // 
            this.labVer.Appearance.Options.UseTextOptions = true;
            this.labVer.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labVer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labVer.Location = new System.Drawing.Point(80, 42);
            this.labVer.Name = "labVer";
            this.labVer.Size = new System.Drawing.Size(210, 21);
            this.labVer.TabIndex = 0;
            // 
            // labProduct
            // 
            this.labProduct.Appearance.Options.UseTextOptions = true;
            this.labProduct.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labProduct.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labProduct.Location = new System.Drawing.Point(80, 15);
            this.labProduct.Name = "labProduct";
            this.labProduct.Size = new System.Drawing.Size(210, 21);
            this.labProduct.TabIndex = 0;
            // 
            // labProductName
            // 
            this.labProductName.Appearance.Options.UseTextOptions = true;
            this.labProductName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labProductName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labProductName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labProductName.Location = new System.Drawing.Point(10, 15);
            this.labProductName.Name = "labProductName";
            this.labProductName.Size = new System.Drawing.Size(70, 21);
            this.labProductName.TabIndex = 0;
            this.labProductName.Text = "产品名称：";
            // 
            // About
            // 
            this.ClientSize = new System.Drawing.Size(484, 312);
            this.ControlBox = false;
            this.Controls.Add(this.picAbout);
            this.Name = "About";
            this.Text = "关于…";
            this.Controls.SetChildIndex(this.confirm, 0);
            this.Controls.SetChildIndex(this.close, 0);
            this.Controls.SetChildIndex(this.picAbout, 0);
            this.Controls.SetChildIndex(this.panel, 0);
            this.Controls.SetChildIndex(this.cancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FolderNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAbout.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit picAbout;
        internal DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl labVersion;
        private DevExpress.XtraEditors.LabelControl labDevelopers;
        internal DevExpress.XtraEditors.LabelControl labDev;
        internal DevExpress.XtraEditors.LabelControl labVer;
        internal DevExpress.XtraEditors.LabelControl labProduct;
        private DevExpress.XtraEditors.LabelControl labProductName;
    }
}