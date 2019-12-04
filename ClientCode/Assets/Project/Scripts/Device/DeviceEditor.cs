/**************************
 * 文件名:DeviceEditor.cs
 * 文件描述:编辑原生功能类
 * 创建日期:2019/08/17
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceEditor : DeviceBase
{
    public DeviceEditor()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        SetSDKPlatform(enSDKPlatform.Develop_Editor);

        PathResRead = Application.dataPath.Replace("Assets", "");
        PathPrefixWWW = PathResReadWirte;

#if UNITY_ANDROID

        FolderPlatform = "DataRoot/PlatformAssets/Android/";
        PathRoot = PathResRead + FolderPlatform;

#elif UNITY_IOS

        FolderPlatform = "DataRoot/PlatformAssets/IOS/";
        PathRoot = PathResRead + FolderPlatform;

#elif UNITY_EDITOR

        FolderPlatform = "DataRoot/PlatformAssets/Android/";
        PathRoot = PathResRead + FolderPlatform;

#endif

    }

    /// <summary>
    /// 获取 - 资源路径MD5值
    /// </summary>

    public override string GetResPathKey(string relaPath, string rootPath)
    {
        int _index = rootPath.IndexOf("DataRoot/");

        if (_index >= 0)
        {
            return rootPath.Substring(_index + 9, rootPath.Length - (_index + 9)) + GetMD5Path(relaPath);
        }
        else
        {
            return GetMD5Path(relaPath);
        }
    }
}