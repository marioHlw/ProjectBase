using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 打印类型
/// </summary>

public enum enLogType
{
    /// <summary>
    /// 调试
    /// </summary>

    Debug = 0,

    /// <summary>
    /// 信息
    /// </summary>

    Info,

    /// <summary>
    /// 警告
    /// </summary>

    Warning,

    /// <summary>
    /// 错误
    /// </summary>

    Error,

    /// <summary>
    /// 严重错误
    /// </summary>

    Fatal,
}