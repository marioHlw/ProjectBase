/*****************************************************
 * 文件名:ResLoaderAssetBundle.cs
 * 文件描述:资源加载器类 - AssetBundle
 * 创建日期:2019/11/24
 * 作者:ZB
 *****************************************************/



using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Res
{
    public class ResLoaderAssetBundle : ResLoaderBase
    {
        private class Info
        {
            public string assetPath;
            public string assetName;
            public Type assetType;
            public bool isScene;
            public Stopwatch stopwatch;
            public ResHelperAssetBundle.CompleteCallback completeCallback = null;
            public ResHelperAssetBundle.UpdateCallback updateCallback = null;
            public ResHelperAssetBundle.ErrorCallback errorCallback = null;
        }

        private Dictionary<string, AssetBundle> m_assetBundleMap = new Dictionary<string, AssetBundle>();                       // 已经加载完成的AssetBundle
        private Dictionary<string, int> m_assetBundleRefMap = new Dictionary<string, int>();                                    // AssetBundle资源引用计数
        private Dictionary<string, bool> m_assetBundleLoadings = new Dictionary<string, bool>();                                // 正在加载中的AssetBundle
        private Dictionary<string, bool> m_assetBundleUsings = new Dictionary<string, bool>();                                  // 正在使用中的AssetBundle

        private List<ResHelperAssetBundle> m_freeHelpers = new List<ResHelperAssetBundle>();                                    // 空闲中的资源加载辅助器
        private List<ResHelperAssetBundle> m_useHelpers = new List<ResHelperAssetBundle>();                                     // 使用中的资源加载辅助器

        private List<Info> m_waitLoadInfos = new List<Info>();                                                                  // 等待加载资源信息
        private List<Info> m_loadingInfos = new List<Info>();                                                                   // 正在加载资源信息
        private int m_helperCount = 3;
        private Transform m_parent;

        public override void OnInit()
        {
            base.OnInit();

            GameObject _obj = new GameObject("Res Helper AssetBundle");
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

            // 设置资源加载辅助器数量
            int _totalCount = m_useHelpers.Count + m_freeHelpers.Count;
            if (_totalCount < m_helperCount)
            {
                for (int i = 0; i < m_helperCount - _totalCount; i++)
                {
                    m_freeHelpers.Add(AddHelper());
                }
            }
            else
            {
                for (int i = 0, max = _totalCount - m_helperCount; i < max; i++)
                {
                    if (m_freeHelpers.Count > 0)
                    {
                        GameObject.DestroyImmediate(m_freeHelpers[0].gameObject);
                        m_freeHelpers.RemoveAt(0);
                        m_helperCount--;
                    }
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
                int _waitCount = m_waitLoadInfos.Count;
                for (int i = m_freeHelpers.Count - 1; i >= 0; i--)
                {
                    if (_waitCount <= 0) break;

                    // 获取可以进行资源加载的
                    Info _info = null;
                    for (int j = 0; j < m_waitLoadInfos.Count; j++)
                    {
                        _info = m_waitLoadInfos[j];

                        // AssetBundle正在加载中
                        if (m_assetBundleLoadings.ContainsKey(_info.assetPath))
                        {
                            if (m_assetBundleLoadings[_info.assetPath])
                            {
                                _info = null;
                                continue;
                            }
                        }

                        // AssetBundle正在使用中
                        if (m_assetBundleUsings.ContainsKey(_info.assetPath))
                        {
                            if (m_assetBundleUsings[_info.assetPath])
                            {
                                _info = null;
                                continue;
                            }
                        }

                        break;
                    }

                    if (_info != null)
                    {
                        ResHelperAssetBundle _resHelper = m_freeHelpers[i];

                        if (m_assetBundleMap.ContainsKey(_info.assetPath))
                        {
                            m_assetBundleUsings[_info.assetPath] = true;
                            _resHelper.OnLoadAsync(m_assetBundleMap[_info.assetPath], _info.assetName, _info.assetType, _info.isScene, AssetBundleUseCompleteCallback,
                                _info.completeCallback, _info.updateCallback, _info.errorCallback);
                        }
                        else
                        {
                            m_assetBundleLoadings[_info.assetPath] = true;
                            m_assetBundleUsings[_info.assetPath] = true;
                            _resHelper.OnLoadAsync(_info.assetPath, _info.assetName, _info.assetType, _info.isScene, AssetBundleLoadCompleteCallback, AssetBundleUseCompleteCallback,
                                _info.completeCallback, _info.updateCallback, _info.errorCallback);
                        }

                        m_useHelpers.Add(m_freeHelpers[i]);
                        m_freeHelpers.RemoveAt(i);

                        m_loadingInfos.Add(_info);
                        m_waitLoadInfos.Remove(_info);

                        _waitCount--;
                    }
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

        // 异步加载资源
        public void OnLoadAsync(string assetBundlePath, string assetName, Type assetType, bool isScene,
            ResHelperAssetBundle.CompleteCallback complete, ResHelperAssetBundle.UpdateCallback update = null, ResHelperAssetBundle.ErrorCallback error = null)
        {
            Info _info = new Info()
            {
                assetName = assetName,
                assetPath = assetBundlePath,
                assetType = assetType,
                isScene = isScene,
                stopwatch = Activator.CreateInstance<Stopwatch>(),
                completeCallback = complete,
                updateCallback = update,
                errorCallback = error,
            };
            _info.stopwatch.Start();
            m_waitLoadInfos.Add(_info);
        }

        // AssetBundle加载完成
        private void AssetBundleLoadCompleteCallback(string assetPath, AssetBundle assetBundle)
        {
            m_assetBundleMap[assetPath] = assetBundle;
            m_assetBundleLoadings[assetPath] = false;
        }

        // AssetBundle使用完成
        private void AssetBundleUseCompleteCallback(string assetPath, string assetName)
        {
            m_assetBundleUsings[assetPath] = false;

            for (int i = 0; i < m_loadingInfos.Count; i++)
            {
                if (assetPath == m_loadingInfos[i].assetPath && assetName == m_loadingInfos[i].assetName)
                {
                    Log.Info("加载{0}总耗时：{1}毫秒", assetName, m_loadingInfos[i].stopwatch.ElapsedMilliseconds);
                    m_loadingInfos[i].stopwatch.Stop();
                    m_loadingInfos.RemoveAt(i);
                    break;
                }
            }
        }

        // 添加资源加载辅助器
        private ResHelperAssetBundle AddHelper()
        {
            ResHelperAssetBundle _helper = (new GameObject()).AddComponent<ResHelperAssetBundle>();

            _helper.name = Utility.ZText.Format("AssetBundle Load Agent Helper - {0}", m_freeHelpers.Count + 1);
            Transform transform = _helper.transform;
            transform.SetParent(m_parent);
            transform.localScale = Vector3.one;

            return _helper;
        }
    }
}