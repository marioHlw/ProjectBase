using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GameFramework))]
public class GameFrameworkInspector : Editor
{
    private GameFramework component;
    private int fps = 60;
    private bool lockFPS = false;

    public string[] fpsArrary = { "30", "45", "60" };
    public int[] fpsValArrary = { 30, 45, 60 };
    private int m_fpsIndex = 0;

    private SerializedProperty sp_FPS;
    private SerializedProperty sp_stateLockFPS;
    private SerializedProperty sp_uiUseAssetBundle;

    private void OnEnable()
    {
        component = target as GameFramework;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        sp_uiUseAssetBundle = serializedObject.FindProperty("uiUseAssetBundle");

        sp_stateLockFPS = serializedObject.FindProperty("stateLockFPS");
        bool _stateLockFPS = EditorGUILayout.Toggle("锁定FPS:", sp_stateLockFPS.boolValue);
        sp_stateLockFPS.boolValue = _stateLockFPS;

        sp_FPS = serializedObject.FindProperty("FPS");
        m_fpsIndex = EditorGUILayout.Popup("FPS:", m_fpsIndex, fpsArrary);
        if (GUILayout.Button("更新"))
        {
            component.SetFPS(fpsValArrary[m_fpsIndex]);
        }

        serializedObject.ApplyModifiedProperties();
    }
}