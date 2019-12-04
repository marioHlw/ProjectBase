/**************************
 * 文件名:UIResManager.cs
 * 文件描述:NGUI资源管理类
 * 创建日期:2019/08/18
 * 作者:ZB
 ***************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    public class UIResManager : Singleton<UIResManager>
    {
        private bool m_logState = true;            // log状态

        public delegate void LoadCompleteCallBack(GameObject obj, string path);                 // 异步加载加调委托

        public const string DirPath = "ui/";

        private List<string> m_commonResources = null;                      // 通用UI资源,资源加载之后不会被清理.
        private Dictionary<string, bool> m_commonDependenciesConfirm;       // 公共UI依赖资源统计确认(上面填写的UI可能就是实际中用到的,但不会具体写他所依赖的资源,
                                                                            // 所以要在第一次载入这个UI的时候把所依赖的资源也加入到公共列表中,以名被清理）  


        private Dictionary<string, UnityEngine.AssetBundle> m_mapAssetBundle;            // 所有资源AssetBundle文件，此文件将在无引用的时候卸载
        private Dictionary<string, Object[]> m_mapCommonResObject;           // 公共资源列表，此列表不会被清理                                                                         
        private Dictionary<string, Object[]> m_mapAssetsObject;              // 所有资源文件，此文件将在无引用的时候卸载                                                                          
        private Dictionary<string, int> m_mapRefCount;                       // 资源引用计数

        private AssetBundleManifest m_manifest;                 // 所有UI的依赖关系数据

        private bool m_useAssetBundle = false;                  // 使用AssetBundle

        public override void Init()
        {
            base.Init();

            m_mapAssetBundle = new Dictionary<string, UnityEngine.AssetBundle>();
            m_mapCommonResObject = new Dictionary<string, Object[]>();
            m_mapAssetsObject = new Dictionary<string, Object[]>();
            m_mapRefCount = new Dictionary<string, int>();

            // 通用UI资源,资源加载之后不会被清理.
            m_commonResources = new List<string>()
            {

            };

            Log.Info(Ctrl.LogInfos[0] + " - NGUI资源管理器");
        }

        public override void UnInit()
        {
            base.UnInit();

            m_commonResources = null;

            Log.Info(Ctrl.LogInfos[1] + " - NGUI资源管理器");
        }

        #region Load

        public GameObject OnLoad(string path, LoadCompleteCallBack callBack = null)
        {
            GameObject _obj = null;
            string _loadPath = path.ToLower() + ".unity3d";

            // 从Resources目录中加载
            if (!m_useAssetBundle)
            {
                // 异步加载
                if (callBack != null)
                {

                }

                return Resources.Load(path) as GameObject;
            }
            else
            {               
                if (m_manifest == null)
                {
                    string _path = Ctrl.device.PathRoot + "ui/ui";
                    string _n = Ctrl.device.GetResPathKey("ui/ui", Ctrl.device.PathRoot);
                    m_manifest = UnityEngine.AssetBundle.LoadFromFile(_path).LoadAllAssets<AssetBundleManifest>()[0];
                }
            }

            string[] _dependencies = m_manifest.GetAllDependencies(_loadPath);
            return Load(_loadPath, _dependencies);
        }

        private GameObject Load(string path, string[] dependencies)
        {
            string _refPath = "";
            string _abPath = "";
            UnityEngine.AssetBundle _assetBundle = null;
            Object[] objArray;

            // 加载依赖资源
            if (dependencies.Length > 0)
            {
                for (int i = 0; i < dependencies.Length; i++)
                {
                    _refPath = dependencies[i];

                    if (!CheackObjectCache(_refPath))
                    {
                        _abPath = DirPath + _refPath;
                        _abPath = Ctrl.device.SetRootPathAndVersion(_abPath, _abPath, true, Ctrl.device.PathRoot);
                        _assetBundle = UnityEngine.AssetBundle.LoadFromFile(_abPath);
                        objArray = _assetBundle.LoadAllAssets();
                        AddToCache(_refPath, objArray, _assetBundle);
                    }
                }
            }

            //加载主资源
            _refPath = path;
            if (!CheackObjectCache(_refPath))
            {
                _abPath = DirPath + _refPath;
                _abPath = Ctrl.device.SetRootPathAndVersion(_abPath, _abPath, true, Ctrl.device.PathRoot);
                _assetBundle = UnityEngine.AssetBundle.LoadFromFile(_abPath);
                objArray = _assetBundle.LoadAllAssets();
                AddToCache(_refPath, objArray, _assetBundle);
            }

            return GetObjectCache(_refPath);
        }

        private bool CheackObjectCache(string path)
        {
            return m_mapCommonResObject.ContainsKey(path) || m_mapAssetsObject.ContainsKey(path);
        }

        private GameObject GetObjectCache(string path)
        {
            if (m_mapCommonResObject.ContainsKey(path))
            {
                return m_mapCommonResObject[path][0] as GameObject;
            }

            if (m_mapAssetsObject.ContainsKey(path))
            {
                return m_mapAssetsObject[path][0] as GameObject;
            }

            return null;
        }

        private void AddToCache(string path, UnityEngine.AssetBundle assetBundle)
        {
            m_mapAssetBundle.Add(path, assetBundle);
        }

        private void AddToCache(string path, Object[] objs, UnityEngine.AssetBundle assetBundle = null)
        {
            if (m_commonResources.IndexOf(path) == -1)
            {
                m_mapAssetsObject.Add(path, objs);
            }
            else
            {
                m_mapCommonResObject.Add(path, objs);
            }

            if (assetBundle != null)
            {
                m_mapAssetBundle.Add(path, assetBundle);
            }
        }

        private void ClearCache(string path)
        {
            int _refCount = 0;
            if (m_mapRefCount.TryGetValue(path, out _refCount) && _refCount > 0)
            {
                return;
            }

            if (m_mapAssetsObject.ContainsKey(path))
            {
                m_mapAssetsObject.Remove(path);

                UnityEngine.AssetBundle _assetBundle;

                if (m_mapAssetBundle.TryGetValue(path, out _assetBundle))
                {
                    _assetBundle.Unload(true);
                    m_mapAssetBundle.Remove(path);
                }
            }
        }

        #endregion

        #region Set

        /// <summary>
        /// 设置 - 从AssetBundle中加载资源
        /// </summary>

        public void SetUseAssetBundle(bool val)
        {
            m_useAssetBundle = val;
        }

        #endregion
    }
}