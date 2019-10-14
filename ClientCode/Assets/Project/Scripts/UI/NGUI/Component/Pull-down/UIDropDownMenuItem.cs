/**************************
 * 文件名:UIDropDownMenuItem.cs
 * 文件描述:下拉菜单子物体
 * 创建日期:2019/07/26
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    public class UIDropDownMenuItem : MonoBehaviour
    {
        public UILabel txtInfo;
        public GameObject select;

        private UIDropDownMenuData m_data = null;              // 数据
        private bool m_selectState;                            // 选中与没选中标示

        public UIDropDownMenuData Data { get { return m_data; } }
        public bool SelectState { get { return m_selectState; } }

        public virtual void SetData(UIDropDownMenuData data)
        {
            m_data = data;

            if (m_data != null)
            {
                if (txtInfo != null)
                {
                    txtInfo.text = m_data.Name;
                }
            }
            else
            {
                if (txtInfo != null)
                {
                    txtInfo.text = "";
                }
            }

            SetSelectState(false);
        }

        public virtual void SetSelectState(bool state)
        {
            m_selectState = state;

            if (select != null)
            {
                select.SetActive(state);
            }
        }
    }
}