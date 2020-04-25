namespace Insight.Utils.NetEaseIM.Controls
{
    partial class TimeLabel
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
            this.labTime = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // labTime
            // 
            this.labTime.Appearance.Options.UseTextOptions = true;
            this.labTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTime.Location = new System.Drawing.Point(0, 0);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(300, 20);
            this.labTime.TabIndex = 0;
            // 
            // TimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labTime);
            this.Name = "TimeControl";
            this.Size = new System.Drawing.Size(300, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labTime;
    }
}
