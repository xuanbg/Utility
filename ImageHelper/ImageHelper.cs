using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FastReport;
using Insight.Utils.Common;
using Insight.Utils.Entity;

namespace Insight.Utils.ImageHelper
{
    public class ImageHelper
    {
        /// <summary>
        /// 增加水印
        /// </summary>
        /// <param name="fr">Report对象实体</param>
        /// <param name="str">水印文字</param>
        /// <param name="size"></param>
        /// <returns>Report对象实体</returns>
        public static void AddWatermark(Report fr, string str, int size = 72)
        {
            var wm = new Watermark
            {
                Enabled = true,
                Text = str,
                Font = new Font("宋体", size, FontStyle.Bold)
            };

            for (var i = 0; i < fr.PreparedPages.Count; i++)
            {
                var pag = fr.PreparedPages.GetPage(i);
                pag.Watermark = wm;
                fr.PreparedPages.ModifyPage(i, pag);
            }
        }

        /// <summary>
        /// 将打开的本地文档转换成电子影像
        /// </summary>
        /// <param name="slv">附件涉密等级</param>
        /// <param name="uid">登录用户ID</param>
        /// <param name="did">登录部门ID</param>
        /// <param name="type">附件类型（默认0：附件）</param>
        /// <returns>ImageData List 电子影像对象集合</returns>
        public List<ImageData> AddFiles(string slv, string uid, string did = null, int type = 0)
        {
            var imgs = new List<ImageData>();
            using (var dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                if (dialog.ShowDialog() != DialogResult.OK) return null;

                var array = dialog.FileNames;
                foreach (var fileName in array)
                {
                    var fs = new FileStream(fileName, FileMode.Open);
                    var br = new BinaryReader(fs);
                    var bf = br.ReadBytes((int)fs.Length);
                    fs.Close();

                    var img = new ImageData
                    {
                        id = Util.NewId(),
                        imageType = type,
                        name = Path.GetFileNameWithoutExtension(fileName),
                        expand = Path.GetExtension(fileName),
                        secrecyDegree = slv,
                        size = bf.LongLength,
                        image = bf,
                        creatorDeptId = did,
                        creatorId = uid
                    };
                    imgs.Add(img);
                }
            }
            return imgs;
        }

        /// <summary>
        /// 根据ID获取ImageData对象实体
        /// </summary>
        /// <param name="id">电子影像ID</param>
        /// <returns>ImageData 电子影像对象实体</returns>
        public static void OpenAttach(Guid id)
        {
            var img = new ImageData();
            var fn = img.name + img.id.Substring(23) + img.expand;
            Util.SaveFile(img.image, fn, true);
        }
    }
}
