using Insight.Utils.BaseControllers;
using Insight.Utils.Client;
using Insight.Utils.MainForm.Dtos;
using Insight.Utils.MainForm.ViewModels;

namespace Insight.Utils.MainForm
{
    public class Controller : BaseController
    {
        private readonly Model dataModel = new Model();

        /// <summary>
        /// 主窗体View Model
        /// </summary>
        public readonly MainModel mainModel;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Controller()
        {
            var title = Setting.appName;
            mainModel = new MainModel(title);
            mainModel.callbackEvent += (sender, args) => buttonClick(args.methodName, args.param);

            var model = new LoginModel(title);
            model.callbackEvent += (sender, args) => buttonClick(args.methodName, args.param);
            model.showDialog();
        }

        /// <summary>
        /// 登录
        /// </summary>
        public void loadMainWindow()
        {
            var waiting = new WaitingModel();
            waiting.view.Show();
            waiting.view.Refresh();

            mainModel.showMainWindow(dataModel.getNavigators());
            waiting.view.Close();

            mainModel.autoLoad();
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
            var dto = new PasswordDto {old = password};
            var model = new ChangPwModel("修改密码", dto);
            model.callbackEvent += (sender, args) =>
            {
                var data = (PasswordDto)args.param[0];
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
