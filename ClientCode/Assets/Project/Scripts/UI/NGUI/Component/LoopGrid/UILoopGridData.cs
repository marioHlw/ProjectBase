using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    public class UILoopGridData 
    {
        private int m_x;
        private int m_y;
        private int m_index = 0;
        private object m_data = null;

        public int X { get { return m_x; } }
        public int Y { get { return m_y; } }
        public int Index { get { return m_index; } }
        public object Data { get { return m_data; } }

        /// <summary>
        /// 实例化数据类
        /// </summary>
        /// <param name="index">系列号</param>
        /// <param name="x">横坐标</param>        
        /// <param name="y">纵坐标</param>

        public UILoopGridData(int index, int x, int y)
        {
            m_index = index;
            m_x = x;
            m_y = y;
        }

        /// <summary>
        /// 设置 - 位置索引
        /// </summary>

        public void SetIndex(int index)
        {
            m_index = index;
        }

        /// <summary>
        /// 设置 - 坐标系
        /// </summary>

        public virtual void SetXandY(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        /// <summary>
        /// 设置 - 自定义数据
        /// </summary>

        public void SetData(object data)
        {
            m_data = data;
        }
    }
}