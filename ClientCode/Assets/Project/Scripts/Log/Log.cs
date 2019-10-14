/**************************
 * 文件名:Log.cs
 * 文件描述:日志工具集
 * 创建日期:2019/08/16
 * 作者:ZB
 ***************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public static class Log
{
    private static StringBuilder s_cachedStringBuilder = new StringBuilder(0x400);          // 1024
    private static LogHelper m_logHelper = null;

    public static void Init()
    {
        if (m_logHelper == null)
        {
            m_logHelper = new LogHelper();
        }

        Log.Info(Ctrl.LogInfos[0] + " - 日志打印");
    }

    public static void UnInit()
    {
        m_logHelper = null;

        Log.Info(Ctrl.LogInfos[1] + " - 日志打印");
    }

    /// <summary>
    /// 记录调试级别日志，用于记录调试类日志信息。
    /// </summary>
    /// <param name="message">日志内容。</param>
    /// <remarks>仅在带有 DEBUG 预编译选项且带有 ENABLE_LOG、ENABLE_DEBUG_LOG 或 ENABLE_DEBUG_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_DEBUG_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    public static void Debug(object message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Debug, message);
    }

    /// <summary>
    /// 记录调试级别日志，用于记录调试类日志信息。
    /// </summary>
    /// <param name="message">日志内容。</param>
    /// <remarks>仅在带有 DEBUG 预编译选项且带有 ENABLE_LOG、ENABLE_DEBUG_LOG 或 ENABLE_DEBUG_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_DEBUG_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    public static void Debug(string message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Debug, message);
    }

    /// <summary>
    /// 记录调试级别日志，用于记录调试类日志信息。
    /// </summary>
    /// <param name="format">日志格式。</param>
    /// <param name="args">日志参数。</param>
    /// <remarks>仅在带有 DEBUG 预编译选项且带有 ENABLE_LOG、ENABLE_DEBUG_LOG 或 ENABLE_DEBUG_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_DEBUG_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    public static void Debug(string format, params object[] args)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Debug, GetFormat(format, args));
    }

    /// <summary>
    /// 打印信息级别日志，用于记录程序正常运行日志信息。
    /// </summary>
    /// <param name="message">日志内容</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG 或 ENABLE_INFO_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_INFO_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    public static void Info(object message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Info, message);
    }

    /// <summary>
    /// 打印信息级别日志，用于记录程序正常运行日志信息。
    /// </summary>
    /// <param name="message">日志内容</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG 或 ENABLE_INFO_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_INFO_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    public static void Info(string message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Info, message);
    }

    /// <summary>
    /// 打印信息级别日志，用于记录程序正常运行日志信息。
    /// </summary>
    /// <param name="format">日志格式。</param>
    /// <param name="args">日志参数。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG 或 ENABLE_INFO_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_INFO_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    public static void Info(string format, params object[] args)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Info, GetFormat(format, args));
    }

    /// <summary>
    /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
    /// </summary>
    /// <param name="message">日志内容。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG、ENABLE_INFO_AND_ABOVE_LOG 或 ENABLE_WARNING_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_WARNING_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    [Conditional("ENABLE_WARNING_AND_ABOVE_LOG")]
    public static void Warning(object message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Warning, message);
    }

    /// <summary>
    /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
    /// </summary>
    /// <param name="message">日志内容。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG、ENABLE_INFO_AND_ABOVE_LOG 或 ENABLE_WARNING_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_WARNING_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    [Conditional("ENABLE_WARNING_AND_ABOVE_LOG")]
    public static void Warning(string message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Warning, message);
    }
 
    /// <summary>
    /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
    /// </summary>
    /// <param name="format">日志格式。</param>
    /// <param name="args">日志参数。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG、ENABLE_INFO_AND_ABOVE_LOG 或 ENABLE_WARNING_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_WARNING_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    [Conditional("ENABLE_WARNING_AND_ABOVE_LOG")]
    public static void Warning(string format, params object[] args)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Warning, GetFormat(format, args));
    }

    /// <summary>
    /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
    /// </summary>
    /// <param name="message">日志内容。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG、ENABLE_INFO_AND_ABOVE_LOG、ENABLE_WARNING_AND_ABOVE_LOG 或 ENABLE_ERROR_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_ERROR_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    [Conditional("ENABLE_WARNING_AND_ABOVE_LOG")]
    [Conditional("ENABLE_ERROR_AND_ABOVE_LOG")]
    public static void Error(object message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Error, message);
    }

    /// <summary>
    /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
    /// </summary>
    /// <param name="message">日志内容。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG、ENABLE_INFO_AND_ABOVE_LOG、ENABLE_WARNING_AND_ABOVE_LOG 或 ENABLE_ERROR_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_ERROR_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    [Conditional("ENABLE_WARNING_AND_ABOVE_LOG")]
    [Conditional("ENABLE_ERROR_AND_ABOVE_LOG")]
    public static void Error(string message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Error, message);
    }
   
    /// <summary>
    /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
    /// </summary>
    /// <param name="format">日志格式。</param>
    /// <param name="args">日志参数。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG、ENABLE_INFO_AND_ABOVE_LOG、ENABLE_WARNING_AND_ABOVE_LOG 或 ENABLE_ERROR_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_ERROR_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    [Conditional("ENABLE_WARNING_AND_ABOVE_LOG")]
    [Conditional("ENABLE_ERROR_AND_ABOVE_LOG")]
    public static void Error(string format, params object[] args)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Error, GetFormat(format, args));
    }

    /// <summary>
    /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
    /// </summary>
    /// <param name="message">日志内容。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG、ENABLE_INFO_AND_ABOVE_LOG、ENABLE_WARNING_AND_ABOVE_LOG、ENABLE_ERROR_AND_ABOVE_LOG 或 ENABLE_FATAL_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_FATAL_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    [Conditional("ENABLE_WARNING_AND_ABOVE_LOG")]
    [Conditional("ENABLE_ERROR_AND_ABOVE_LOG")]
    [Conditional("ENABLE_FATAL_AND_ABOVE_LOG")]
    public static void Fatal(object message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Fatal, message);
    }

    /// <summary>
    /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
    /// </summary>
    /// <param name="message">日志内容。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG、ENABLE_INFO_AND_ABOVE_LOG、ENABLE_WARNING_AND_ABOVE_LOG、ENABLE_ERROR_AND_ABOVE_LOG 或 ENABLE_FATAL_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_FATAL_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    [Conditional("ENABLE_WARNING_AND_ABOVE_LOG")]
    [Conditional("ENABLE_ERROR_AND_ABOVE_LOG")]
    [Conditional("ENABLE_FATAL_AND_ABOVE_LOG")]
    public static void Fatal(string message)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Fatal, message);
    }

    /// <summary>
    /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
    /// </summary>
    /// <param name="format">日志格式。</param>
    /// <param name="args">日志参数。</param>
    /// <remarks>仅在带有 ENABLE_LOG、ENABLE_INFO_LOG、ENABLE_DEBUG_AND_ABOVE_LOG、ENABLE_INFO_AND_ABOVE_LOG、ENABLE_WARNING_AND_ABOVE_LOG、ENABLE_ERROR_AND_ABOVE_LOG 或 ENABLE_FATAL_AND_ABOVE_LOG 预编译选项时生效。</remarks>
    [Conditional("ENABLE_LOG")]
    [Conditional("ENABLE_FATAL_LOG")]
    [Conditional("ENABLE_DEBUG_AND_ABOVE_LOG")]
    [Conditional("ENABLE_INFO_AND_ABOVE_LOG")]
    [Conditional("ENABLE_WARNING_AND_ABOVE_LOG")]
    [Conditional("ENABLE_ERROR_AND_ABOVE_LOG")]
    [Conditional("ENABLE_FATAL_AND_ABOVE_LOG")]
    public static void Fatal(string format, params object[] args)
    {
        if (m_logHelper == null) return;
        m_logHelper.Log(enLogType.Fatal, GetFormat(format, args));
    }

    /// <summary>
    /// 获取 - 格式化字符串
    /// </summary>

    private static string GetFormat(string format, params object[] args)
    {
        if (format == null)
        {
            throw new Exception("Format is invalid.");
        }

        s_cachedStringBuilder.Length = 0;
        s_cachedStringBuilder.AppendFormat(format, args);
        return s_cachedStringBuilder.ToString();
    }
}