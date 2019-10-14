using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace zb.NGUILibrary
{
    [CustomEditor(typeof(UILoopGrid))]
    public class UILoopGridInspector : Editor
    {
        private UILoopGrid loopGrid;
        private SerializedProperty serializedProperty;

        public string[] arrangementTypeArrary = { "水平", "垂直" };
        public string[] gotoSelectTypeArrary1 = { "顶部", "中心", "底部" };
        public string[] gotoSelectTypeArrary2 = { "左边", "中心", "右边" };

        private void OnEnable()
        {
            loopGrid = target as UILoopGrid;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            SerializedProperty spArrangementType = serializedObject.FindProperty("arrangementType");
            int controllerTypeIndex = EditorGUILayout.Popup("布局类型:", spArrangementType.enumValueIndex, arrangementTypeArrary);
            spArrangementType.enumValueIndex = controllerTypeIndex;

            SerializedProperty gotoSelectTypeType = serializedObject.FindProperty("gotoSelectType");
            int gotoSelectTypeTypeIndex = EditorGUILayout.Popup("跳转物体所处位置类型:", gotoSelectTypeType.enumValueIndex, controllerTypeIndex == 0 ? gotoSelectTypeArrary2 : gotoSelectTypeArrary1);
            gotoSelectTypeType.enumValueIndex = gotoSelectTypeTypeIndex;

            SerializedProperty spUseAbsoluteSize = serializedObject.FindProperty("useAbsoluteSize");

            EditorGUILayout.PropertyField(serializedObject.FindProperty("cellWidth"), new GUIContent("单元格的宽度:"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("cellHeight"), new GUIContent("单元格的高度:"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("extendCount"), new GUIContent("扩展数量:")); 

            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultSelectFrist"), new GUIContent("默认选中第一个:"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("repeatExecuteCallback"), new GUIContent("重复执行回调:"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hideInactive"), new GUIContent("过滤已经隐藏的物体:"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("animateSmoothly"), new GUIContent("动画移动位置:"));

            SerializedProperty spDebugMode = serializedObject.FindProperty("debugMode");
            bool debugMode = EditorGUILayout.Toggle("调试模式:", spDebugMode.boolValue);
            spDebugMode.boolValue = debugMode;
            if (debugMode)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("debugX"), new GUIContent("X:"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("debugY"), new GUIContent("Y:"));
                if (GUILayout.Button("跳转"))
                {
                    loopGrid.OnClick();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}