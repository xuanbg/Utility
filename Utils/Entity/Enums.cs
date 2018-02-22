namespace Insight.Utils.Entity
{
    /// <summary>
    /// 压缩模式
    /// </summary>
    public enum CompressType
    {
        None,
        Gzip,
        Deflate
    }

    /// <summary>
    /// 令牌类型
    /// </summary>
    public enum TokenType
    {
        AccessToken,
        RefreshToken
    }

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum Level
    {
        Emergency,
        Alert,
        Critical,
        Error,
        Warning,
        Notice,
        Informational,
        Debug
    }
}