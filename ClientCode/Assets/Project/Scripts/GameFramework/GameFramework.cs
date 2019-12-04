/**************************
 * 文件名:GameFramework.cs
 * 文件描述:游戏一切的开始
 * 创建日期:2019/08/16
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UI.Common;
using UnityEngine;
using zb.NGUILibrary;

public class GameFramework : MonoSingleton<GameFramework>
{
    public delegate void DelegateOnBaseSystemPrepareComplete();                     // 基础系统准备完成委托

    private static float s_fps = 0;

    [HideInInspector] public int FPS = 60;                                          // 游戏帧率
    [HideInInspector] public bool stateLockFPS = false;                             // 锁定FPS开关

    private double m_lastRealTime;                              // 最后一个实时时间
    private float m_lastUpdateTime;
    private float m_accum;                                      // 累加数
    private float m_accumTime;                                  // 累加时间
    private int m_frames;                                       // 帧数
    private float m_frequency = 0.1f;                           // 频率

    protected override void Init()
    {
        base.Init();

        // 初始化打印类
        Log.Init();

        Ctrl.Init();

        /* 
         * 省电设置，最后活跃用户交互之后，过一些时间，使屏幕变暗。对手持设备非常有用，允许操作系统保护电池最有效的办法.
		 * 以秒计算，0表示不睡眠。默认值根据不同的平台而不同，一般是在移动设备上非零值。
		 */
        Screen.sleepTimeout = -1;

        /* 设置目标帧率 */
        SetTargetFrameRate();
    }

    private void Start()
    {
        // 后台运行
        Application.runInBackground = false;
        // 自动旋转屏幕为向左
        Screen.autorotateToLandscapeLeft = true;
        // 自动旋转屏幕为向右
        Screen.autorotateToLandscapeRight = true;
        // 自动旋转屏幕为纵向
        Screen.autorotateToPortrait = false;
        // 自动旋转屏幕为纵向倒置
        Screen.autorotateToPortraitUpsideDown = false;
        // 屏幕取向
        Screen.orientation = ScreenOrientation.AutoRotation;

        Ctrl.gameStateCtrl.GotoState("LaunchState");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        GameFrameworkEntry.Shutdown();

        Ctrl.UnInit();

        Log.UnInit();
    }

    private void Update()
    {
        UpdateFPS();

        Ctrl.timerManager.CustomUpdate(Time.deltaTime, Time.unscaledDeltaTime);

        GameFrameworkEntry.Update(Time.deltaTime, Time.unscaledDeltaTime);
    }

    private void LateUpdate()
    {
        
    }

    private void FixedUpdate()
    {
        GameFrameworkEntry.FixedUpdate(Time.deltaTime, Time.unscaledDeltaTime);
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 30;
        GUI.color = Color.red;
        GUILayout.BeginArea(new Rect(10, 10, 100, 40));
        GUILayout.Label("FPS:" + ((int)s_fps).ToString(), GUILayout.Height(40));
        GUILayout.EndArea();
    }

    private void OnApplicationFocus(bool focus)
    {
        
    }

    private void OnApplicationPause(bool pause)
    {
        
    }

    private void OnApplicationQuit()
    {
        
    }

    #region Update

    private void UpdateFPS()
    {
        /* 游戏帧数计算 */
        try
        {
            if (!stateLockFPS)
            {
                float _frameTime = Time.realtimeSinceStartup - m_lastUpdateTime;
                m_accumTime += _frameTime;
                m_accum += 1f / _frameTime;
                m_lastUpdateTime = Time.realtimeSinceStartup;
                m_frames++;
                if (m_accumTime >= m_frequency)
                {
                    s_fps = m_accum / (float)m_frames;
                    m_accumTime = 0;
                    m_accum = 0;
                    m_frames = 0;
                }
            }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception);
        }
    }

    #endregion

    #region Set

    public void SetFPS(int val)
    {
        FPS = val;
        SetTargetFrameRate();
    }

    public void SetStateLockFPS(bool val)
    {
        stateLockFPS = val;
        SetTargetFrameRate();
    }

    private void SetTargetFrameRate()
    {
        /* 锁定游戏帧率 */
        if (stateLockFPS)
        {
            Application.targetFrameRate = FPS;

            StartCoroutine("WaitForTargetFrameRate");
        }
        else
        {
            StopCoroutine("WaitForTargetFrameRate");

            Application.targetFrameRate = FPS;
        }
    }

    #endregion

    #region 锁定FPS

    private IEnumerator WaitForTargetFrameRate()
    {
        yield return new GameWaitForTargetFrameRateIterator
        {
            _this = this
        };
    }

    private sealed class GameWaitForTargetFrameRateIterator : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object current;

        internal int pc;

        internal GameFramework _this;

        internal double dt;

        internal float delta;

        internal double sleepTime;

        internal int sleepTimeInt;

        internal double t;

        object IEnumerator.Current
        {
            get
            {
                return current;
            }
        }

        object IEnumerator<object>.Current
        {
            get
            {
                return current;
            }
        }

        public void Dispose()
        {
            pc = -1;
        }

        public bool MoveNext()
        {
            uint _num = (uint)pc;

            pc = -1;

            switch (_num)
            {
                case 0:

                    break;

                case 1:

                    t = Time.realtimeSinceStartup;

                    dt = (t - _this.m_lastRealTime) * 1000.0f;

                    sleepTime = (1000f / _this.FPS) - dt;

                    sleepTimeInt = Mathf.Clamp((int)sleepTime, 0, _this.FPS);

                    if (sleepTimeInt > 0)
                    {
                        Thread.Sleep(sleepTimeInt);
                    }

                    _this.m_lastRealTime = Time.realtimeSinceStartup;

                    delta = Time.realtimeSinceStartup - _this.m_lastUpdateTime;

                    _this.m_accumTime += delta;

                    _this.m_accum += 1f / delta;

                    _this.m_lastUpdateTime = Time.realtimeSinceStartup;

                    _this.m_frames++;

                    if (_this.m_accumTime >= _this.m_frequency)
                    {
                        s_fps = _this.m_accum / (float)_this.m_frames;

                        _this.m_accum = 0;

                        _this.m_accumTime = 0;

                        _this.m_frames = 0;
                    }

                    break;

                default:

                    return false;
            }

            current = new WaitForEndOfFrame();

            pc = 1;

            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}