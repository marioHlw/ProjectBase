/**************************
 * 文件名:UILoopGrid.cs
 * 文件描述:NGUI循环网格组件(注意：水平方向的暂时没做好，要使用的时候再做吧。)
 * 使用教程：1.调用SetAmount()  第一个参数是数据总量 第二个参数是需不需要把所有数据都创建
 *           2.调用OnLayoutGrid()
 * 创建日期:2019/07/02
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace zb.NGUILibrary
{
    public enum Arrangement
    {
        Horizontal = 1,
        Vertical,
        //CellSnap,
    }

    public enum GotoSelectType
    {
        Top = 1,
        Center,
        Bottom,
    }

    public class UILoopGrid : MonoBehaviour
    {
        public delegate void DelegateItemEvent(UILoopGridItem item);
        private DelegateItemEvent m_selectItemCallback = null;                      // 选择子类回调执行
        private DelegateItemEvent m_unselectItemCallback = null;                    // 取消选择子类回调执行
        private DelegateItemEvent m_updateItemCallback = null;                      // 更新子类回调执行

        [HideInInspector] public Arrangement arrangementType = Arrangement.Horizontal;          // 布局方式
        [HideInInspector] public GotoSelectType gotoSelectType = GotoSelectType.Top;            // 选中的物体所处位置
        [HideInInspector] public float cellWidth = 200;                                         // 间距宽
        [HideInInspector] public float cellHeight = 200;                                        // 间距高
        [HideInInspector] public int extendCount = 3;                                           // 扩展数量,也就是屏幕外多创建的行/列数

        [HideInInspector] public bool defaultSelectFrist = false;                               // 默认选中第一个
        [HideInInspector] public bool repeatExecuteCallback = false;                            // 重复执行回调
        [HideInInspector] public bool hideInactive = false;                                     // 过滤已经隐藏的物体
        [HideInInspector] public bool animateSmoothly = false;                                  // 动画移动位置

        public UILoopGridItem itemPrefab;
        public UIScrollView scrollView;

        private GameObject m_pool;                              // 对象池
        private UIPanel m_viewPanel;                            // UIScrollView上的UIPanel
        private Vector2 m_viewClipOffset = Vector2.zero;        // UIPanel.clipOffset上一次的值
        private int m_viewWidth;                                // 视图窗口宽度
        private int m_viewHight;                                // 视图窗口高度
        private int m_viewHorizontalCount;                      // 视图窗口显示行数
        private int m_viewVerticalCount;                        // 视图窗口显示列数
        private int m_horizontalCount;                          // 创建总行数
        private int m_verticalCount;                            // 创建总列数
        private int m_maxHorizontalCount;                       // 数据最大行数
        private int m_maxVerticalCount;                         // 数据最大列数

        private float m_panelUp = 0;                            // UIPanel剪切矩形四个角的空间坐标.顺序是左下，左上，右上，右下。   
        private float m_panelDown = 0;
        private float m_panelLeft = 0;
        private float m_panelRight = 0;

        private List<UILoopGridItem> m_items = new List<UILoopGridItem>();          // 缓存用的 
        private List<UILoopGridItem> m_itemFrees = new List<UILoopGridItem>();      // 还没有使用的Item
        private List<UILoopGridItem> m_itemShows = new List<UILoopGridItem>();      // 已经使用的Item

        private int m_itemShowCount = 0;                                            // 已经使用的Item数量
        private bool m_useCustomPosition = false;                                   // 使用自定义位置
        private bool m_startMove = false;
        private UILoopGridItem m_selectItem = null;                                 // 选中的子类
        private int m_amount = -1;                                                  // 数据总数
        private int m_realIndex = 0;
        private int m_selectX = -1;
        private int m_selectY = -1;

        public UILoopGridItem SelectItem { get { return m_selectItem; } }

        private void Awake()
        {
            if (itemPrefab != null)
            {
                m_itemFrees.Add(itemPrefab);
            }

            if (m_pool == null)
            {
                m_pool = new GameObject("Pool");
                m_pool.transform.parent = transform.parent;
            }
            m_pool.SetActive(false);

            m_viewPanel = NGUITools.FindInParents<UIPanel>(scrollView.gameObject);

            if (scrollView != null)
            {
                if (arrangementType == Arrangement.Horizontal)
                {
                    scrollView.movement = UIScrollView.Movement.Horizontal;
                }
                else if (arrangementType == Arrangement.Vertical)
                {
                    scrollView.movement = UIScrollView.Movement.Vertical;
                }

                scrollView.onDragFinished = OnDragFinished;
                scrollView.onDragStarted = OnDragStarted;
                scrollView.onMomentumMove = OnMomentumMove;
                scrollView.onStoppedMoving = OnStoppedMoving;
            }

            m_viewClipOffset = m_viewPanel.clipOffset;

            // UIPanel剪切矩形四个角的空间坐标.顺序是左下，左上，右上，右下。        
            m_panelDown = m_viewPanel.worldCorners[0].y;
            m_panelUp = m_viewPanel.worldCorners[1].y;
            m_panelLeft = m_viewPanel.worldCorners[0].x;
            m_panelRight = m_viewPanel.worldCorners[2].x;
        }

        private void OnValidate()
        {
            if (!Application.isPlaying && NGUITools.GetActive(this))
            {
                if (scrollView == null)
                {
                    Debug.LogError("请为" + name + ".UILoopGrid.scrollView赋值");
                    return;
                }

                m_viewPanel = NGUITools.FindInParents<UIPanel>(scrollView.gameObject);

                if (m_viewPanel == null)
                {
                    return;
                }

                if (scrollView != null)
                {
                    scrollView.enabled = true;
                    scrollView.contentPivot = UIWidget.Pivot.TopLeft;
                    scrollView.movement = arrangementType == Arrangement.Vertical ? UIScrollView.Movement.Vertical : UIScrollView.Movement.Horizontal;
                }

                m_viewPanel.transform.localPosition = Vector3.zero;
                m_viewPanel.clipOffset = Vector2.zero;
                m_viewPanel.baseClipRegion = new Vector4((m_viewPanel.baseClipRegion.z - cellWidth) / 2f, cellHeight / 2f - m_viewPanel.baseClipRegion.w / 2f, m_viewPanel.baseClipRegion.z, m_viewPanel.baseClipRegion.w);

                if (itemPrefab != null)
                {
                    itemPrefab.transform.localPosition = Vector3.zero;
                }
            }
        }

        protected virtual void Update()
        {
            if (m_startMove)
            {
                OnClipMove();
            }
        }

        protected virtual void OnDestroy()
        {
            m_selectItemCallback = null;
            m_unselectItemCallback = null;
            m_updateItemCallback = null;

            m_selectItem = null;

            m_items = null;
            m_itemFrees = null;
            m_itemShows = null;
        }

        public void Clear()
        {
            for (int i = m_itemShowCount - 1; i >= 0; i--)
            {
                Recycle(m_itemShows[i]);
            }
            m_itemShows.Clear();

            m_items.Clear();

            m_selectItem = null;
            m_selectX = -1;
            m_selectY = -1;
            m_realIndex = 0;
            m_amount = -1;
        }          

        /// <summary>
        /// 设置 - Item预设
        /// </summary>

        public void SetItemPrefab(UILoopGridItem item)
        {
            if (item != null)
            {
                if (itemPrefab == null)
                {
                    m_itemFrees.Add(item);
                }

                itemPrefab = item;
            }
        }

        /// <summary>
        /// 设置 - 数据总数
        /// </summary>

        public void SetAmount(int val, bool createAll = false)
        {
            m_amount = val;

            m_viewHight = (int)m_viewPanel.GetViewSize().y;
            m_viewWidth = (int)m_viewPanel.GetViewSize().x;
            m_viewHorizontalCount = (int)Mathf.Ceil(m_viewWidth / cellWidth);
            m_viewVerticalCount = (int)Mathf.Ceil(m_viewHight / cellHeight);
            m_maxVerticalCount = m_viewVerticalCount;
            m_maxHorizontalCount = m_viewHorizontalCount;
            m_verticalCount = m_viewVerticalCount;
            m_horizontalCount = m_viewHorizontalCount;

            if (arrangementType == Arrangement.Horizontal)
            {
                m_maxVerticalCount = (m_amount - 1) / m_viewHorizontalCount + 1;
            }
            else if (arrangementType == Arrangement.Vertical)
            {
                m_maxHorizontalCount = (m_amount - 1) / m_viewVerticalCount + 1;
            }

            if (createAll)
            {
                if (arrangementType == Arrangement.Horizontal)
                {
                    m_verticalCount = (m_amount - 1) / m_viewHorizontalCount + 1;
                }
                else if (arrangementType == Arrangement.Vertical)
                {
                    m_horizontalCount = (m_amount - 1) / m_viewVerticalCount + 1;
                }
            }
            else
            {
                if (arrangementType == Arrangement.Horizontal)
                {
                    m_verticalCount = m_viewVerticalCount + extendCount;
                }
                else if (arrangementType == Arrangement.Vertical)
                {
                    m_horizontalCount = m_viewHorizontalCount + extendCount;
                }
            }
        }

        /// <summary>
        /// 设置 - 使用自定义位置
        /// </summary>

        public void SetCustomPosition(bool state)
        {
            m_useCustomPosition = state;
        }

        /// <summary>
        ///  设置 - 选择子物体执行回调
        /// </summary>

        public void SetSelectItemCallback(DelegateItemEvent callback)
        {
            m_selectItemCallback = callback;
        }

        /// <summary>
        ///  设置 - 取消选择子物体执行回调
        /// </summary>

        public void SetUnSelectItemCallback(DelegateItemEvent callback)
        {
            m_unselectItemCallback = callback;
        }

        /// <summary>
        ///  设置 - 更新子物体执行回调
        /// </summary>

        public void SetUpdateItemCallback(DelegateItemEvent callback)
        {
            m_updateItemCallback = callback;
        }

        /// <summary>
        /// 获取 - 所有显示的Items
        /// </summary>

        public List<UILoopGridItem> GetShowItems()
        {
            return m_itemShows;
        }

        public void OnSelectItem(int x, int y)
        {
            OnClickItem(GetLoopGridItem(x, y));
        }

        public void OnClickItem(UILoopGridItem item)
        {
            if (item == null)
            {
                Debug.LogError("数据不存在!");
                return;
            }

            // 已经选中
            if (item.SelectState)
            {
                if (repeatExecuteCallback && m_selectItem == item)
                {
                    if (m_selectItemCallback != null)
                    {
                        m_selectItemCallback(item);
                    }
                    return;
                }
            }
            else
            {
                if (m_selectItem != null)
                {
                    m_selectItem.SetStateSelect(false);
                    if (m_unselectItemCallback != null)
                    {
                        m_unselectItemCallback(m_selectItem);
                    }
                }

                m_selectItem = item;
                m_selectItem.SetStateSelect(true);

                if (m_selectItemCallback != null)
                {
                    m_selectItemCallback(item);
                }
            }

            m_selectX = m_selectItem.Data.X;
            m_selectY = m_selectItem.Data.Y;
        }

        public void OnMoveStart()
        {
            if (m_amount == -1)
            {
                Debug.LogError("请先调用SetAmount()设置数据总量!");
                return;
            }

            for (int i = m_itemShowCount - 1; i >= 0; i--)
            {
                Recycle(m_itemShows[i]);
            }
            m_itemShows.Clear();

            m_realIndex = 0;

            Relayout(0, 0);
        }

        public void OnMoveEnd()
        {
            if (m_amount == -1)
            {
                Debug.LogError("请先调用SetAmount()设置数据总量!");
                return;
            }

            for (int i = m_itemShowCount - 1; i >= 0; i--)
            {
                Recycle(m_itemShows[i]);
            }
            m_itemShows.Clear();

            m_realIndex = 0;

            if (arrangementType == Arrangement.Vertical)
            {
                Relayout(0, (m_amount - 1) / m_viewVerticalCount);
            }
            else if (arrangementType == Arrangement.Horizontal)
            {
                Relayout((m_amount - 1) / m_viewHorizontalCount, 0);
            }
        }

        public void OnMove(int x, int y)
        {
            if (m_amount == -1)
            {
                Debug.LogError("请先调用SetAmount()设置数据总量!");
                return;
            }

            if (x >= m_maxVerticalCount || y >= m_maxHorizontalCount || x < 0 || y < 0)
            {
                return;
            }

            for (int i = m_itemShows.Count - 1; i >= 0; i--)
            {
                Recycle(m_itemShows[i]);
            }
            m_itemShows.Clear();

            m_realIndex = 0;

            Relayout(x, y);
        }

        /// <summary>
        /// 更新 - 只更新所有子物体数据，不做其他处理
        /// </summary>

        public void OnUpdateData()
        {
            for (int i = 0; i < m_itemShowCount; i++)
            {
                m_itemShows[i].OnUpdate();
                if (m_updateItemCallback != null)
                {
                    m_updateItemCallback(m_itemShows[i]);
                }
            }
        }

        protected virtual void OnClipMove()
        {
            // 总数不够屏幕显示最大数量
            if (m_verticalCount * m_horizontalCount >= m_amount)
            {
                return;
            }

            Vector2 _curOffset = m_viewPanel.clipOffset;
            if (arrangementType == Arrangement.Horizontal)
            {
                float _offsetX = _curOffset.x - m_viewClipOffset.x;
                // 向右拉，向左扩展
                if (_offsetX < 0)
                {
                    if (m_realIndex - m_horizontalCount * m_verticalCount < 0)
                    {
                        m_viewClipOffset = m_viewPanel.clipOffset;
                        return;
                    }

                    // 计算UIScrollView上方边界以及底部边界的本地坐标Y值
                    int min = 0;
                    int max = 0;
                    Vector3[] worldCorners = m_viewPanel.worldCorners;
                    min = (int)scrollView.transform.InverseTransformPoint(worldCorners[0]).x;
                    max = (int)scrollView.transform.InverseTransformPoint(worldCorners[2]).x;

                    int _index = m_itemShows.Count - 1;
                    if (m_itemShows[_index].transform.localPosition.y + cellWidth / 2f >= max)
                    {
                        int _sibling = 0;
                        int _y = 0;
                        // 移动到左边
                        for (int i = m_itemShows.Count - m_viewHorizontalCount; i <= _index; i++)
                        {
                            m_realIndex--;

                            m_itemShows[i].transform.SetSiblingIndex(_sibling);
                            if (!m_useCustomPosition)
                            {
                                m_itemShows[i].transform.localPosition = new Vector3(m_itemShows[0].transform.localPosition.x - cellWidth, m_itemShows[i].transform.localPosition.y, 0);
                            }    
                            m_itemShows[i].Data.SetIndex(m_itemShows[i].Data.Index - m_itemShowCount);
                            m_itemShows[i].Data.SetXandY(m_itemShows[0].Data.X - 1, _y);
                            m_itemShows[i].OnUpdate();
                            m_itemShows[i].gameObject.SetActive(m_realIndex >= 0);
                            m_itemShows[i].SetStateSelect(m_selectX == m_itemShows[0].Data.X - 1 && m_selectY == _y);

                            if (m_updateItemCallback != null && m_realIndex >= 0)
                            {
                                m_updateItemCallback(m_itemShows[i]);
                            }

                            _sibling++;
                            _y++;
                        }

                        // 更新child
                        m_items.Clear();
                        for (int i = m_itemShowCount - m_viewHorizontalCount; i <= _index; i++)
                        {
                            m_items.Add(m_itemShows[i]);
                        }
                        m_itemShows.RemoveRange(m_itemShowCount - m_viewHorizontalCount, m_viewHorizontalCount);
                        m_itemShows.InsertRange(0, m_items);
                    }
                }
                // 向左拉，向右扩展
                else if (_offsetX > 0)
                {
                    if (m_realIndex + 1 >= m_amount)
                    {
                        m_viewClipOffset = m_viewPanel.clipOffset;
                        return;
                    }

                    // 计算UIScrollView上方边界以及底部边界的本地坐标Y值
                    int min = 0;
                    int max = 0;
                    Vector3[] worldCorners = m_viewPanel.worldCorners;
                    min = (int)scrollView.transform.InverseTransformPoint(worldCorners[0]).x;
                    max = (int)scrollView.transform.InverseTransformPoint(worldCorners[2]).x;

                    int _index = 0;
                    if (m_itemShows[_index].transform.localPosition.x - cellWidth / 2f <= min)
                    {
                        int _y = 0;
                        // 移动到右边
                        for (int i = _index; i < m_viewHorizontalCount; i++)
                        {
                            m_realIndex++;
                            m_itemShows[i].transform.SetAsLastSibling();
                            if (!m_useCustomPosition)
                            {
                                m_itemShows[i].transform.localPosition = new Vector3(m_itemShows[m_itemShowCount - 1].transform.localPosition.x + cellWidth, m_itemShows[i].transform.localPosition.y, 0);
                            }
                            m_itemShows[i].Data.SetIndex(m_realIndex);
                            m_itemShows[i].Data.SetXandY(m_itemShows[m_itemShowCount - 1].Data.X + 1, _y);
                            m_itemShows[i].OnUpdate();
                            m_itemShows[i].gameObject.SetActive(m_realIndex < m_amount);
                            m_itemShows[i].SetStateSelect(m_selectX == m_itemShows[m_itemShowCount - 1].Data.X + 1 && m_selectY == _y);

                            if (m_updateItemCallback != null && m_realIndex < m_amount)
                            {
                                m_updateItemCallback(m_itemShows[i]);
                            }
                            _y++;
                        }

                        // 更新child
                        m_items.Clear();
                        for (int i = _index; i < m_viewHorizontalCount; i++)
                        {
                            m_items.Add(m_itemShows[i]);
                        }
                        m_itemShows.RemoveRange(0, m_viewHorizontalCount);
                        m_itemShows.InsertRange(m_itemShowCount - m_viewHorizontalCount, m_items);
                    }
                }
            }
            else if (arrangementType == Arrangement.Vertical)
            {
                float _offsetY = _curOffset.y - m_viewClipOffset.y;

                // 向上拉，向下扩展
                if (_offsetY < 0)
                {
                    if (m_realIndex + 1 >= m_amount)
                    {
                        m_viewClipOffset = m_viewPanel.clipOffset;
                        return;
                    }

                    // 计算UIScrollView上方边界以及底部边界的本地坐标Y值
                    int min = 0;
                    int max = 0;
                    Vector3[] worldCorners = m_viewPanel.worldCorners;
                    min = (int)scrollView.transform.InverseTransformPoint(worldCorners[0]).y;
                    max = (int)scrollView.transform.InverseTransformPoint(worldCorners[2]).y;

                    int _index = 0;
                    if (m_itemShows[_index].transform.localPosition.y - cellHeight / 2f >= max)
                    {
                        int _x = 0;
                        // 顶部移动到底部
                        for (int i = _index; i < m_viewVerticalCount; i++)
                        {
                            m_realIndex++;

                            m_itemShows[i].transform.SetAsLastSibling();
                            if (!m_useCustomPosition)
                            {
                                m_itemShows[i].transform.localPosition = new Vector3(m_itemShows[i].transform.localPosition.x, m_itemShows[m_itemShowCount - 1].transform.localPosition.y - cellHeight, 0);
                            }
                            m_itemShows[i].Data.SetIndex(m_realIndex);
                            m_itemShows[i].Data.SetXandY(_x, m_itemShows[m_itemShowCount - 1].Data.Y + 1);
                            m_itemShows[i].OnUpdate();
                            m_itemShows[i].gameObject.SetActive(m_realIndex < m_amount);
                            m_itemShows[i].SetStateSelect(_x == m_selectX && m_itemShows[m_itemShowCount - 1].Data.Y + 1 == m_selectY);

                            if (m_updateItemCallback != null && m_realIndex < m_amount)
                            {
                                m_updateItemCallback(m_itemShows[i]);
                            }

                            _x++;
                        }

                        // 更新child
                        m_items.Clear();
                        for (int i = _index; i < m_viewVerticalCount; i++)
                        {
                            m_items.Add(m_itemShows[i]);
                        }
                        m_itemShows.RemoveRange(0, m_viewVerticalCount);
                        m_itemShows.InsertRange(m_itemShowCount - m_viewVerticalCount, m_items);
                    }
                }
                else if (_offsetY > 0)
                {
                    // 向下拉，向上扩展
                    if (m_realIndex - m_horizontalCount * m_verticalCount < 0)
                    {
                        m_viewClipOffset = m_viewPanel.clipOffset;
                        return;
                    }

                    // 计算UIScrollView上方边界以及底部边界的本地坐标Y值
                    int min = 0;
                    int max = 0;
                    Vector3[] worldCorners = m_viewPanel.worldCorners;
                    min = (int)scrollView.transform.InverseTransformPoint(worldCorners[0]).y;
                    max = (int)scrollView.transform.InverseTransformPoint(worldCorners[2]).y;

                    int _index = m_itemShowCount - 1;
                    if (m_itemShows[_index].transform.localPosition.y + cellHeight / 2f <= min)
                    {
                        int _sibling = 0;
                        int _x = 0;
                        // 底部移动到顶部
                        for (int i = m_itemShowCount - m_viewVerticalCount; i <= _index; i++)
                        {
                            m_realIndex--;

                            m_itemShows[i].transform.SetSiblingIndex(_sibling);
                            if (!m_useCustomPosition)
                            {
                                m_itemShows[i].transform.localPosition = new Vector3(m_itemShows[i].transform.localPosition.x, m_itemShows[0].transform.localPosition.y + cellHeight, 0);
                            }
                            m_itemShows[i].Data.SetIndex(m_itemShows[i].Data.Index - m_itemShowCount);
                            m_itemShows[i].Data.SetXandY(_x, m_itemShows[0].Data.Y - 1);
                            m_itemShows[i].OnUpdate();
                            m_itemShows[i].gameObject.SetActive(m_realIndex >= 0);
                            m_itemShows[i].SetStateSelect(_x == m_selectX && m_itemShows[0].Data.Y - 1 == m_selectY);

                            if (m_updateItemCallback != null && m_realIndex >= 0)
                            {
                                m_updateItemCallback(m_itemShows[i]);
                            }

                            _sibling++;
                            _x++;
                        }

                        // 更新child
                        m_items.Clear();
                        for (int i = m_itemShowCount - m_viewVerticalCount; i <= _index; i++)
                        {
                            m_items.Add(m_itemShows[i]);
                        }
                        m_itemShows.RemoveRange(m_itemShowCount - m_viewVerticalCount, m_viewVerticalCount);
                        m_itemShows.InsertRange(0, m_items);
                    }
                }
            }

            m_viewClipOffset = m_viewPanel.clipOffset;
        }

        protected virtual void OnDragStarted()
        {
            m_startMove = true;
        }

        protected virtual void OnDragFinished()
        {

        }

        protected virtual void OnMomentumMove()
        {

        }

        protected virtual void OnStoppedMoving()
        {
            m_startMove = false;
        }

        private void Relayout(int x = 0, int y = 0)
        {
            int _cx = x;
            int _cy = y;

            if (arrangementType == Arrangement.Horizontal)
            {
                if (gotoSelectType == GotoSelectType.Top)
                {
                    y = 0;
                }
                else if (gotoSelectType == GotoSelectType.Center)
                {
                    y = 0;
                    x = x - m_viewVerticalCount / 2;
                    _cx = x;
                }
                else if (gotoSelectType == GotoSelectType.Bottom)
                {
                    y = 0;
                    x = x - m_viewVerticalCount + 1;
                    _cx = x;
                }

                if (x > m_maxVerticalCount - (m_viewVerticalCount + extendCount))
                {
                    x = m_maxVerticalCount - (m_viewVerticalCount + extendCount);
                }
            }
            else if (arrangementType == Arrangement.Vertical)
            {
                if (gotoSelectType == GotoSelectType.Top)
                {
                    x = 0;
                }
                else if (gotoSelectType == GotoSelectType.Center)
                {
                    x = 0;
                    y = y - m_viewHorizontalCount / 2;
                    _cy = y;
                }
                else if (gotoSelectType == GotoSelectType.Bottom)
                {
                    x = 0;
                    y = y - m_viewHorizontalCount + 1;
                    _cy = y;
                }

                if (y > m_maxHorizontalCount - (m_viewHorizontalCount + extendCount))
                {
                    y = m_maxHorizontalCount - (m_viewHorizontalCount + extendCount);
                }
            }

            // 防止越界
            if (x < 0) { x = 0; }
            if (y < 0) { y = 0; }
            if (x > m_maxVerticalCount) { x = m_maxVerticalCount; }
            if (y > m_maxHorizontalCount) { y = m_maxHorizontalCount; }
            if (_cx < 0) { _cx = 0; }
            if (_cy < 0) { _cy = 0; }
            if (_cx > m_maxVerticalCount) { _cx = m_maxVerticalCount; }
            if (_cy > m_maxHorizontalCount) { _cy = m_maxHorizontalCount; }

            // 创建默认的item
            if (arrangementType == Arrangement.Vertical)
            {
                for (int j = y; j < m_horizontalCount + y; j++)
                {
                    for (int i = 0; i < m_viewVerticalCount; i++)
                    {
                        m_realIndex = i + j * m_viewVerticalCount;

                        UILoopGridData _data = new UILoopGridData(m_realIndex, i, j);
                        UILoopGridItem _item = CreateItem();
                        _item.OnInit(_data);
                        _item.SetStateSelect(false);
                        _item.OnUpdate();
                        _item.gameObject.SetActive(m_realIndex < m_amount);

                        m_itemShows.Add(_item);

                        if (m_updateItemCallback != null && m_realIndex < m_amount && m_realIndex >= 0)
                        {
                            m_updateItemCallback(_item);
                        }
                    }
                }
            }
            else if (arrangementType == Arrangement.Horizontal)
            {
                for (int i = x; i < m_verticalCount + x; i++)
                {
                    for (int j = 0; j < m_viewHorizontalCount; j++)
                    {
                        m_realIndex = i + j * m_viewVerticalCount;

                        UILoopGridData _data = new UILoopGridData(m_realIndex, i, j);
                        UILoopGridItem _item = CreateItem();
                        _item.OnInit(_data);
                        _item.SetStateSelect(false);
                        _item.OnUpdate();
                        _item.gameObject.SetActive(m_realIndex < m_amount);

                        m_itemShows.Add(_item);

                        if (m_updateItemCallback != null && m_realIndex < m_amount && m_realIndex >= 0)
                        {
                            m_updateItemCallback(_item);
                        }
                    }
                }
            }

            // 隐藏多余的item
            for (int i = m_amount, count = m_itemShows.Count; i < count; i++)
            {
                m_itemShows[i].gameObject.SetActive(false);
            }
            m_itemShowCount = m_itemShows.Count;

            ResetPosition(m_itemShows);
            scrollView.ResetPosition();

            // 总数不够屏幕显示最大数量
            if (m_viewHorizontalCount * m_viewVerticalCount >= m_amount)
            {

            }
            else
            {
                if (arrangementType == Arrangement.Vertical)
                {
                    UILoopGridItem _item = GetLoopGridItem(_cx, _cy);
                    scrollView.MoveRelative(Vector3.down * _item.transform.localPosition.y);
                    scrollView.RestrictWithinBounds(true);

                }
                else if (arrangementType == Arrangement.Horizontal)
                {
                    
                }
            }

            //if (defaultSelectFrist)
            //{
            //    OnClickItem(m_itemShows[0]);
            //    defaultSelectFrist = false;
            //}
            //else
            //{
            //    OnClickItem(GetLoopGridItem(m_selectX, m_selectY));
            //}
        }

        private UILoopGridItem GetLoopGridItem(int x, int y)
        {
            for (int i = 0, count = m_itemShows.Count; i < count; i++)
            {
                if (m_itemShows[i].Data.X == x && m_itemShows[i].Data.Y == y)
                {
                    return m_itemShows[i];
                }
            }

            return null;
        }

        private void ResetPosition(List<UILoopGridItem> list)
        {
            int x = 0;
            int y = 0;
            int maxX = 0;
            int maxY = 0;
            Transform myTrans = transform;

            for (int i = 0, imax = list.Count; i < imax; ++i)
            {
                Transform t = list[i].transform;
                Vector3 pos = t.localPosition;
                float depth = pos.z;

                if (arrangementType == Arrangement.Horizontal)
                {
                    pos = new Vector3(cellWidth * y, -cellHeight * x, depth);
                }
                else if (arrangementType == Arrangement.Vertical)
                {
                    pos = new Vector3(cellWidth * x, -cellHeight * y, depth);
                }

                if (animateSmoothly && Application.isPlaying && Vector3.SqrMagnitude(t.localPosition - pos) >= 0.0001f)
                {
                    SpringPosition sp = SpringPosition.Begin(t.gameObject, pos, 15f);
                    sp.updateScrollView = true;
                    sp.ignoreTimeScale = true;
                }
                else
                {
                    t.localPosition = pos;
                }

                maxX = Mathf.Max(maxX, x);
                maxY = Mathf.Max(maxY, y);

                if (arrangementType == Arrangement.Horizontal)
                {
                    if (++x >= m_viewHorizontalCount && m_viewHorizontalCount > 0)
                    {
                        x = 0;
                        ++y;
                    }
                }
                else if (arrangementType == Arrangement.Vertical)
                {
                    if (++x >= m_viewVerticalCount && m_viewVerticalCount > 0)
                    {
                        x = 0;
                        ++y;
                    }
                }

            }
        }

        private UILoopGridItem CreateItem()
        {
            UILoopGridItem _item = null;
            int _freeCount = m_itemFrees.Count;

            if (_freeCount > 0)
            {
                _item = m_itemFrees[_freeCount - 1];
                m_itemFrees.RemoveAt(_freeCount - 1);
            }

            if (_item == null)
            {
                GameObject _go = NGUITools.AddChild(scrollView.gameObject, itemPrefab.gameObject);
                _item = _go.GetComponent<UILoopGridItem>();

                if (_item == null)
                {
                    return null;
                }
            }

            _item.transform.parent = scrollView.transform;
            _item.gameObject.SetActive(true);

            return _item;
        }

        private void Recycle(UILoopGridItem item)
        {
            item.SetStateSelect(false);
            m_itemShows.Remove(item);
            m_itemFrees.Add(item);
            item.transform.parent = m_pool.transform;
            item.transform.localPosition = Vector3.zero;
        }

        #region 调试模式

        [HideInInspector] public bool debugMode = false;           // 调试模式
        [HideInInspector] public int debugX;
        [HideInInspector] public int debugY;

        public void OnClick()
        {
            OnMove(debugX, debugY);
        }

        #endregion
    }
}