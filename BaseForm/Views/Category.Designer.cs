using System.ComponentModel;

namespace Insight.Utils.Views
{
    partial class Category
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Category));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labParent = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.labMemo = new DevExpress.XtraEditors.LabelControl();
            this.trlParent = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treParent = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.labIndex = new DevExpress.XtraEditors.LabelControl();
            this.spiIndex = new DevExpress.XtraEditors.SpinEdit();
            this.memRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labAlias = new DevExpress.XtraEditors.LabelControl();
            this.txtAlias = new DevExpress.XtraEditors.TextEdit();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FolderNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trlParent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spiIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAlias.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.memRemark);
            this.panel.Controls.Add(this.spiIndex);
            this.panel.Controls.Add(this.trlParent);
            this.panel.Controls.Add(this.txtCode);
            this.panel.Controls.Add(this.txtAlias);
            this.panel.Controls.Add(this.txtName);
            this.panel.Controls.Add(this.labCode);
            this.panel.Controls.Add(this.labIndex);
            this.panel.Controls.Add(this.labAlias);
            this.panel.Controls.Add(this.labMemo);
            this.panel.Controls.Add(this.labName);
            this.panel.Controls.Add(this.labParent);
            this.panel.Size = new System.Drawing.Size(370, 200);
            // 
            // Cancel
            // 
            this.cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cancel.Appearance.Options.UseFont = true;
            this.cancel.Location = new System.Drawing.Point(200, 224);
            // 
            // Confirm
            // 
            this.confirm.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.Appearance.Options.UseFont = true;
            this.confirm.Location = new System.Drawing.Point(290, 224);
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
            // txtName
            // 
            this.txtName.EnterMoveNextControl = true;
            this.txtName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtName.Location = new System.Drawing.Point(80, 55);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(140, 20);
            this.txtName.TabIndex = 1;
            // 
            // labParent
            // 
            this.labParent.Appearance.Options.UseTextOptions = true;
            this.labParent.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labParent.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labParent.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labParent.Location = new System.Drawing.Point(0, 20);
            this.labParent.Name = "labParent";
            this.labParent.Size = new System.Drawing.Size(80, 21);
            this.labParent.TabIndex = 0;
            this.labParent.Text = "父分类：";
            // 
            // labName
            // 
            this.labName.Appearance.Options.UseTextOptions = true;
            this.labName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labName.Location = new System.Drawing.Point(0, 55);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(80, 21);
            this.labName.TabIndex = 0;
            this.labName.Text = "分类名称：";
            // 
            // labMemo
            // 
            this.labMemo.Appearance.Options.UseTextOptions = true;
            this.labMemo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labMemo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labMemo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labMemo.Location = new System.Drawing.Point(0, 120);
            this.labMemo.Name = "labMemo";
            this.labMemo.Size = new System.Drawing.Size(80, 21);
            this.labMemo.TabIndex = 0;
            this.labMemo.Text = "备注：";
            // 
            // trlParent
            // 
            this.trlParent.Location = new System.Drawing.Point(80, 20);
            this.trlParent.Name = "trlParent";
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.trlParent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.trlParent.Properties.NullText = "请选择…";
            this.trlParent.Properties.PopupFormSize = new System.Drawing.Size(230, 200);
            this.trlParent.Properties.TreeList = this.treParent;
            this.trlParent.Size = new System.Drawing.Size(270, 22);
            this.trlParent.TabIndex = 4;
            // 
            // treParent
            // 
            this.treParent.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.treParent.Location = new System.Drawing.Point(0, 0);
            this.treParent.Name = "treParent";
            this.treParent.OptionsView.ShowIndentAsRowStyle = true;
            this.treParent.SelectImageList = this.FolderNode;
            this.treParent.Size = new System.Drawing.Size(400, 200);
            this.treParent.TabIndex = 0;
            // 
            // colName
            // 
            this.colName.Caption = "名称";
            this.colName.FieldName = "name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // labIndex
            // 
            this.labIndex.Appearance.Options.UseTextOptions = true;
            this.labIndex.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labIndex.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labIndex.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labIndex.Location = new System.Drawing.Point(220, 55);
            this.labIndex.Name = "labIndex";
            this.labIndex.Size = new System.Drawing.Size(60, 21);
            this.labIndex.TabIndex = 0;
            this.labIndex.Text = "序号：";
            // 
            // spiIndex
            // 
            this.spiIndex.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spiIndex.Location = new System.Drawing.Point(280, 55);
            this.spiIndex.Name = "spiIndex";
            this.spiIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spiIndex.Properties.IsFloatValue = false;
            this.spiIndex.Properties.Mask.EditMask = "N00";
            this.spiIndex.Size = new System.Drawing.Size(70, 20);
            this.spiIndex.TabIndex = 6;
            // 
            // memRemark
            // 
            this.memRemark.ImeMode = System.Windows.Forms.ImeMode.On;
            this.memRemark.Location = new System.Drawing.Point(80, 120);
            this.memRemark.Name = "memRemark";
            this.memRemark.Properties.NullText = "请输入备注信息…";
            this.memRemark.Size = new System.Drawing.Size(270, 60);
            this.memRemark.TabIndex = 7;
            // 
            // labAlias
            // 
            this.labAlias.Appearance.Options.UseTextOptions = true;
            this.labAlias.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labAlias.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labAlias.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labAlias.Location = new System.Drawing.Point(0, 85);
            this.labAlias.Name = "labAlias";
            this.labAlias.Size = new System.Drawing.Size(80, 21);
            this.labAlias.TabIndex = 0;
            this.labAlias.Text = "简称：";
            // 
            // txtAlias
            // 
            this.txtAlias.EnterMoveNextControl = true;
            this.txtAlias.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAlias.Location = new System.Drawing.Point(80, 85);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(140, 20);
            this.txtAlias.TabIndex = 2;
            // 
            // labCode
            // 
            this.labCode.Appearance.Options.UseTextOptions = true;
            this.labCode.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labCode.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labCode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labCode.Location = new System.Drawing.Point(220, 85);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(60, 21);
            this.labCode.TabIndex = 0;
            this.labCode.Text = "编码：";
            // 
            // txtCode
            // 
            this.txtCode.EditValue = "";
            this.txtCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtCode.Location = new System.Drawing.Point(280, 85);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(70, 20);
            this.txtCode.TabIndex = 3;
            // 
            // Category
            // 
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.Name = "Category";
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FolderNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgTreeNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trlParent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spiIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAlias.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        internal DevExpress.XtraEditors.TreeListLookUpEdit trlParent;
        internal DevExpress.XtraTreeList.TreeList treParent;
        internal DevExpress.XtraEditors.TextEdit txtName;
        internal DevExpress.XtraEditors.SpinEdit spiIndex;
        internal DevExpress.XtraEditors.TextEdit txtAlias;
        internal DevExpress.XtraEditors.TextEdit txtCode;
        internal DevExpress.XtraEditors.MemoEdit memRemark;
        private DevExpress.XtraEditors.LabelControl labParent;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.LabelControl labIndex;
        private DevExpress.XtraEditors.LabelControl labAlias;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labMemo;

        #endregion

        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
    }
}