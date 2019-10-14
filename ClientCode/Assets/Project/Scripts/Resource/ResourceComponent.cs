using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zb.NGUILibrary;

public class ResourceComponent : MonoBehaviour
{
    private ResourceLoader m_resourceLoader = null;
    private Transform m_instanceRoot = null;

    [HideInInspector] public int resourceLoadHelperCount = 0;      // 资源加载辅助器数量

    [HideInInspector] public bool dataTableUseAsync = false;                                                                        // 表格数据使用异步
    [HideInInspector] public enResourceLoadType dataTableResourceLoadType = enResourceLoadType.LoadFromBytes;                       // 表格数据资源加载方式
    [HideInInspector] public enResourceLoadPathType dataTableResourceLoadPathType = enResourceLoadPathType.LoadPathFromDirctory;    // 表格数据资源加载路径类型

    [HideInInspector] public bool nguiUseAssetBundle = false;             // NGUI资源使用AssetBundle

    private void Awake()
    {
        Ctrl.SetResourceComponent(this);  
    }

    private void Start()
    {
        m_resourceLoader = MonoSingleton<ResourceLoader>.GetInstance();
        m_resourceLoader.name = "Resource Loader";
        Transform transform = m_resourceLoader.transform;
        transform.SetParent(this.transform);
        transform.localScale = Vector3.one;

        if (m_instanceRoot == null)
        {
            m_instanceRoot = (new GameObject("Resource Load Helper Instances")).transform;
            m_instanceRoot.SetParent(this.transform);
            m_instanceRoot.localScale = Vector3.one;
        }

        for (int i = 0; i < resourceLoadHelperCount; i++)
        {
            AddLoadResourceAgentHelper(i);
        }
    }

    /// <summary>
    /// 增加资源加载辅助器。
    /// </summary>
    /// <param name="index">加载资源代理辅助器索引。</param>

    private void AddLoadResourceAgentHelper(int index)
    {
        ResourceLoadHelper _loadResourceHelper = (new GameObject()).AddComponent<ResourceLoadHelper>();

        if (_loadResourceHelper == null)
        {
            Log.Error("Can not create load resource agent helper.");
            return;
        }

        _loadResourceHelper.name = Utility.ZText.Format("Resource Load Agent Helper - {0}", index.ToString());
        Transform transform = _loadResourceHelper.transform;
        transform.SetParent(m_instanceRoot);
        transform.localScale = Vector3.one;

        m_resourceLoader.AddResourceLoadHelper(_loadResourceHelper);
    }
}