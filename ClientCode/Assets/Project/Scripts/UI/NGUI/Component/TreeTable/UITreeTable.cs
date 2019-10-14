/**************************
 * 文件名:NGUITreeTable.cs
 * 文件描述:NGUI树型表组件
 * 创建日期:2019/06/29
 * 作者:ZB
 ***************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace zb.NGUILibrary
{
    /// <summary>
    /// 选中后位置类型
    /// </summary>

    public enum UITreeSelectPosType
    {
        None = 0,

        /// <summary>
        /// 最上方
        /// </summary>

        Top,

        /// <summary>
        /// 居中
        /// </summary>

        Center,

        /// <summary>
        /// 最下方
        /// </summary>

        Down,
    }

    public class UITreeTable : MonoBehaviour
    {
        public delegate void DelegateSelectItem(UITreeTableData data);            // 选择子类回调委托

        public GameObject pool;
        public UITreeTableItem itemPrefab;

        public UITreeSelectPosType selectPosType = UITreeSelectPosType.Center;      // 选中后选中的物体将在那里显示
        public bool defaultFrist;           // 默认选中第一个
        public bool fatherExecute;          // 父级执行回调 
        public bool repeatExecute;          // 重复执行回调

        private DelegateSelectItem m_selectItemCallback = null;                     // 选择子类回调执行
        private DelegateSelectItem m_unselectItemCallback = null;                   // 取消选择子类回调执行

        private UITreeTableItem m_selectItem = null;                                // 选中的子类
        private UITreeTableItem m_selectParentItem = null;                          // 选中的父级
        private List<UITreeTableItem> m_itemFrees = new List<UITreeTableItem>();    // 还没有使用的Item
        private List<UITreeTableItem> m_itemShows = new List<UITreeTableItem>();    // 已经使用的Item
        private Dictionary<UITreeTableData, UITreeTableItem> m_dataItemMap = new Dictionary<UITreeTableData, UITreeTableItem>();        // 数据对应物体

        private UIPanel m_uiPanel = null;
        private UIScrollView m_uiScrollView;
        private UITable m_uiTable;

        public UITreeTableItem SelectItem { get { return m_selectItem; } }
        public UITreeTableItem SelectParentItem { get { return m_selectParentItem; } }
        public List<UITreeTableItem> Items { get { return m_itemShows; } }

        private void Awake()
        {
            m_uiPanel = transform.GetComponent<UIPanel>();
            m_uiScrollView = transform.GetComponent<UIScrollView>();
            m_uiTable = transform.GetComponentInChildren<UITable>();

            m_itemFrees.Add(itemPrefab);
        }

        private void Start()
        {
            UITreeTableData _data = new UITreeTableData(true);
            UITreeTableData _item;
            _item = new UITreeTableData(true);
            _data.AddChild(_item);
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item = new UITreeTableData(true);
            _data.AddChild(_item);
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item = new UITreeTableData(true);
            _data.AddChild(_item);
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item = new UITreeTableData(true);
            _data.AddChild(_item);
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item = new UITreeTableData(true);
            _data.AddChild(_item);
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item = new UITreeTableData(true);
            _data.AddChild(_item);
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item = new UITreeTableData(true);
            _data.AddChild(_item);
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item = new UITreeTableData(true);
            _data.AddChild(_item);
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));
            _item.AddChild(new UITreeTableData(false));

            OnInit(_data);
        }

        public void OnInit(UITreeTableData data)
        {
            if (m_uiScrollView == null || pool == null || itemPrefab == null || m_uiTable == null || m_uiPanel == null)
            {
                Debug.LogError("配置错误，请检查。");
                return;
            }

            // 确保显示在上面
            UIPanel _parentPanel = NGUITools.FindInParents<UIPanel>(gameObject);
            m_uiPanel = m_uiScrollView.GetComponent<UIPanel>();
            m_uiPanel.depth = _parentPanel.depth + 2;
            m_uiPanel.renderQueue = _parentPanel.renderQueue;
            m_uiPanel.startingRenderQueue = _parentPanel.startingRenderQueue + 10;

            DrawItem(data);
        }

        public void OnClear()
        {
            m_selectItem = null;
            m_selectParentItem = null;
            m_dataItemMap.Clear();
            for (int i = m_itemShows.Count - 1; i >= 0; i--)
            {
                RecycleItem(m_itemShows[i]);
            }
        }

        public void SetSelectItemCallback(DelegateSelectItem delegateSelectItem)
        {
            m_selectItemCallback = delegateSelectItem;
        }

        public void SetUnselectItemCallback(DelegateSelectItem delegateSelectItem)
        {
            m_unselectItemCallback = delegateSelectItem;
        }

        public UITreeTableItem GetItem(UITreeTableData data)
        {
            if (m_dataItemMap.ContainsKey(data))
            {
                return m_dataItemMap[data];
            }

            return null;
        }

        public void OnClickItem(UITreeTableItem item)
        {
            if (item.Data == null) return;

            // 父级
            if (item.Data.IsParent)
            {
                // 已经选中
                if (item.SelectState)
                {
                    // 点击已经选择的父级需要调用回调
                    if (repeatExecute && m_selectParentItem == item)
                    {
                        if (fatherExecute)
                        {
                            if (m_selectItemCallback != null)
                            {
                                m_selectItemCallback(item.Data);
                            }
                        }

                        return;
                    }

                    if (m_selectParentItem == item)
                    {
                        // 关闭上一个选中的
                        if (m_selectParentItem.Data != null)
                        {
                            UndrawItem(m_selectParentItem.Data);

                            if (fatherExecute)
                            {
                                if (m_unselectItemCallback != null)
                                {
                                    m_unselectItemCallback(m_selectParentItem.Data);
                                }
                            }
                        }

                        m_selectParentItem.SetSelectState(false);
                    }
                }
                else
                {
                    // 关闭上一个选中的
                    if (m_selectParentItem != null && m_selectParentItem.Data != null && m_selectParentItem.Data != item.Data && m_selectParentItem.SelectState)
                    {
                        m_selectParentItem.SetSelectState(false);
                        UndrawItem(m_selectParentItem.Data);

                        if (fatherExecute)
                        {
                            if (m_unselectItemCallback != null)
                            {
                                m_unselectItemCallback(m_selectParentItem.Data);
                            }
                        }
                    }

                    item.SetSelectState(true);
                    m_selectParentItem = item;
                    DrawItem(item.Data);

                    if (fatherExecute)
                    {
                        if (m_selectItemCallback != null)
                        {
                            m_selectItemCallback(item.Data);
                        }
                    }
                }
            }
            // 子类
            else
            {
                // 已经选中
                if (item.SelectState)
                {
                    if (repeatExecute && m_selectItem == item)
                    {
                        if (m_selectItemCallback != null)
                        {
                            m_selectItemCallback(item.Data);
                        }
                        return;
                    }
                }
                else
                {
                    if (m_selectItem != null)
                    {
                        m_selectItem.SetSelectState(false);
                        if (m_unselectItemCallback != null)
                        {
                            m_unselectItemCallback(m_selectItem.Data);
                        }
                    }

                    m_selectItem = item;
                    m_selectItem.SetSelectState(true);

                    if (m_selectItemCallback != null)
                    {
                        m_selectItemCallback(item.Data);
                    }
                }
            }
        }

        private void DrawItem(UITreeTableData data)
        {
            int _index = -1;
            if (m_dataItemMap.ContainsKey(data))
            {
                _index = m_itemShows.IndexOf(m_dataItemMap[data]);
            }
            if (_index == -1)
            {
                _index = m_itemShows.Count - 1;
            }

            List<UITreeTableItem> _listItem = new List<UITreeTableItem>();
            UITreeTableItem _item = null;
            for (int i = 0; i < data.ChildCount; i++)
            {
                _item = CreateItem();
                _item.transform.SetSiblingIndex(i + _index + 1);
                _item.OnInit(data.Childs[i]);
                _listItem.Add(_item);
                m_dataItemMap.Add(data.Childs[i], _item);
            }
            m_itemShows.InsertRange(_index + 1, _listItem);

            if (defaultFrist && _listItem.Count == 1)
            {
                OnClickItem(_listItem[0]);
            }

            m_uiTable.Reposition();
            if (data.IsParent)
            {
                if (_index >= 0 && _index < m_itemShows.Count)
                {
                    UITools.OnScrollViewMove(m_uiPanel, m_uiScrollView, m_itemShows[_index].gameObject, (int)selectPosType);
                }
            }
            else
            {
                _index = 0;
                if (_listItem.Count > _index)
                {
                    UITools.OnScrollViewMove(m_uiPanel, m_uiScrollView, _listItem[_index].gameObject, (int)selectPosType);
                }
            }
        }

        private void UndrawItem(UITreeTableData data)
        {
            int _index = 0;
            UITreeTableItem _item;

            while (_index < m_itemShows.Count)
            {
                _item = m_itemShows[_index];

                if (_item.Data != null && _item.Data.Parent == data)
                {
                    if (_item.Data.IsParent)
                    {
                        UndrawItem(_item.Data);
                    }

                    RecycleItem(_item);
                }
                else
                {
                    _index++;
                }
            }

            m_uiTable.Reposition();
            m_uiScrollView.RestrictWithinBounds(true);
        }

        private UITreeTableItem CreateItem()
        {
            UITreeTableItem _item = null;
            int _freeCount = m_itemFrees.Count;

            if (_freeCount > 0)
            {
                _item = m_itemFrees[_freeCount - 1];
                m_itemFrees.RemoveAt(_freeCount - 1);
            }

            if (_item == null)
            {
                GameObject _go = NGUITools.AddChild(m_uiTable.gameObject, itemPrefab.gameObject);
                _item = _go.GetComponent<UITreeTableItem>();

                if (_item == null)
                {
                    Debug.LogError(string.Format("\"{0}.UITreeTable.itemPrefab\"没有添加脚本<UITreeTableItem>!", m_uiScrollView.name));
                    return null;
                }
            }

            _item.transform.parent = m_uiTable.transform;
            _item.gameObject.SetActive(true);

            return _item;
        }

        private void RecycleItem(UITreeTableItem item)
        {
            item.SetSelectState(false);
            m_dataItemMap.Remove(item.Data);
            m_itemShows.Remove(item);
            m_itemFrees.Add(item);
            item.transform.parent = pool.transform;
            item.transform.localPosition = Vector3.zero;
        }
    }
}
