/**************************
 * 文件名:UIDropDownMenuData.cs
 * 文件描述:下拉菜单子物体数据
 * 创建日期:2019/07/26
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    public class UIDropDownMenuData
    {
        private string m_name;                          // 名字
        private object m_data = null;                   // 数据
        private int m_index;                            // 索引

        public string Name { get { return m_name; } }
        public object Data { get { return m_data; } }
        public int Index { get { return m_index; } }

        /// <summary>
        /// 实例化数据类
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="name">名字</param>
        /// <param name="data">自定义数据</param>
        public UIDropDownMenuData(int index, string name = "", object data = null)
        {
            m_index = index;
            m_name = name;
            m_data = data;
        }
    }
}