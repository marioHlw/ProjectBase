/**************************
 * 文件名:LogHelper.cs
 * 文件描述:日志辅助器
 * 创建日期:2019/08/16
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LogHelper
{
    /// <summary>
    /// 创建每个线程的静态变量
    /// </summary>
    [ThreadStatic]
    private static StringBuilder s_cachedStringBuilder = new StringBuilder(0x400);          // 1024

    private Dictionary<enLogType, string> m_logColorMap = new Dictionary<enLogType, string>()
    {
        { enLogType.Debug, "<color=#888888>{0}</color>" },
    };

    public void Log(int level, object message)
    {
        try
        {
            Log((enLogType)level, message);
        }
        catch
        {
            Debug.LogError(DateTime.Now.ToString("dd:HH:mm:ss:fff") + " --> " + "检查打印类型枚举(enLogType.cs)!");
        }
    }

    public void Log(enLogType level, object message)
    {
        try
        {
            switch (level)
            {
                case enLogType.Debug:
                    Debug.Log(DateTime.Now.ToString("dd:HH:mm:ss:fff") + " --> " + GetFormat(m_logColorMap[level], message.ToString()));
                    break;
                case enLogType.Info:
                    Debug.Log(DateTime.Now.ToString("dd:HH:mm:ss:fff") + " --> " + message.ToString());
                    break;
                case enLogType.Warning:
                    Debug.LogWarning(DateTime.Now.ToString("dd:HH:mm:ss:fff") + " --> " + message.ToString());
                    break;
                case enLogType.Error:
                    Debug.LogError(DateTime.Now.ToString("dd:HH:mm:ss:fff") + " --> " + message.ToString());
                    break;
            }
        }
        catch
        {
            Debug.LogError(DateTime.Now.ToString("dd:HH:mm:ss:fff") + " --> " + "检查打印类型颜色字典(LogHelper.cs -> m_logColorMap)!");
        }
    }

    /// <summary>
    /// 获取 - 格式化字符串
    /// </summary>

    private string GetFormat(string format, params object[] args)
    {
        if (format == null)
        {
            throw new Exception(DateTime.Now.ToString("dd:HH:mm:ss:fff") + " --> " + "Format is invalid.");
        }

        if (args == null)
        {
            throw new Exception(DateTime.Now.ToString("dd:HH:mm:ss:fff") + " --> " + "Args is invalid.");
        }

        s_cachedStringBuilder.Length = 0;
        s_cachedStringBuilder.AppendFormat(format, args);
        return s_cachedStringBuilder.ToString();
    }
}