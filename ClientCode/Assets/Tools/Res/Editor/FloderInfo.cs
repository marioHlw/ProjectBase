/**************************
 * 文件名:FloderInfo.cs
 * 文件描述:文件夹信息
 * 创建日期:2019/11/13
 * 作者:ZB
 ***************************/



using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace Res
{
    public class FloderInfo
    {
        public string Name;                                     // 名称
        public string AbsolutePath;                             // 绝对路径
        public FloderInfo BelongFloder;                         // 所属父级目录
        public List<FloderInfo> ChildFloderInfos;               // 子目录
        public List<FileInfo> ChildFileInfos;                   // 子文件信息
        public bool IsToggle;
        public bool IsFoldout;
        public int Layer;

        /// <summary>
        /// 更新 - 目录信息
        /// </summary>
        /// <param name="absolutePath">目录绝对路径</param>
        /// <param name="parent">所属父级目录(传空为顶层)</param>
        /// <param name="layer">所属层级(传0为顶层)</param>
        /// <param name="buildFileExtension">包含文件后缀名</param>

        public void OnUpdate(string absolutePath, FloderInfo parent, int layer, string[] buildFileExtension)
        {
            ChildFloderInfos = new List<FloderInfo>();
            ChildFileInfos = new List<FileInfo>();

            Layer = layer;
            AbsolutePath = absolutePath;
            BelongFloder = parent;
            Name = Path.GetDirectoryName(AbsolutePath);

            string[] _files = Directory.GetFiles(AbsolutePath, "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < _files.Length; i++)
            {
                // 过滤文件 
                bool _isBuild = false;
                for (int j = 0, max = buildFileExtension.Length; j < max; j++)
                {
                    if (_files[i].EndsWith(buildFileExtension[j]))
                    {
                        _isBuild = true;
                    }
                }
                if (!_isBuild)
                {
                    continue;
                }

                FileInfo _fileInfo = new FileInfo();
                _fileInfo.OnUpdate(_files[i].Replace('\\', '/'), this, layer + 1);
                ChildFileInfos.Add(_fileInfo);
            }

            string[] _floders = Directory.GetDirectories(AbsolutePath, "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < _floders.Length; i++)
            {
                FloderInfo _floderInfo = new FloderInfo();
                _floderInfo.OnUpdate(_floders[i].Replace('\\', '/'), this, layer + 1, buildFileExtension);
                ChildFloderInfos.Add(_floderInfo);
            }
        }

        // 获取 - 目录下所有文件信息，包括子目录
        public List<FileInfo> GetFileInfos()
        {
            List<FileInfo> _list = new List<FileInfo>(ChildFileInfos.ToArray());

            if (_list == null)
            {
                _list = new List<FileInfo>();
            }

            for (int i = 0; i < ChildFloderInfos.Count; i++)
            {
                List<FileInfo> _fileInfos = ChildFloderInfos[i].GetFileInfos();

                for (int j = 0; j < _fileInfos.Count; j++)
                {
                    _list.Add(_fileInfos[j]);
                }
            }

            return _list;
        }
    }
}