/**************************
 * 文件名:UI_MoviePlay.cs
 * 文件描述:UI视频
 * 创建日期:2019/09/23
 * 作者:ZB
 ***************************/




using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MoviePlay : MonoBehaviour
{
    public UITexture u_texMovie;
    public UIWidget u_widBackground;

    public bool musicState = false;                                             // 声音打开状态
    public float effectInTime = 0;
    public float effectOut = 0;

    private MediaPlayerCtrl m_mediaPlayerCtrl;                                  // Easy Movie Texture Video Texture媒体播放器
    private MediaPlayerCtrl.VideoReady m_videoReady;                            // 视频准备就绪回调
    private MediaPlayerCtrl.VideoEnd m_videoEnd;                                // 播放完直接回调，在淡出之前
    private MediaPlayerCtrl.VideoEnd m_end;                                     // 全部结束
    public MediaPlayerCtrl.VideoResize m_videoResize;
    private MediaPlayerCtrl.VideoFirstFrameReady m_videoFirstFrameReady;

    public MediaPlayerCtrl LMediaPlayerCtrl
    {
        get
        {
            if (m_mediaPlayerCtrl == null)
            {
                m_mediaPlayerCtrl = gameObject.AddComponent<MediaPlayerCtrl>();
                m_mediaPlayerCtrl.m_TargetMaterial = new GameObject[] { u_texMovie.gameObject };
            }

            return m_mediaPlayerCtrl;
        }
    }

    private void OnDestroy()
    {
        OnStop();
    }

    /// <summary>
    /// 设置 - 视频准备就绪回调
    /// </summary>

    public void SetVideoReady(MediaPlayerCtrl.VideoReady videoReady)
    {
        m_videoReady = videoReady;
    }

    /// <summary>
    /// 设置 - 视频第一帧准备就绪回调
    /// </summary>
    /// <param name="videoFirstFrameReady"></param>

    public void SetVideoFirstFrameReady(MediaPlayerCtrl.VideoFirstFrameReady videoFirstFrameReady)
    {
        m_videoFirstFrameReady = videoFirstFrameReady;
    }

    /// <summary>
    /// 设置 - 视频播放结束但谈出特效没有播放结束回调
    /// </summary>

    public void SetVideoEnd(MediaPlayerCtrl.VideoEnd videoEnd)
    {
        m_videoEnd = videoEnd;
    }

    /// <summary>
    /// 设置 - 视频播放结束且谈出特效播放结束回调
    /// </summary>

    public void SetEnd(MediaPlayerCtrl.VideoEnd videoEnd)
    {
        m_end = videoEnd;
    }

    public void SetVideoResize(MediaPlayerCtrl.VideoResize videoResize)
    {
        m_videoResize = videoResize;
    }

    public void SetvideoError(MediaPlayerCtrl.VideoError videoError)
    {
        m_mediaPlayerCtrl.OnVideoError = videoError;
    }

    public bool IsPlaying()
    {
        if (m_mediaPlayerCtrl != null)
        {
            return m_mediaPlayerCtrl.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING;
        }

        return false;
    }

    public void OnPlay(string path, bool isLoop = false, bool useAlphaEnterView = false)
    {
        if (IsPlaying())
        {
            OnStop();
        }

        path = "video/" + path;

        if (!Application.isMobilePlatform)
        {
            path = "file://" + Ctrl.device.PlatformDataRoot + path;
        }

#if UNITY_IPHONE  || UNITY_TVOS || UNITY_EDITOR || UNITY_STANDALONE
        u_texMovie.uvRect = new Rect(0, 1, 1, -1);
#endif

        if (m_mediaPlayerCtrl != null)
        {
            m_mediaPlayerCtrl.m_bLoop = isLoop;
            m_mediaPlayerCtrl.OnEnd = End;
            m_mediaPlayerCtrl.OnVideoFirstFrameReady = m_videoFirstFrameReady;
            if (useAlphaEnterView)
            {
                m_mediaPlayerCtrl.OnReady = MovieReadyNoAlpha;
            }
            else
            {
                m_mediaPlayerCtrl.OnReady = MovieReady;
            }
        }

        if (u_widBackground != null)
        {
            u_texMovie.alpha = 0;
            u_widBackground.alpha = 0;

            if (m_mediaPlayerCtrl != null)
            {
                Ctrl.tweenerManager.ToAlpha(u_widBackground.transform, 1, effectInTime, delegate ()
                {
                    m_mediaPlayerCtrl.Load(path);
                });
            }
        }
        else
        {
            m_mediaPlayerCtrl.Load(path);
        }
    }

    public void OnPlay()
    {
        if (m_mediaPlayerCtrl != null)
        {
            m_mediaPlayerCtrl.Play();
        }
    }

    public void OnPause()
    {
        if (m_mediaPlayerCtrl != null)
        {
            m_mediaPlayerCtrl.Pause();
        }
    }

    public void OnResume()
    {
        if (m_mediaPlayerCtrl != null)
        {
            m_mediaPlayerCtrl.Play();
        }
    }

    public void OnStop()
    {
        if (m_mediaPlayerCtrl != null)
        {
            if (IsPlaying())
            {
                m_mediaPlayerCtrl.Stop();
                m_mediaPlayerCtrl.UnLoad();
                End();
            }
        }
    }

    private void End()
    {
        if (u_widBackground != null)
        {
            u_texMovie.alpha = 0;
            u_widBackground.alpha = 1;

            if (m_videoEnd != null)
            {
                m_videoEnd();
            }

            Ctrl.tweenerManager.ToAlpha(u_widBackground.transform, 0, effectOut, delegate ()
            {
                if (m_end != null)
                {
                    m_end();
                }
            });
        }
        else
        {
            if (m_videoEnd != null)
            {
                m_videoEnd();
            }
            if (m_end != null)
            {
                m_end();
            }
        }

        if (m_mediaPlayerCtrl != null)
        {
            m_mediaPlayerCtrl.OnReady = MovieReady;
        }
    }

    private void MovieReady()
    {
        u_texMovie.alpha = 1;

        if (m_mediaPlayerCtrl != null)
        {
            m_mediaPlayerCtrl.SetVolume(musicState ? 1 : 0);
        }

        if (m_videoReady != null)
        {
            m_videoReady();
        }
    }

    private void MovieReadyNoAlpha()
    {
        u_texMovie.alpha = 0;

        if (m_mediaPlayerCtrl != null)
        {
            m_mediaPlayerCtrl.SetVolume(musicState ? 1 : 0);
        }

        if (m_videoReady != null)
        {
            m_videoReady();
        }
    }
}