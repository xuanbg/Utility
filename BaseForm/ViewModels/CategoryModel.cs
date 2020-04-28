using System.Collections.Generic;
using System.Linq;
using Insight.Base.BaseForm.Controls;
using Insight.Base.BaseForm.Entities;
using Insight.Base.BaseForm.Views;

namespace Insight.Base.BaseForm.ViewModels
{
    public class CategoryModel<T> : BaseDialogModel<Catalog<T>, Category>
    {
        private readonly List<TreeLookUpMember> list;
        private readonly string parentId;
        private readonly int index;

        /// <summary>
        /// 构造函数,初始化窗体及绑定事件
        /// </summary>
        /// <param name="title">窗体标题</param>
        /// <param name="cat">当前分类</param>
        /// <param name="cats">分类集合</param>
        public CategoryModel(string title, Catalog<T> cat, List<Catalog<T>> cats):base(title, cat)
        {
            view.trlParent.EditValue = item.parentId;
            view.txtName.EditValue = item.name;
            view.spiIndex.Value = item.index;
            view.txtAlias.EditValue = item.alias;
            view.txtCode.EditValue = item.code;
            view.memRemark.EditValue = item.remark;

            // 订阅控件事件实现数据双向绑定
            view.trlParent.EditValueChanged += (sender, args) =>
            {
                item.parentId = view.trlParent.EditValue?.ToString();
                setIndexValue();
            };
            view.trlParent.ButtonPressed += (sender, args) => view.trlParent.EditValue = null;
            view.txtName.EditValueChanged += (sender, args) =>{item.name = view.txtName.Text.Trim();};
            view.spiIndex.ValueChanged += (sender, args) => item.index = (int) view.spiIndex.Value;
            view.txtAlias.EditValueChanged += (sender, args) => item.alias = view.txtAlias.Text.Trim();
            view.txtCode.EditValueChanged += (sender, args) => item.code = view.txtCode.Text.Trim();
            view.memRemark.EditValueChanged += (sender, args) =>
            {
                var text = view.memRemark.EditValue?.ToString().Trim();
                item.remark = string.IsNullOrEmpty(text) ? null : text;
            };

            parentId = cat.parentId;
            index = cat.index;

            list = getTreeList(cats);
            Format.initTreeListLookUpEdit(view.trlParent, list);
            setIndexValue();
        }

        /// <summary>
        /// 
        /// </summary>
        public new void confirm()
        {
            base.confirm();
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
        private List<TreeLookUpMember> getTreeList(IEnumerable<Catalog<T>> cats)
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
