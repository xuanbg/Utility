using Insight.Utils.Common;

namespace Insight.Utils.Client
{
    public static class Setting
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public static readonly string appName = Util.getAppSetting("AppName");

        /// <summary>
        /// 网关地址
        /// </summary>
        public static string gateway = Util.getAppSetting("Gateway");

        /// <summary>
        /// 客户端更新URL
        /// </summary>
        public static readonly string updateUrl = Util.getAppSetting("UpdateUrl");

        /// <summary>
        /// 界面主题样式
        /// </summary>
        public static readonly string lookAndFeel = Util.getAppSetting("DefaultLookAndFeel");

        /// <summary>
        /// 文档打印机
        /// </summary>
        public static string docPrint = Util.getAppSetting("DocPrint");

        /// <summary>
        /// 标签打印机
        /// </summary>
        public static string tagPrint = Util.getAppSetting("TagPrint");

        /// <summary>
        /// 票据打印机
        /// </summary>
        public static string bilPrint = Util.getAppSetting("BilPrint");

        /// <summary>
        /// 票据是否合并打印
        /// </summary>
        public static bool isMergerPrint = bool.Parse(Util.getAppSetting("IsMergerPrint"));

        /// <summary>
        /// 应用ID
        /// </summary>
        public static readonly string appId = Util.getAppSetting("AppId");

        /// <summary>
        /// 令牌管理器
        /// </summary>
        public static readonly TokenHelper tokenHelper = new TokenHelper {appId = appId};

        /// <summary>
        /// 租户ID
        /// </summary>
        public static string tenantId;

        /// <summary>
        /// 登录部门ID
        /// </summary>
        public static string deptId;

        /// <summary>
        /// 登录部门编码
        /// </summary>
        public static string deptCode;

        /// <summary>
        /// 登录部门名称
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
        /// 获取是否保存用户名选项设置
        /// </summary>
        /// <returns></returns>
        public static bool isSaveUserInfo()
        {
            return bool.Parse(Util.getAppSetting("IsSaveUserInfo"));
        }

        /// <summary>
        /// 获取保存的用户名
        /// </summary>
        /// <returns></returns>
        public static string getAccount()
        {
            return Util.getAppSetting("UserName");
        }

        /// <summary>
        /// 保存默认样式
        /// </summary>
        /// <param name="defaultLookAndFeel"></param>
        public static void saveLookAndFeel(string defaultLookAndFeel)
        {
            Util.saveAppSetting("DefaultLookAndFeel", defaultLookAndFeel);
        }

        /// <summary>
        /// 保存默认打印机
        /// </summary>
        /// <param name="print"></param>
        /// <param name="printName"></param>
        public static void savePrinter(string print, string printName)
        {
            Util.saveAppSetting(print, printName);
        }

        /// <summary>
        /// 保存用户信息保存选项
        /// </summary>
        public static void saveIsMergerPrint()
        {
            Util.saveAppSetting("IsMergerPrint", isMergerPrint.ToString());
        }

        /// <summary>
        /// 保存用户信息保存选项
        /// </summary>
        /// <param name="isSave"></param>
        public static void saveIsSaveUserInfo(bool isSave)
        {
            Util.saveAppSetting("IsSaveUserInfo", isSave.ToString());
        }

        /// <summary>
        /// 保存用户名
        /// </summary>
        /// <param name="account"></param>
        public static void saveUserName(string account)
        {
            Util.saveAppSetting("UserName", account);
        }
        
        /// <summary>
        /// 保存验证服务地址和端口
        /// </summary>
        public static void saveGateway()
        {
            Util.saveAppSetting("Gateway", gateway);
        }
    }
}
