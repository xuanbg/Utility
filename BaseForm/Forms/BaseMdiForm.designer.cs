using System.ComponentModel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Insight.Base.BaseForm.Forms
{
    partial class BaseMdiForm
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
            this.Wait = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Insight.Base.BaseForm.Views.Waiting), true, true);
            this.xtraScrollable = new DevExpress.XtraEditors.XtraScrollableControl();
            this.SuspendLayout();
            // 
            // Wait
            // 
            this.Wait.ClosingDelay = 500;
            // 
            // xtraScrollable
            // 
            this.xtraScrollable.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraScrollable.Appearance.Options.UseBackColor = true;
            this.xtraScrollable.AutoScrollMinSize = new System.Drawing.Size(1476, 876);
            this.xtraScrollable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollable.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollable.Name = "xtraScrollable";
            this.xtraScrollable.Size = new System.Drawing.Size(1420, 860);
            this.xtraScrollable.TabIndex = 0;
            // 
            // BaseMdiForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1420, 860);
            this.Controls.Add(this.xtraScrollable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BaseMdiForm";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

        #endregion
        protected XtraScrollableControl xtraScrollable;
        internal DevExpress.XtraSplashScreen.SplashScreenManager Wait;
    }
}