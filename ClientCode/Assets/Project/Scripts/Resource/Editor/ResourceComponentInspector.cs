using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceComponent))]
public class ResourceComponentInspector : Editor
{
    private ResourceComponent component;

    private SerializedProperty sp_resourceLoadHelperCount;

    private void OnEnable()
    {
        component = target as ResourceComponent;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        sp_resourceLoadHelperCount = serializedObject.FindProperty("resourceLoadHelperCount");
        int _resourceLoadHelperCount = EditorGUILayout.IntField("资源加载辅助器数量", sp_resourceLoadHelperCount.intValue);
        sp_resourceLoadHelperCount.intValue = _resourceLoadHelperCount;

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

            enResourceLoadType _resourceLoadType = (enResourceLoadType)EditorGUILayout.EnumPopup("加载方式类型", component.dataTableResourceLoadType);
            if (GUI.changed)
            {           
                component.dataTableResourceLoadType = _resourceLoadType;
            }
            if (_resourceLoadType != enResourceLoadType.LoadFromResources)
            {
                enResourceLoadPathType _dataTableResourceLoadPathType = (enResourceLoadPathType)EditorGUILayout.EnumPopup("路径方式类型", component.dataTableResourceLoadPathType);
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