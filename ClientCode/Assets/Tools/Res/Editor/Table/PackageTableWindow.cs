/*****************************************************
 * 文件名:PackageTableWindow.cs
 * 文件描述:表格资源窗口类
 * 创建日期:2019/11/22
 * 作者:ZB
 *****************************************************/


using NetProto;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace Res
{
    public class PackageTableWindow : PackageBaseWindow
    {
        // 参与打包文件扩展名
        protected string[] m_buildFileExtension = new string[1] { ".bytes" };
        // 所选目录路径
        protected string selectFolder = "";
        // 配置文件导出路径
        protected string resConfigFilePath;
        // 所选目录信息
        protected FloderInfo selectFloderInfo;

        // 索引信息
        private string m_searchInfo = "";
        private Vector2 m_scrollViewPos;

        // 版本号
        private string m_version = "1.0.0";
        // true:目录打包   false:单资源打包
        private bool m_directoryBuild = false;

        public override void OnGUI()
        {
            base.OnGUI();

            // 打包资源源目录
            GUILayout.BeginHorizontal("box");
            {
                if (GUILayout.Button("选择目录", GUILayout.Height(20), GUILayout.Width(100)))
                {
                    selectFolder = EditorUtility.OpenFolderPanel("选择目录", Application.dataPath, "");

                    if (!string.IsNullOrEmpty(selectFolder))
                    {
                        selectFloderInfo.OnUpdate(selectFolder, null, 0, m_buildFileExtension);
                    }
                }

                if (GUILayout.Button("默认目录", GUILayout.Height(20), GUILayout.Width(100)))
                {
                    selectFloderInfo.OnUpdate(ResUtility.ResSourcePathTable, null, 0, m_buildFileExtension);
                }

                GUILayout.Label("打包资源目录：", GUILayout.Width(80));
                GUILayout.TextField(selectFloderInfo.AbsolutePath);
            }
            GUILayout.EndHorizontal();

            // 筛选相关
            GUILayout.BeginHorizontal("box");
            {
                if (GUILayout.Button("全选", GUILayout.Height(20), GUILayout.Width(100)))
                {
                    if (selectFloderInfo != null)
                    {
                        selectFloderInfo.IsToggle = true;
                        SelectFolder(selectFloderInfo);
                    }
                }

                if (GUILayout.Button("反选", GUILayout.Height(20), GUILayout.Width(100)))
                {
                    if (selectFloderInfo != null)
                    {
                        selectFloderInfo.IsToggle = false;
                        SelectFolder(selectFloderInfo);
                    }
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            {
                m_directoryBuild = CtrlEditor.Toggle("同资源包：", m_directoryBuild, 50, 40);
                m_version = CtrlEditor.TextField("版本号：", m_version, 60, 100);
            }
            GUILayout.EndHorizontal();

            // 详细目录/文件信息
            GUILayout.BeginVertical("box");
            {
                m_scrollViewPos = EditorGUILayout.BeginScrollView(m_scrollViewPos);
                OnGUIFolder(selectFloderInfo);
                EditorGUILayout.EndScrollView();
            }
            GUILayout.EndVertical();

            if (GUILayout.Button("导出配置文件", GUILayout.Height(30)))
            {
                ExportConfigFile();
            }

            if (GUILayout.Button("开始打包", GUILayout.Height(30)))
            {
                //ExportConfigFile();
                OnPackageAll();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            titleName = "表格资源打包";
            m_buildFileExtension = new string[1] { ".bytes" };
            resConfigFilePath = ResUtility.ResConfigFilePathTable;

            selectFloderInfo = new FloderInfo();
            selectFloderInfo.IsFoldout = true;
            selectFloderInfo.IsToggle = false;
            selectFloderInfo.OnUpdate(ResUtility.ResSourcePathTable, null, 0, m_buildFileExtension);
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

            AssetBundle.UnloadAllAssetBundles(true);

            string _outPath = ResUtility.AssetBundleOutPathTable;
            List<string> _resPaths = new List<string>();
            List<FileInfo> _resFileInfos = selectFloderInfo.GetFileInfos();

            if (!Directory.Exists(_outPath))
            {
                Directory.CreateDirectory(_outPath);
            }

            for (int i = 0, max = _resFileInfos.Count; i < max; i++)
            {
                if (_resFileInfos[i].IsToggle)
                {
                    _resPaths.Add(_resFileInfos[i].AbsolutePath.Replace(Application.dataPath, "Assets").Replace('\\', '/'));
                }
            }

            List<AssetBundleBuild> _assetBundles = new List<AssetBundleBuild>();

            if (m_directoryBuild)
            {
                AssetBundleBuild _assetBundleBuild = new AssetBundleBuild();
                _assetBundleBuild.assetBundleName = "table.unity3d";
                _assetBundleBuild.assetNames = _resPaths.ToArray();
                _assetBundles.Add(_assetBundleBuild);
            }
            else
            {
                for (int i = 0, max = _resFileInfos.Count; i < max; i++)
                {
                    if (_resFileInfos[i].IsToggle)
                    {
                        AssetBundleBuild _assetBundleBuild = new AssetBundleBuild();
                        _assetBundleBuild.assetBundleName = Path.GetFileNameWithoutExtension(_resFileInfos[i].Name) + ".unity3d";
                        _assetBundleBuild.assetNames = new string[] { _resFileInfos[i].AbsolutePath.Replace(Application.dataPath, "Assets").Replace('\\', '/') };
                        _assetBundles.Add(_assetBundleBuild);
                    }
                }
            }
            BuildPipeline.BuildAssetBundles(_outPath, _assetBundles.ToArray(), BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        }

        // 导出 - 配置文件(单个文件)
        private void ExportConfigFile()
        {
            List<FileInfo> _list = selectFloderInfo.GetFileInfos();

            Dictionary<string, string> _newMD5Map = new Dictionary<string, string>();

            // 新版本的资源配置信息
            ResConfigInfo _newConfigInfo = new ResConfigInfo();
            _newConfigInfo.version = m_version;
            _newConfigInfo.resInfos.Clear();
            for (int i = 0, max = _list.Count; i < max; i++)
            {
                FileInfo _fileInfo = _list[i];

                if (_fileInfo.IsToggle)
                {
                    ResInfo _resInfo = new ResInfo();
                    _resInfo.md5 = GetFileMD5(_fileInfo.AbsolutePath);
                    _resInfo.size = (ulong)GetFileSize(_fileInfo.AbsolutePath);
                    _resInfo.name = _fileInfo.AbsolutePath.Replace(Application.dataPath, "Assets").ToLower();
                    _resInfo.name = _resInfo.name.Substring(0, _resInfo.name.LastIndexOf('.')) + ".unity3d";
                    _resInfo.name = _resInfo.name.Replace("\\", "/");
                    _resInfo.assets.Clear();
                    _resInfo.dependencies.Clear();

                    _resInfo.assets.Add(_fileInfo.AbsolutePath.Replace(Application.dataPath, "Assets").ToLower());

                    string[] _dependesPath = AssetDatabase.GetDependencies(_fileInfo.AbsolutePath);
                    for (int j = 0, count = _dependesPath.Length; j < count; j++)
                    {
                        string _path = _dependesPath[i].Replace(Application.dataPath, "Assets").ToLower();
                        _resInfo.dependencies.Add(_path);
                    }

                    _newConfigInfo.resInfos.Add(_resInfo);
                    _newMD5Map.Add(_resInfo.name, _resInfo.md5);
                }
            }

            // 上一个版本的资源配置信息
            ResConfigInfo _oldConfigInfo = null;
            if (File.Exists(resConfigFilePath))
            {
                _oldConfigInfo = Utility.ZProtobuf.Deserialize<ResConfigInfo>(resConfigFilePath);
            }

            // 存在老资源配置信息，对比将需要更新的资源拿出来
            if (_oldConfigInfo != null)
            {
                Dictionary<string, string> _oldMD5Map = new Dictionary<string, string>();
                for (int i = 0; i < _oldConfigInfo.resInfos.Count; i++)
                {
                    _oldMD5Map.Add(_oldConfigInfo.resInfos[i].name, _oldConfigInfo.resInfos[i].md5);
                }

                for (int i = 0; i < _newConfigInfo.resInfos.Count; i++)
                {
                    if (!_oldMD5Map.ContainsKey(_newConfigInfo.resInfos[i].name))
                    {
                        Debug.Log("需要增加" + _newConfigInfo.resInfos[i].name);
                    }
                    else if (_oldMD5Map[_newConfigInfo.resInfos[i].name] != _newConfigInfo.resInfos[i].md5)
                    {
                        Debug.Log("需要更新" + _newConfigInfo.resInfos[i].name);
                    }
                }

                for (int i = 0; i < _oldConfigInfo.resInfos.Count; i++)
                {
                    if (!_newMD5Map.ContainsKey(_oldConfigInfo.resInfos[i].name))
                    {
                        Debug.Log("需要删除" + _oldConfigInfo.resInfos[i].name);
                    }
                }
            }

            Utility.ZProtobuf.Serialize<ResConfigInfo>(_newConfigInfo, resConfigFilePath);
        }

        // 导出 - 配置文件(单个文件夹)
        private void ExportConfigFloder()
        {
            // 上一个版本的资源配置信息
            ResConfigInfo _oldConfigInfo = null;
            if (File.Exists(resConfigFilePath))
            {
                _oldConfigInfo = Utility.ZProtobuf.Deserialize<ResConfigInfo>(resConfigFilePath);
            }

            if (_oldConfigInfo != null)
            {
                
            }
        }
    }
}