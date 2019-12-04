using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Res
{
    public class ResUtility
    {
        /// <summary>
        /// AssetBundle打包输出文件夹相对路径
        /// </summary>

        public static string AssetBundleOutRelativePath
        {
            get
            {
                return Ctrl.device.FolderPlatform;
            }
        }

        /// <summary>
        /// AssetBundle打包输出文件夹绝对路径
        /// </summary>

        public static string AssetBundleOutAbsolutePath
        {
            get
            {
                return Ctrl.device.PathRoot;
            }
        }

        #region NGUI

        /// <summary>
        /// NGUI资源打包源路径
        /// </summary>

        public static string ResSourcePathNGUI
        {
            get
            {
                return Application.dataPath + "/Project/UI";
            }
        }

        /// <summary>
        /// NGUI资源打包输出路径
        /// </summary>

        public static string AssetBundleOutPathNGUI
        {
            get
            {
                return AssetBundleOutAbsolutePath + "ui/";
            }
        }

        /// <summary>
        /// NGUI资源打包配置信息文件输出路径
        /// </summary>

        public static string ResConfigFilePathNGUI
        {
            get
            {
                return AssetBundleOutAbsolutePath + "config_ngui.byte";
            }
        }

        #endregion

        #region UGUI

        /// <summary>
        /// NGUI资源打包源路径
        /// </summary>

        public static string ResSourcePathUGUI
        {
            get
            {
                return Application.dataPath + "/Project/UI/UGUI";
            }
        }

        /// <summary>
        /// NGUI资源打包输出路径
        /// </summary>

        public static string AssetBundleOutPathUGUI
        {
            get
            {
                return AssetBundleOutAbsolutePath + "ui/";
            }
        }

        /// <summary>
        /// NGUI资源打包配置信息文件输出路径
        /// </summary>

        public static string ResConfigFilePathUGUI
        {
            get
            {
                return AssetBundleOutAbsolutePath + "config_ugui.byte";
            }
        }

        #endregion

        #region Table

        /// <summary>
        /// 表格资源打包源路径
        /// </summary>

        public static string ResSourcePathTable
        {
            get
            {
                return Application.dataPath + "/Resources/Databin/TableRes";
            }
        }

        /// <summary>
        /// 表格资源打包输出路径
        /// </summary>

        public static string AssetBundleOutPathTable
        {
            get
            {
                return AssetBundleOutAbsolutePath + "table/";
            }
        }

        /// <summary>
        /// 表格资源打包配置信息文件输出路径
        /// </summary>

        public static string ResConfigFilePathTable
        {
            get
            {
                return AssetBundleOutAbsolutePath + "config_table.byte";
            }
        }

        #endregion
    }

    public enum enResType
    {
        UGUI_Prefab = 20,
        UGUI_Texture = 21,
        UGUI_Atlas = 22,
    }
}