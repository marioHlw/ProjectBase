using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 资源加载方式类型
/// </summary>

public enum enResourceLoadType
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

/// <summary>
/// 资源加载路径类型
/// </summary>

public enum enResourceLoadPathType
{
    /// <summary>
    /// 从读写路径加载
    /// </summary>

    LoadPathFromReadWrite = 0,

    /// <summary>
    /// 从只读路径加载
    /// </summary>

    LoadPathFromOnlyRead,

    /// <summary>
    /// 从磁盘路径加载(仅在PC上使用)
    /// </summary>

    LoadPathFromDirctory,
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