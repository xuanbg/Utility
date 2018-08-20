namespace Insight.Utils.Entity
{
    /// <summary>
    /// 压缩模式
    /// </summary>
    public enum CompressType
    {
        NONE,
        GZIP,
        DEFLATE
    }

    /// <summary>
    /// 令牌类型
    /// </summary>
    public enum TokenType
    {
        ACCESS_TOKEN,
        REFRESH_TOKEN
    }

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum Level
    {
        EMERGENCY,
        ALERT,
        CRITICAL,
        ERROR,
        WARNING,
        NOTICE,
        INFORMATIONAL,
        DEBUG
    }

    /// <summary>
    /// TreeList NodeIcon Type
    /// </summary>
    public enum NodeIconType
    {
        GENERAL,
        CATEGORY,
        NODE_TYPE,
        ORGANIZATION,
        ONLY_LEVEL0,
        CUSTOM
    }
}