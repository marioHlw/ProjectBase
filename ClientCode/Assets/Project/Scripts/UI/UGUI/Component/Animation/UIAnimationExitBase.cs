/**************************
 * 文件名:UIAnimationExitBase.cs
 * 文件描述:NGUI系统窗口退场动画基类，如果想扩展系统窗口关闭的动画效果，请继承自这个类。
 * 创建日期:2019/09/16
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.UGUILibrary
{
    public class UIAnimationExitBase : MonoBehaviour
    {
        protected Action m_finishCallback = null;

        public virtual void OnInit()
        {

        }

        public virtual void OnPlay()
        {

        }

        public virtual void OnStop()
        {

        }

        /// <summary>
        /// 立即结束
        /// </summary>
        /// <param name="isExcute">是否执行回调</param>

        public virtual void OnEnd(bool isExcute = false)
        {
            OnStop();

            if (isExcute)
            {
                if (m_finishCallback != null)
                {
                    m_finishCallback.Invoke();
                }
            }
        }

        public void SetFinishCallback(Action action)
        {
            m_finishCallback = action;
        }
    }
}