using System.Collections.Generic;
using System.Linq;
using Insight.Utils.Client;
using Insight.Utils.Controls;
using Insight.Utils.Entity;
using Insight.Utils.Views;

namespace Insight.Utils.Models
{
    public class CategoryModel<T> : BaseModel
    {
        private readonly string baseUrl;
        private readonly List<TreeLookUpMember> list;
        private readonly Catalog<T> item;
        private readonly string parentId;
        private readonly int index;

        public Category view;

        /// <summary>
        /// 构造函数,初始化窗体及绑定事件
        /// </summary>
        /// <param name="title">窗体标题</param>
        /// <param name="cat">当前分类</param>
        /// <param name="cats">分类集合</param>
        /// <param name="url">接口地址</param>
        public CategoryModel(string title, Catalog<T> cat, List<Catalog<T>> cats, string url)
        {
            item = cat;
            list = getTreeList(cats);
            baseUrl = $"{baseServer}/{url}";
            parentId = cat.parentId;
            index = cat.index;

            view = new Category
            {
                Text = title,
                trlParent = {EditValue = cat.parentId},
                txtName = {EditValue = cat.name},
                spiIndex = {Value = cat.index},
                txtAlias = {EditValue = cat.alias},
                txtCode = {EditValue = cat.code},
                memRemark = {EditValue = cat.remark}
            };

            // 订阅控件事件实现数据双向绑定
            view.trlParent.EditValueChanged += (sender, args) =>
            {
                item.parentId = view.trlParent.EditValue?.ToString();
                setIndexValue();
            };
            view.trlParent.ButtonPressed += (sender, args) => view.trlParent.EditValue = null;
            view.txtName.EditValueChanged += (sender, args) =>
            {
                item.name = view.txtName.Text.Trim();
                setCheckItem(view.txtName, item.name, "请输入分类名称！", true);
            };
            view.spiIndex.ValueChanged += (sender, args) => item.index = (int) view.spiIndex.Value;
            view.txtAlias.EditValueChanged += (sender, args) => item.alias = view.txtAlias.Text.Trim();
            view.txtCode.EditValueChanged += (sender, args) => item.code = view.txtCode.Text.Trim();
            view.memRemark.EditValueChanged += (sender, args) =>
            {
                var text = view.memRemark.EditValue?.ToString().Trim();
                item.remark = string.IsNullOrEmpty(text) ? null : text;
            };

            Format.initTreeListLookUpEdit(view.trlParent, list, NodeIconType.CATEGORY);
            setIndexValue();
            setCheckItem(view.txtName, item.name, "请输入分类名称！", true);
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <returns>分类对象实体</returns>
        public Catalog<T> add()
        {
            if (!inputExamine()) return null;

            var msg = $"新建分类【{item.name}】失败！";
            var dict = new Dictionary<string, object> {{"catalog", item}};
            var client = new HttpClient<Catalog<T>>(tokenHelper);

            return client.post(baseUrl, dict, msg) ? client.data : null;
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <returns>分类对象实体</returns>
        public Catalog<T> edit()
        {
            if (!inputExamine()) return null;

            var msg = $"编辑分类【{item.name}】失败！";
            var dict = new Dictionary<string, object> {{"catalog", item}};
            var client = new HttpClient<object>(tokenHelper);

            return client.put($"{baseUrl}/{item.id}", dict, msg) ? item : null;
        }

        /// <summary>
        /// 设置Index值
        /// </summary>
        private void setIndexValue()
        {
            var maxValue = list.Count(i => i.parentId == item.parentId) + 1;
            view.spiIndex.Properties.MinValue = 1;
            view.spiIndex.Properties.MaxValue = maxValue;
            view.spiIndex.EditValue = item.parentId == parentId && index > 0 ? index : maxValue;
        }

        /// <summary>
        /// 获取分类树节点集合
        /// </summary>
        /// <param name="cats">分类集合</param>
        /// <returns>分类树节点集合</returns>
        private List<TreeLookUpMember> getTreeList(List<Catalog<T>> cats)
        {
            var catalogs = cats.Where(i => item.id == null || i.id != item.id).Select(i => new TreeLookUpMember
                {
                    id = i.id,
                    parentId = i.parentId,
                    index = i.index,
                    name = i.name
                }).ToList();

            if (item.id == null) return catalogs;

            removeItem(item.id, catalogs);

            return catalogs.Where(i => i.id != null).ToList();
        }

        /// <summary>
        /// 将指定ID的节点的下级节点从功能树中移除
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <param name="list">数据集</param>
        private static void removeItem(string id, List<TreeLookUpMember> list)
        {
            foreach (var node in list.Where(i => i.parentId == id))
            {
                removeItem(node.id, list);
                node.id = null;
            }
        }
    }
}
