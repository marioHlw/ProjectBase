/*****************************************************
 * 文件名:ResourceComponent.cs
 * 文件描述:资源设置组件类
 * 创建日期:2019/11/24
 * 作者:ZB
 *****************************************************/



using Res;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zb.NGUILibrary;

public class ResourceComponent : MonoBehaviour
{
    [HideInInspector] public int ResourcesHelperCount = 3;          // Resources资源加载辅助器数量
    [HideInInspector] public int ByteHelperCount = 3;               // Byte资源加载辅助器数量
    [HideInInspector] public int AssetBundleHelperCount = 3;        // Byte资源加载辅助器数量

    [HideInInspector] public bool dataTableUseAsync = false;                                                                    // 表格数据使用异步
    [HideInInspector] public enResLoaderType dataTableResourceLoadType = enResLoaderType.LoadFromBytes;                         // 表格数据资源加载方式
    [HideInInspector] public enResPathType dataTableResourceLoadPathType = enResPathType.LoadPathFromDirctory;                  // 表格数据资源加载路径类型

    [HideInInspector] public bool nguiUseAssetBundle = false;             // NGUI资源使用AssetBundle

    private void Awake()
    {
        Ctrl.SetResourceComponent(this);  
    }

    private void Start()
    {
        Ctrl.resManager.LoaderResources.SetHelperCount(ResourcesHelperCount);
        Ctrl.resManager.LoaderResources.OnInit();

        Ctrl.resManager.LoaderByte.SetHelperCount(ByteHelperCount);
        Ctrl.resManager.LoaderByte.OnInit();

        Ctrl.resManager.LoaderAssetBundle.SetHelperCount(AssetBundleHelperCount);
        Ctrl.resManager.LoaderAssetBundle.OnInit();
    }

    private void Update()
    {
        Ctrl.resManager.LoaderResources.OnUpdate();
        Ctrl.resManager.LoaderByte.OnUpdate();
        Ctrl.resManager.LoaderAssetBundle.OnUpdate();
    }
}