/*****************************************************
 * 文件名:ResHelperResources.cs
 * 文件描述:资源加载辅助器类 - Resources
 * 创建日期:2019/11/24
 * 作者:ZB
 *****************************************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Res
{
    public class ResHelperResources : MonoBehaviour
    {
        public delegate void CompleteCallback(UnityEngine.Object asset);
        public delegate void UpdateCallback(float progress);
        public delegate void ErrorCallback(enLoadResStatus status);

        private CompleteCallback m_completeCallback = null;
        private UpdateCallback m_updateCallback = null;
        private ErrorCallback m_errorCallback = null;

        private ResourceRequest m_resourceRequest = null;
        private string m_assetName = null;
        private float m_lastProgress = 0f;

        public bool IsFinish
        {
            get
            {
                if (m_resourceRequest != null)
                {
                    return m_resourceRequest.isDone;
                }

                return true;
            }
        }

        public void OnLoadAsync(string assetName, Type assetType, CompleteCallback complete, UpdateCallback update = null, ErrorCallback error = null)
        {
            m_resourceRequest = Resources.LoadAsync(assetName);

            m_completeCallback = complete;
            m_updateCallback = update;
            m_errorCallback = error;
        }

        private void Reset()
        {
            m_resourceRequest = null;
        }

        private void Update()
        {
            UpdateResourceRequest();
        }

        private void UpdateResourceRequest()
        {
            if (m_resourceRequest != null)
            {
                if (m_resourceRequest.isDone)
                {
                    if (m_resourceRequest.asset != null)
                    {
                        m_completeCallback?.Invoke(m_resourceRequest.asset);

                        m_assetName = null;
                        m_lastProgress = 0f;
                        m_resourceRequest = null;
                    }
                    else
                    {
                        Log.Error(Utility.ZText.Format("Can not load resource '{0}'.", m_assetName));

                        m_errorCallback?.Invoke(enLoadResStatus.AssetError);
                    }
                }
                else if (m_resourceRequest.progress != m_lastProgress)
                {
                    m_lastProgress = m_resourceRequest.progress;

                    m_updateCallback?.Invoke(m_lastProgress);
                }
            }
        }
    }
}