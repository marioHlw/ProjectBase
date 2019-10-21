/**************************
 * 文件名:UIResToolsWin_PrefabDepend.cs
 * 文件描述:NGUI资源工具-预制类
 *          1.查看UI预制使用了那些资源，图集、UITexture、Shader等
 * 创建日期:2019/10/21
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class UIResToolsWin_PrefabDepend : UIResToolsWin_Base
{
    private GameObject m_selectObj = null;

    private List<string> m_nameTypes = new List<string>() { "脚本", "图集", "图片", "Shader" };
    private List<bool> m_stateTypes = new List<bool>(2) { false, false, false, false };

    private List<string> m_scriptPaths = new List<string>();
    private List<string> m_atlasPaths = new List<string>();
    private List<string> m_texturePaths = new List<string>();
    private List<string> m_shaderPaths = new List<string>();

    private Vector2 m_viewPosition = Vector2.zero;

    public override void OnGUI()
    {
        base.OnGUI();

        EditorGUILayout.LabelField("请选中要检查的预制（*.prefab)");

        EditorGUILayout.BeginHorizontal("box");
        {
            m_selectObj = EditorGUILayout.ObjectField(new GUIContent("预制："), m_selectObj, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("选择"))
            {
                string url = EditorUtility.OpenFilePanel("请选中要检查的预制（*.prefab)", Application.dataPath + "/Project/UI", "prefab");
                if (!string.IsNullOrEmpty(url))
                {
                    m_selectObj = AssetDatabase.LoadAssetAtPath(url.Replace(Application.dataPath, "Assets"), typeof(GameObject)) as GameObject;
                    m_viewPosition = Vector2.zero;
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        if (m_selectObj == null)
        {
            return;
        }

        m_viewPosition = EditorGUILayout.BeginScrollView(m_viewPosition);
        {
            // 脚本
            EditorGUILayout.BeginVertical("box");
            {
                m_stateTypes[0] = NGUIEditorTools.DrawHeader(m_nameTypes[0]);
                if (m_stateTypes[0])
                {
                    for (int i = 0; i < m_scriptPaths.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        EditorGUILayout.LabelField(m_scriptPaths[i]);
                        if (GUILayout.Button("跳转"))
                        {
                            Selection.activeObject = AssetDatabase.LoadAssetAtPath(m_scriptPaths[i], typeof(Object));
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.EndVertical();

            // 图集
            EditorGUILayout.BeginVertical("box");
            {
                m_stateTypes[1] = NGUIEditorTools.DrawHeader(m_nameTypes[1]);
                if (m_stateTypes[1])
                {
                    for (int i = 0; i < m_atlasPaths.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal("box");           
                        EditorGUILayout.LabelField(m_atlasPaths[i]);
                        if (GUILayout.Button("跳转"))
                        {
                            Selection.activeObject = AssetDatabase.LoadAssetAtPath(m_atlasPaths[i], typeof(Object));
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.EndVertical();

            // 图片
            EditorGUILayout.BeginVertical("box");
            {
                m_stateTypes[2] = NGUIEditorTools.DrawHeader(m_nameTypes[2]);
                if (m_stateTypes[2])
                {
                    for (int i = 0; i < m_texturePaths.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        EditorGUILayout.LabelField(m_texturePaths[i]);
                        if (GUILayout.Button("跳转"))
                        {
                            Selection.activeObject = AssetDatabase.LoadAssetAtPath(m_texturePaths[i], typeof(Object));
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.EndVertical();

            // 材质
            EditorGUILayout.BeginVertical("box");
            {
                m_stateTypes[3] = NGUIEditorTools.DrawHeader(m_nameTypes[3]);
                if (m_stateTypes[3])
                {
                    for (int i = 0; i < m_shaderPaths.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        EditorGUILayout.LabelField(m_shaderPaths[i]);
                        if (GUILayout.Button("跳转"))
                        {
                            Selection.activeObject = AssetDatabase.LoadAssetAtPath(m_shaderPaths[i], typeof(Object));
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();
    }

    public override void OnSelectionChange()
    {
        base.OnSelectionChange();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_selectObj == null)
        {
            return;
        }

        string[] _dependencies = AssetDatabase.GetDependencies(AssetDatabase.GetAssetPath(m_selectObj), false);

        // 脚本
        m_scriptPaths.Clear();
        for (int i = 0; i < _dependencies.Length; i++)
        {
            if (_dependencies[i].EndsWith(".cs"))
            {
                if (!m_scriptPaths.Contains(_dependencies[i]))
                {
                    m_scriptPaths.Add(_dependencies[i]);
                }
            }
        }

        // 图集
        m_atlasPaths.Clear();
        for (int i = 0; i < _dependencies.Length; i++)
        {
            GameObject _obj = AssetDatabase.LoadAssetAtPath(_dependencies[i], typeof(GameObject)) as GameObject;
            if (_obj != null)
            {
                if (_obj.GetComponent<UIAtlas>())
                {
                    if (!m_atlasPaths.Contains(_dependencies[i]))
                    {
                        m_atlasPaths.Add(_dependencies[i]);
                    }
                }
            }
        }

        // 图片
        m_texturePaths.Clear();
        for (int i = 0; i < _dependencies.Length; i++)
        {
            if (_dependencies[i].EndsWith(".png") || _dependencies[i].EndsWith(".jpg"))
            {
                if (!m_texturePaths.Contains(_dependencies[i]))
                {
                    m_texturePaths.Add(_dependencies[i]);
                }
            }
        }

        // 材质
        m_shaderPaths.Clear();
        for (int i = 0; i < _dependencies.Length; i++)
        {
            if (_dependencies[i].EndsWith(".shader"))
            {
                if (!m_shaderPaths.Contains(_dependencies[i]))
                {
                    m_shaderPaths.Add(_dependencies[i]);
                }
            }
        }

        m_scriptPaths.Sort();
        m_atlasPaths.Sort();
        m_texturePaths.Sort();
        m_shaderPaths.Sort();
    }
}