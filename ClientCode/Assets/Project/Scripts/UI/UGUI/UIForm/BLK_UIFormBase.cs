/**************************
 * 文件名:BLK_UIFormBase.cs
 * 文件描述:UGUI系统单个窗口基类。
 * 1.此脚本附加到窗口父级下
 * 创建日期:2019/09/16
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.UGUILibrary
{
    public class BLK_UIFormBase : MonoBehaviour
    {
        private int m_serialId;                 // 序列编号
        private string m_name;                  // 窗口名称
        private string m_assetName;             // 资源名称
        private int m_depth;                    // 深度
        private bool m_playAnimation;           // 播放动画

        public enUIFormType enFormType;                     // 窗口类型
        public bool openCaches = false;                     // 打开后缓存
        public UIAnimationEntryBase animationEnter;         // 入场动画
        public UIAnimationExitBase animationExit;           // 退场动画

        [HideInInspector] public Action<BLK_UIFormBase> enterCallback = null;
        [HideInInspector] public Action<BLK_UIFormBase> exitCallback = null;

        public int SerialId { get { return m_serialId; } set { m_serialId = value; } }
        public string Name { get { return m_name; } set { m_name = value; } }
        public string AssetName { get { return m_assetName; } set { m_assetName = value; } }
        public int Depth { get { return m_depth; } set { m_depth = value; } }
        public bool PlayAnimation { get { return m_playAnimation; } set { m_playAnimation = value; } }

        private void OnEnable()
        {
            OnActivate();
        }

        private void OnDisable()
        {
            OnHide();
        }

        /// <summary>
        /// 界面初始化，只会调用一次。
        /// </summary>

        public virtual void OnInit()
        {
            
        }

        /// <summary>
        /// 界面打开，每次打开界面都会调用。
        /// </summary>

        public virtual void OnOpen()
        {
            if (animationEnter != null)
            {
                animationEnter.OnInit();
            }
            if (animationExit != null)
            {
                animationExit.OnInit();
            }
            
            OnPlayAnimationEnter();
        }

        /// <summary>
        /// 界面关闭，每次关闭界面都会调用。
        /// </summary>

        public virtual void OnClose()
        {
            if (animationEnter != null)
            {
                animationEnter.OnStop();
            }
            if (animationExit != null)
            {
                animationExit.OnStop();
            }

            OnPlayAnimationExit();
        }

        /// <summary>
        /// 进入界面，播放完入场动画后调用。
        /// </summary>

        public virtual void OnEnter()
        {
            if (enterCallback != null)
            {
                enterCallback.Invoke(this);
                enterCallback = null;
            }
        }

        /// <summary>
        /// 退出界面，播放完退场动画后调用。
        /// </summary>

        public virtual void OnExit()
        {
            if (exitCallback != null)
            {
                exitCallback.Invoke(this);
                exitCallback = null;
            }

            if (!openCaches)
            {
                OnRecycle();
            }
        }

        /// <summary>
        /// 隐藏界面，设置成不可见时调用。
        /// </summary>

        public virtual void OnHide()
        {
            if (animationEnter != null)
            {
                animationEnter.OnStop();
            }
            if (animationExit != null)
            {
                animationExit.OnStop();
            }
        }

        /// <summary>
        /// 界面激活，由不可见设置成可见时调用。
        /// </summary>

        public virtual void OnActivate()
        {

        }

        /// <summary>
        /// 界面重置。
        /// </summary>

        public virtual void OnReset()
        {
            if (animationEnter != null)
            {
                animationEnter.OnStop();
            }
            if (animationExit != null)
            {
                animationExit.OnStop();
            }
        }

        /// <summary>
        /// 界面回收。
        /// </summary>

        public virtual void OnRecycle()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <param name="depth">界面组深度。</param>

        public virtual void OnDepthChanged(int depth)
        {

        }


        #region 入场/退场动画

        /// <summary>
        /// 播放入场动画
        /// </summary>

        private void OnPlayAnimationEnter()
        {
            if (animationEnter != null && m_playAnimation)
            {
                animationEnter.SetFinishCallback(OnOpenAnimationPlayFinish);
                animationEnter.OnPlay();
            }
            else
            {
                OnOpenAnimationPlayFinish();
            }
        }

        /// <summary>
        /// 播放退场动画
        /// </summary>

        private void OnPlayAnimationExit()
        {
            if (animationExit != null && m_playAnimation)
            {
                animationExit.SetFinishCallback(OnCloseAnimationPlayFinish);
                animationExit.OnPlay();
            }
            else
            {
                OnCloseAnimationPlayFinish();
            }
        }

        /// <summary>
        /// 界面打开播放动画完成。
        /// </summary>

        private void OnOpenAnimationPlayFinish()
        {
            OnEnter();
        }

        /// <summary>
        /// 界面关闭播放动画完成。
        /// </summary>

        private void OnCloseAnimationPlayFinish()
        {
            OnExit();
        }

        #endregion
    }
}