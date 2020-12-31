namespace Insight.Base.BaseForm.Entities
{
    public class FileDto
    {
        /**
         * 字节数组
         */
        public byte[] bytes { get; set; }

        /**
         * 文件名
         */
        public string name { get; set; }

        /**
         * 扩展名
         */
        public string ext { get; set; }
    }
}
