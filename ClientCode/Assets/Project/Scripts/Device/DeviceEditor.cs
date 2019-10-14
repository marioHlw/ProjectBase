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

        DataRoot = GetProjectRoot() + "DataRoot/";
        PersistentDataPathWWW = PersistentDataPath;

#if UNITY_ANDROID

        PlatformFolder = "PlatformAssets/Android/";
        PlatformDataRoot = DataRoot + PlatformFolder;

        #region 开启可模拟访问StreamingAssets目录

        //PersistentDataPathWWW = "file:///" + PersistentDataPath;
        //DataRoot = Application.streamingAssetsPath + "/";
        //PlatformDataRoot = DataRoot + PlatformFolder;

        #endregion

#elif UNITY_IOS

        PlatformFolder = "PlatformAssets/IOS/";
        PlatformDataRoot = DataRoot + PlatformFolder;

#elif UNITY_EDITOR

        PlatformFolder = "PlatformAssets/Android/";
        PlatformDataRoot = DataRoot + PlatformFolder;

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

    /// <summary>
    /// 获取 - 项目根目录
    /// </summary>

    private string GetProjectRoot()
    {
        string _path = Application.dataPath;

        return _path.Replace("Assets", "");
    }
}