namespace Insight.Utils.Controls.Nim
{
    partial class ButtonLabel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labButton = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // labButton
            // 
            this.labButton.Appearance.Options.UseTextOptions = true;
            this.labButton.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labButton.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labButton.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labButton.Location = new System.Drawing.Point(0, 0);
            this.labButton.Name = "labButton";
            this.labButton.Size = new System.Drawing.Size(300, 20);
            this.labButton.TabIndex = 0;
            this.labButton.Text = "点击加载更多……";
            // 
            // ButtionLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labButton);
            this.Name = "ButtonLabel";
            this.Size = new System.Drawing.Size(300, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labButton;
    }
}
