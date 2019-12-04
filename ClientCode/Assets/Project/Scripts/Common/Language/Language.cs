using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language
{
    /// <summary>
    /// 获取 - 读取资源状态
    /// </summary>

    public static string GetLanguageLoadResourceStatus(enLoadResStatus status)
    {
        switch (status)
        {
            case enLoadResStatus.Ok: return "读取资源完成";
            case enLoadResStatus.NotReady: return "资源尚未准备完毕";
            case enLoadResStatus.NotExist: return "资源不存在于磁盘上";
            case enLoadResStatus.DependencyError: return "依赖资源错误";
            case enLoadResStatus.TypeError: return "资源类型错误";
            case enLoadResStatus.AssetError: return "读取资源错误";
        }

        return "";
    }

    /// <summary>
    /// 获取 - 时间格式显示00:00:00
    /// </summary>
    /// <param name="value">秒钟</param>

    public static string GetTimekeepingFormatBySecond(ulong value)
    {
        if (value <= 0)
        {
            return "00:00:00";
        }

        ulong _hours = 0;
        ulong _minutes = 0;
        ulong _seconds = 0;

        _hours = value / 3600;
        _minutes = value % 3600 / 60;
        _seconds = value % 3600 % 60;

        return string.Format("{0:D2}:{1:D2}:{2:D2}", _hours, _minutes, _seconds);
    }


}