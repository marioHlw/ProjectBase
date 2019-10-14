/**************************
 * 文件名:UIDropDownMenuComponent.cs
 * 文件描述:下拉菜单
 *          使用时需要设置一下btnFullScreen的适配，适配成覆盖全屏即可。
 * 创建日期:2019/07/26
 * 作者:ZB
 ***************************/



using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    public class UIDropDownMenuComponent : MonoBehaviour
    {
        public delegate void DelegateItemEvent(UIDropDownMenuItem item);
        private DelegateItemEvent m_selectItemCallback = null;                      // 选择子类回调执行
        private DelegateItemEvent m_unselectItemCallback = null;                    // 取消选择子类回调执行
        private DelegateItemEvent m_updateItemCallback = null;                      // 更新子类回调执行

        public EventDelegate selectItemCallback = null;                             // 选择子类回调执行
        public EventDelegate unselectItemCallback = null;                           // 取消选择子类回调执行
        public EventDelegate updateItemCallback = null;                             // 更新子类回调执行
        [HideInInspector] public bool defaultSelectFrist = false;                   // 默认选中第一个
        [HideInInspector] public bool selectCloseWindow = false;                    // 选中后关闭下拉窗口
        [HideInInspector] public bool repeatExecuteCallback = false;                // 重复执行回调

        public UIDropDownMenuItem prefabItem;       // 子物体预设
        public UIPanel panel;
        public UIScrollView scrollView;             // 子物体父级
        public UIGrid grid;                         // 网格排版
        public UISprite bg;                         // 背景图
        public UIButton btnClick;                   // 点击按钮
        public UIButton btnFullScreen;              // 全屏按钮

        public UISprite iconArrow;                  // 箭头图片
        public UILabel txtInfo;                     // 显示信息

        private bool m_initialized = false;         // 是否初始化，用来过滤不需要重复执行的步骤。
        private bool m_firstUpdate = true;          // 第一个更新数据
        private bool m_propDownState = false;       // 下拉状态
        private int m_panelShowCount = 0;           // UIPanel最多能看到数量

        private UIDropDownMenuItem m_selectItem = null;
        private List<UIDropDownMenuItem> m_items = new List<UIDropDownMenuItem>();
        private List<UIDropDownMenuData> m_datas = new List<UIDropDownMenuData>();

        public bool Initialized { get { return m_initialized; } }
        public List<UIDropDownMenuItem> Items { get { return m_items; } }

        private void Awake()
        {
            Init();

            OnUpdate();
            OnClickIndex(1);
        }

        private void OnDestroy()
        {
            m_selectItem = null;
            m_initialized = false;
            m_items.Clear();
            m_items = null;
            m_datas.Clear();
            m_datas = null;

            EventDelegate.Remove(btnClick.onClick, OnClickBtnClick);
            EventDelegate.Remove(btnFullScreen.onClick, OnClickBtnFullScreen);
        }

        /// <summary>
        /// 初始化
        /// </summary>

        public void Init()
        {
            if (!m_initialized)
            {
                m_items.Add(prefabItem);
                EventDelegate.Add(btnClick.onClick, OnClickBtnClick);
                EventDelegate.Add(btnFullScreen.onClick, OnClickBtnFullScreen);
            }

            m_panelShowCount = (int)(panel.GetViewSize().y / grid.cellHeight);
            m_propDownState = false;
            btnFullScreen.gameObject.SetActive(false);
            scrollView.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);

            m_initialized = true;
            m_firstUpdate = true;

            AddData(new UIDropDownMenuData(0, "1001"));
            AddData(new UIDropDownMenuData(1, "1002"));
            AddData(new UIDropDownMenuData(2, "1003"));
            AddData(new UIDropDownMenuData(3, "1004"));
            AddData(new UIDropDownMenuData(4, "1005"));
            AddData(new UIDropDownMenuData(5, "1006"));
            AddData(new UIDropDownMenuData(6, "1007"));
            AddData(new UIDropDownMenuData(7, "1008"));
        }

        /// <summary>
        /// 添加 - 数据
        /// </summary>

        public void AddData(UIDropDownMenuData data)
        {
            if (!m_datas.Contains(data))
            {
                m_datas.Add(data);
            }
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
        /// 更新 - 界面
        /// </summary>

        public void OnUpdate()
        {
            for (int i = 0, length = m_datas.Count; i < length; i++)
            {
                UIDropDownMenuItem _item = CreateItem(i);
                _item.SetData(m_datas[i]);

                if (i == 0 && defaultSelectFrist && m_firstUpdate)
                {
                    m_firstUpdate = false;
                    OnClickItem(_item);
                }

                if (m_updateItemCallback != null)
                {
                    m_updateItemCallback(_item);
                }

                if (updateItemCallback != null)
                {
                    updateItemCallback.Execute();
                }
            }
        }

        /// <summary>
        /// 选中 - 子对象
        /// </summary>

        public void OnClickItem(UIDropDownMenuItem item)
        {
            if (item == null) return;

            if (m_selectItem == item)
            {
                // 需要重复选择
                if (repeatExecuteCallback)
                {
                    if (m_selectItemCallback != null)
                    {
                        m_selectItemCallback(item);
                    }

                    if (selectItemCallback != null)
                    {
                        selectItemCallback.Execute();
                    }

                    m_selectItem.SetSelectState(true);
                }
            }
            else
            {
                if (m_selectItemCallback != null)
                {
                    m_selectItemCallback(item);
                }

                if (selectItemCallback != null)
                {
                    selectItemCallback.Execute();
                }

                if (m_unselectItemCallback != null)
                {
                    m_unselectItemCallback(m_selectItem);
                }

                if (unselectItemCallback != null)
                {
                    unselectItemCallback.Execute();
                }

                if (m_selectItem != null)
                {
                    m_selectItem.SetSelectState(false);
                }

                m_selectItem = item;
                m_selectItem.SetSelectState(true);
            }

            txtInfo.text = m_selectItem.Data.Name;

            if(selectCloseWindow)
            {
                m_propDownState = false;
                btnFullScreen.gameObject.SetActive(m_propDownState);
                scrollView.gameObject.SetActive(m_propDownState);
                bg.gameObject.SetActive(m_propDownState);
                iconArrow.flip = UIBasicSprite.Flip.Nothing;
            }
        }

        /// <summary>
        /// 选中 - 子对象索引
        /// </summary>

        public void OnClickIndex(int index, bool center = false)
        {
            if (index < 0) return;
            if (index >= m_items.Count) return;

            OnClickItem(m_items[index]);
        }

        private void OnClickBtnClick()
        {
            m_propDownState = !m_propDownState;
            btnFullScreen.gameObject.SetActive(m_propDownState);
            scrollView.gameObject.SetActive(m_propDownState);
            bg.gameObject.SetActive(m_propDownState);
            iconArrow.flip = m_propDownState ? UIBasicSprite.Flip.Vertically : UIBasicSprite.Flip.Nothing;

            if (m_propDownState)
            {
                grid.Reposition();
                scrollView.ResetPosition();
                // 移动到中心
                int _count = (m_panelShowCount - 1) / 2 + 1;
                if (m_selectItem.Data.Index >= _count && m_selectItem.Data.Index < m_items.Count - _count)
                {
                    scrollView.MoveRelative(new Vector3(0, -m_selectItem.transform.localPosition.y + grid.cellHeight - panel.GetViewSize().y / 2f - grid.cellHeight / 2f, 0));
                }
                else
                {
                    if (m_selectItem.Data.Index < _count)
                    {
                        return;
                    }

                    if (m_selectItem.Data.Index >= m_items.Count - _count)
                    {
                        scrollView.MoveRelative(new Vector3(0, -m_items[m_items.Count - 1].transform.localPosition.y + grid.cellHeight - panel.GetViewSize().y, 0));
                    }
                }
            }
        }

        private void OnClickBtnFullScreen()
        {
            m_propDownState = false;
            btnFullScreen.gameObject.SetActive(m_propDownState);
            scrollView.gameObject.SetActive(m_propDownState);
            bg.gameObject.SetActive(m_propDownState);
            iconArrow.flip = m_propDownState ? UIBasicSprite.Flip.Vertically : UIBasicSprite.Flip.Nothing;
        }

        private UIDropDownMenuItem CreateItem(int index)
        {
            if (index < m_items.Count)
            {
                return m_items[index];
            }
            else
            {
                UIDropDownMenuItem _item = null;

                if (prefabItem != null)
                {
                    GameObject _go = Instantiate(prefabItem.gameObject, Vector3.zero, Quaternion.identity, grid.transform);
                    _go.SetActive(true);
                    _item = _go.GetComponent<UIDropDownMenuItem>();
                    m_items.Add(_item);
                }

                return _item;
            }
        }
    }
}