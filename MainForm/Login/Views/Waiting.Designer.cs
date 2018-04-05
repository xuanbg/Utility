using System.ComponentModel;
using DevExpress.XtraEditors;

namespace Insight.Utils.MainForm.Login.Views
{
    public partial class Waiting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Waiting));
            this.labLoading = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // labLoading
            // 
            this.labLoading.Location = new System.Drawing.Point(190, 150);
            this.labLoading.Name = "labLoading";
            this.labLoading.Size = new System.Drawing.Size(154, 14);
            this.labLoading.TabIndex = 0;
            this.labLoading.Text = "正在加载应用程序，请稍候…";
            // 
            // Waiting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(520, 320);
            this.Controls.Add(this.labLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Waiting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelControl labLoading;
    }
}