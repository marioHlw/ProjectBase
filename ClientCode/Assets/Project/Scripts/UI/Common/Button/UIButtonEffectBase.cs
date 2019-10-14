/**************************
 * 文件名:UIButtonEffectBase.cs
 * 文件描述:按钮点击效果基类
 * 创建日期:2019/08/21
 * 作者:ZB
 ***************************/



using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI.Common
{
    public class UIButtonEffectBase : MonoBehaviour
    {
        public float effectTimeIn = 0.1f;
        public float effectTimeOut = 0.16f;
        public float scaleValue = 0.9f;
        public float scaleOriginal = 1f;
        public bool isLoop = true;                      // 循环特效
        public bool isDisabled = false;                 // 禁用
        public string soundName = "";                   // 点击声音

        private Transform m_target;
        private Tweener m_tweener;

        private void Awake()
        {
            Init();
        }

        private void OnPress(bool isPressed)
        {
            // 禁用
            if (isDisabled) { return; }

            // 按下
            if (isPressed)
            {
                StartEffect();
            }
            else
            {
                EndEffect();
            }
        }

        private void OnDestroy()
        {
            if (m_tweener != null)
            {
                m_tweener.Kill();
            }
        }

        protected virtual void Init()
        {
            enabled = false;

            if (m_target == null)
            {
                m_target = transform;
            }
        }

        protected virtual void StartEffect()
        {
            if (m_target == null) { return; }

            if (!isLoop)
            {
                Utility.ZTweener.ToScale(m_target, Vector3.one * scaleValue, effectTimeIn, Ease.OutQuad, 0);
            }
            else if (m_tweener == null)
            {
                m_tweener = Utility.ZTweener.ToScale(m_target, Vector3.one * scaleValue, effectTimeIn, Ease.OutQuad, 0);
                m_tweener.SetAutoKill(false);
            }
            else
            {
                m_tweener.ChangeEndValue(Vector3.one * scaleValue, effectTimeIn);
                m_tweener.SetEase(Ease.OutQuad);
                m_tweener.Restart();
            }

            // 播放声音
        }

        protected virtual void EndEffect()
        {
            if (!isLoop)
            {
                Utility.ZTweener.ToScale(m_target, Vector3.one * scaleOriginal, effectTimeOut, Ease.InQuad, 0);
            }
            else if (m_tweener != null && effectTimeOut > 0)
            {
                m_tweener.ChangeValues(m_target.transform.localScale, Vector3.one * scaleOriginal, effectTimeOut);
                m_tweener.SetEase(Ease.InQuad);
                m_tweener.Restart();
            }
            else
            {
                m_target.localScale = Vector3.one * scaleOriginal;
            }
        }
    }
}