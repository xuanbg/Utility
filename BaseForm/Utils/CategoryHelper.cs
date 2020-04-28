using System.Collections.Generic;
using System.Linq;
using Insight.Base.BaseForm.Entities;

namespace Insight.Base.BaseForm.Utils
{
    public class CategoryHelper<T>
    {
        /// <summary>
        /// 插入索引
        /// </summary>
        /// <param name="cats"></param>
        /// <param name="cat"></param>
        public static void insertIndex(List<Catalog<T>> cats, Catalog<T> cat)
        {
            var list = cats.Where(i => i.parentId == cat.parentId && i.index >= cat.index);
            foreach (var item in list)
            {
                item.index++;
            }
        }

        /// <summary>
        /// 更新索引
        /// </summary>
        /// <param name="cats"></param>
        /// <param name="old"></param>
        /// <param name="cat"></param>
        public static void updateIndex(List<Catalog<T>> cats, Catalog<T> old, Catalog<T> cat)
        {
            if (cat.parentId == old.parentId)
            {
                if (cat.index < old.index)
                {
                    var list = cats.Where(i => i.index >= cat.index && i.index < old.index);
                    foreach (var item in list)
                    {
                        item.index++;
                    }
                }
                else if (cat.index > old.index)
                {
                    var list = cats.Where(i => i.index > old.index && i.index <= cat.index);
                    foreach (var item in list)
                    {
                        item.index--;
                    }
                }
            }
            else
            {
                var oldList = cats.Where(i => i.parentId == old.parentId && i.index >= old.index);
                foreach (var item in oldList)
                {
                    item.index--;
                }

                var newList = cats.Where(i => i.parentId == cat.parentId && i.index >= cat.index);
                foreach (var item in newList)
                {
                    item.index++;
                }
            }
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="cats"></param>
        /// <param name="cat"></param>
        public static void deleteIndex(List<Catalog<T>> cats, Catalog<T> cat)
        {
            var list = cats.Where(i => i.parentId == cat.parentId && i.index > cat.index);
            foreach (var item in list)
            {
                item.index--;
            }
        }
    }
}
