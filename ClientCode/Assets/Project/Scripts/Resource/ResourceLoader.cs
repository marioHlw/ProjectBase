/**************************
 * 文件名:ResourceLoader.cs
 * 文件描述:资源加载器
 * 1.读取资源只能从Resources目录读取或者从AssetBundle中加载
 * 2.读取字节流数据
 * 创建日期:2019/09/05
 * 作者:ZB
 ***************************/



using Res;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceLoader : MonoSingleton<ResourceLoader>
{
    public class LoadAssetInfo
    {
        public string assetName;
        public Type assetType;
        public bool isScene;
        public enResLoaderType loadType;
        public enResPathType loadPathType;
        public ResourceLoadHelper.LoadAssetCompleteCallback completeCallback = null;
        public ResourceLoadHelper.LoadAssetUpdateCallback updateCallback = null;
        public ResourceLoadHelper.LoadAssetErrorCallback errorCallback = null;
    }

    public class ReadBytesInfo
    {
        public string assetName;
        public enResPathType loadType;
        public ResourceLoadHelper.ReadBytesCompleteCallback completeCallback = null;
        public ResourceLoadHelper.ReadBytesUpdateCallback updateCallback = null;
        public ResourceLoadHelper.ReadBytesErrorCallback errorCallback = null;
    }

    private List<ResourceLoadHelper> m_resourceLoadHelpers;
    private List<ResourceLoadHelper> m_useResourceLoadHelpers;

    private List<LoadAssetInfo> m_waitLoadAssetInfos;
    private List<ReadBytesInfo> m_waitReadBytesInfos;

    protected override void Init()
    {
        base.Init();

        m_resourceLoadHelpers = new List<ResourceLoadHelper>();
        m_useResourceLoadHelpers = new List<ResourceLoadHelper>();

        m_waitLoadAssetInfos = new List<LoadAssetInfo>();
        m_waitReadBytesInfos = new List<ReadBytesInfo>();
    }

    private void Update()
    {
        for (int i = m_useResourceLoadHelpers.Count - 1; i >= 0; i--)
        {
            if (m_useResourceLoadHelpers[i].LoadFinish)
            {
                m_resourceLoadHelpers.Add(m_useResourceLoadHelpers[i]);
                m_useResourceLoadHelpers.RemoveAt(i);
            }
        }

        while (m_waitReadBytesInfos.Count > 0)
        {
            for (int i = m_resourceLoadHelpers.Count - 1; i >= 0; i--)
            {
                ReadBytesInfo _info = m_waitReadBytesInfos[0];

                m_useResourceLoadHelpers.Add(m_resourceLoadHelpers[m_resourceLoadHelpers.Count - 1]);

                m_resourceLoadHelpers[m_resourceLoadHelpers.Count - 1].OnReadBytesAsync(_info.assetName, _info.loadType, _info.completeCallback, _info.updateCallback, _info.errorCallback,
                    DefaultDecryptResourceCallback);
                m_resourceLoadHelpers.RemoveAt(m_resourceLoadHelpers.Count - 1);

                m_waitReadBytesInfos.RemoveAt(0);
            }
        }

        while (m_waitLoadAssetInfos.Count > 0)
        {
            for (int i = m_resourceLoadHelpers.Count - 1; i >= 0; i--)
            {
                LoadAssetInfo _info = m_waitLoadAssetInfos[0];

                m_useResourceLoadHelpers.Add(m_resourceLoadHelpers[m_resourceLoadHelpers.Count - 1]);

                m_resourceLoadHelpers[m_resourceLoadHelpers.Count - 1].OnLoadAssetAsync(_info.assetName, _info.assetType, _info.isScene, _info.loadType == enResLoaderType.LoadFromResources,
                    _info.loadPathType, _info.completeCallback, _info.updateCallback, _info.errorCallback, DefaultDecryptResourceCallback);
                m_resourceLoadHelpers.RemoveAt(m_resourceLoadHelpers.Count - 1);

                m_waitLoadAssetInfos.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// 增加资源加载辅助器
    /// </summary>

    public void AddResourceLoadHelper(ResourceLoadHelper helper)
    {
        m_resourceLoadHelpers.Add(helper);
    }

    public void OnReadBytesAsync(string assetName, enResPathType loadPathType, ResourceLoadHelper.ReadBytesCompleteCallback completeCallback = null,
        ResourceLoadHelper.ReadBytesUpdateCallback updateCallback = null, ResourceLoadHelper.ReadBytesErrorCallback errorCallback = null)
    {
        // 加载辅助器足够
        if (m_resourceLoadHelpers.Count > 0)
        {
            m_useResourceLoadHelpers.Add(m_resourceLoadHelpers[m_resourceLoadHelpers.Count - 1]);

            m_resourceLoadHelpers[m_resourceLoadHelpers.Count - 1].OnReadBytesAsync(assetName, loadPathType, completeCallback, updateCallback, errorCallback, DefaultDecryptResourceCallback);
            m_resourceLoadHelpers.RemoveAt(m_resourceLoadHelpers.Count - 1);
        }
        // 加载辅助器不够，进入等待队列
        else
        {
            m_waitReadBytesInfos.Add(new ReadBytesInfo()
            {
                assetName = assetName,
                loadType = loadPathType,
                completeCallback = completeCallback,
                updateCallback = updateCallback,
                errorCallback = errorCallback,
            });
        }
    }

    public void OnLoadAssetAsync(string assetName, Type assetType, bool isScene, enResLoaderType loadType, enResPathType loadPathType = enResPathType.LoadPathFromReadWrite,
        ResourceLoadHelper.LoadAssetCompleteCallback completeCallback = null, ResourceLoadHelper.LoadAssetUpdateCallback updateCallback = null,
        ResourceLoadHelper.LoadAssetErrorCallback errorCallback = null)
    {
        // 加载辅助器足够
        if (m_resourceLoadHelpers.Count > 0)
        {
            m_useResourceLoadHelpers.Add(m_resourceLoadHelpers[m_resourceLoadHelpers.Count - 1]);

            m_resourceLoadHelpers[m_resourceLoadHelpers.Count - 1].OnLoadAssetAsync(assetName, assetType, isScene, loadType == enResLoaderType.LoadFromResources, loadPathType,
                completeCallback, updateCallback, errorCallback, DefaultDecryptResourceCallback);
            m_resourceLoadHelpers.RemoveAt(m_resourceLoadHelpers.Count - 1);
        }
        // 加载辅助器不够，进入等待队列
        else
        {
            m_waitLoadAssetInfos.Add(new LoadAssetInfo()
            {
                assetName = assetName,
                assetType = assetType,
                isScene = isScene,
                loadType = loadType,
                loadPathType = loadPathType,
                completeCallback = completeCallback,
                updateCallback = updateCallback,
                errorCallback = errorCallback,
            });
        }
    }

    public byte[] OnReadBytes(string assetName, enResPathType loadPathType)
    {
        string _fullPath = "";
        switch (loadPathType)
        {
            case enResPathType.LoadPathFromOnlyRead:
                _fullPath = Application.streamingAssetsPath;
                break;
            case enResPathType.LoadPathFromReadWrite:
                _fullPath = Application.persistentDataPath;
                break;
            case enResPathType.LoadPathFromDirctory:
                _fullPath = Ctrl.device.PathRoot;
                break;
        }
        _fullPath = _fullPath + assetName;
        // 这里写二进制资源同步读取方式

        return null;
    }

    public UnityEngine.Object OnLoadAsset(string assetName, Type assetType, bool isScene, enResLoaderType loadType, enResPathType loadPathType = enResPathType.LoadPathFromReadWrite)
    {
        if (loadType == enResLoaderType.LoadFromResources)
        {
            if (assetType != null)
            {
                return Resources.Load(assetName, assetType);
            }
            else
            {
                return Resources.Load(assetName);
            }
        }
        else
        {
            string _fullPath = "";
            switch (loadPathType)
            {
                case enResPathType.LoadPathFromOnlyRead:
                    _fullPath = Application.streamingAssetsPath;
                    break;
                case enResPathType.LoadPathFromReadWrite:
                    _fullPath = Application.persistentDataPath;
                    break;
                case enResPathType.LoadPathFromDirctory:
                    _fullPath = Ctrl.device.PathRoot;
                    break;
            }
            _fullPath = _fullPath + assetName;

            return LoadAsset(UnityEngine.AssetBundle.LoadFromFile(_fullPath), assetName, assetType, isScene);
        }
    }

    private UnityEngine.Object LoadAsset(UnityEngine.AssetBundle assetBundle, string assetName, Type assetType, bool isScene)
    {
        if (assetBundle == null)
        {
            Log.Error("Can not load asset bundle from loaded resource which is not an asset bundle.");
            return null;
        }

        if (string.IsNullOrEmpty(assetName))
        {
            Log.Error("Can not load asset from asset bundle which child name is invalid.");
            return null;
        }

        if (isScene)
        {
            int sceneNamePositionStart = assetName.LastIndexOf('/');
            int sceneNamePositionEnd = assetName.LastIndexOf('.');
            if (sceneNamePositionStart <= 0 || sceneNamePositionEnd <= 0 || sceneNamePositionStart > sceneNamePositionEnd)
            {
                return null;
            }

            string sceneName = assetName.Substring(sceneNamePositionStart + 1, sceneNamePositionEnd - sceneNamePositionStart - 1);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            return null;
        }
        else
        {
            if (assetType != null)
            {
                return assetBundle.LoadAsset(assetName, assetType);
            }
            else
            {
                return assetBundle.LoadAsset(assetName);
            }
        }
    }

    /// <summary>
    /// 默认资源解密回调
    /// </summary>

    private byte[] DefaultDecryptResourceCallback(string name, string variant, int loadType, int length, int hashCode, bool storageInReadOnly, byte[] bytes)
    {
        return bytes;
    }
}