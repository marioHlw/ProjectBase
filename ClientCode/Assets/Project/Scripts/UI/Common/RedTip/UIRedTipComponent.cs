/**************************
 * 文件名:UIRedTipComponent.cs
 * 文件描述:红点提示组件，将此脚本拖拽至红点物体上。
 * 创建日期:2019/07/25
 * 作者:ZB
 ***************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI.Common
{
    public class UIRedTipComponent : MonoBehaviour
    {
        [HideInInspector] public UIRedTipType redTipType = UIRedTipType.None;                       // 红点类型
        [HideInInspector] public bool autoInitialize = true;                                        // 自动初始化，自动加入红点提示管理器。
        [HideInInspector] public int soleID = 0;                                                    // 唯一ID，用来做多个同类型时的一对一处理。

        public UIRedTipType RedTipType { get { return redTipType; } }

        private void Start()
        {
            if (autoInitialize)
            {
                UIRedTipManager.Instance.AddRedTip(this);
            }
        }

        private void OnDestroy()
        {
            Reset();
        }

        private void Reset()
        {
            if (redTipType == UIRedTipType.None) return;

            UIRedTipManager.Instance.RemoveRedTip(this);
            SetActive(false);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void OnUpdate()
        {
            SetActive(UIRedTipManager.Instance.IsActive(redTipType, soleID));
        }
    }
}