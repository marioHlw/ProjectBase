/**************************
 * 文件名:DeviceBase.cs
 * 文件描述:平台原生功能抽象类
 * 创建日期:2019/08/16
 * 作者:ZB
 ***************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public abstract class DeviceBase
{
    /// <summary>
    /// 更新下来的资源版本信息
    /// </summary>

    public static Dictionary<string, Hash128> localAssetHash = new Dictionary<string, Hash128>();

    private bool logState = false;                      // Log开关

    private int m_defaultWidth;
    private int m_defaultHeight;
    private int m_lastHeight;

    private enSDKPlatform m_sdkPlatform;

    public enSDKPlatform SDKPlatform { get { return m_sdkPlatform; } }

    /// <summary>
    /// 用于存储更新下来的资源文件的目录
    /// </summary>

    public string PersistentDataPath
    {
        get { return Application.persistentDataPath + "/DataRoot/"; }
    }

    /// <summary>
    /// 用www读取PersistentDataPath目录时可能要加前缀
    /// </summary>

    public string PersistentDataPathWWW
    {
        get;
        protected set;
    }

    /// <summary>
    /// 照片分享目录
    /// </summary>

    public string PersistentDataPathSharePhoto
    {
        get { return Application.persistentDataPath + "/sharephoto/"; }
    }

    /// <summary>
    /// 平台原始资源数据路径（其实就是streamingAssetsPath）
    /// </summary>

    public string DataRoot
    {
        get;
        protected set;
    }

    /// <summary>
    /// 平台资源文件夹名
    /// </summary>

    public string PlatformFolder
    {
        get;
        protected set;
    }

    /// <summary>
    /// 平台最终资源路径
    /// </summary>

    public string PlatformDataRoot
    {
        get;
        protected set;
    }

    public string PathHead1
    {
        get
        {
            return "file://";
        }
    }

    public string PathHead2
    {
        get
        {
            return "file:///";
        }
    }

    public virtual void Init()
    {
        // 记录原始分辨率
        m_defaultWidth = Screen.currentResolution.width;
        m_defaultHeight = Screen.currentResolution.height;
    }

    public virtual void InitSDK()
    {

    }

    #region Is

    /// <summary>
    /// 判断 - 宽屏手机
    /// </summary>

    public virtual bool IsFullScreen()
    {
        return (float)m_defaultWidth / m_defaultHeight > 2;
    }

    #endregion

    #region Get

    /// <summary>
    /// 获取 - 内存信息
    /// </summary>

    public virtual string GetMemoryInfo()
    {
        return string.Empty;
    }

    /// <summary>
    /// 获取 - CPU型号
    /// </summary>

    public virtual string GetCpuType()
    {
        return string.Empty;
    }

    /// <summary>
    /// 获取 - CPU名称
    /// </summary>

    public virtual string GetCpuName()
    {
        return string.Empty;
    }

    /// <summary>
    /// 获取 - 机器性能等级
    /// </summary>

    public virtual enPerformanceLevel GetPerformanceLevel()
    {
        return enPerformanceLevel.HIGH;
    }

    /// <summary>
    /// 获取 - 没有使用的内存大小
    /// </summary>

    public virtual long GetUnuseMemorySize()
    {
        return 0;
    }

    /// <summary>
    /// 获取 - 全部内存大小
    /// </summary>

    public virtual long GetTotalMemorySize()
    {
        return 0;
    }

    /// <summary>
    /// 获取 - 电量
    /// </summary>

    public virtual int GetBattery()
    {
        return 100;
    }

    /// <summary>
    /// 获取 - wifi等级
    /// </summary>

    public virtual int GetWifiLevel()
    {
        return 100;
    }

    /// <summary>
    /// 获取 - md5字典文件中的key
    /// </summary>

    public virtual string GetResPathKey(string relaPath, string rootPath)
    {
        return relaPath;
    }

    /// <summary>
    /// 获取 - 路径的MD5值
    /// </summary>
    /// <remarks>传入的路径全部传小写，以减少因大小写引起的问题</remarks>

    public virtual string GetMD5Path(string path)
    {
        return path;
    }

    /// <summary>
    /// 配置资源路径与Hash128
    /// </summary>

    public virtual string SetRootPathAndVersion(string relaPath, string nPath, bool isABFile, string rootPath)
    {
        Hash128 hash = default(Hash128);

        // 获得MD5 完整路径
        string _key = GetResPathKey(relaPath, rootPath);     // 字典中的key 路径

#if UNITY_EDITOR 

        // 为了兼容本地编辑器的路径
        if (_key.IndexOf("file://") >= 0)
        {
            return _key;
        } 

#endif

        string path = _key;

        //与远程更新下来的md5文件对比，如果有，就读取下载下来的
        if (localAssetHash.TryGetValue(_key, out hash))
        {
            if (!isABFile)
            {
                path = PersistentDataPathWWW + _key;
            }
            else
            {
                path = PersistentDataPath + _key;
            }
        }
        else
        {
            path = DataRoot + _key;

            // 非AB文件得加上 file://  jar等开头，才能用www读取 
            if (!isABFile)
            {
                path = PathHead1 + path;          
            }
        }

        return path;
    }

    #endregion

    #region Set

    /// <summary>
    /// 设置 - 分辨率
    /// </summary>
    /// <remarks>iphone5不能设置为480（将会卡死）</remarks>

    public virtual void SetResolution(int height, bool force = false)
    {
        int width = 0;

        if (logState)
        {
            Log.Info("原始分辨率：{0}X{1}", m_defaultWidth, m_defaultHeight);
            Log.Info("当前分辨率：{0}X{1}", Screen.width, Screen.height);
        }

        if (m_defaultHeight >= height)
        {
            width = Mathf.CeilToInt(height * m_defaultWidth * 1f / m_defaultHeight);
        }
        else
        {
            width = m_defaultWidth;
            height = m_defaultHeight;
            Screen.SetResolution(m_defaultWidth, m_defaultHeight, true);

            if (logState)
            {
                Log.Info("因默认分辨率小于限制值{0}p ，修正为默认分辨率。", height);
            }
        }

        // 如果是设置相同的分辨率，直接跳过。
        if (m_lastHeight == height && force == false)
        {
            return;
        }

        m_lastHeight = height;

        if (logState)
        {
            Log.Info("分辨率调整为: {0}×{1}", width, height);
        }
        Screen.SetResolution(width, height, true);

        float _initialAspect = m_defaultWidth / m_defaultHeight;
        float _aspect = (float)Screen.width / Screen.height;

        if (_initialAspect > _aspect)
        {
            //Vector3 vector = uiCamera3D.transform.localPosition;
            //vector.z = 1000 - 1000 * (_initialAspect - _aspect);
            //uiCamera3D.transform.localPosition = vector;
        }
    }

    /// <summary>
    /// 设置 - SDK平台
    /// </summary>

    public virtual void SetSDKPlatform(enSDKPlatform platform)
    {
        m_sdkPlatform = platform;
    }

    #endregion
}