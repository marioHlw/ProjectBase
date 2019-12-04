/**************************
 * 文件名:PackageDetailInfoWindow.cs
 * 文件描述:资源打包 - 详细信息窗口
 * 创建日期:2019/11/09
 * 作者:ZB
 ***************************/




using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Res
{
    public class PackageDetailInfoWindow : PackageBaseWindow
    {
        public override void OnGUI()
        {
            base.OnGUI();

            if (GUILayout.Button("NGUI资源详细信息", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("角色模型资源详细信息", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("场景资源详细信息", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("表格资源详细信息", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("特效资源详细信息", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("音效资源详细信息", GUILayout.Height(30)))
            {

            }

            if (GUILayout.Button("Lua资源详细信息", GUILayout.Height(30)))
            {

            }
        }
    }
}