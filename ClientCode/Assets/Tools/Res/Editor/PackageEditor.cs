/**************************
 * 文件名:PackageEditor.cs
 * 文件描述:资源打包类
 * 创建日期:2019/08/01
 * 作者:ZB
 ***************************/




using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Res
{
    public class PackageEditor : EditorWindow
    {
        public static PackageEditor window = null;

        private static bool m_stateHomePage = true;                 // 主页
        private static bool m_stateClassify = false;                // 分类打包
        private static PackageBaseWindow m_selectWindow;            // 打开的窗口
        private static Dictionary<int, PackageBaseWindow> m_windows = new Dictionary<int, PackageBaseWindow>
        {
            {1, new PackageDetailInfoWindow() },
            {2, new PackageNGUIWindow() },
            {3, new PackageTableWindow() },
            {4, new PackageUGUIWindow() },
        };


        [MenuItem("Tools/资源打包")]
        public static void OnPackageNGUIInfo()
        {
            window = GetWindow<PackageEditor>("资源打包");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal("box");
            {
                if (GUILayout.Button("返回", GUILayout.Height(30)))
                {

                }
                if (GUILayout.Button("主页", GUILayout.Height(30)))
                {
                    window.titleContent = new GUIContent("资源打包");
                    m_stateHomePage = true;
                    m_stateClassify = false;
                    m_selectWindow = null;
                }
                if (GUILayout.Button("刷新", GUILayout.Height(30)))
                {
                    if (m_selectWindow != null)
                    {
                        m_selectWindow.OnUpdate();
                    }

                }
            }
            GUILayout.EndHorizontal();

            if (m_stateHomePage)
            {
                if (GUILayout.Button("分类打包", GUILayout.Height(30)))
                {
                    m_stateHomePage = false;
                    m_stateClassify = true;
                }

                if (GUILayout.Button("一键打包", GUILayout.Height(30)))
                {
                    m_stateClassify = false;
                }

                if (GUILayout.Button("查看详细信息", GUILayout.Height(30)))
                {
                    OnChanage(1);
                }
            }

            if (m_stateClassify)
            {
                OnGUIClassify();
            }

            if (m_selectWindow != null)
            {
                m_selectWindow.OnGUI();
            }
        }

        private void OnGUIClassify()
        {
            if (GUILayout.Button("NGUI资源打包", GUILayout.Height(30)))
            {
                OnChanage(2);
            }

            if (GUILayout.Button("UGUI资源打包", GUILayout.Height(30)))
            {
                OnChanage(4);
            }

            if (GUILayout.Button("角色模型资源打包", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("场景资源打包", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("表格资源打包", GUILayout.Height(30)))
            {
                OnChanage(3);
            }

            if (GUILayout.Button("特效资源打包", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("音效资源打包", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("Lua资源打包", GUILayout.Height(30)))
            {

            }
        }

        public void OnChanage(int index)
        {
            m_stateClassify = false;
            m_stateHomePage = false;
            if (m_selectWindow != null)
            {
                m_selectWindow.OnExit();
            }
            m_selectWindow = m_windows[index];
            m_selectWindow.OnUpdate();
            window.titleContent = new GUIContent(m_selectWindow.titleName);
        }
    }
}