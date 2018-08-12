using Insight.Utils.Common;

namespace Insight.Utils.Client
{
    public class Setting
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public static string appName = Util.GetAppSetting("AppName");

        /// <summary>
        /// 应用服务地址
        /// </summary>
        public static string appServer = Util.GetAppSetting("AppServer"); 

        /// <summary>
        /// 基础服务地址
        /// </summary>
        public static string baseServer = GetBaseServer();

        /// <summary>
        /// 界面主题样式
        /// </summary>
        public static string lookAndFeel = Util.GetAppSetting("DefaultLookAndFeel");

        /// <summary>
        /// 文档打印机
        /// </summary>
        public static string docPrint = Util.GetAppSetting("DocPrint");

        /// <summary>
        /// 标签打印机
        /// </summary>
        public static string tagPrint = Util.GetAppSetting("TagPrint");

        /// <summary>
        /// 票据打印机
        /// </summary>
        public static string bilPrint = Util.GetAppSetting("BilPrint");

        /// <summary>
        /// 票据是否合并打印
        /// </summary>
        public static bool isMergerPrint = bool.Parse(Util.GetAppSetting("IsMergerPrint"));

        /// <summary>
        /// 令牌管理器
        /// </summary>
        public static TokenHelper tokenHelper = new TokenHelper { appId = Util.GetAppSetting("AppId") };

        /// <summary>
        /// 当前登录部门ID
        /// </summary>
        public static string deptId => tokenHelper.deptId;

        /// <summary>
        /// 当前登录部门全称
        /// </summary>
        public static string deptName;

        /// <summary>
        /// 用户ID
        /// </summary>
        public static string userId;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string userName;

        /// <summary>
        /// 是否需要修改密码
        /// </summary>
        public static bool needChangePw;

        /// <summary>
        /// 获取基础服务地址
        /// </summary>
        /// <returns></returns>
        private static string GetBaseServer()
        {
            var server = Util.GetAppSetting("BaseServer");
            if (string.IsNullOrEmpty(server)) server = appServer;

            return server;
        }

        /// <summary>
        /// 获取是否保存用户名选项设置
        /// </summary>
        /// <returns></returns>
        public static bool IsSaveUserInfo()
        {
            return bool.Parse(Util.GetAppSetting("IsSaveUserInfo"));
        }

        /// <summary>
        /// 获取保存的用户名
        /// </summary>
        /// <returns></returns>
        public static string GetAccount()
        {
            return Util.GetAppSetting("UserName");
        }

        /// <summary>
        /// 保存默认样式
        /// </summary>
        /// <param name="defaultLookAndFeel"></param>
        public static void SaveLookAndFeel(string defaultLookAndFeel)
        {
            Util.SaveAppSetting("DefaultLookAndFeel", defaultLookAndFeel);
        }

        /// <summary>
        /// 保存默认打印机
        /// </summary>
        /// <param name="print"></param>
        /// <param name="printName"></param>
        public static void SavePrinter(string print, string printName)
        {
            Util.SaveAppSetting(print, printName);
        }

        /// <summary>
        /// 保存用户信息保存选项
        /// </summary>
        public static void SaveIsMergerPrint()
        {
            Util.SaveAppSetting("IsMergerPrint", isMergerPrint.ToString());
        }

        /// <summary>
        /// 保存用户信息保存选项
        /// </summary>
        /// <param name="isSave"></param>
        public static void SaveIsSaveUserInfo(bool isSave)
        {
            Util.SaveAppSetting("IsSaveUserInfo", isSave.ToString());
        }

        /// <summary>
        /// 保存用户名
        /// </summary>
        /// <param name="account"></param>
        public static void SaveUserName(string account)
        {
            Util.SaveAppSetting("UserName", account);
        }

        /// <summary>
        /// 保存应用服务地址和端口
        /// </summary>
        public static void SaveAppServer()
        {
            Util.SaveAppSetting("AppServer", appServer);
        }

        /// <summary>
        /// 保存验证服务地址和端口
        /// </summary>
        public static void SaveBaseServer()
        {
            Util.SaveAppSetting("BaseServer", baseServer);
        }
    }
}
