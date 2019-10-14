using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language
{
    /// <summary>
    /// 获取 - 读取资源状态
    /// </summary>

    public static string GetLanguageLoadResourceStatus(enLoadResourceStatus status)
    {
        switch (status)
        {
            case enLoadResourceStatus.Ok:return "读取资源完成";
            case enLoadResourceStatus.NotReady: return "资源尚未准备完毕";
            case enLoadResourceStatus.NotExist: return "资源不存在于磁盘上";
            case enLoadResourceStatus.DependencyError: return "依赖资源错误";
            case enLoadResourceStatus.TypeError: return "资源类型错误";
            case enLoadResourceStatus.AssetError: return "读取资源错误";
        }

        return "";
    }
}