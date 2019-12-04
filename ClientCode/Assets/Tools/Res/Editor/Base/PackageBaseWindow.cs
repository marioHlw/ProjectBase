/**************************
 * 文件名:PackageBaseWindow.cs
 * 文件描述:资源打包 - 窗口基类
 * 创建日期:2019/11/09
 * 作者:ZB
 ***************************/



using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using NetProto;
using System.IO;
using System.Text;
using System;

namespace Res
{
    public class PackageBaseWindow
    {
        // 窗口头名称   
        public string titleName = "";

        // 更新 - (切换时调用一次)
        public virtual void OnUpdate()
        {

        }

        public virtual void OnGUI()
        {
        }

        // 退出 - (退出时调用一次)
        public virtual void OnExit()
        {

        }

        // 选择 - 改变选择物体时调用
        public virtual void OnSelectionChange()
        {

        }

        // 打包 - 资源
        public virtual void OnPackageAll()
        {

        }

        // 绘制 - 目录
        protected void OnGUIFolder(FloderInfo floderInfo)
        {
            if (floderInfo == null) return;

            if (floderInfo.IsFoldout)
            {
                for (int i = 0, max = floderInfo.ChildFloderInfos.Count; i < max; i++)
                {
                    FloderInfo _tempFloder = floderInfo.ChildFloderInfos[i];
                    EditorGUILayout.BeginHorizontal();
                    {
                        bool _state = _tempFloder.IsToggle;
                        GUILayout.Space((_tempFloder.Layer - 1) * 20);
                        _tempFloder.IsToggle = EditorGUILayout.Toggle(_tempFloder.IsToggle, GUILayout.Width(15));
                        if (_state != _tempFloder.IsToggle)
                        {
                            SelectFolder(_tempFloder);
                        }
                        _tempFloder.IsFoldout = EditorGUILayout.Foldout(_tempFloder.IsFoldout, _tempFloder.AbsolutePath, EditorStyles.foldout);
                    }
                    EditorGUILayout.EndHorizontal();

                    OnGUIFolder(_tempFloder);
                }

                for (int j = 0, count = floderInfo.ChildFileInfos.Count; j < count; j++)
                {
                    FileInfo _fileInfo = floderInfo.ChildFileInfos[j];
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space((_fileInfo.Layer - 1) * 20);
                        _fileInfo.IsToggle = EditorGUILayout.Toggle(_fileInfo.IsToggle, GUILayout.Width(15));
                        EditorGUILayout.LabelField(new GUIContent(_fileInfo.AbsolutePath, CtrlEditor.GetTextureFormExtension(Path.GetExtension(_fileInfo.AbsolutePath))));
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        // 选择 - 目录
        protected void SelectFolder(FloderInfo floderInfo)
        {
            for (int i = 0, max = floderInfo.ChildFloderInfos.Count; i < max; i++)
            {
                floderInfo.ChildFloderInfos[i].IsToggle = floderInfo.IsToggle;
                SelectFolder(floderInfo.ChildFloderInfos[i]);
            }

            for (int j = 0, count = floderInfo.ChildFileInfos.Count; j < count; j++)
            {
                floderInfo.ChildFileInfos[j].IsToggle = floderInfo.IsToggle;
            }
        }

        // 获取 - 文件MD5值
        protected string GetFileMD5(string file)
        {
            try
            {
                if (!File.Exists(file))
                {
                    return "";
                }

                FileStream fs = new FileStream(file, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }

        // 获取 - 文件大小
        protected long GetFileSize(string file)
        {
            FileStream _fs = new FileStream(file, FileMode.Open);
            if (_fs != null)
            {
                long _size = _fs.Length;
                _fs.Close();

                return _size;
            }

            return 0;
        }
    }
}