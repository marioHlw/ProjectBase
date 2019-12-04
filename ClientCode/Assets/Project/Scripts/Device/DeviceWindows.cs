/**************************
 * 文件名:DeviceWindows.cs
 * 文件描述:Windows平台原生功能类
 * 创建日期:2019/08/17
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class DeviceWindows : DeviceBase
{
    public override void Init()
    {
        base.Init();

        PathResRead = Application.streamingAssetsPath + "/";
        FolderPlatform = "PlatformAssets/Window/";
        PathRoot = PathResRead + FolderPlatform;

        SetSDKPlatform(enSDKPlatform.Develop_Windows);
    }

    /// <summary>
    /// 获取md5字典文件中的key
    /// </summary>

    public override string GetResPathKey(string relaPath, string rootPath)
    {
        int _index = rootPath.IndexOf("PlatformAssets/");

        if (_index >= 0)
        {
            return GetMD5Path(rootPath.Substring(_index, rootPath.Length - _index) + relaPath);
        }
        else
        {
            return GetMD5Path(relaPath);
        }
    }

    public override string GetMD5Path(string path)
    {
        MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
        byte[] _hashArray = _md5.ComputeHash(Encoding.UTF8.GetBytes(path.ToLower()));
        return System.BitConverter.ToString(_hashArray).Replace("-", "").ToLower();
    }
}
