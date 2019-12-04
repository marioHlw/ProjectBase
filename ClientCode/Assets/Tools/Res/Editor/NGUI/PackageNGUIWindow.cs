/**************************
 * 文件名:PackageNGUIWindow.cs
 * 文件描述:NGUI资源打包窗口类
 * 创建日期:2019/11/02
 * 作者:ZB
 ***************************/


using NetProto;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Res
{
    public class PackageNGUIWindow : PackageBaseWindow
    {       
        public override void OnGUI()
        {
            base.OnGUI();            
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            titleName = "NGUI资源打包";
            //m_buildFileExtension = new string[1] { ".prefab" };
            //resConfigFilePath = ResUtility.ResConfigFilePathNGUI;
        }

        public override void OnSelectionChange()
        {
            base.OnSelectionChange();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        //public override void OnSelectDefaultFloder()
        //{
        //    base.OnSelectDefaultFloder();

        //    selectFolder = ResUtility.ResSourcePathNGUI;
        //}

        public override void OnPackageAll()
        {
            base.OnPackageAll();

            Singleton<PackageNGUIEditor>.Instance.OnPackageAll();
        }
    }
}