using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Insight.Utils.BaseControllers;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.ViewModels;

namespace Insight.Utils.MainForm
{
    public class Controller : BaseController
    {
        private readonly DataModel dataModel = new DataModel();
        private readonly MainModel mainModel;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Controller()
        {
            update(true);
            login();

            var navigators = dataModel.getNavigators();
            mainModel = new MainModel(Setting.appName) { navigators = navigators };
            mainModel.callbackEvent += (sender, args) => buttonClick(args.methodName, args.param);
        }

        /// <summary>
        /// 打开登录对话框
        /// </summary>
        public void login()
        {
            var model = new LoginModel(Setting.appName);
            model.callbackEvent += (sender, args) =>
            {
                switch (args.methodName)
                {
                    case "loadDept":
                        var account = (string)args.param[0];
                        if (string.IsNullOrEmpty(account)) return;

                        var list = dataModel.getDepts(account);
                        if (!list.Any()) return;

                        model.initDepts(list);

                        break;
                    case "setServerIp":
                        var setModel = new SetModel("服务器设置");
                        setModel.showDialog();

                        break;
                    case "loadMainWindow":
                        model.hide();
                        mainModel.showMainWindow();
                        model.close();
                        if (Setting.needChangePw) changPassword("123456");

                        break;
                    default:
                        Application.Exit();
                        break;
                }
            };

            model.showDialog();
        }

        /// <summary>
        /// 打开MDI子窗体
        /// </summary>
        /// <param name="name">c窗体名称</param>
        public void openMdiWindow(string name)
        {
            if (existForm(name)) return;

            var mod = mainModel.navigators.Single(m => m.moduleInfo.module == name);
            var path = $"{Application.StartupPath}\\{mod.moduleInfo.file}";
            if (!File.Exists(path))
            {
                var msg = $"对不起，{mod.name}模块无法加载！\r\n未能发现{path}文件。";
                Messages.showError(msg);
                return;
            }

            var asm = Assembly.LoadFrom(path);
            var type = asm.GetTypes().SingleOrDefault(i => i.FullName != null && i.FullName.EndsWith($"{mod.moduleInfo.module}.Controller"));
            if (type == null || string.IsNullOrEmpty(type.FullName))
            {
                var msg = $"对不起，{mod.name}模块无法加载！\r\n您的应用程序中缺少相应组件。";
                Messages.showError(msg);

                return;
            }

            asm.CreateInstance(type.FullName, false, BindingFlags.Default, null, new object[] { mod }, CultureInfo.CurrentCulture, null);
        }

        /// <summary>
        /// 点击菜单项：修改密码，弹出修改密码对话框
        /// </summary>
        /// <param name="password">原密码</param>
        public void changPassword(string password)
        {
            var model = new ChangPwModel(password, "修改密码");
            model.callbackEvent += (sender, args) =>
            {
                if (!dataModel.changPassword(model.item)) return;

                Setting.tokenHelper.signature(model.item.password);
                Messages.showMessage("更换密码成功！请牢记新密码并使用新密码登录系统。");

                model.closeDialog();
            };

            model.showDialog();
        }

        /// <summary>
        /// 点击菜单项：锁定，弹出解锁对话框
        /// </summary>
        public void lockWindow()
        {
            var model = new LockModel("解除屏幕锁定");
            model.showDialog();
        }

        /// <summary>
        /// 点击菜单项：打印机设置，打开打印机设置对话框
        /// </summary>
        public void printSet()
        {
            var model = new PrintModel("设置打印机");
            model.showDialog();
        }

        /// <summary>
        /// 点击菜单项：检查更新，如有更新，提示是否更新
        /// </summary>
        /// <param name="isStart">是否启动</param>
        public void update(bool isStart)
        {
            var info = dataModel.checkUpdate();
            if (info == null) return;

            if (isStart && !info.update) return;

            if (!info.data.Any())
            {
                if (isStart) return;

                Messages.showMessage("您的系统是最新版本！");
                return;
            }

            var model = new UpdateModel("更新文件", info);
            model.callbackEvent += (sender, args) =>
            {
                switch (args.methodName)
                {
                    case "updateFile":
                        var ver = (FileVersion)args.param[0];
                        var file = dataModel.getFile(ver.file);
                        model.updateFile(ver, file);

                        break;
                    case "complete" when (bool)args.param[0]:
                        Process.Start(model.createBat());
                        Application.Exit();

                        break;
                    case "complete":
                        model.closeDialog();
                        break;
                }
            };
            model.showDialog();
        }

        /// <summary>
        /// 点击菜单项：关于，打开关于对话框
        /// </summary>
        public void about()
        {
            var model = new AboutModel("关于");
            model.showDialog();
        }
    }
}