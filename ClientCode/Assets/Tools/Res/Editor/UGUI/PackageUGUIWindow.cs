/*****************************************************
 * 文件名:PackageUGUIWindow.cs
 * 文件描述:资源打包 - UGUI窗口类
 * 创建日期:2019/11/26
 * 作者:ZB
 *****************************************************/



using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Res
{
    public class PackageUGUIWindow : PackageBaseWindow
    {
        // 参与打包文件扩展名
        protected string[] m_buildFileExtension = new string[2] { ".prefab", ".spriteatlas" };

        // 选中的目录信息
        private Dictionary<enResType, FloderInfo> m_floderMap = new Dictionary<enResType, FloderInfo>();

        private Dictionary<enResType, bool> m_floderMapFoldout = new Dictionary<enResType, bool>();

        // 配置文件导出完全路径
        private string m_configFileFullPath;

        public override void OnUpdate()
        {
            base.OnUpdate();

            titleName = "UGUI资源打包";
            m_buildFileExtension = new string[2] { ".prefab", ".spriteatlas" };
            m_configFileFullPath = ResUtility.ResConfigFilePathUGUI;

            m_floderMap.Clear();
            m_floderMapFoldout.Clear();

            m_floderMap.Add(enResType.UGUI_Prefab, new FloderInfo());
            m_floderMap.Add(enResType.UGUI_Texture, new FloderInfo());
            m_floderMap.Add(enResType.UGUI_Atlas, new FloderInfo());

            foreach (KeyValuePair<enResType, FloderInfo> temp in m_floderMap)
            {
                temp.Value.AbsolutePath = ResUtility.ResSourcePathUGUI + "/" + temp.Key.ToString().Substring(5);

                m_floderMapFoldout.Add(temp.Key, false);
            }
        }

        public override void OnGUI()
        {
            base.OnGUI();

            // 打包资源源目录配置
            GUILayout.BeginVertical();
            {
                foreach (KeyValuePair<enResType, FloderInfo> temp in m_floderMap)
                {
                    GUILayout.BeginHorizontal("box");
                    {
                        GUILayout.Label(temp.Key.ToString().Substring(5) + "：", GUILayout.Width(60));

                        if (GUILayout.Button("选择路径", GUILayout.Height(20), GUILayout.Width(100)))
                        {
                            temp.Value.AbsolutePath = EditorUtility.OpenFolderPanel("选择目录", Application.dataPath, "");
                            temp.Value.OnUpdate(temp.Value.AbsolutePath, null, 0, m_buildFileExtension);
                        }

                        if (GUILayout.Button("默认路径", GUILayout.Height(20), GUILayout.Width(100)))
                        {
                            temp.Value.AbsolutePath = ResUtility.ResSourcePathUGUI + "/" + temp.Key.ToString().Substring(5);
                        }
                        GUILayout.Label("资源路径:", GUILayout.Width(55));
                        GUILayout.TextField(temp.Value.AbsolutePath);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();

            // 打包资源源目录信息
            GUILayout.BeginVertical();
            {
                foreach (KeyValuePair<enResType, bool> temp in m_floderMapFoldout)
                {
                    bool _val = EditorGUILayout.Foldout(temp.Value, temp.Key.ToString().Substring(5), EditorStyles.foldout);
                    m_floderMapFoldout[temp.Key] = _val;
                }
            }
            GUILayout.EndVertical();
        }

        public override void OnSelectionChange()
        {
            base.OnSelectionChange();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnPackageAll()
        {
            base.OnPackageAll();
        }
    }
}