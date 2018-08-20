using System.Threading;
using System.Windows.Forms;
using Insight.Utils.Controller;
using Insight.Utils.MainForm.Login.Models;

namespace Insight.Utils.MainForm.Login
{
    public class Controller : BaseController<LoginModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Controller()
        {
            // 构造登录Model并订阅登录窗体事件
            manage = new LoginModel();
            var view = manage.view;
            view.LoginButton.Click += (sender, args) => userLogin();
            view.SetButton.Click += (sender, args) => configServer();
            view.CloseButton.Click += (sender, args) => Application.Exit();

            // 显示登录界面
            view.Show();
            view.Refresh();

            manage.initUserName();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        private void userLogin()
        {
            if (!manage.login()) return;

            // 显示等待界面
            var waiting = new WaitingModel();
            var view = waiting.view;
            view.Show();
            view.Refresh();


            // 关闭登录对话框，进入主窗体
            manage.view.Close();
            // ReSharper disable once UnusedVariable
            var controller = new MainForm.Controller();

            // 关闭等待界面
            Thread.Sleep(1000);
            view.Close();
        }

        /// <summary>
        /// 修改服务器配置
        /// </summary>
        private void configServer()
        {
            var set = new SetModel();
            var view = set.view;

            subCloseEvent(view);
            view.Confirm.Click += (sender, args) =>
            {
                manage.initUserName();
                set.save();

                closeDialog(view);
            };

            view.ShowDialog();
        }
    }
}
