namespace Insight.Base.MainForm.Views
{
    public partial class MainWindow
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.meuMain = new DevExpress.XtraBars.Bar();
            this.bmuUser = new DevExpress.XtraBars.BarSubItem();
            this.bmiChangPassWord = new DevExpress.XtraBars.BarButtonItem();
            this.bmiLock = new DevExpress.XtraBars.BarButtonItem();
            this.bmiLogout = new DevExpress.XtraBars.BarButtonItem();
            this.bmiExit = new DevExpress.XtraBars.BarButtonItem();
            this.bmuSet = new DevExpress.XtraBars.BarSubItem();
            this.bmiPrintSet = new DevExpress.XtraBars.BarButtonItem();
            this.bmiSkin = new DevExpress.XtraBars.SkinBarSubItem();
            this.mdiList = new DevExpress.XtraBars.BarMdiChildrenListItem();
            this.bmuHelp = new DevExpress.XtraBars.BarSubItem();
            this.bmiHelp = new DevExpress.XtraBars.BarButtonItem();
            this.bmiUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.bmiAbout = new DevExpress.XtraBars.BarButtonItem();
            this.barStatus = new DevExpress.XtraBars.Bar();
            this.StbTime = new DevExpress.XtraBars.BarButtonItem();
            this.StbDept = new DevExpress.XtraBars.BarButtonItem();
            this.StbUser = new DevExpress.XtraBars.BarButtonItem();
            this.StbMessger = new DevExpress.XtraBars.BarButtonItem();
            this.StbThing = new DevExpress.XtraBars.BarButtonItem();
            this.StbNotice = new DevExpress.XtraBars.BarButtonItem();
            this.StbServer = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.NavMain = new DevExpress.XtraNavBar.NavBarControl();
            this.mdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.splitterControl = new DevExpress.XtraEditors.SplitterControl();
            this.myFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mdiManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowItemAnimatedHighlighting = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.meuMain,
            this.barStatus});
            this.barManager.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("StatusBar", new System.Guid("bb5179f7-aee9-4a1f-8fc5-39d222e973a5")),
            new DevExpress.XtraBars.BarManagerCategory("MainMenu", new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc"))});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bmuUser,
            this.bmuSet,
            this.mdiList,
            this.bmuHelp,
            this.bmiChangPassWord,
            this.bmiLock,
            this.bmiLogout,
            this.bmiExit,
            this.bmiPrintSet,
            this.bmiSkin,
            this.bmiHelp,
            this.bmiUpdate,
            this.bmiAbout,
            this.StbTime,
            this.StbDept,
            this.StbUser,
            this.StbMessger,
            this.StbNotice,
            this.StbThing,
            this.StbServer});
            this.barManager.MainMenu = this.meuMain;
            this.barManager.MaxItemId = 55;
            this.barManager.StatusBar = this.barStatus;
            // 
            // meuMain
            // 
            this.meuMain.BarName = "Main menu";
            this.meuMain.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.meuMain.DockCol = 0;
            this.meuMain.DockRow = 0;
            this.meuMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.meuMain.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bmuUser),
            new DevExpress.XtraBars.LinkPersistInfo(this.bmuSet),
            new DevExpress.XtraBars.LinkPersistInfo(this.mdiList),
            new DevExpress.XtraBars.LinkPersistInfo(this.bmuHelp)});
            this.meuMain.OptionsBar.UseWholeRow = true;
            this.meuMain.Text = "Main menu";
            // 
            // bmuUser
            // 
            this.bmuUser.Caption = "用户(&U)";
            this.bmuUser.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmuUser.Id = 0;
            this.bmuUser.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bmiChangPassWord, "", false, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bmiLock, "", true, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bmiLogout, "", false, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bmiExit, "", true, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard)});
            this.bmuUser.Name = "bmuUser";
            this.bmuUser.ShortcutKeyDisplayString = "U";
            // 
            // bmiChangPassWord
            // 
            this.bmiChangPassWord.Caption = "更换密码";
            this.bmiChangPassWord.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmiChangPassWord.Id = 8;
            this.bmiChangPassWord.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("MubChangPassWord.ImageOptions.Image")));
            this.bmiChangPassWord.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("MubChangPassWord.ImageOptions.LargeImage")));
            this.bmiChangPassWord.Name = "bmiChangPassWord";
            // 
            // bmiLock
            // 
            this.bmiLock.Caption = "锁定";
            this.bmiLock.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmiLock.Id = 9;
            this.bmiLock.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("MubLock.ImageOptions.Image")));
            this.bmiLock.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
            this.bmiLock.Name = "bmiLock";
            this.bmiLock.ShortcutKeyDisplayString = "Ctrl+O";
            // 
            // bmiLogout
            // 
            this.bmiLogout.Caption = "注销";
            this.bmiLogout.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmiLogout.Id = 10;
            this.bmiLogout.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("MubLogout.ImageOptions.Image")));
            this.bmiLogout.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("MubLogout.ImageOptions.LargeImage")));
            this.bmiLogout.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L));
            this.bmiLogout.Name = "bmiLogout";
            this.bmiLogout.ShortcutKeyDisplayString = "Ctrl+L";
            // 
            // bmiExit
            // 
            this.bmiExit.Caption = "退出";
            this.bmiExit.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmiExit.Id = 11;
            this.bmiExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("MubExit.ImageOptions.Image")));
            this.bmiExit.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4));
            this.bmiExit.Name = "bmiExit";
            this.bmiExit.ShortcutKeyDisplayString = "Alt+F4";
            // 
            // bmuSet
            // 
            this.bmuSet.Caption = "设置(&S)";
            this.bmuSet.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmuSet.Id = 3;
            this.bmuSet.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bmiPrintSet),
            new DevExpress.XtraBars.LinkPersistInfo(this.bmiSkin, true)});
            this.bmuSet.Name = "bmuSet";
            this.bmuSet.ShortcutKeyDisplayString = "S";
            // 
            // bmiPrintSet
            // 
            this.bmiPrintSet.Caption = "设置打印机";
            this.bmiPrintSet.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmiPrintSet.Id = 14;
            this.bmiPrintSet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("MubPrintSet.ImageOptions.Image")));
            this.bmiPrintSet.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("MubPrintSet.ImageOptions.LargeImage")));
            this.bmiPrintSet.Name = "bmiPrintSet";
            // 
            // bmiSkin
            // 
            this.bmiSkin.Caption = "更换皮肤";
            this.bmiSkin.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmiSkin.Id = 37;
            this.bmiSkin.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mubSkin.ImageOptions.Image")));
            this.bmiSkin.Name = "bmiSkin";
            // 
            // mdiList
            // 
            this.mdiList.Caption = "窗口(&W)";
            this.mdiList.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.mdiList.Id = 34;
            this.mdiList.Name = "mdiList";
            this.mdiList.ShortcutKeyDisplayString = "W";
            // 
            // bmuHelp
            // 
            this.bmuHelp.Caption = "帮助(&H)";
            this.bmuHelp.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmuHelp.Id = 5;
            this.bmuHelp.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bmiHelp),
            new DevExpress.XtraBars.LinkPersistInfo(this.bmiUpdate, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bmiAbout)});
            this.bmuHelp.Name = "bmuHelp";
            this.bmuHelp.ShortcutKeyDisplayString = "H";
            // 
            // bmiHelp
            // 
            this.bmiHelp.Caption = "查看帮助";
            this.bmiHelp.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmiHelp.Id = 12;
            this.bmiHelp.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("MubHelp.ImageOptions.Image")));
            this.bmiHelp.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("MubHelp.ImageOptions.LargeImage")));
            this.bmiHelp.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H));
            this.bmiHelp.Name = "bmiHelp";
            this.bmiHelp.ShortcutKeyDisplayString = "Ctrl+H";
            // 
            // bmiUpdate
            // 
            this.bmiUpdate.Caption = "检查更新";
            this.bmiUpdate.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmiUpdate.Id = 53;
            this.bmiUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("MubUpdate.ImageOptions.Image")));
            this.bmiUpdate.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("MubUpdate.ImageOptions.LargeImage")));
            this.bmiUpdate.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U));
            this.bmiUpdate.Name = "bmiUpdate";
            this.bmiUpdate.ShortcutKeyDisplayString = "Ctrl+U";
            // 
            // bmiAbout
            // 
            this.bmiAbout.Caption = "关于";
            this.bmiAbout.CategoryGuid = new System.Guid("ba703b74-a3ae-461f-94c8-943ed9da4cdc");
            this.bmiAbout.Id = 13;
            this.bmiAbout.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("MubAbout.ImageOptions.Image")));
            this.bmiAbout.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("MubAbout.ImageOptions.LargeImage")));
            this.bmiAbout.Name = "bmiAbout";
            // 
            // barStatus
            // 
            this.barStatus.BarName = "Status bar";
            this.barStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barStatus.DockCol = 0;
            this.barStatus.DockRow = 0;
            this.barStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barStatus.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.StbTime, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.StbDept, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.StbUser, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.StbMessger, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.StbThing),
            new DevExpress.XtraBars.LinkPersistInfo(this.StbNotice),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.StbServer, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barStatus.OptionsBar.AllowQuickCustomization = false;
            this.barStatus.OptionsBar.DrawDragBorder = false;
            this.barStatus.OptionsBar.UseWholeRow = true;
            this.barStatus.Text = "Status bar";
            // 
            // StbTime
            // 
            this.StbTime.CategoryGuid = new System.Guid("bb5179f7-aee9-4a1f-8fc5-39d222e973a5");
            this.StbTime.Id = 42;
            this.StbTime.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StbTime.ImageOptions.Image")));
            this.StbTime.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("StbTime.ImageOptions.LargeImage")));
            this.StbTime.Name = "StbTime";
            // 
            // StbDept
            // 
            this.StbDept.CategoryGuid = new System.Guid("bb5179f7-aee9-4a1f-8fc5-39d222e973a5");
            this.StbDept.Id = 43;
            this.StbDept.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StbDept.ImageOptions.Image")));
            this.StbDept.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("StbDept.ImageOptions.LargeImage")));
            this.StbDept.Name = "StbDept";
            // 
            // StbUser
            // 
            this.StbUser.CategoryGuid = new System.Guid("bb5179f7-aee9-4a1f-8fc5-39d222e973a5");
            this.StbUser.Id = 44;
            this.StbUser.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StbUser.ImageOptions.Image")));
            this.StbUser.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("StbUser.ImageOptions.LargeImage")));
            this.StbUser.Name = "StbUser";
            // 
            // StbMessger
            // 
            this.StbMessger.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StbMessger.CategoryGuid = new System.Guid("bb5179f7-aee9-4a1f-8fc5-39d222e973a5");
            this.StbMessger.Enabled = false;
            this.StbMessger.Id = 51;
            this.StbMessger.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StbMessger.ImageOptions.Image")));
            this.StbMessger.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("StbMessger.ImageOptions.LargeImage")));
            this.StbMessger.Name = "StbMessger";
            // 
            // StbThing
            // 
            this.StbThing.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StbThing.CategoryGuid = new System.Guid("bb5179f7-aee9-4a1f-8fc5-39d222e973a5");
            this.StbThing.Enabled = false;
            this.StbThing.Id = 45;
            this.StbThing.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StbThing.ImageOptions.Image")));
            this.StbThing.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("StbThing.ImageOptions.LargeImage")));
            this.StbThing.Name = "StbThing";
            // 
            // StbNotice
            // 
            this.StbNotice.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StbNotice.CategoryGuid = new System.Guid("bb5179f7-aee9-4a1f-8fc5-39d222e973a5");
            this.StbNotice.Enabled = false;
            this.StbNotice.Id = 39;
            this.StbNotice.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StbNotice.ImageOptions.Image")));
            this.StbNotice.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("StbNotice.ImageOptions.LargeImage")));
            this.StbNotice.Name = "StbNotice";
            // 
            // StbServer
            // 
            this.StbServer.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StbServer.CategoryGuid = new System.Guid("bb5179f7-aee9-4a1f-8fc5-39d222e973a5");
            this.StbServer.Id = 40;
            this.StbServer.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StbServer.ImageOptions.Image")));
            this.StbServer.Name = "StbServer";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(1584, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 934);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(1584, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 910);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1584, 24);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 910);
            // 
            // NavMain
            // 
            this.NavMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.NavMain.Location = new System.Drawing.Point(0, 24);
            this.NavMain.Name = "NavMain";
            this.NavMain.OptionsNavPane.ExpandedWidth = 160;
            this.NavMain.Size = new System.Drawing.Size(160, 910);
            this.NavMain.StoreDefaultPaintStyleName = true;
            this.NavMain.TabIndex = 1;
            // 
            // mdiManager
            // 
            this.mdiManager.MdiParent = this;
            // 
            // splitterControl
            // 
            this.splitterControl.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.splitterControl.Appearance.Options.UseBackColor = true;
            this.splitterControl.Location = new System.Drawing.Point(160, 24);
            this.splitterControl.MinExtra = 880;
            this.splitterControl.MinSize = 120;
            this.splitterControl.Name = "splitterControl";
            this.splitterControl.Size = new System.Drawing.Size(5, 910);
            this.splitterControl.TabIndex = 0;
            this.splitterControl.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1584, 961);
            this.Controls.Add(this.splitterControl);
            this.Controls.Add(this.NavMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mdiManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DevExpress.LookAndFeel.DefaultLookAndFeel myFeel;
        internal DevExpress.XtraNavBar.NavBarControl NavMain;
        internal DevExpress.XtraBars.BarButtonItem bmiChangPassWord;
        internal DevExpress.XtraBars.BarButtonItem bmiLock;
        internal DevExpress.XtraBars.BarButtonItem bmiLogout;
        internal DevExpress.XtraBars.BarButtonItem bmiExit;
        internal DevExpress.XtraBars.BarButtonItem bmiPrintSet;
        internal DevExpress.XtraBars.BarButtonItem bmiHelp;
        internal DevExpress.XtraBars.BarButtonItem bmiUpdate;
        internal DevExpress.XtraBars.BarButtonItem bmiAbout;
        internal DevExpress.XtraBars.BarButtonItem StbTime;
        internal DevExpress.XtraBars.BarButtonItem StbDept;
        internal DevExpress.XtraBars.BarButtonItem StbUser;
        internal DevExpress.XtraBars.BarButtonItem StbMessger;
        internal DevExpress.XtraBars.BarButtonItem StbThing;
        internal DevExpress.XtraBars.BarButtonItem StbNotice;
        internal DevExpress.XtraBars.BarButtonItem StbServer;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar meuMain;
        private DevExpress.XtraBars.Bar barStatus;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager mdiManager;
        private DevExpress.XtraBars.BarSubItem bmuUser;
        private DevExpress.XtraBars.BarSubItem bmuSet;
        private DevExpress.XtraBars.BarSubItem bmuHelp;
        private DevExpress.XtraBars.SkinBarSubItem bmiSkin;
        private DevExpress.XtraBars.BarMdiChildrenListItem mdiList;
        private DevExpress.XtraEditors.SplitterControl splitterControl;
    }
}

