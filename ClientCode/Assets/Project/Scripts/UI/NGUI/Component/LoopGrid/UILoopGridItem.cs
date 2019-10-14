using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    [RequireComponent(typeof(UIDragScrollView))]
    public class UILoopGridItem : MonoBehaviour
    {
        public UILabel txtName;
        public GameObject select;

        private Transform m_selfTransform;

        private UILoopGridData m_data = null;                   // 数据
        private bool m_selectState;                             // 选中与没选中标示

        public Transform SelfTransform { get { return m_selfTransform; } }
        public UILoopGridData Data { get { return m_data; } }
        public bool SelectState { get { return m_selectState; } }

        private void Awake()
        {
            m_selfTransform = transform;
        }

        /// <summary>
        /// 初始化 - 可重写此方法来做初始化时的处理
        /// </summary>

        public virtual void OnInit(UILoopGridData data)
        {
            m_data = data;

            if (m_data != null && txtName != null)
            {
                txtName.text = string.Format("({0},{1},{2})", m_data.X, m_data.Y, m_data.Index);
            }

            SetStateSelect(false);
        }

        /// <summary>
        /// 更新 - 可重写此方法来做数据改变时的处理
        /// </summary>

        public virtual void OnUpdate()
        {
            if (m_data != null && txtName != null)
            {
                txtName.text = string.Format("({0},{1},{2})", m_data.X, m_data.Y, m_data.Index);
            }
        }

        /// <summary>
        /// 更新 - 可重写此方法来做数据改变时的处理
        /// </summary>

        public virtual void OnUpdate(object data)
        {

        }

        /// <summary>
        /// 更新 - 可重写此方法来做状态改变时的处理
        /// </summary>

        public virtual void OnUpdateSelectState()
        {

        }

        /// <summary>
        /// 设置 - 局部坐标
        /// </summary>

        public virtual void SetLocalPosition(Vector3 vector)
        {
            if (m_selfTransform == null)
            {
                m_selfTransform = transform;
            }

            m_selfTransform.localPosition = vector;
        }

        /// <summary>
        /// 设置 - 状态
        /// </summary>

        public virtual void SetStateSelect(bool state)
        {
            if (select != null)
            {
                select.SetActive(state);
            }

            OnUpdateSelectState();
        }
    }
}