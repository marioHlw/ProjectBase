/**************************
 * 文件名:UtilityPackage.cs
 * 文件描述:资源打包帮助类
 * 创建日期:2019/08/01
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Package
{
    public class UtilityPackage
    {
        /// <summary>
        /// AssetBundle打包文件输出路径
        /// </summary>

        public static string AssetBundleOutPath
        {
            get
            {
                if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
                {
                    return "DataRoot/PlatformAssets/iOS/";
                }

                if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                {
                    return "DataRoot/PlatformAssets/Android/";
                }

                return "DataRoot/PlatformAssets/Window/";
            }
        }

        /// <summary>
        /// AssetBundle打包平台资源输出文件夹路径
        /// </summary>

        public static string PlatformAssetsFolder
        {
            get
            {
                if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
                {
                    return Application.dataPath.Replace("Assets", "DataRoot/PlatformAssets") + "/iOS/";
                }

                if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                {
                    return Application.dataPath.Replace("Assets", "DataRoot/PlatformAssets") + "/Android/";
                }

                return Application.dataPath.Replace("Assets", "DataRoot/PlatformAssets") + "/Window/";
            }
        }

        /// <summary>
        /// 清理 - 控制台打印信息
        /// </summary>

        public static void OnClearConsole()
        {
            //获取UnityEditor程序集里面的UnityEditorInternal.LogEntries类型，也就是把关于Console的类提出来
            var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
            //在logEntries类里面找到名为Clear的方法，且其属性必须是public static的，等同于得到了Console控制台左上角的clear，然后通过Invoke进行点击实现
            var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

            clearMethod.Invoke(null, null);
        }
    }
}