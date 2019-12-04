/*****************************************************
 * 文件名:ResHelperByte.cs
 * 文件描述:资源加载辅助器类 - Byte
 * 创建日期:2019/11/24
 * 作者:ZB
 *****************************************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Res
{
    public class ResHelperByte : MonoBehaviour
    {
        public delegate void CompleteCallback(byte[] bytes);
        public delegate void UpdateCallback(float progress);
        public delegate void ErrorCallback(enLoadResStatus status);

        private CompleteCallback m_completeCallback = null;
        private UpdateCallback m_updateCallback = null;
        private ErrorCallback m_errorCallback = null;

        private UnityWebRequest m_unityWebRequest = null;                           // 使用UnityWebRequest从磁盘中读取文件
        private WWW m_www = null;                                                   // 使用www从磁盘中读取文件
        private float m_lastProgress = 0f;
        private string m_fullPath = null;

        public bool IsFinish
        {
            get
            {
                if (m_unityWebRequest != null)
                {
                    return m_unityWebRequest.isDone;
                }

                if (m_www != null)
                {
                    return m_www.isDone;
                }

                return true;
            }
        }

        public void OnLoadAsync(string assetName, CompleteCallback complete, UpdateCallback update = null, ErrorCallback error = null)
        {
#if UNITY_5_4_OR_NEWER
            m_unityWebRequest = UnityWebRequest.Get(Utility.ZPath.GetRemotePath(m_fullPath));
#if UNITY_2017_2_OR_NEWER
            m_unityWebRequest.SendWebRequest();
#else
            m_unityWebRequest.Send();
#endif
#else
            m_www = new WWW(Utility.ZPath.GetRemotePath(m_fullPath));
#endif

            m_completeCallback = complete;
            m_updateCallback = update;
            m_errorCallback = error;
        }

        private void Reset()
        {
            if (m_unityWebRequest != null)
            {
                m_unityWebRequest.Dispose();
                m_unityWebRequest = null;
            }

            if (m_www != null)
            {
                m_www.Dispose();
                m_www = null;
            }
        }

        private void Update()
        {
            UpdateUnityWebRequest();
            UpdateWWW();
        }

        private void UpdateUnityWebRequest()
        {
            if (m_unityWebRequest != null)
            {
                if (m_unityWebRequest.isDone)
                {
                    if (string.IsNullOrEmpty(m_unityWebRequest.error))
                    {
                        m_completeCallback?.Invoke(m_unityWebRequest.downloadHandler.data);

                        m_unityWebRequest.Dispose();
                        m_unityWebRequest = null;
                        m_fullPath = null;
                        m_lastProgress = 0f;
                    }
                    else
                    {
                        bool isError = false;
#if UNITY_2017_1_OR_NEWER
                        isError = m_unityWebRequest.isNetworkError;
#else
                        isError = m_unityWebRequest.isError;
#endif
                        Log.Error(Utility.ZText.Format("Can not load asset bundle '{0}' with error message '{1}'.", m_fullPath, isError ? m_unityWebRequest.error : null));
                        m_errorCallback?.Invoke(enLoadResStatus.NotExist);
                    }
                }
                else if (m_unityWebRequest.downloadProgress != m_lastProgress)
                {
                    m_lastProgress = m_unityWebRequest.downloadProgress;
                    m_updateCallback?.Invoke(m_unityWebRequest.downloadProgress);
                }
            }
        }

        private void UpdateWWW()
        {
            if (m_www != null)
            {
                if (m_www.isDone)
                {
                    if (string.IsNullOrEmpty(m_www.error))
                    {
                        m_completeCallback?.Invoke(m_www.bytes);
                    }
                    else
                    {
                        Log.Error(Utility.ZText.Format("Can not load asset bundle '{0}' with error message '{1}'.", m_fullPath, m_www.error));

                        m_errorCallback?.Invoke(enLoadResStatus.NotExist);
                    }

                    m_www.Dispose();
                    m_www = null;
                    m_fullPath = null;
                    m_lastProgress = 0f;
                }
                else
                {
                    m_lastProgress = m_www.progress;

                    m_updateCallback?.Invoke(m_lastProgress);
                }
            }
        }
    }
}