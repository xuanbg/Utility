namespace Insight.Base.BaseForm.Entities
{
    public class ReportDto
    {
        /**
         * 报表ID
         */
        public long id;

        /**
         * 二进制报表内容
         */
        public byte[] bytes;

        /**
         * 报表内容
         */
        public string content;
    }
}
