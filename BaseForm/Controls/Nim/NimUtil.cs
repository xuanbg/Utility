using System;
using System.Drawing;
using System.Net;
using Insight.Utils.Common;

namespace Insight.Utils.Controls.Nim
{
    public static class NimUtil
    {

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Image getHeadImage(string url)
        {
            var iconUrl = Uri.UnescapeDataString(url);
            if (Uri.IsWellFormedUriString(iconUrl, UriKind.RelativeOrAbsolute))
            {
                try
                {
                    using (var stream = WebRequest.Create(url).GetResponse().GetResponseStream())
                    {
                        if (stream != null)
                        {
                            return Image.FromStream(stream);
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return Util.getImage("icons/head.png");
        }
    }
}
