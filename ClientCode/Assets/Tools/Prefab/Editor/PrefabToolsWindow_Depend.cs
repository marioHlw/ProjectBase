/**************************
 * 文件名:PrefabToolsWindow_Depend.cs
 * 文件描述:预制依赖资源详细信息类
 * 创建日期:2019/10/21
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabToolsWindow_Depend : PrefabToolsWindow_Base
{
    private GameObject m_selectObj = null;

    private List<string> m_nameTypes = new List<string>() { "脚本", "图集", "图片", "Shader", "字体", "材质", "预设" };
    private List<bool> m_stateTypes = new List<bool>(2) { false, false, false, false, false, false, false };

    private List<string> m_scriptPaths = new List<string>();
    private List<string> m_atlasPaths = new List<string>();
    private List<string> m_texturePaths = new List<string>();
    private List<string> m_shaderPaths = new List<string>();
    private List<string> m_fontPaths = new List<string>();
    private List<string> m_materialPaths = new List<string>();

    private Dictionary<int, List<string>> m_pathMap = new Dictionary<int, List<string>>();

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
                string url = EditorUtility.OpenFilePanel("请选中要检查的预制（*.prefab)", Application.dataPath + "/Project", "prefab");
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
            int _index = 0;
            foreach (List<string> data in m_pathMap.Values)
            {
                EditorGUILayout.BeginVertical("box");
                {
                    m_stateTypes[_index] = NGUIEditorTools.DrawHeader(m_nameTypes[_index]);
                    if (m_stateTypes[_index])
                    {
                        EditorGUILayout.BeginVertical();
                        {
                            GUI.color = Color.green;
                            GUILayout.Label(string.Format("数量：({0})", m_pathMap[_index].Count));
                            GUI.color = Color.white;

                            for (int i = 0; i < m_pathMap[_index].Count; i++)
                            {
                                EditorGUILayout.BeginHorizontal("box");
                                EditorGUILayout.LabelField(m_pathMap[_index][i]);
                                if (GUILayout.Button("跳转"))
                                {
                                    Selection.activeObject = AssetDatabase.LoadAssetAtPath(m_pathMap[_index][i], typeof(Object));
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                }
                EditorGUILayout.EndVertical();

                _index++;
            }
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

        m_pathMap.Clear();
        for (int i = 0; i < m_stateTypes.Count; i++)
        {
            m_pathMap.Add(i, new List<string>());
        }

        string[] _dependencies = AssetDatabase.GetDependencies(AssetDatabase.GetAssetPath(m_selectObj), true);
        for (int i = 0; i < _dependencies.Length; i++)
        {
            bool _isSelf = (i == 0);
            bool _isAtlas = false;

            // 脚本
            if (_dependencies[i].EndsWith(".cs"))
            {
                if (!m_pathMap[0].Contains(_dependencies[i]))
                {
                    m_pathMap[0].Add(_dependencies[i]);
                }
            }

            // 图集
            GameObject _obj = AssetDatabase.LoadAssetAtPath(_dependencies[i], typeof(GameObject)) as GameObject;
            if (_obj != null)
            {
                if (_obj.GetComponent<UIAtlas>())
                {
                    _isAtlas = true;

                    if (!m_pathMap[1].Contains(_dependencies[i]))
                    {
                        m_pathMap[1].Add(_dependencies[i]);
                    }
                }
            }

            // 图片
            if (_dependencies[i].EndsWith(".png") || _dependencies[i].EndsWith(".jpg"))
            {
                if (!m_pathMap[2].Contains(_dependencies[i]))
                {
                    m_pathMap[2].Add(_dependencies[i]);
                }
            }

            // Shader
            Shader _shader = AssetDatabase.LoadAssetAtPath(_dependencies[i], typeof(Shader)) as Shader;
            if (_shader != null)
            {
                if (!m_pathMap[3].Contains(_dependencies[i]))
                {
                    m_pathMap[3].Add(_dependencies[i]);
                }
            }

            // 字体
            Font _font = AssetDatabase.LoadAssetAtPath(_dependencies[i], typeof(Font)) as Font;
            if (_font != null)
            {
                if (!m_pathMap[4].Contains(_dependencies[i]))
                {
                    m_pathMap[4].Add(_dependencies[i]);
                }
            }

            // 材质
            if (_dependencies[i].EndsWith(".mat"))
            {
                if (!m_pathMap[5].Contains(_dependencies[i]))
                {
                    m_pathMap[5].Add(_dependencies[i]);
                }
            }

            // 预设
            if (_dependencies[i].EndsWith(".prefab"))
            {
                if (!m_pathMap[6].Contains(_dependencies[i]) && !_isAtlas && !_isSelf)
                {
                    m_pathMap[6].Add(_dependencies[i]);
                }
            }
        }

        for (int i = 0; i < m_stateTypes.Count; i++)
        {
            m_pathMap[i].Sort();
        }
    }
}