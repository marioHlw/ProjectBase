/**************************
 * 文件名:FileInfo.cs
 * 文件描述:文件信息
 * 创建日期:2019/11/13
 * 作者:ZB
 ***************************/



using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace Res
{
    public class FileInfo
    {
        public string Name;                                     // 名称
        public string AbsolutePath;                             // 绝对路径
        public FloderInfo BelongFloder;                         // 所属目录信息
        public bool IsToggle;
        public int Layer;

        public void OnUpdate(string absolutePath, FloderInfo belongFloder, int layer)
        {
            Layer = layer;
            AbsolutePath = absolutePath;
            BelongFloder = belongFloder;
            Name = Path.GetFileName(AbsolutePath);
        }

        public int GetSize()
        {
            int _value = 0;

            if (File.Exists(AbsolutePath))
            {
                FileStream _fs = new FileStream(AbsolutePath, FileMode.Open);

                if (_fs != null)
                {
                    _value = (int)(_fs.Length);

                    _fs.Close();
                }
            }

            return _value;
        }
    }
}