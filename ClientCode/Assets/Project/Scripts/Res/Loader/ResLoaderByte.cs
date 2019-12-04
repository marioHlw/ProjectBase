/*****************************************************
 * 文件名:ResLoaderByte.cs
 * 文件描述:资源加载器类 - Byte
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
    public class ResLoaderByte : ResLoaderBase
    {
        private class Info
        {
            public string assetName;
            public ResHelperByte.CompleteCallback completeCallback = null;
            public ResHelperByte.UpdateCallback updateCallback = null;
            public ResHelperByte.ErrorCallback errorCallback = null;
        }

        private Dictionary<string, UnityEngine.Object> m_resMap = new Dictionary<string, UnityEngine.Object>();                 // Byte资源
        private Dictionary<string, int> m_refCountMap = new Dictionary<string, int>();                                          // Byte资源引用计数

        private List<ResHelperByte> m_freeHelpers = new List<ResHelperByte>();                                                  // 空闲中的资源加载辅助器
        private List<ResHelperByte> m_useHelpers = new List<ResHelperByte>();                                                   // 使用中的资源加载辅助器

        private List<Info> m_waitLoadInfos = new List<Info>();                                                                  // 等待加载资源信息
        private int m_helperCount = 3;
        private Transform m_parent;

        public override void OnInit()
        {
            base.OnInit();

            GameObject _obj = new GameObject("Res Helper Byte");
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

                    m_freeHelpers[0].OnLoadAsync(m_waitLoadInfos[0].assetName, m_waitLoadInfos[0].completeCallback, m_waitLoadInfos[0].updateCallback, m_waitLoadInfos[0].errorCallback);

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
            return null;
        }

        public void OnLoadAsync(string assetName, ResHelperByte.CompleteCallback complete, ResHelperByte.UpdateCallback update = null, ResHelperByte.ErrorCallback error = null)
        {
            m_waitLoadInfos.Add(new Info()
            {
                assetName = assetName,
                completeCallback = complete,
                updateCallback = update,
                errorCallback = error,
            });
        }

        private ResHelperByte AddHelper()
        {
            ResHelperByte _helper = (new GameObject()).AddComponent<ResHelperByte>();

            _helper.name = Utility.ZText.Format("Byte Load Agent Helper - {0}", m_freeHelpers.Count + 1);
            Transform transform = _helper.transform;
            transform.SetParent(m_parent);
            transform.localScale = Vector3.one;

            return _helper;
        }
    }
}