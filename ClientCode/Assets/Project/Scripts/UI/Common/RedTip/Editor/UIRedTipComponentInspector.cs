using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace UI.Common
{
    [CustomEditor(typeof(UIRedTipComponent))]
    public class UIRedTipComponentInspector : Editor
    {
        private UIRedTipComponent component;
        private SerializedProperty sp_redTipType;
        private SerializedProperty sp_autoInitialize;
        private SerializedProperty sp_soleID;

        private string[] redTipTypeArray = new string[] { "默认" };

        private void OnEnable()
        {
            component = target as UIRedTipComponent;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            sp_redTipType = serializedObject.FindProperty("redTipType");
            int _redTipTypeIndex = EditorGUILayout.Popup("红点类型:", sp_redTipType.enumValueIndex, redTipTypeArray);
            sp_redTipType.enumValueIndex = _redTipTypeIndex;

            sp_autoInitialize = serializedObject.FindProperty("autoInitialize");
            bool _autoInitialize = EditorGUILayout.Toggle("自动初始化", sp_autoInitialize.boolValue);
            sp_autoInitialize.boolValue = _autoInitialize;

            sp_soleID = serializedObject.FindProperty("soleID");
            int _soleID = EditorGUILayout.IntField("唯一ID", sp_soleID.intValue);
            sp_soleID.intValue = _soleID;

            serializedObject.ApplyModifiedProperties();
        }
    }
}