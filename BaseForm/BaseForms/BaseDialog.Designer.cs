using System.ComponentModel;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace Insight.Utils.BaseForm
{
    partial class BaseDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseDialog));
            this.panel = new DevExpress.XtraEditors.PanelControl();
            this.Confirm = new DevExpress.XtraEditors.SimpleButton();
            this.Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.FolderNode = new DevExpress.Utils.ImageCollection(this.components);
            this.CategoryNode = new DevExpress.Utils.ImageCollection(this.components);
            this.OrgTreeNode = new DevExpress.Utils.ImageCollection(this.components);
            this.Close = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FolderNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Location = new System.Drawing.Point(7, 7);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(370, 150);
            this.panel.TabIndex = 0;
            // 
            // Confirm
            // 
            this.Confirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Confirm.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Confirm.Appearance.Options.UseFont = true;
            this.Confirm.Location = new System.Drawing.Point(290, 174);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(80, 23);
            this.Confirm.TabIndex = 101;
            this.Confirm.Text = "确  定";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cancel.Appearance.Options.UseFont = true;
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(200, 174);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(80, 23);
            this.Cancel.TabIndex = 102;
            this.Cancel.Text = "取  消";
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
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Close.Appearance.Options.UseFont = true;
            this.Close.Location = new System.Drawing.Point(290, 174);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(80, 23);
            this.Close.TabIndex = 103;
            this.Close.Text = "关  闭";
            // 
            // BaseDialog
            // 
            this.AcceptButton = this.Confirm;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(384, 212);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.Close);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FolderNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected PanelControl panel;
        public SimpleButton Cancel;
        public SimpleButton Confirm;
        protected ImageCollection FolderNode;
        protected ImageCollection CategoryNode;
        protected ImageCollection OrgTreeNode;
        public SimpleButton Close;
    }
}