/**************************
 * 文件名:NGUIPackage.cs
 * 文件描述:NGUI资源打包类
 * 创建日期:2019/08/01
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using NGUI.Tools;
using UnityEditor;

namespace Package
{
    public class NGUIPackage : Singleton<NGUIPackage>, IPackageOperate
    {
        private string m_sourceDir;                     // 需要打包资源文件夹路径
        private string m_assetBundleOutDir;             // 资源打包输出文件夹路径
        private Dictionary<string, string> m_assetBundleMap = new Dictionary<string, string>();

        public void OnUpdateSvn()
        {

        }

        public void OnProcessAll()
        {

        }

        public void OnPackageAll()
        {
            //AtlasEditor.OnClearUnuseAtlas(EditorUserBuildSettings.activeBuildTarget);

            m_sourceDir = Application.dataPath + "/Project/UI";
            m_assetBundleOutDir = UtilityPackage.AssetBundleOutPath + "ui"; ;
            m_assetBundleMap.Clear();

            List<string> _sourcePaths = new List<string>();         // 需要打包的资源(不包含依赖资源)
            List<string> _assetPaths = new List<string>();          // 需要打包的资源(包含依赖资源)

            GetSourcePaths(m_sourceDir, _sourcePaths, m_assetBundleMap);
            _assetPaths.AddRange(_sourcePaths);
            for (int i = 0, length = _sourcePaths.Count; i < length; i++)
            {
                GetDependencies(_sourcePaths[i], _assetPaths);
            }

            List<AssetBundleBuild> _assetBundles = new List<AssetBundleBuild>();
            AssetBundleBuild _assetBundle;
            string _sourcePath;
            string _outPath;

            for (int i = 0, length = _assetPaths.Count; i < length; i++)
            {
                _sourcePath = _assetPaths[i];

                if (m_assetBundleMap.TryGetValue(_sourcePath, out _outPath))
                {
                    _outPath = _outPath.Replace(".prefab", ".unity3d");
                }
                else
                {
                    _outPath = Path.GetFileNameWithoutExtension(_sourcePath) + ".unity3d";
                }

                _assetBundle = new AssetBundleBuild();
                _assetBundle.assetBundleName = _outPath;
                _assetBundle.assetNames = new string[] { _sourcePath };
                _assetBundles.Add(_assetBundle);
            }

            if (_assetBundles.Count > 0)
            {
                BuildPipeline.BuildAssetBundles(m_assetBundleOutDir, _assetBundles.ToArray(), BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);
            }
        }

        public void OnSubmitSvn()
        {

        }

        public void OnOver()
        {

        }

        private string GetFileMD5(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return "";
                }

                FileStream _fs = new FileStream(filePath, FileMode.Open);
                System.Security.Cryptography.MD5 _md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] _byteArray = _md5.ComputeHash(_fs);
                _fs.Close();

                StringBuilder _stringBuilder = new StringBuilder();
                for (int i = 0, length = _byteArray.Length; i<length; i++)
                {
                    _stringBuilder.Append(_byteArray[i].ToString("x2"));
                }

                return _stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Package.NGUIPackage.GetFileMD5() fail, error:" + ex.Message);
            }
        }

        private void GetSourcePaths(string dir, List<string> paths, Dictionary<string, string> pathMap)
        {
            string[] _subsetDirArray = Directory.GetDirectories(dir);        // 返回指定目录中的子目录的名称(包括其路径)
            string[] _pathArray = null;                                      // 目录下所有文件
            string _path = null;
            int _index = 0;

            for (int i = 0, length = _subsetDirArray.Length; i < length; i++)
            {
                _path = _subsetDirArray[i];

                if (Path.GetFileName(_path) == "Resources")
                {
                    _index = _path.Length + 1;

                    _pathArray = Directory.GetFiles(_path, "*.prefab", SearchOption.AllDirectories);

                    for (int j = 0, length1 = _pathArray.Length; j < length1; j++)
                    {
                        _path = _pathArray[j].Replace(Application.dataPath, "Assets").Replace("\\", "/");

                        if (!pathMap.ContainsKey(_path))
                        {
                            paths.Add(_path);
                            pathMap.Add(_path, _pathArray[j].Substring(_index).Replace("\\", "/"));
                        }
                    }
                }
                else
                {
                    GetSourcePaths(_path, paths, pathMap);
                }
            }
        }

        private void GetDependencies(string prefabPath, List<string> paths)
        {
            string _path = "";
            string _extension = "";     // 文件后缀名
            string[] _dependencieArray = AssetDatabase.GetDependencies(prefabPath, false);

            for (int i = 0; i < _dependencieArray.Length; i++)
            {
                _path = _dependencieArray[i];
                _extension = Path.GetExtension(_path).ToLower();

                if (_extension == ".prefab" && paths.IndexOf(_path) == -1)
                {
                    paths.Add(_path);

                    GetDependencies(_path, paths);
                }
                else if (_extension == ".ttf" && paths.IndexOf(_path) == -1)
                {
                    paths.Add(_path);
                }
                else
                {

                }
            }
        }
    }
}