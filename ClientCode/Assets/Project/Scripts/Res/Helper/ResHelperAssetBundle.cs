/*****************************************************
 * 文件名:ResHelperAssetBundle.cs
 * 文件描述:资源加载辅助器类 - AssetBundle
 * 创建日期:2019/11/24
 * 作者:ZB
 *****************************************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResHelperAssetBundle : MonoBehaviour
{
    public delegate void CompleteCallback(UnityEngine.Object asset);
    public delegate void UpdateCallback(float progress);
    public delegate void ErrorCallback(enLoadResStatus status);

    public delegate void AssetBundleLoadCompleteCallback(string assetPath, AssetBundle assetBundle);
    public delegate void AssetBundleUseCompleteCallback(string assetPath, string assetName);

    private CompleteCallback m_completeCallback = null;
    private UpdateCallback m_updateCallback = null;
    private ErrorCallback m_errorCallback = null;

    private AssetBundleLoadCompleteCallback m_assetBundleLoadCompleteCallback = null;
    private AssetBundleUseCompleteCallback m_assetBundleUseCompleteCallback = null;

    private AssetBundleCreateRequest m_fileAssetBundleCreateRequest = null;     // 异步读取AssetBundle文件
    private AssetBundleCreateRequest m_bytesAssetBundleCreateRequest = null;    // 异步将二进制流解析成AssetBundle中文件
    private AssetBundleRequest m_assetBundleRequest = null;                     // 加载资源
    private AsyncOperation m_asyncOperation = null;                             // 加载场景

    private float m_lastProgress = 0f;
    private string m_assetName = null;
    private Type m_assetType = null;
    private bool m_isScene = false;
    private string m_assetBundlePath = null;

    public bool IsFinish
    {
        get
        {
            if (m_assetBundleRequest != null)
            {
                return m_assetBundleRequest.isDone;
            }

            if (m_asyncOperation != null)
            {
                return m_asyncOperation.isDone;
            }

            return true;
        }
    }

    public void OnLoadAsync(string assetBundlePath, string assetName, Type assetType, bool isScene, AssetBundleLoadCompleteCallback assetBundleLoadCompleteCallback,
        AssetBundleUseCompleteCallback assetBundleUseCompleteCallback, CompleteCallback complete, UpdateCallback update = null, ErrorCallback error = null)
    {
        m_assetBundlePath = assetBundlePath;
        m_assetName = assetName;
        m_assetType = assetType;
        m_isScene = isScene;

        m_assetBundleLoadCompleteCallback = assetBundleLoadCompleteCallback;
        m_assetBundleUseCompleteCallback = assetBundleUseCompleteCallback;
        m_completeCallback = complete;
        m_updateCallback = update;
        m_errorCallback = error;

        Log.Info("加载AssetBundle：{0}", assetBundlePath);
        m_fileAssetBundleCreateRequest = AssetBundle.LoadFromFileAsync(m_assetBundlePath);
    }

    public void OnLoadAsync(AssetBundle assetBundle, string assetName, Type assetType, bool isScene, AssetBundleUseCompleteCallback assetBundleUseCompleteCallback,
        CompleteCallback complete, UpdateCallback update = null, ErrorCallback error = null)
    {
        m_assetName = assetName;
        m_assetType = assetType;
        m_isScene = isScene;

        m_completeCallback = complete;
        m_updateCallback = update;
        m_errorCallback = error;

        m_assetBundleUseCompleteCallback = assetBundleUseCompleteCallback;

        if (!isScene)
        {
            LoadAssetAsync(assetBundle, assetName, assetType, isScene);
        }
    }

    private void Reset()
    {
        m_lastProgress = 0f;
        m_assetName = null;
        m_assetType = null;
        m_isScene = false;
        m_assetBundlePath = null;

        m_fileAssetBundleCreateRequest = null;
        m_bytesAssetBundleCreateRequest = null;
        m_assetBundleRequest = null;
        m_asyncOperation = null;
    }

    private void Update()
    {
        UpdateFileAssetBundleCreateRequest();
        UpdateBytesAssetBundleCreateRequest();
        UpdateAssetBundleRequest();
        UpdateAsyncOperation();
    }

    private void LoadAssetAsync(AssetBundle assetBundle, string assetName, Type assetType, bool isScene)
    {
        if (assetBundle == null)
        {
            Log.Error("Can not load asset bundle from loaded resource which is not an asset bundle.");
            m_errorCallback?.Invoke(enLoadResStatus.TypeError);
            return;
        }

        if (string.IsNullOrEmpty(assetName))
        {
            Log.Error("Can not load asset from asset bundle which child name is invalid.");
            m_errorCallback?.Invoke(enLoadResStatus.AssetError);
            return;
        }

        Log.Info("从{0}中加载资源：{1}", assetBundle.name, assetName);

        if (isScene)
        {
            int sceneNamePositionStart = assetName.LastIndexOf('/');
            int sceneNamePositionEnd = assetName.LastIndexOf('.');
            if (sceneNamePositionStart <= 0 || sceneNamePositionEnd <= 0 || sceneNamePositionStart > sceneNamePositionEnd)
            {
                m_errorCallback?.Invoke(enLoadResStatus.AssetError);
                return;
            }

            string sceneName = assetName.Substring(sceneNamePositionStart + 1, sceneNamePositionEnd - sceneNamePositionStart - 1);
            m_asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        else
        {
            if (assetType != null)
            {
                m_assetBundleRequest = assetBundle.LoadAssetAsync(assetName, assetType);
            }
            else
            {
                m_assetBundleRequest = assetBundle.LoadAssetAsync(assetName);
            }
        }
    }

    private void UpdateFileAssetBundleCreateRequest()
    {
        if (m_fileAssetBundleCreateRequest != null)
        {
            if (m_fileAssetBundleCreateRequest.isDone)
            {
                AssetBundle _assetBundle = m_fileAssetBundleCreateRequest.assetBundle;
                if (_assetBundle != null)
                {
                    m_assetBundleLoadCompleteCallback?.Invoke(m_assetBundlePath, _assetBundle);

                    LoadAssetAsync(_assetBundle, m_assetName, m_assetType, m_isScene);
                    m_fileAssetBundleCreateRequest = null;
                    m_assetBundleLoadCompleteCallback = null;
                    m_lastProgress = 0;
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load asset bundle from file '{0}' which is not a valid asset bundle.", m_assetBundlePath));
                    m_errorCallback?.Invoke(enLoadResStatus.NotExist);
                }
            }
            else if (m_fileAssetBundleCreateRequest.progress != m_lastProgress)
            {
                m_lastProgress = m_fileAssetBundleCreateRequest.progress;
            }
        }
    }

    private void UpdateBytesAssetBundleCreateRequest()
    {
        if (m_bytesAssetBundleCreateRequest != null)
        {
            if (m_bytesAssetBundleCreateRequest.isDone)
            {
                AssetBundle _assetBundle = m_bytesAssetBundleCreateRequest.assetBundle;
                if (_assetBundle != null)
                {
                    m_assetBundleLoadCompleteCallback?.Invoke(m_assetBundlePath, _assetBundle);

                    LoadAssetAsync(_assetBundle, m_assetName, m_assetType, m_isScene);
                    m_bytesAssetBundleCreateRequest = null;
                    m_assetBundleLoadCompleteCallback = null;
                    m_lastProgress = 0f;
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load asset bundle from file '{0}' which is not a valid asset bundle.", m_assetBundlePath));
                    m_errorCallback?.Invoke(enLoadResStatus.NotExist);
                }
            }
            else if (m_bytesAssetBundleCreateRequest.progress != m_lastProgress)
            {
                m_lastProgress = m_bytesAssetBundleCreateRequest.progress;
            }
        }
    }

    private void UpdateAssetBundleRequest()
    {
        if (m_assetBundleRequest != null)
        {
            if (m_assetBundleRequest.isDone)
            {
                if (m_assetBundleRequest.asset != null)
                {
                    m_assetBundleUseCompleteCallback?.Invoke(m_assetBundlePath, m_assetName);
                    m_completeCallback?.Invoke(m_assetBundleRequest.asset);

                    m_assetName = null;
                    m_lastProgress = 0f;
                    m_assetBundleRequest = null;
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load asset '{0}' from asset bundle which is not exist.", m_assetName));
                    m_errorCallback?.Invoke(enLoadResStatus.AssetError);
                }
            }
            else if (m_assetBundleRequest.progress != m_lastProgress)
            {
                m_lastProgress = m_assetBundleRequest.progress;
                m_updateCallback?.Invoke(m_lastProgress);
            }
        }
    }

    private void UpdateAsyncOperation()
    {
        if (m_asyncOperation != null)
        {
            if (m_asyncOperation.isDone)
            {
                if (m_asyncOperation.allowSceneActivation)
                {
                    m_assetBundleUseCompleteCallback?.Invoke(m_assetBundlePath, m_assetName);
                    m_completeCallback?.Invoke(new UnityEngine.Object());

                    m_assetName = null;
                    m_lastProgress = 0f;
                    m_asyncOperation = null;
                }
                else
                {
                    Log.Error(Utility.ZText.Format("Can not load scene asset '{0}' from asset bundle.", m_assetName));
                    m_errorCallback?.Invoke(enLoadResStatus.AssetError);
                }
            }
            else if (m_asyncOperation.progress != m_lastProgress)
            {
                m_lastProgress = m_asyncOperation.progress;
                m_updateCallback?.Invoke(m_lastProgress);
            }
        }
    }
}