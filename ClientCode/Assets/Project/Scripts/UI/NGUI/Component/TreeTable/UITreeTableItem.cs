/**************************
 * 文件名:NGUITreeTableItem.cs
 * 文件描述:NGUI树型表组件子类型
 * 创建日期:2019/06/29
 * 作者:ZB
 ***************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    public class UITreeTableItem : MonoBehaviour
    {
        public UILabel txtName;
        public UISprite iconFold;
        public UISprite iconSelect;

        private UITreeTableData m_data = null;                  // 数据
        private bool m_selectState;                             // 选中与没选中标示

        public UITreeTableData Data { get { return m_data; } }
        public bool SelectState { get { return m_selectState; } }

        /// <summary>
        /// 初始化 - 可重写此方法来做初始化时的处理
        /// </summary>

        public virtual void OnInit(UITreeTableData data)
        {
            m_data = data;

            string _str = m_data.Index.ToString();
            UITreeTableData _data = m_data;

            while (_data.Parent != null)
            {
                if (_data.Parent.Parent == null)
                {
                    _str = _data.Level + "-" + _str;
                }
                else
                {
                    _str = _data.Parent.Index + "-" + _str;
                }

                _data = _data.Parent;
            }

            if (txtName != null)
            {
                txtName.text = _str;
            }

            if (iconFold != null)
            {
                iconFold.enabled = data.IsParent;
            }

            if (!m_data.IsParent)
            {
                if (iconSelect != null)
                {
                    iconSelect.enabled = m_selectState;
                }
            }
            else
            {
                if (iconSelect != null)
                {
                    iconSelect.enabled = false;
                }
            }

            gameObject.name = _str;
        }

        /// <summary>
        /// 更新 - 可重写此方法来做状态改变时的处理
        /// </summary>

        public virtual void OnUpdateSelectState()
        {

        }

        public void SetSelectState(bool state)
        {
            m_selectState = state;

            if (iconFold != null)
            {
                iconFold.flip = state ? UIBasicSprite.Flip.Vertically : UIBasicSprite.Flip.Nothing;
            }

            if (!m_data.IsParent)
            {
                if (iconSelect != null)
                {
                    iconSelect.enabled = m_selectState;
                }
            }

            OnUpdateSelectState();
        }
    }
}