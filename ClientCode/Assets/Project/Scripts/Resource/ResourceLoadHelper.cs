/**************************
 * 文件名:ResourceLoadHelper.cs
 * 文件描述:资源加载辅助器
 * 创建日期:2019/09/05
 * 作者:ZB
 ***************************/


using Res;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ResourceLoadHelper : MonoBehaviour
{
    public delegate void ReadBytesCompleteCallback(byte[] bytes);
    public delegate void ReadBytesUpdateCallback(float progress);
    public delegate void ReadBytesErrorCallback(enLoadResStatus status);

    public delegate void LoadAssetBundleCompleteCallback(UnityEngine.AssetBundle asset);
    public delegate void LoadAssetBundleUpdateCallback(float progress);
    public delegate void LoadAssetBundleErrorCallback(enLoadResStatus status);

    public delegate void LoadAssetCompleteCallback(UnityEngine.Object asset);
    public delegate void LoadAssetUpdateCallback(float progress);
    public delegate void LoadAssetErrorCallback(enLoadResStatus status);

    public delegate byte[] DecryptResourceCallback(string name, string variant, int loadType, int length, int hashCode, bool storageInReadOnly, byte[] bytes);

    private UnityWebRequest m_UnityWebRequest = null;                           // 使用UnityWebRequest从磁盘中读取文件
    private WWW m_WWW = null;                                                   // 使用www从磁盘中读取文件

    private AssetBundleCreateRequest m_FileAssetBundleCreateRequest = null;     // 异步读取AssetBundle文件
    private AssetBundleCreateRequest m_BytesAssetBundleCreateRequest = null;    // 异步将二进制流解析成AssetBundle中文件

    private AssetBundleRequest m_AssetBundleRequest = null;                     // 加载资源
    private AsyncOperation m_AsyncOperation = null;                             // 加载场景
    private ResourceRequest m_ResourceRequest = null;

    private string m_FileFullPath = null;
    private string m_BytesFullPath = null;
    private bool m_Disposed = false;
    private float m_LastProgress = 0f;
    private string m_AssetName = null;
    private Type m_AssetType = null;
    private bool m_IsScene = false;

    private ReadBytesCompleteCallback m_readBytesCompleteCallback = null;
    private ReadBytesUpdateCallback m_readBytesUpdateCallback = null;
    private ReadBytesErrorCallback m_readBytesErrorCallback = null;

    private LoadAssetBundleCompleteCallback m_loadAssetBundleCompleteCallback = null;
    private LoadAssetBundleUpdateCallback m_loadAssetBundleUpdateCallback = null;
    private LoadAssetBundleErrorCallback m_loadAssetBundleErrorCallback = null;

    private LoadAssetCompleteCallback m_loadAssetCompleteCallback = null;
    private LoadAssetUpdateCallback m_loadAssetUpdateCallback = null;
    private LoadAssetErrorCallback m_loadAssetErrorCallback = null;

    private DecryptResourceCallback m_decryptResourceCallback = null;                           // 解密方法

    public bool LoadFinish
    {
        get
        {
            if (m_FileAssetBundleCreateRequest != null)
            {
                if (!m_FileAssetBundleCreateRequest.isDone)
                {
                    return false;
                }
            }

            if (m_BytesAssetBundleCreateRequest != null)
            {
                if (!m_BytesAssetBundleCreateRequest.isDone)
                {
                    return false;
                }
            }

            if (m_AssetBundleRequest != null)
            {
                if (!m_AssetBundleRequest.isDone)
                {
                    return false;
                }
            }

            if (m_AsyncOperation != null)
            {
                if (!m_AsyncOperation.isDone)
                {
                    return false;
                }
            }

            if (m_ResourceRequest != null)
            {
                if (!m_ResourceRequest.isDone)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public void Reset()
    {
        if (m_UnityWebRequest != null)
        {
            m_UnityWebRequest.Dispose();
            m_UnityWebRequest = null;
        }

        if (m_WWW != null)
        {
            m_WWW.Dispose();
            m_WWW = null;
        }

        m_FileAssetBundleCreateRequest = null;
        m_BytesAssetBundleCreateRequest = null;
        m_AssetBundleRequest = null;
        m_AsyncOperation = null;
        m_ResourceRequest = null;

        m_readBytesCompleteCallback = null;
        m_readBytesUpdateCallback = null;
        m_readBytesErrorCallback = null;
        m_loadAssetBundleCompleteCallback = null;
        m_loadAssetBundleUpdateCallback = null;
        m_loadAssetBundleErrorCallback = null;
        m_loadAssetCompleteCallback = null;
        m_loadAssetUpdateCallback = null;
        m_loadAssetErrorCallback = null;
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (m_Disposed)
        {
            return;
        }

        if (disposing)
        {
            if (m_UnityWebRequest != null)
            {
                m_UnityWebRequest.Dispose();
                m_UnityWebRequest = null;
            }

            if (m_WWW != null)
            {
                m_WWW.Dispose();
                m_WWW = null;
            }
        }

        m_Disposed = true;
    }

    public void OnReadBytesAsync(string assetName, enResPathType loadType, ReadBytesCompleteCallback completeCallback, ReadBytesUpdateCallback updateCallback = null, ReadBytesErrorCallback errorCallback = null,
        DecryptResourceCallback decryptResourceCallback = null)
    {
        m_readBytesCompleteCallback = completeCallback;
        m_readBytesUpdateCallback = updateCallback;
        m_readBytesErrorCallback = errorCallback;

        switch (loadType)
        {
            case enResPathType.LoadPathFromOnlyRead:
                m_BytesFullPath = Application.streamingAssetsPath;
                break;
            case enResPathType.LoadPathFromReadWrite:
                m_BytesFullPath = Application.persistentDataPath;
                break;
            case enResPathType.LoadPathFromDirctory:
                m_BytesFullPath = Ctrl.device.PathRoot;
                break;
        }
        m_BytesFullPath = m_BytesFullPath + assetName;

#if UNITY_5_4_OR_NEWER
        m_UnityWebRequest = UnityWebRequest.Get(Utility.ZPath.GetRemotePath(m_BytesFullPath));
#if UNITY_2017_2_OR_NEWER
        m_UnityWebRequest.SendWebRequest();
#else
        m_UnityWebRequest.Send();
#endif
#else
        m_WWW = new WWW(Utility.ZPath.GetRemotePath(m_BytesFullPath));
#endif
    }

    public void OnLoadAssetAsync(string assetName, Type assetType, bool isScene, bool isResource, enResPathType loadType, LoadAssetCompleteCallback completeCallback, LoadAssetUpdateCallback updateCallback = null,
        LoadAssetErrorCallback errorCallback = null, DecryptResourceCallback decryptResourceCallback = null)
    {
        m_AssetName = assetName;
        m_AssetType = assetType;
        m_IsScene = isScene;

        m_loadAssetCompleteCallback = completeCallback;
        m_loadAssetUpdateCallback = updateCallback;
        m_loadAssetErrorCallback = errorCallback;

        if (isResource)
        {
            m_ResourceRequest = Resources.LoadAsync(assetName);
        }
        else
        {
            switch (loadType)
            {
                case enResPathType.LoadPathFromOnlyRead:
                    m_FileFullPath = Application.streamingAssetsPath;
                    break;
                case enResPathType.LoadPathFromReadWrite:
                    m_FileFullPath = Application.persistentDataPath;
                    break;
                case enResPathType.LoadPathFromDirctory:
                    m_FileFullPath = Ctrl.device.PathRoot;
                    break;
            }
            m_FileFullPath = m_FileFullPath + assetName;

            if (decryptResourceCallback == null)
            {
                m_FileAssetBundleCreateRequest = UnityEngine.AssetBundle.LoadFromFileAsync(m_FileFullPath);
            }
            else
            {
                m_decryptResourceCallback = decryptResourceCallback;
                OnReadBytesAsync(assetName, loadType, ReadBytesComplete, ReadBytesUpdate, ReadBytesError);
            }
        }
    }

    private void LoadAssetAsync(AssetBundle assetBundle, string assetName, Type assetType, bool isScene)
    {
        if (assetBundle == null)
        {
            Log.Error("Can not load asset bundle from loaded resource which is not an asset bundle.");
            if (m_loadAssetErrorCallback != null)
            {
                m_loadAssetErrorCallback(enLoadResStatus.TypeError);
            }
            return;
        }

        if (string.IsNullOrEmpty(assetName))
        {
            Log.Error("Can not load asset from asset bundle which child name is invalid.");
            if (m_loadAssetErrorCallback != null)
            {
                m_loadAssetErrorCallback(enLoadResStatus.AssetError);
            }
            return;
        }

        if (isScene)
        {
            int sceneNamePositionStart = assetName.LastIndexOf('/');
            int sceneNamePositionEnd = assetName.LastIndexOf('.');
            if (sceneNamePositionStart <= 0 || sceneNamePositionEnd <= 0 || sceneNamePositionStart > sceneNamePositionEnd)
            {
                if (m_loadAssetErrorCallback != null)
                {
                    m_loadAssetErrorCallback(enLoadResStatus.AssetError);
                }
                return;
            }

            string sceneName = assetName.Substring(sceneNamePositionStart + 1, sceneNamePositionEnd - sceneNamePositionStart - 1);
            m_AsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        else
        {
            if (assetType != null)
            {
                m_AssetBundleRequest = assetBundle.LoadAssetAsync(assetName, assetType);
            }
            else
            {
                m_AssetBundleRequest = assetBundle.LoadAssetAsync(assetName);
            }
        }
    }

    private void ReadBytesComplete(byte[] bytes)
    {
        m_BytesAssetBundleCreateRequest = UnityEngine.AssetBundle.LoadFromMemoryAsync(bytes);
    }

    private void ReadBytesUpdate(float progress)
    {

    }

    private void ReadBytesError(enLoadResStatus status)
    {

    }

    private void Update()
    {
#if UNITY_5_4_OR_NEWER
        UpdateUnityWebRequest();
#else
        UpdateWWW();
#endif
        UpdateFileAssetBundleCreateRequest();
        UpdateBytesAssetBundleCreateRequest();
        UpdateResourceRequest();
        UpdateAssetBundleRequest();
        UpdateAsyncOperation();
    }

    private void UpdateUnityWebRequest()
    {
        if (m_UnityWebRequest != null)
        {
            if (m_UnityWebRequest.isDone)
            {
                if (string.IsNullOrEmpty(m_UnityWebRequest.error))
                {
                    m_readBytesCompleteCallback?.Invoke(m_UnityWebRequest.downloadHandler.data);

                    m_UnityWebRequest.Dispose();
                    m_UnityWebRequest = null;
                    m_BytesFullPath = null;
                    m_LastProgress = 0f;
                }
                else
                {
                    bool isError = false;
#if UNITY_2017_1_OR_NEWER
                    isError = m_UnityWebRequest.isNetworkError;
#else
                    isError = m_UnityWebRequest.isError;
#endif
                    Log.Error(Utility.ZText.Format("Can not load asset bundle '{0}' with error message '{1}'.", m_BytesFullPath, isError ? m_UnityWebRequest.error : null));
                    if (m_readBytesErrorCallback != null)
                    {
                        m_readBytesErrorCallback(enLoadResStatus.NotExist);
                    }
                }
            }
            else if (m_UnityWebRequest.downloadProgress != m_LastProgress)
            {
                m_LastProgress = m_UnityWebRequest.downloadProgress;
                if (m_readBytesUpdateCallback != null)
                {
                    m_readBytesUpdateCallback(m_UnityWebRequest.downloadProgress);
                }
            }
        }
    }

    private void UpdateWWW()
    {
        if (m_WWW != null)
        {
            if (m_WWW.isDone)
            {
                if (string.IsNullOrEmpty(m_WWW.error))
                {
                    if (m_readBytesCompleteCallback != null)
                    {
                        m_readBytesCompleteCallback(m_WWW.bytes);
                    }
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load asset bundle '{0}' with error message '{1}'.", m_BytesFullPath, m_WWW.error));

                    if (m_readBytesErrorCallback != null)
                    {
                        m_readBytesErrorCallback(enLoadResStatus.NotExist);
                    }
                }

                m_WWW.Dispose();
                m_WWW = null;
                m_BytesFullPath = null;
                m_LastProgress = 0f;
            }
            else
            {
                m_LastProgress = m_WWW.progress;

                if (m_readBytesUpdateCallback != null)
                {
                    m_readBytesUpdateCallback(m_LastProgress);
                }
            }
        }
    }

    private void UpdateFileAssetBundleCreateRequest()
    {
        if (m_FileAssetBundleCreateRequest != null)
        {
            if (m_FileAssetBundleCreateRequest.isDone)
            {
                AssetBundle assetBundle = m_FileAssetBundleCreateRequest.assetBundle;
                if (assetBundle != null)
                {
                    m_loadAssetBundleCompleteCallback?.Invoke(assetBundle);
                    LoadAssetAsync(assetBundle, m_AssetName, m_AssetType, m_IsScene);
                    m_FileAssetBundleCreateRequest = null;
                    m_LastProgress = 0f;
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load asset bundle from file '{0}' which is not a valid asset bundle.", m_FileFullPath));
                    m_loadAssetBundleErrorCallback?.Invoke(enLoadResStatus.NotExist);
                }
            }
            else if (m_FileAssetBundleCreateRequest.progress != m_LastProgress)
            {
                m_LastProgress = m_FileAssetBundleCreateRequest.progress;
                m_loadAssetBundleUpdateCallback?.Invoke(m_LastProgress);
            }
        }
    }

    private void UpdateBytesAssetBundleCreateRequest()
    {
        if (m_BytesAssetBundleCreateRequest != null)
        {
            if (m_BytesAssetBundleCreateRequest.isDone)
            {
                UnityEngine.AssetBundle assetBundle = m_BytesAssetBundleCreateRequest.assetBundle;
                if (assetBundle != null)
                {
                    if (m_loadAssetBundleCompleteCallback != null)
                    {
                        m_loadAssetBundleCompleteCallback(assetBundle);
                    }
                    LoadAssetAsync(assetBundle, m_AssetName, m_AssetType, m_IsScene);
                    m_BytesAssetBundleCreateRequest = null;
                    m_LastProgress = 0f;
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load asset bundle from file '{0}' which is not a valid asset bundle.", m_FileFullPath));
                    if (m_loadAssetBundleErrorCallback != null)
                    {
                        m_loadAssetBundleErrorCallback(enLoadResStatus.NotExist);
                    }
                }
            }
            else if (m_BytesAssetBundleCreateRequest.progress != m_LastProgress)
            {
                m_LastProgress = m_BytesAssetBundleCreateRequest.progress;
                if (m_loadAssetBundleUpdateCallback != null)
                {
                    m_loadAssetBundleUpdateCallback(m_LastProgress);
                }
            }
        }
    }

    private void UpdateResourceRequest()
    {
        if (m_ResourceRequest != null)
        {
            if (m_ResourceRequest.isDone)
            {
                if (m_ResourceRequest.asset != null)
                {
                    if (m_loadAssetCompleteCallback != null)
                    {
                        m_loadAssetCompleteCallback(m_ResourceRequest.asset);
                    }
                    m_AssetName = null;
                    m_LastProgress = 0f;
                    m_ResourceRequest = null;
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load resource '{0}'.", m_AssetName));
                    if (m_loadAssetErrorCallback != null)
                    {
                        m_loadAssetErrorCallback(enLoadResStatus.AssetError);
                    }
                }
            }
            else if (m_ResourceRequest.progress != m_LastProgress)
            {
                m_LastProgress = m_ResourceRequest.progress;
                if (m_loadAssetUpdateCallback != null)
                {
                    m_loadAssetUpdateCallback(m_LastProgress);
                }
            }
        }
    }

    private void UpdateAssetBundleRequest()
    {
        if (m_AssetBundleRequest != null)
        {
            if (m_AssetBundleRequest.isDone)
            {
                if (m_AssetBundleRequest.asset != null)
                {
                    if (m_loadAssetCompleteCallback != null)
                    {
                        m_loadAssetCompleteCallback(m_AssetBundleRequest.asset);
                    }
                    m_AssetName = null;
                    m_LastProgress = 0f;
                    m_AssetBundleRequest = null;
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load asset '{0}' from asset bundle which is not exist.", m_AssetName));
                    if (m_loadAssetErrorCallback != null)
                    {
                        m_loadAssetErrorCallback(enLoadResStatus.AssetError);
                    }
                }
            }
            else if (m_AssetBundleRequest.progress != m_LastProgress)
            {
                m_LastProgress = m_AssetBundleRequest.progress;
                if (m_loadAssetUpdateCallback != null)
                {
                    m_loadAssetUpdateCallback(m_LastProgress);
                }
            }
        }
    }

    private void UpdateAsyncOperation()
    {
        if (m_AsyncOperation != null)
        {
            if (m_AsyncOperation.isDone)
            {
                if (m_AsyncOperation.allowSceneActivation)
                {
                    if (m_loadAssetCompleteCallback != null)
                    {
                        m_loadAssetCompleteCallback(new UnityEngine.Object());
                    }
                    m_AssetName = null;
                    m_LastProgress = 0f;
                    m_AsyncOperation = null;
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load scene asset '{0}' from asset bundle.", m_AssetName));
                    if (m_loadAssetErrorCallback != null)
                    {
                        m_loadAssetErrorCallback(enLoadResStatus.AssetError);
                    }
                }
            }
            else if (m_AsyncOperation.progress != m_LastProgress)
            {
                m_LastProgress = m_AsyncOperation.progress;
                if (m_loadAssetUpdateCallback != null)
                {
                    m_loadAssetUpdateCallback(m_LastProgress);
                }
            }
        }
    }
}