/**************************
 * 文件名:UIAnimationEntry01.cs
 * 文件描述:NGUI系统窗口入场动画类型1。
 *          从界面的上下左右四个方向移动到指定位置
 * 创建日期:2019/09/16
 * 作者:ZB
 ***************************/



using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace zb.UGUILibrary
{
    public class UIAnimationEntry01 : UIAnimationEntryBase
    {
        public GameObject topWindow;
        public GameObject bottonWindow;
        public GameObject leftWindow;
        public GameObject rightWindow;

        public Vector3[] targetPosArray = new Vector3[4];
        public float[] timeArray = new float[4];

        private TweenPosition m_tweenPositionTop;
        private TweenPosition m_tweenPositionBotton;
        private TweenPosition m_tweenPositionLeft;
        private TweenPosition m_tweenPositionRight;

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnPlay()
        {
            base.OnPlay();

            if (topWindow != null)
            {
                m_tweenPositionTop = TweenPosition.Begin(topWindow, timeArray[0], targetPosArray[0]);
            }

            if (bottonWindow != null)
            {
                m_tweenPositionBotton = TweenPosition.Begin(bottonWindow, timeArray[1], targetPosArray[1]);
            }

            if (leftWindow != null)
            {
                m_tweenPositionLeft = TweenPosition.Begin(leftWindow, timeArray[2], targetPosArray[2]);
            }

            if (rightWindow != null)
            {
                m_tweenPositionRight = TweenPosition.Begin(rightWindow, timeArray[3], targetPosArray[3]);
            }
        }

        public override void OnStop()
        {
            base.OnStop();

            if (m_tweenPositionTop != null) m_tweenPositionTop.DOKill();
            if (m_tweenPositionBotton != null) m_tweenPositionBotton.DOKill();
            if (m_tweenPositionLeft != null) m_tweenPositionLeft.DOKill();
            if (m_tweenPositionRight != null) m_tweenPositionRight.DOKill();
        }

        public override void OnEnd(bool isExcute = false)
        {
            base.OnEnd(isExcute);

            if (m_tweenPositionTop != null) m_tweenPositionTop.DOKill(isExcute);
            if (m_tweenPositionBotton != null) m_tweenPositionBotton.DOKill(isExcute);
            if (m_tweenPositionLeft != null) m_tweenPositionLeft.DOKill(isExcute);
            if (m_tweenPositionRight != null) m_tweenPositionRight.DOKill(isExcute);
        }
    }
}