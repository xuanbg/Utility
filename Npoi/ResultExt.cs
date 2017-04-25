namespace Insight.Utils.Npoi
{
    public static class ResultExt
    {
        /// <summary>
        /// 接口调用成功（200）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="data">承载的数据</param>
        public static Result Success(this Result result, object data = null)
        {
            result.successful = true;
            result.code = "200";
            result.name = "OK";
            result.message = "接口调用成功";
            result.data = Util.Serialize(data ?? "NoContent");
            return result;
        }

        /// <summary>
        /// 无可用内容（204）
        /// </summary>
        /// <param name="result"></param>
        public static Result NoContent(this Result result)
        {
            result.successful = true;
            result.code = "204";
            result.name = "NoContent";
            result.message = "无可用内容";
            result.data = "[]";
            return result;
        }

        /// <summary>
        /// 未找到指定的文件（430）
        /// </summary>
        public static Result FileNotExists(this Result result)
        {
            result.successful = false;
            result.code = "430";
            result.name = "FileNotExists";
            result.message = "未找到指定的文件";
            return result;
        }

        /// <summary>
        /// 未找到指定的Sheet（431）
        /// </summary>
        public static Result SheetNotExists(this Result result)
        {
            result.successful = false;
            result.code = "431";
            result.name = "SheetNotExists";
            result.message = "未找到指定的Sheet";
            return result;
        }

        /// <summary>
        /// 未从文件中读取任何数据行（432）
        /// </summary>
        public static Result NoRowsRead(this Result result)
        {
            result.successful = false;
            result.code = "432";
            result.name = "NoRowsRead";
            result.message = "未从文件中读取任何数据行";
            return result;
        }

        /// <summary>
        /// Excel格式不正确（433）
        /// </summary>
        public static Result IncorrectExcelFormat(this Result result)
        {
            result.successful = false;
            result.code = "433";
            result.name = "IncorrectExcelFormat";
            result.message = "Excel格式不正确";
            return result;
        }
    }
}
