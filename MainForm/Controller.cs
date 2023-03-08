using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Insight.Base.BaseForm.Controllers;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.Utils;
using Insight.Base.MainForm.ViewModels;

namespace Insight.Base.MainForm
{
    public class Controller : BaseController
    {
        private readonly DataModel dataModel = new DataModel();
        private readonly MainModel mainModel;

        /**
         * 是否需要退出应用
         */
        public bool exit;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Controller()
        {
            login();
            mainModel = new MainModel(Setting.appName);
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
                    case "loadTenants":
                        var account = (string)args.param[0];
                        if (string.IsNullOrEmpty(account)) return;

                        var list = dataModel.getTenants(account);
                        model.initTenants(list);

                        break;
                    case "setServerIp":
                        var setModel = new SetModel("网关设置");
                        setModel.callbackEvent += (o, eventArgs) => setModel.closeDialog();
                        setModel.showDialog();

                        break;
                    case "loadMainWindow":
                        model.hide();
                        mainModel.showMainWindow(dataModel.getNavigators());
                        model.close();

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

            mod.functions = dataModel.getActions(mod.id);
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
        /// 点击菜单项：关于，打开关于对话框
        /// </summary>
        public void about()
        {
            var model = new AboutModel("关于");
            model.showDialog();
        }
    }
}