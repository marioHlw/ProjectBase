/**************************
 * 文件名:TweenerManager.cs
 * 文件描述:Tweener扩展管理
 * 创建日期:2019/09/23
 * 作者:ZB
 ***************************/



using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenerManager : Singleton<TweenerManager>
{
    /// <summary>
    /// 透明变化
    /// </summary>
    /// <param name="target"></param>
    /// <param name="endAlpha"></param>
    /// <param name="duration"></param>
    /// <param name="callback"></param>
    /// <param name="isFrom"></param>
    /// <param name="delay"></param>
    /// <param name="easeType"></param>
    /// <param name="ingoreTimeScale"></param>
    /// <returns></returns>

    public Tweener ToAlpha(Transform target, float endAlpha, float duration, TweenCallback callback = null, bool isFrom = false, float delay = 0, Ease easeType = Ease.Linear, bool ingoreTimeScale = false)
    {
        UIRect rect = target.GetComponent<UIRect>();

        Tweener _tweener = DOTween.To(() => rect.alpha, x => rect.alpha = x, endAlpha, duration);
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