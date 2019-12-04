/**************************
 * 文件名:ResToolsWindow.cs
 * 文件描述:预制资源信息查看工具类
 * 创建日期:2019/10/21
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabToolsWindow : EditorWindow
{
    public enum ToggleType
    {
        None = -1,
        Depend = 0,
        BeDepend = 1,
        allUIPrefabDepend = 2,
        scriptUse = 3,
    }

    [MenuItem("Tools/Prefab工具", false, 4)]
    public static void Open()
    {
        PrefabToolsWindow _window = GetWindow<PrefabToolsWindow>();
        _window.Show();
    }

    private ToggleType m_lastToggleType = ToggleType.None;
    private PrefabToolsWindow_Base m_curResToolsWin = null;
    private List<PrefabToolsWindow_Base> m_resToolsWins = new List<PrefabToolsWindow_Base>()
    {
        new PrefabToolsWindow_Depend(),
    };

    private void OnSelectionChange()
    {
        if (m_curResToolsWin != null)
        {
            m_curResToolsWin.OnSelectionChange();
            Repaint();
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("返回上一级", GUILayout.Height(40)))
            {
                ChanageToggleType(m_lastToggleType);
            }
            if (GUILayout.Button("返回主页", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.None);
            }
            if (GUILayout.Button("刷新", GUILayout.Height(40)))
            {
                if (m_curResToolsWin != null)
                {
                    m_curResToolsWin.OnUpdate();
                }
            }
        }
        GUILayout.EndHorizontal();

        if (m_curResToolsWin != null)
        {
            m_curResToolsWin.OnGUI();
        }
        else
        {
            if (GUILayout.Button("查看Prefab依赖详细信息", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.Depend);
            }
            else if (GUILayout.Button("查看Prefab被依赖详细信息", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.BeDepend);
            }
            else if (GUILayout.Button("查看所有Prefab详细信息", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.allUIPrefabDepend);
            }
            else if (GUILayout.Button("查看脚本使用情况", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.scriptUse);
            }
            else if (GUILayout.Button("查看资源GUID", GUILayout.Height(40)))
            {
                string _path = EditorUtility.OpenFilePanel("选择资源", Application.dataPath, "*.*");

                if (!string.IsNullOrEmpty(_path))
                {
                    Debug.LogError(_path.Replace(Application.dataPath, "Assets") + "::" + AssetDatabase.AssetPathToGUID(_path.Replace(Application.dataPath, "Assets")));
                }
            }
        }
    }

    private void ChanageToggleType(ToggleType toggleType)
    {
        if (toggleType < 0)
        {
            m_curResToolsWin = null;
            m_lastToggleType = ToggleType.None;
            return;
        }
        else
        {
            if (m_curResToolsWin != null)
            {
                m_lastToggleType = (ToggleType)m_resToolsWins.IndexOf(m_curResToolsWin);
            }
        }

        int _index = (int)toggleType;
        if (m_resToolsWins.Count > _index)
        {
            m_curResToolsWin = m_resToolsWins[_index];
            m_curResToolsWin.OnUpdate();
        }
    }
}