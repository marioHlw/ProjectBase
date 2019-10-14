using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace zb.NGUILibrary
{
    [CustomEditor(typeof(UIDropDownMenuComponent))]
    public class UIDropDownMenuComponentInspector : Editor
    {
        private UIDropDownMenuComponent component;

        private SerializedProperty sp_defaultSelectFrist;
        private SerializedProperty sp_selectCloseWindow;
        private SerializedProperty sp_repeatExecuteCallback;

        private void OnEnable()
        {
            component = target as UIDropDownMenuComponent;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            sp_defaultSelectFrist = serializedObject.FindProperty("defaultSelectFrist");
            bool _defaultSelectFrist = EditorGUILayout.Toggle("默认选中第一个", sp_defaultSelectFrist.boolValue);
            sp_defaultSelectFrist.boolValue = _defaultSelectFrist;

            sp_selectCloseWindow = serializedObject.FindProperty("selectCloseWindow");
            bool _selectCloseWindow = EditorGUILayout.Toggle("选中后关闭下拉菜单", sp_selectCloseWindow.boolValue);
            sp_selectCloseWindow.boolValue = _selectCloseWindow;

            sp_repeatExecuteCallback = serializedObject.FindProperty("repeatExecuteCallback");
            bool _repeatExecuteCallback = EditorGUILayout.Toggle("可重复选中", sp_repeatExecuteCallback.boolValue);
            sp_repeatExecuteCallback.boolValue = _repeatExecuteCallback;

            serializedObject.ApplyModifiedProperties();
        }
    }
}