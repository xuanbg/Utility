using System.Collections.Generic;
using DevExpress.XtraEditors;
using NIM.Session;

namespace Insight.Utils.Controls
{
    public partial class NimSessions : XtraUserControl
    {
        private List<SessionInfo> sessions = new List<SessionInfo>();

        /// <summary>
        /// 构造方法
        /// </summary>
        public NimSessions()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 获取会话列表
        /// </summary>
        public void getSessions()
        {
            SessionAPI.QueryAllRecentSession((count, data) =>
            {
                if (data?.SessionList == null) return;

                sessions = data.SessionList;
            });
        }

    }
}
