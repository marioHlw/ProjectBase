using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace zb.NGUILibrary
{
    public class UITreeTableData
    {
        private string m_name;                          // 名字
        private object m_data = null;                   // 数据
        private int m_level;                            // 所属层级 最顶层为1
        private int m_index;                            // 所属层级中的索引
        private bool m_isParent = false;                // 父级标示
        private UITreeTableData m_parent = null;        // 父级对象
        private List<UITreeTableData> m_childs;         // 子集数据集合
        private int m_childCount;                       // 子集数据集合数量

        public string Name { get { return m_name; } }
        public object Data { get { return m_data; } }
        public int Level { get { return m_level; } }
        public int Index { get { return m_index; } }
        public bool IsParent { get { return m_isParent; } }
        public UITreeTableData Parent { get { return m_parent; } }
        public List<UITreeTableData> Childs { get { return m_childs; } }
        public int ChildCount { get { return m_childCount; } }

        /// <summary>
        /// 实例化数据类
        /// </summary>
        /// <param name="parent">是否是父级</param>
        /// <param name="name">名字</param>
        /// <param name="data">自定义数据</param>
        public UITreeTableData(bool parent, string name = "", object data = null)
        {
            m_isParent = parent;
            m_name = name;
            m_data = data;
        }

        public void SetParent(UITreeTableData data)
        {
            m_parent = data;
        }

        public void SetLevel(int level)
        {
            m_level = level;
        }

        public void SetIndex(int index)
        {
            m_index = index;
        }

        public void AddChild(UITreeTableData data)
        {
            if (m_childs == null)
            {
                m_childs = new List<UITreeTableData>();
            }

            data.SetParent(this);
            data.SetLevel(m_level + 1);
            data.SetIndex(m_childCount);

            m_childs.Add(data);
            m_childCount++;
        }

        public void AddChild(bool parent, string name = "", object data = null)
        {
            AddChild(new UITreeTableData(parent, name, data));
        }
    }
}