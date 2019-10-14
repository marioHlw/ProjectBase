/**************************
 * 文件名:Package.cs
 * 文件描述:资源打包类
 * 创建日期:2019/08/01
 * 作者:ZB
 ***************************/




using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Package
{
    public class BasePackage
    {
        [MenuItem("Tools/资源打包/UI")]
        public static void OnPackageNGUI()
        {
            UtilityPackage.OnClearConsole();
            Singleton<NGUIPackage>.Instance.OnPackageAll();
        }
    }
}