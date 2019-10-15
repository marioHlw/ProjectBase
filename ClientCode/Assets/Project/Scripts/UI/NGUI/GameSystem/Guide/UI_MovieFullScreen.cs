/**************************
 * 文件名:UI_MovieFullScreen.cs
 * 文件描述:全屏视频
 * 创建日期:2019/09/23
 * 作者:ZB
 ***************************/




using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MovieFullScreen : MonoSingleton<UI_MovieFullScreen>
{
    //public UIPanel u_panel;
    //public GameObject u_btnPass;
    //public GameObject u_btnBg;

    //public UI_MoviePlay u_moviePlay;

    //private float m_tweenDestroy = 0;
    //private Action m_playFinishCallBack;

    ///// <summary>
    ///// 初始化 
    ///// </summary>
    ///// <param name="delayTime">延迟显示全屏按钮或者跳过按钮的时间</param>
    ///// <param name="btnPass">控制全屏/跳过按钮，true：显示跳过按钮，false：显示全屏按钮</param>
    ///// <param name="tweenDestroy">渐变退出时间</param>
    ///// <param name="playFinishCallback">播放完成回调</param>

    //public void OnInit(int delayTime, bool btnPass, float tweenDestroy, Action playFinishCallback = null)
    //{
    //    m_playFinishCallBack = playFinishCallback;

    //    if (delayTime == 0)
    //    {
    //        u_btnPass.SetActive(btnPass);
    //        u_btnBg.SetActive(!btnPass);
    //    }
    //    else
    //    {
    //        u_btnPass.SetActive(false);
    //        u_btnBg.SetActive(false);
    //        Ctrl.timerManager.AddTimer(delayTime, 1, new Action(() =>
    //        {
    //            OnInit(0, btnPass, tweenDestroy);
    //        }), null);
    //    }
    //}

    //protected override void OnDestroy()
    //{
    //    base.OnDestroy();

    //    m_playFinishCallBack = null;
    //    u_moviePlay.OnStop();
    //    u_moviePlay.u_texMovie.mainTexture = null;
    //}

    //public void OnPlay(string path)
    //{
    //    if (u_moviePlay.LMediaPlayerCtrl.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.END)
    //    {
    //        u_moviePlay.SetVideoReady(VideoReady);
    //        u_moviePlay.SetVideoFirstFrameReady(VideoFirstFrameReady);
    //    }
    //    else
    //    {
    //        u_moviePlay.SetVideoReady(VideoFirstFrameReady);
    //        u_moviePlay.SetVideoFirstFrameReady(null);
    //    }

    //    u_moviePlay.SetEnd(End);
    //    u_moviePlay.SetVideoReady(VideoReady);
    //    u_moviePlay.SetVideoFirstFrameReady(VideoFirstFrameReady);
    //    u_moviePlay.SetvideoError(VideoError);
    //    u_moviePlay.OnPlay(path);
    //}

    //public void OnClickBg()
    //{

    //}

    //public void OnClickBtnPass()
    //{
    //    u_moviePlay.OnStop();
    //}

    //private void VideoReady()
    //{
    //    Debug.Log("视频准备就绪");
    //    u_moviePlay.u_texMovie.alpha = 0;
    //}

    //private void VideoFirstFrameReady()
    //{
    //    Debug.Log("视频第一帧准备就绪");
    //    DOTween.To(() => u_moviePlay.u_texMovie.alpha, x => u_moviePlay.u_texMovie.alpha = x, 1, 0.2f);
    //}

    //private void End()
    //{
    //    u_moviePlay.OnStop();

    //    PlayEnd();

    //    if (m_tweenDestroy > 0)
    //    {
    //        DOTween.To(() => u_panel.alpha, x => u_panel.alpha = x, 0, m_tweenDestroy).OnComplete(Destroy);
    //    }
    //    else if (m_tweenDestroy == 0)
    //    {
    //        Destroy();
    //    }
    //}

    //private void PlayEnd()
    //{
    //    if (m_playFinishCallBack != null)
    //    {
    //        m_playFinishCallBack();
    //        m_playFinishCallBack = null;
    //    }
    //}

    //private void VideoError(MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCode, MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCodeExtra)
    //{
    //    PlayEnd();
    //}

    //private void Destroy()
    //{
    //    m_playFinishCallBack = null;
    //    u_moviePlay.OnStop();
    //    GameObject.Destroy(gameObject);
    //}
}