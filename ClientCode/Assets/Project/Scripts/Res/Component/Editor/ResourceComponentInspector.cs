using Res;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceComponent))]
public class ResourceComponentInspector : Editor
{
    private ResourceComponent component;

    private SerializedProperty sp_resourceLoadHelperCount;
    private SerializedProperty sp_resourcesHelperCount;
    private SerializedProperty sp_byteHelperCount;
    private SerializedProperty sp_assetBundleHelperCount;

    private void OnEnable()
    {
        component = target as ResourceComponent;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        sp_resourcesHelperCount = serializedObject.FindProperty("ResourcesHelperCount");
        int _resourcesHelperCount = EditorGUILayout.IntField("Resources加载辅助器数量：", sp_resourcesHelperCount.intValue);
        sp_resourcesHelperCount.intValue = _resourcesHelperCount;

        sp_byteHelperCount = serializedObject.FindProperty("ByteHelperCount");
        int _byteHelperCount = EditorGUILayout.IntField("Byte加载辅助器数量：", sp_byteHelperCount.intValue);
        sp_byteHelperCount.intValue = _byteHelperCount;

        sp_assetBundleHelperCount = serializedObject.FindProperty("AssetBundleHelperCount");
        int _assetBundleHelperCount = EditorGUILayout.IntField("AssetBundle加载辅助器数量：", sp_assetBundleHelperCount.intValue);
        sp_assetBundleHelperCount.intValue = _assetBundleHelperCount;

        DrawDataTable();
        DrawNGUI();

        serializedObject.ApplyModifiedProperties();
    }

    #region 表格数据

    private void DrawDataTable()
    {
        if (NGUIEditorTools.DrawHeader("表格数据"))
        {
            NGUIEditorTools.BeginContents();
            bool _dataTableUseAsync = EditorGUILayout.Toggle("使用异步:", component.dataTableUseAsync);
            if (GUI.changed)
            {
                component.dataTableUseAsync = _dataTableUseAsync;
            }

            enResLoaderType _resourceLoadType = (enResLoaderType)EditorGUILayout.EnumPopup("加载方式类型", component.dataTableResourceLoadType);
            if (GUI.changed)
            {           
                component.dataTableResourceLoadType = _resourceLoadType;
            }
            if (_resourceLoadType != enResLoaderType.LoadFromResources)
            {
                enResPathType _dataTableResourceLoadPathType = (enResPathType)EditorGUILayout.EnumPopup("路径方式类型", component.dataTableResourceLoadPathType);
                if (GUI.changed)
                {
                    component.dataTableResourceLoadPathType = _dataTableResourceLoadPathType;
                }
            }

            NGUIEditorTools.EndContents();
        }
    }

    #endregion

    #region NGUI资源

    private void DrawNGUI()
    {
        if (NGUIEditorTools.DrawHeader("NGUI资源"))
        {
            NGUIEditorTools.BeginContents();
            bool _nguiUseAssetBundle = EditorGUILayout.Toggle("资源使用AssetBundle", component.nguiUseAssetBundle);
            if (GUI.changed)
            {
                component.nguiUseAssetBundle = _nguiUseAssetBundle;
            }

            NGUIEditorTools.EndContents();
        }
    }

    #endregion
}