using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Insight.Utils.BaseControllers;
using Insight.Utils.Client;
using Insight.Utils.Entity;
using Insight.Utils.MainForm.ViewModels;

namespace Insight.Utils.MainForm
{
    public class Controller : BaseController
    {
        private readonly Model dataModel = new Model();
        private readonly MainModel mainModel;
        private readonly LoginModel loginModel;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Controller()
        {
            var title = Setting.appName;
            mainModel = new MainModel(title);
            mainModel.callbackEvent += (sender, args) => buttonClick(args.methodName, args.param);

            loginModel = new LoginModel(title);
            loginModel.callbackEvent += (sender, args) => buttonClick(args.methodName, args.param);
            loginModel.showDialog();
        }

        /// <summary>
        /// 加载可选登录部门
        /// </summary>
        /// <param name="account">登录账号</param>
        public void loadDept(string account)
        {
            if (string.IsNullOrEmpty(account)) return;

            var list = dataModel.getDepts(account);
            if (!list.Any()) return;

            loginModel.initDepts(list);
        }

        /// <summary>
        /// 加载主窗体
        /// </summary>
        public void loadMainWindow()
        {
            var model = new WaitingModel(Setting.appName);
            model.show();
            loginModel.close();

            mainModel.showMainWindow(dataModel.getNavigators());
            model.close();

            mainModel.autoLoad();
            if (Setting.needChangePw) changPassword("123456");
        }

        /// <summary>
        /// 设置服务地址
        /// </summary>
        public void setServerIp()
        {
            var model = new SetModel("服务器设置");
            model.showDialog();
        }

        /// <summary>
        /// 点击菜单项：修改密码，弹出修改密码对话框
        /// </summary>
        /// <param name="password">原密码</param>
        public void changPassword(string password)
        {
            var model = new ChangPwModel("修改密码", password);
            model.callbackEvent += (sender, args) =>
            {
                var data = (PasswordDto) args.param[0];
                if (!dataModel.changPassword(data)) return;

                model.close();
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
        public void update()
        {
            var model = new UpdateModel("检查更新");
            model.callbackEvent += (sender, args) =>
            {
                if (args.methodName == "getFile")
                {
                    var info = (ClientFile) args.param[0];
                    model.updateFile(dataModel.getFile(info.id));
                    return;
                }

                if (args.methodName == "complete")
                {
                    if ((bool) args.param[0])
                    {
                        Process.Start(model.createBat());
                        Application.Exit();
                    }
                    else
                    {
                        model.close();
                    }
                }
            };

            var files = dataModel.getFiles(Setting.appId);
            if (files == null || !files.Any()) return;

            if (model.checkUpdate(files)) model.showDialog();
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