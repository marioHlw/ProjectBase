/**************************
 * 文件名:UIResToolsWindow.cs
 * 文件描述:NGUI资源工具窗口类
 * 创建日期:2019/10/14
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIResToolsWindow : EditorWindow
{
    public enum ToggleType
    {
        None = -1,

        Atlas = 0,

        prefabDepend = 1,

        prefabBeUse = 2,

        allUIPrefabDepend = 3,

        scriptUse = 4,
    }

    [MenuItem("Tools/UI/资源引用详情", false, 4)]
    public static void Open()
    {
        UIResToolsWindow window = GetWindow<UIResToolsWindow>();
        window.Show();
    }

    private ToggleType m_lastToggleType = ToggleType.None;
    private UIResToolsWin_Base m_curResToolsWin = null;
    private List<UIResToolsWin_Base> m_resToolsWins = new List<UIResToolsWin_Base>()
    {
        new UIResToolsWin_Atlas(),
        new UIResToolsWin_PrefabDepend(),
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
            if (GUILayout.Button("纹理集使用情况", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.Atlas);
            }
            else if (GUILayout.Button("UIPrefab引用情况", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.prefabDepend);
            }
            else if (GUILayout.Button("UIPrefab被使用情况", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.prefabBeUse);
            }
            else if (GUILayout.Button("检查脚本使用情况", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.scriptUse);
            }
            else if (GUILayout.Button("查看所有UIPrefab引用情况（除公共资源）", GUILayout.Height(40)))
            {
                ChanageToggleType(ToggleType.allUIPrefabDepend);
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