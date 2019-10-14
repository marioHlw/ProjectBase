/**************************
 * 文件名:Utility.ZTweener.cs
 * 文件描述:Tweener实用相关类
 * 1.需要加入DOTween.dll至Plugins目录下
 * 创建日期:2019/08/21
 * 作者:ZB
 ***************************/



using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static partial class Utility
{
    public static class ZTweener
    {
        public static Tweener ToScale(Transform target, Vector3 endValue, float duration, Ease easeType = Ease.Linear, float delay = 0, bool isFrom = false, TweenCallback callback = null, bool ingoreTimeScale = false)
        {
            Tweener _tweener = target.DOScale(endValue, duration);
            _tweener.SetEase(easeType);

            if (delay > 0)
            {
                _tweener.SetDelay(delay);
            }
            if (callback != null)
            {
                _tweener.OnComplete(callback);
            }
            if (ingoreTimeScale)
            {
                _tweener.SetUpdate(ingoreTimeScale);
            }
            if (isFrom)
            {
                _tweener.From();
            }

            return _tweener;
        }
    }
}