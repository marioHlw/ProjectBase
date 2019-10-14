/**************************
 * 文件名:BLK_UIFormBase.cs
 * 文件描述:NGUI系统单个窗口基类。
 * 创建日期:2019/09/16
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    public class BLK_UIFormBase : MonoBehaviour
    {
        private int m_serialId;                 // 序列编号
        private string m_name;                  // 窗口名称
        private string m_assetName;             // 资源名称
        private int m_depth;                    // 深度
        private bool m_initialized;             // 已经初始化标识

        public enUIFormType enFormType;                     // 窗口类型
        public bool openCaches = false;                     // 打开后缓存
        public UIAnimationEntryBase animationEntry;         // 入场动画
        public UIAnimationExitBase animationExit;           // 退场动画

        private Action<BLK_UIFormBase> m_playAnimationEnterCallback = null;
        private Action<BLK_UIFormBase> m_playAnimationExitCallback = null;

        public int SerialId { get { return m_serialId; } }
        public string Name { get { return m_name; } }
        public string AssetName { get { return m_assetName; } }
        public int Depth { get { return m_depth; } }
        public bool Initialized { get { return m_initialized; } }

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
            if (animationEntry != null)
            {
                animationEntry.OnInit();
            }
            if (animationExit != null)
            {
                animationExit.OnInit();
            }

            UpdateChildPanelDepth();

            m_initialized = true;
        }

        /// <summary>
        /// 界面关闭。
        /// </summary>

        public virtual void OnClose()
        {
            m_initialized = false;

            if (animationEntry != null)
            {
                animationEntry.OnStop();
            }
            if (animationExit != null)
            {
                animationExit.OnStop();
            }
        }

        /// <summary>
        /// 进入界面，播放完入场动画后调用。
        /// </summary>

        public virtual void OnEnter()
        {
            if (m_playAnimationEnterCallback != null)
            {
                m_playAnimationEnterCallback.Invoke(this);
                m_playAnimationEnterCallback = null;
            }
        }

        /// <summary>
        /// 退出界面，播放完退场动画后调用。
        /// </summary>

        public virtual void OnExit()
        {
            if (m_playAnimationExitCallback != null)
            {
                m_playAnimationExitCallback.Invoke(this);
                m_playAnimationExitCallback = null;
            }
        }

        /// <summary>
        /// 隐藏界面，设置成不可见时调用。
        /// </summary>

        public virtual void OnHide()
        {
            if (animationEntry != null)
            {
                animationEntry.OnStop();
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
            m_initialized = false;
        }

        /// <summary>
        /// 界面回收。
        /// </summary>

        public virtual void OnRecycle()
        {
            OnClose();
            Destroy(gameObject);
        }

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <param name="depth">界面组深度。</param>

        public virtual void OnDepthChanged(int depth)
        {

        }

        protected void UpdateChildPanelDepth()
        {
            UIPanel _rootPanel = GetComponent<UIPanel>();

            if (_rootPanel == null)
            {
                return;
            }

            UIPanel[] _childPanels = GetComponentsInChildren<UIPanel>(true);

            for (int i = 0; i < _childPanels.Length; i++)
            {
                _childPanels[i].depth = _rootPanel.depth + i;
            }
        }

        #region 入场/退场动画

        /// <summary>
        /// 播放入场动画
        /// </summary>

        public virtual void OnPlayAnimationEnter(Action<BLK_UIFormBase> action)
        {
            m_playAnimationEnterCallback = action;

            if (animationEntry != null)
            {
                animationEntry.SetFinishCallback(OnOpenAnimationPlayFinish);
                animationEntry.OnPlay();
            }
            else
            {
                OnOpenAnimationPlayFinish();
            }
        }

        /// <summary>
        /// 播放退场动画
        /// </summary>

        public virtual void OnPlayAnimationExit(Action<BLK_UIFormBase> action)
        {
            m_playAnimationExitCallback = action;

            if (animationExit != null)
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