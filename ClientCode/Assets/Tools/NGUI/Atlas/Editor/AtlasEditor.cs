/**************************
 * 文件名:FormatEditor.cs
 * 文件描述:NGUI纹理集编辑类
 * 创建日期:2019/08/01
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace NGUI.Tools
{
    public class AtlasEditor
    {
        [MenuItem("Tools/NGUI/转换选中纹理集")]
        public static void OnSwitchAtlas()
        {
            Debug.ClearDeveloperConsole();

            GameObject _go = Selection.activeGameObject as GameObject;
            if (_go == null || _go.GetComponent<UIAtlas>() == null)
            {
                Debug.LogError("未选择正确的纹理图集物体.");
                return;
            }

            UIAtlas _atlas = _go.GetComponent<UIAtlas>();
            OnClearPlatformSetting(_atlas);
        }

        [MenuItem("Tools/NGUI/清理纹理集的Alpha通道")]
        public static void OnClearAtlasAlphaTxture()
        {
            string[] _pathArray = Directory.GetFiles(Application.dataPath + "/Project/UI", "*.prefab", SearchOption.AllDirectories);

            for (int i = 0; i < _pathArray.Length; i++)
            {
                GameObject _go = AssetDatabase.LoadAssetAtPath(_pathArray[i].Replace(Application.dataPath, "Assets"), typeof(GameObject)) as GameObject;

                if (_go != null)
                {
                    UIAtlas atlas = _go.GetComponent<UIAtlas>();

                    if (atlas != null && atlas.spriteMaterial != null)
                    {
                        Texture _alphaTexture = atlas.spriteMaterial.GetTexture("_AlphaTex");
                        if (_alphaTexture != null)
                        {
                            File.Delete(AssetDatabase.GetAssetPath(_alphaTexture));
                        }
                    }
                }
            }
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 清理 - NGUI图集平台设置
        /// </summary>

        public static void OnClearPlatformSetting(UIAtlas atlas)
        {
            if (atlas == null)
            {
                return;
            }

            if (atlas.texture == null)
            {
                Debug.LogError(atlas.name + "的Texture未设置.");
                return;
            }

            if (atlas.spriteMaterial == null)
            {
                Debug.LogError(atlas.name + "的SpriteMaterial未设置.");
                return;
            }

            Texture _mainTexture = atlas.spriteMaterial.GetTexture("_MainTex");
            Texture _alphaTexture = atlas.spriteMaterial.GetTexture("_AlphaTex");

            if (_alphaTexture != null)
            {
                string _alphaPath = AssetDatabase.GetAssetPath((Texture2D)_alphaTexture);
                if (File.Exists(_alphaPath))
                {
                    File.Delete(_alphaPath);
                }
            }

            if (_mainTexture != null)
            {
                string _mainPath = AssetDatabase.GetAssetPath(_mainTexture);

                TextureImporter _textureImporter = AssetImporter.GetAtPath(_mainPath) as TextureImporter;
                _textureImporter.isReadable = true;
                _textureImporter.mipmapEnabled = false;
                _textureImporter.textureType = TextureImporterType.Default;
                _textureImporter.textureCompression = TextureImporterCompression.Uncompressed;

                TextureImporterPlatformSettings _androidSetting = new TextureImporterPlatformSettings();
                _androidSetting.overridden = true;
                _androidSetting.name = "Android";
                _androidSetting.maxTextureSize = 2048;
                _androidSetting.compressionQuality = 100;
                _androidSetting.allowsAlphaSplitting = true;
                _androidSetting.format = TextureImporterFormat.RGBA32;

                _textureImporter.SetPlatformTextureSettings(_androidSetting);

                _androidSetting.name = "iPhone";
                _textureImporter.SetPlatformTextureSettings(_androidSetting);

                AssetDatabase.ImportAsset(_mainPath);
                AssetDatabase.Refresh();
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// 清理 - 没有使用的NGUI图集
        /// </summary>

        public static void OnClearUnuseAtlas(BuildTarget target)
        {            
            string _assetBundlePath = Application.dataPath + "/Project/UI/AssetBundleResources";

            if (Directory.Exists(_assetBundlePath))
            {
                Directory.Delete(_assetBundlePath, true);
            }

            if (target == BuildTarget.Android)
            {
                OnClearAtlasAlphaTxture();
            }
        }
    }
}