namespace Res
{
    /// <summary>
    /// 资源加载方式类型
    /// </summary>

    public enum enResLoaderType
    {
        /// <summary>
        /// Resources中加载
        /// </summary>

        LoadFromResources = 0,

        /// <summary>
        /// AssetBundle中加载
        /// </summary>

        LoadFromAssetBundle,

        /// <summary>
        /// 字节流中加载
        /// </summary>

        LoadFromBytes,
    }
}

/// <summary>
/// 加密类型
/// </summary>

public enum enEncryptType
{
    /// <summary>
    /// 没有加密类型
    /// </summary>

    None = 0,

    /// <summary>
    /// 快速加密
    /// </summary>

    QuickEncrypt,
}