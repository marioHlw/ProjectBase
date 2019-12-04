/*****************************************************
 * 文件名:AtlasEditor.cs
 * 文件描述:自动设置所有UI图集类
 * 1.自动设置所有UI图集，使用Unity的图集机制 SpritePacker
 * 2.所有UIAtlas目录下的图片，都会根据其所在目录被设置成图集
 * 创建日期:2019/11/22
 * 作者:ZB
 *****************************************************/


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace zb.UGUILibrary
{
    public class AtlasEditor : Editor
    {
        private static string AtlasSourceFolder = "Assets/Project/UI/UGUI/UIAtlas/";

        [MenuItem("Tools/UGUI/生成图集", false, 3)]
        public static void Execute()
        {
            Object[] _selects = Selection.objects;

            for (int i = 0; i < _selects.Length; i++)
            {
                string _path = AssetDatabase.GetAssetPath(_selects[i]);
                string _folderName = _path.Substring(_path.LastIndexOf('/') + 1);
                string _fullPath = Application.dataPath.Replace("Assets", "") + _path;
                string _fullAtlasPath = _fullPath + "/Atlas_" + _folderName + ".spriteatlas";
                string _atlasPath = _fullAtlasPath.Replace(Application.dataPath, "Assets");

                if (File.Exists(_fullAtlasPath))
                {
                    File.Delete(_fullAtlasPath);
                }

                // 是文件夹
                if (Directory.Exists(_fullPath))
                {
                    // 可以打图集
                    if (_path.StartsWith(AtlasSourceFolder))
                    {
                        // 修改图片信息
                        string[] _imgFiles = Directory.GetFiles(_fullPath, "*.png");
                        for (int j = 0, max = _imgFiles.Length; j < max; j++)
                        {
                            string _imgPath = _imgFiles[j].Replace(Application.dataPath, "Assets");
                            TextureImporter texImpoter = TextureImporter.GetAtPath(_imgPath) as TextureImporter;
                            if (texImpoter != null)
                            {
                                texImpoter.spriteImportMode = SpriteImportMode.Single;
                                texImpoter.textureType = TextureImporterType.Sprite;
                                texImpoter.mipmapEnabled = false;

                                // 原图非真彩，安卓进行压缩分离 
                                //if (texImpoter.textureFormat != TextureImporterFormat.AutomaticTruecolor && 
                                //    !texImpoter.textureFormat.ToString().Contains("32")) 
                                //{ 
                                //    texImpoter.SetPlatformTextureSettings("Android", texImpoter.maxTextureSize, texImpoter.textureFormat, true); 
                                //} 
                            }
                            AssetDatabase.ImportAsset(_imgPath);
                        }

                        SpriteAtlas _spriteAtlas = new SpriteAtlas();
                        _spriteAtlas.name = "Atlas_" + _folderName;

                        // 设置参数 可根据项目具体情况进行设置
                        SpriteAtlasPackingSettings packSetting = new SpriteAtlasPackingSettings()
                        {
                            blockOffset = 1,
                            enableRotation = false,
                            enableTightPacking = false,
                            padding = 2,
                        };
                        _spriteAtlas.SetPackingSettings(packSetting);

                        SpriteAtlasTextureSettings textureSetting = new SpriteAtlasTextureSettings()
                        {
                            readable = false,
                            generateMipMaps = false,
                            sRGB = true,
                            filterMode = FilterMode.Bilinear,
                        };
                        _spriteAtlas.SetTextureSettings(textureSetting);

                        TextureImporterPlatformSettings platformSetting = new TextureImporterPlatformSettings()
                        {
                            maxTextureSize = 2048,
                            format = TextureImporterFormat.Automatic,
                            crunchedCompression = true,
                            textureCompression = TextureImporterCompression.Compressed,
                            compressionQuality = 50,
                        };
                        _spriteAtlas.SetPlatformSettings(platformSetting);

                        AssetDatabase.CreateAsset(_spriteAtlas, _atlasPath);

                        _spriteAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(_atlasPath);

                        // 1、添加文件
                        DirectoryInfo _dir = new DirectoryInfo(_fullPath);
                        FileInfo[] _fileInfos = _dir.GetFiles("*.png");
                        foreach (FileInfo file in _fileInfos)
                        {
                            Object[] _objs = new[] { AssetDatabase.LoadAssetAtPath<Sprite>($"{_path}/{file.Name}") };
                            _spriteAtlas.Add(_objs);
                        }
                        // 2、添加文件夹
                        Object _obj = AssetDatabase.LoadAssetAtPath(_path, typeof(Object));
                        _spriteAtlas.Add(new[] { _obj });

                        SpriteAtlasUtility.PackAllAtlases(BuildTarget.StandaloneWindows);

                        AssetDatabase.SaveAssets();
                        Debug.Log(string.Format("成功创建图集：{0}", _atlasPath));
                    }
                    else
                    {
                        Debug.LogError(string.Format("{0}不是{1}下的子文件夹，无法创建图集", _path, AtlasSourceFolder));
                    }
                }
                else
                {
                    Debug.LogError(string.Format("{0}不是文件夹，无法创建图集", _path));
                }
            }
        }
    }
}