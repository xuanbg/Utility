using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Insight.Utils.Client;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.MainForm
{
    internal class DataModel
    {

        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <returns>导航数据集合</returns>
        internal List<ModuleDto> getNavigators()
        {
            var list = new List<ModuleDto>
            {
                new ModuleDto
                {
                    id = "6ceada3d78aa11ea9bf40242ac110005",
                    type = 1,
                    index = 1,
                    name = "客服系统",
                    moduleInfo = new ModuleInfo {iconUrl = "icons/service.png"}
                },
                new ModuleDto
                {
                    id = "04dcfb5978ab11ea9bf40242ac110005",
                    parentId = "6ceada3d78aa11ea9bf40242ac110005",
                    type = 2,
                    index = 0,
                    name = "客服工作台",
                    moduleInfo = new ModuleInfo{module = "Workbench", file = "Service.dll", iconUrl = "icons/workbench.png", autoLoad = true}
                },
                new ModuleDto
                {
                    id = "82e9906a78a911ea9bf40242ac110005",
                    type = 1,
                    index = 2,
                    name = "售后工单",
                    moduleInfo = new ModuleInfo {iconUrl = "icons/order.png"}
                },
                new ModuleDto
                {
                    id = "04dcfb1478ab11ea9bf40242ac110005",
                    parentId = "82e9906a78a911ea9bf40242ac110005",
                    type = 2,
                    index = 1,
                    name = "工单管理",
                    moduleInfo = new ModuleInfo{module = "Orders", file = "AfterSale.dll", iconUrl = "icons/orders.png", autoLoad = false}
                },
                new ModuleDto
                {
                    id = "04dcfa3478ab11ea9bf40242ac110005",
                    parentId = "82e9906a78a911ea9bf40242ac110005",
                    type = 2,
                    index = 2,
                    name = "待处理工单",
                    moduleInfo = new ModuleInfo{module = "Pends", file = "AfterSale.dll", iconUrl = "icons/pending.png", autoLoad = false}
                }
            };

            return list;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        public UserInfo getUserInfo()
        {
            var url = $"{Setting.authServer}/userapi/v1.0/users/myself";
            var client = new HttpClient<UserInfo>();

            return client.getData(url);
        }

        /// <summary>
        /// 获取店铺信息
        /// </summary>
        /// <returns>店铺信息</returns>
        public ShopInfo getShopInfo()
        {
            const string url = "http://uwaapi.yindianmall.cn/member_ship/account/findUserInfo";
            var client = new HttpClient<ShopInfo>();

            return client.getData(url);
        }

        /// <summary>
        /// 获取可登录部门列表
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <returns>可登录部门列表</returns>
        internal List<TreeLookUpMember> getDepts(string account)
        {
            var url = $"{Setting.authServer}/userapi/v1.0/users/{account}/depts";
            var dict = new Dictionary<string, object> {{"account", account}};
            var client = new HttpClient<List<TreeLookUpMember>>();

            return client.request(url, dict);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>是否成功</returns>
        internal bool changPassword(PasswordDto dto)
        {
            const string msg = "更换密码失败！请检查网络状况，并再次进行更换密码操作。";
            var url = $"{Setting.authServer}/userapi/v1.0/users/{Setting.userId}/signature";
            var client = new HttpClient<object>();

            return client.put(url, dto, msg);
        }

        /// <summary>
        /// 获取服务器上的客户端文件版本信息
        /// </summary>
        /// <returns>文件版本信息</returns>
        internal Update checkUpdate()
        {
            var url = $"{Setting.updateUrl}/update.json";
            var client = new HttpRequest();
            if (!client.send(url))
            {
                Messages.showError("无法获取更新信息，请稍后再试……");
                return null;
            }

            // 获取更新信息
            var info = Util.deserialize<Update>(client.data);
            var root = Application.StartupPath;

            // 清除本地备份
            var locals = Util.getClientFiles(".bak");
            locals.ForEach(i =>
            {
                var filePath = root;
                if (string.IsNullOrEmpty(i.localPath)) filePath = $"{filePath}\\{i.localPath}\\";

                Util.deleteFile(filePath + i.file);
            });

            // 比较文件版本
            locals = Util.getClientFiles(".exe|.dll|.frl");
            var updates = new List<FileVersion>();
            foreach (var ver in info.data)
            {
                var cf = locals.FirstOrDefault(i => i.file == ver.file && i.localPath == ver.localPath);
                if (cf == null)
                {
                    updates.Add(ver);
                    continue;
                }

                var cv = new Version(cf.version);
                var sv = new Version(ver.version);
                if (cv.CompareTo(sv) >= 0) continue;

                updates.Add(ver);
            }
            info.data = updates;

            return info;
        }

        /// <summary>
        /// 根据URL获取更新文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>Result</returns>
        internal string getFile(string file)
        {
            var url = $"{Setting.updateUrl}/{file}";
            var client = new HttpRequest();

            return client.send(url) ? client.data : null;
        }
    }

    public class ShopInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string realname { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string supplierId { get; set; }
    }
}
