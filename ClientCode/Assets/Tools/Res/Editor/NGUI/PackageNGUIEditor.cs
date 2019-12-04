/**************************
 * 文件名:PackageNGUIEditor.cs
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

namespace Res
{
    public class PackageNGUIEditor : Singleton<PackageNGUIEditor>, IPackageEditor
    {
        private string m_sourceDir;                     // 需要打包资源文件夹路径
        private string m_outDir;                        // 资源打包输出文件夹路径
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
            m_outDir = ResUtility.AssetBundleOutRelativePath + "ui";
            if (!Directory.Exists(m_outDir))
            {
                Directory.CreateDirectory(m_outDir);
            }

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

                Debug.Log(_outPath);
                Debug.Log(_sourcePath);
                _assetBundle = new AssetBundleBuild();
                _assetBundle.assetBundleName = _outPath;
                _assetBundle.assetNames = new string[] { _sourcePath };
                _assetBundles.Add(_assetBundle);
            }

            //if (_assetBundles.Count > 0)
            //{
            //    BuildPipeline.BuildAssetBundles(m_outDir, _assetBundles.ToArray(), BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);
            //}
        }

        public void OnSubmitSvn()
        {

        }

        public void OnOver()
        {

        }

        // 删除纹理集的alpha通道
        public static void OnRemoveAtlasAlphaTex(string path)
        {
            if (!Directory.Exists(path)) return;

            string[] _files = Directory.GetFiles(path, "*.prefab", SearchOption.AllDirectories);
            for (int i = 0, max = _files.Length; i < max; i++)
            {
                GameObject _tempObj = AssetDatabase.LoadAssetAtPath(_files[i].Replace(Application.dataPath, "Assets"), typeof(GameObject)) as GameObject;
                if (_tempObj != null)
                {
                    UIAtlas _tempAtals = _tempObj.GetComponent<UIAtlas>();
                    if (_tempAtals != null && _tempAtals.spriteMaterial != null)
                    {
                        Texture _tempTex = _tempAtals.spriteMaterial.GetTexture("_AlphaTex");
                        if (_tempTex != null)
                        {
                            File.Delete(AssetDatabase.GetAssetPath(_tempTex));
                        }
                    }
                }
            }
            AssetDatabase.Refresh();
        }

        // 清除平台设置
        public static void OnClearPlatformTextureSettings(UIAtlas atlas)
        {
            if (atlas.texture != null && atlas.spriteMaterial != null)
            {
                Texture mainTex = atlas.spriteMaterial.GetTexture("_MainTex");
                Texture alphaTex = atlas.spriteMaterial.GetTexture("_AlphaTex");

                if (alphaTex != null)
                {
                    string AlphaPath = AssetDatabase.GetAssetPath((Texture2D)alphaTex);
                    if (File.Exists(AlphaPath))
                    {
                        File.Delete(AlphaPath);
                    }
                }

                if (mainTex != null)
                {
                    string SourcePath = AssetDatabase.GetAssetPath(mainTex);
                    TextureImporter SoureTextureImporter = AssetImporter.GetAtPath(SourcePath) as TextureImporter;
                    SoureTextureImporter.isReadable = true;
                    SoureTextureImporter.mipmapEnabled = false;
                    SoureTextureImporter.textureType = TextureImporterType.Default;
                    SoureTextureImporter.textureCompression = TextureImporterCompression.Uncompressed;
     
                    TextureImporterPlatformSettings androidSetting = new TextureImporterPlatformSettings();
                    androidSetting.overridden = true;
                    androidSetting.name = "Android";
                    androidSetting.maxTextureSize = 2048;
                    androidSetting.compressionQuality = 100;
                    androidSetting.allowsAlphaSplitting = true;
                    androidSetting.format = TextureImporterFormat.RGBA32;
                    SoureTextureImporter.SetPlatformTextureSettings(androidSetting);
                    androidSetting.name = "iPhone";
                    SoureTextureImporter.SetPlatformTextureSettings(androidSetting);
                    AssetDatabase.ImportAsset(SourcePath);
                    AssetDatabase.Refresh();
                }

                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }
        }

        public static void OnChangeAtlasFormat(UIAtlas atlas, string url = "")
        {
            if (!CheckTextureSize(atlas))
            {
                Debug.LogError("转换失败：纹理大小必须为<=1024的正方形 ！  name:" + atlas.name + "   path:" + url);
                return;
            }

            TextureImporterFormat aFormat = TextureImporterFormat.ETC2_RGBA8;
            TextureImporterFormat iFormat = TextureImporterFormat.PVRTC_RGB4;

            if (atlas.name.IndexOf("_RGBA32") != -1)
            {
                aFormat = TextureImporterFormat.RGBA32;
                iFormat = TextureImporterFormat.RGBA32;
            }
            else if (atlas.name.IndexOf("_RGBA16") != -1)
            {
                aFormat = TextureImporterFormat.RGBA16;
                iFormat = TextureImporterFormat.RGBA16;
            }
            else if (atlas.name.IndexOf("_RGB16") != -1)
            {
                aFormat = TextureImporterFormat.RGB16;
                iFormat = TextureImporterFormat.RGB16;
            }
            else if (atlas.name.IndexOf("_RGB24") != -1)
            {
                aFormat = TextureImporterFormat.RGB24;
                iFormat = TextureImporterFormat.RGB24;
            }

            if (atlas.name.IndexOf("_IOSRGBA32") != -1)
            {
                iFormat = TextureImporterFormat.RGBA32;
            }
            else if (atlas.name.IndexOf("_IOSRGBA16") != -1)
            {
                iFormat = TextureImporterFormat.RGBA16;
            }
            else if (atlas.name.IndexOf("_IOSRGB24") != -1)
            {
                iFormat = TextureImporterFormat.RGB24;
            }
            else if (atlas.name.IndexOf("_IOSRGB16") != -1)
            {
                iFormat = TextureImporterFormat.RGB16;
            }

            if (atlas.name.IndexOf("_ANDRGBA32") != -1)
            {
                aFormat = TextureImporterFormat.RGBA32;
            }
            else if (atlas.name.IndexOf("_ANDRGBA16") != -1)
            {
                aFormat = TextureImporterFormat.RGBA16;
            }
            else if (atlas.name.IndexOf("_ANDRGB16") != -1)
            {
                aFormat = TextureImporterFormat.RGB16;
            }
            else if (atlas.name.IndexOf("_ANDRGB24") != -1)
            {
                aFormat = TextureImporterFormat.RGB24;
            }
            else if (atlas.name.IndexOf("_ANDRGB16") != -1)
            {
                aFormat = TextureImporterFormat.RGB16;
            }

            ChangeAtlasType(atlas, aFormat, iFormat);
        }

        private static void ChangeAtlasType(UIAtlas atlas, TextureImporterFormat aFormat, TextureImporterFormat iFormat)
        {
            TextureImporter SoureTextureImporter = null;
            TextureImporter AlphaTextureImporter = null;
            Texture2D SourceTexture = null;
            Texture2D AlphaTexture = null;
            string SourcePath = "";
            string AlphaPath = "";

            if (atlas.texture != null && atlas.spriteMaterial != null)
            {
                if (!atlas.spriteMaterial.HasProperty("_AlphaTex"))
                {
                    Debug.LogError(atlas.name + "材质Shader已替换为Unlit/Transparent Colored（" + atlas.spriteMaterial.shader + "）");
                    atlas.spriteMaterial.shader = Shader.Find("Unlit/Transparent Colored");
                    AssetDatabase.Refresh();
                }
                Texture mainTex = atlas.spriteMaterial.GetTexture("_MainTex");
                Texture alphaTex = atlas.spriteMaterial.GetTexture("_AlphaTex");
                if (mainTex != null)
                {
                    SourceTexture = (Texture2D)mainTex;
                    SourcePath = AssetDatabase.GetAssetPath(SourceTexture);
                    SoureTextureImporter = AssetImporter.GetAtPath(SourcePath) as TextureImporter;
                }
                if (alphaTex != null)
                {
                    AlphaTexture = (Texture2D)alphaTex;
                    AlphaPath = AssetDatabase.GetAssetPath(AlphaTexture);
                }
            }
            if (SoureTextureImporter == null)
            {
                return;
            }
            SoureTextureImporter.isReadable = true;
            SoureTextureImporter.mipmapEnabled = false;
            SoureTextureImporter.textureType = TextureImporterType.Default;
            if (iFormat == TextureImporterFormat.PVRTC_RGB4 || iFormat == TextureImporterFormat.RGB16 || iFormat == TextureImporterFormat.RGB24)
            {
                bool isCreatAlpha = true;
                if (!string.IsNullOrEmpty(AlphaPath))
                {
                    DateTime SoureLastWriteTime = File.GetLastWriteTime(Application.dataPath + SourcePath.Replace("Assets/", "/"));
                    DateTime AlphaLastWriteTime = File.GetLastWriteTime(Application.dataPath + AlphaPath.Replace("Assets/", "/"));
                    if (AlphaLastWriteTime < SoureLastWriteTime)
                    {
                        isCreatAlpha = true;
                        File.Delete(AlphaPath);
                    }
                    else
                    {
                        isCreatAlpha = false;
                    }
                }
                if (isCreatAlpha)
                {
                    SoureTextureImporter.ClearPlatformTextureSettings("Android");
                    SoureTextureImporter.ClearPlatformTextureSettings("iPhone");
                    //SoureTextureImporter.SetPlatformTextureSettings("Android", 2048, TextureImporterFormat.RGBA32, 80, true);
                    //SoureTextureImporter.SetPlatformTextureSettings("iPhone", 2048, TextureImporterFormat.RGBA32, 100, true);
                    AssetDatabase.ImportAsset(SourcePath);
                    AssetDatabase.Refresh();

                    AlphaPath = Path.GetFileNameWithoutExtension(SourcePath);
                    AlphaPath = SourcePath.Replace(Path.GetFileName(SourcePath), AlphaPath) + "_Alpha.png";
                    AlphaTexture = new Texture2D(SourceTexture.width, SourceTexture.height, TextureFormat.RGBA32, false);
                    Color32[] color32S = AlphaTexture.GetPixels32();
                    Color32[] srcColor32S = SourceTexture.GetPixels32();
                    for (int n = 0; n < color32S.Length; ++n)
                    {
                        color32S[n] = new Color32(srcColor32S[n].a, 0, 0, 255);
                    }
                    AlphaTexture.SetPixels32(color32S);
                    AlphaTexture.Apply(false);
                    File.WriteAllBytes(Application.dataPath.Substring(0, Application.dataPath.Length - 6) + AlphaPath, AlphaTexture.EncodeToPNG());
                    AssetDatabase.Refresh(ImportAssetOptions.Default);
                    AlphaTextureImporter = AssetImporter.GetAtPath(AlphaPath) as TextureImporter;
                    AlphaTextureImporter.textureType = TextureImporterType.Default;
                    AlphaTextureImporter.spriteImportMode = SpriteImportMode.None;
                    AlphaTextureImporter.mipmapEnabled = false;
                    AlphaTextureImporter.isReadable = false;


                    TextureImporterPlatformSettings iosseting = new TextureImporterPlatformSettings();
                    iosseting.overridden = true;
                    iosseting.name = "iPhone";
                    iosseting.maxTextureSize = 2048;
                    iosseting.compressionQuality = 100;
                    iosseting.allowsAlphaSplitting = true;
                    iosseting.format = iFormat;

                    AlphaTextureImporter.SetPlatformTextureSettings(iosseting);

                    AlphaTexture = AssetDatabase.LoadAssetAtPath(AlphaPath, typeof(Texture2D)) as Texture2D;
                    if (AlphaTexture != null)
                    {
                        atlas.spriteMaterial.SetTexture("_AlphaTex", AlphaTexture);
                        atlas.spriteMaterial.EnableKeyword("R_CHANNEL");
                    }
                }
            }
            else if (!string.IsNullOrEmpty(AlphaPath))
            {
                File.Delete(AlphaPath);
            }
            SoureTextureImporter.isReadable = false;
            bool isSetFormat = false;
            int size = 2048;
            TextureImporterFormat targetFormat;
            if (SoureTextureImporter.GetPlatformTextureSettings("Android", out size, out targetFormat) && targetFormat == aFormat)
            {

            }
            else
            {
                TextureImporterPlatformSettings androidSetting = new TextureImporterPlatformSettings();
                androidSetting.overridden = true;
                androidSetting.name = "Android";
                androidSetting.maxTextureSize = 2048;
                androidSetting.compressionQuality = 80;
                androidSetting.allowsAlphaSplitting = true;
                androidSetting.format = aFormat;
                SoureTextureImporter.SetPlatformTextureSettings(androidSetting);
                isSetFormat = true;
            }
            if (SoureTextureImporter.GetPlatformTextureSettings("iPhone", out size, out targetFormat) && targetFormat == iFormat)
            {

            }
            else
            {
                TextureImporterPlatformSettings iosSetting = new TextureImporterPlatformSettings();
                iosSetting.overridden = true;
                iosSetting.name = "iPhone";
                iosSetting.maxTextureSize = 2048;
                iosSetting.compressionQuality = 100;
                iosSetting.allowsAlphaSplitting = true;
                iosSetting.format = iFormat;

                SoureTextureImporter.SetPlatformTextureSettings(iosSetting);
                isSetFormat = true;
            }
            if (isSetFormat)
            {
                AssetDatabase.ImportAsset(SourcePath);
                AssetDatabase.Refresh();
            }
        }

        // 检查纹理大小是否是<=1024，,并且为正方形
        private static bool CheckTextureSize(UIAtlas atlas)
        {
            if (atlas.spriteMaterial != null)
            {
                Texture mainTex = atlas.spriteMaterial.GetTexture("_MainTex");

                if (mainTex != null && mainTex.width == mainTex.height && mainTex.width <= 1024)
                {
                    return true;
                }
            }
            return false;
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