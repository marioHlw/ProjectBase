/*****************************************************
 * 文件名:ResLoaderResources.cs
 * 文件描述:资源加载器类 - Resources
 * 创建日期:2019/11/24
 * 作者:ZB
 *****************************************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Res
{
    public class ResLoaderResources : ResLoaderBase
    {
        private class Info
        {
            public string assetName;
            public Type assetType;
            public ResHelperResources.CompleteCallback completeCallback = null;
            public ResHelperResources.UpdateCallback updateCallback = null;
            public ResHelperResources.ErrorCallback errorCallback = null;
        }

        private Dictionary<string, UnityEngine.Object> m_resMap = new Dictionary<string, UnityEngine.Object>();                 // Resources目录资源
        private Dictionary<string, int> m_refCountMap = new Dictionary<string, int>();                                          // Resources目录资源引用计数

        private List<ResHelperResources> m_freeHelpers = new List<ResHelperResources>();                                        // 空闲中的资源加载辅助器
        private List<ResHelperResources> m_useHelpers = new List<ResHelperResources>();                                         // 使用中的资源加载辅助器

        private List<Info> m_waitLoadInfos = new List<Info>();                                                                  // 等待加载资源信息
        private int m_helperCount = 3;
        private Transform m_parent;

        public override void OnInit()
        {
            base.OnInit();

            GameObject _obj = new GameObject("Res Helper Resources");
            _obj.transform.SetParent(Ctrl.resourceComponent.transform);
            _obj.transform.localScale = Vector3.one;
            m_parent = _obj.transform;

            for (int i = 0; i < m_helperCount; i++)
            {
                m_freeHelpers.Add(AddHelper());
            }
        }

        public override void OnUnInit()
        {
            base.OnUnInit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            // 需要增加
            int _totalCount = m_useHelpers.Count + m_freeHelpers.Count;
            if (_totalCount < m_helperCount)
            {
                for (int i = 0; i < m_helperCount - _totalCount; i++)
                {
                    m_freeHelpers.Add(AddHelper());
                }
            }

            for (int i = m_useHelpers.Count - 1; i >= 0; i--)
            {
                if (m_useHelpers[i].IsFinish)
                {
                    m_freeHelpers.Add(m_useHelpers[i]);
                    m_useHelpers.RemoveAt(i);
                }
            }

            if (m_waitLoadInfos.Count > 0 && m_freeHelpers.Count > 0)
            {
                while (m_freeHelpers.Count > 0)
                {
                    if (m_waitLoadInfos.Count <= 0)
                    {
                        break;
                    }

                    m_freeHelpers[0].OnLoadAsync(m_waitLoadInfos[0].assetName, m_waitLoadInfos[0].assetType,
                        m_waitLoadInfos[0].completeCallback, m_waitLoadInfos[0].updateCallback, m_waitLoadInfos[0].errorCallback);

                    m_useHelpers.Add(m_freeHelpers[0]);
                    m_freeHelpers.RemoveAt(0);
                    m_waitLoadInfos.RemoveAt(0);
                }
            }
        }

        // 设置 - 资源加载辅助器数量
        // 注意：设置后不会立马生效，如果在使用中，会等到加载完成后注销
        public void SetHelperCount(int count)
        {
            m_helperCount = count;
        }

        public UnityEngine.Object OnLoad(string assetName, Type assetType)
        {
            if (m_resMap.ContainsKey(assetName))
            {
                return m_resMap[assetName];
            }

            if (assetType != null)
            {
                m_resMap[assetName] = Resources.Load(assetName, assetType);
                return m_resMap[assetName];
            }
            else
            {
                m_resMap[assetName] = Resources.Load(assetName);
                return m_resMap[assetName];
            }
        }

        public void OnLoadAsync(string assetName, Type assetType,
            ResHelperResources.CompleteCallback complete, ResHelperResources.UpdateCallback update = null, ResHelperResources.ErrorCallback error = null)
        {
            m_waitLoadInfos.Add(new Info()
            {
                assetName = assetName,
                assetType = assetType,
                completeCallback = complete,
                updateCallback = update,
                errorCallback = error,
            });
        }

        private ResHelperResources AddHelper()
        {
            ResHelperResources _helper = (new GameObject()).AddComponent<ResHelperResources>();

            _helper.name = Utility.ZText.Format("Resource Load Agent Helper - {0}", m_freeHelpers.Count + 1);
            Transform transform = _helper.transform;
            transform.SetParent(m_parent);
            transform.localScale = Vector3.one;

            return _helper;
        }
    }
}