/**************************
 * 文件名:Timer.cs
 * 文件描述:定时器类
 * 创建日期:2019/08/17
 * 作者:ZB
 ***************************/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float m_currentTime = 0;                        // 定时器已经启动的时间
    private int m_looped = 0;                               // 已经执行次数

    private int m_pauseTime = 0;                            // 暂停等待的时间
    private float m_pauseTimed = 0;                         // 暂停已经过去的时间

    private bool m_statePause = false;                      // 暂停状态
    private bool m_stateFnish = false;                      // 完成状态
    private bool m_statePauseAlways = false;                // 永久暂停状态，必须主动恢复。

    private object m_userData = null;                       // 自定义数据，用来做传递参数。

    /// <summary>
    /// 定时器序列号(唯一)
    /// </summary>

    public int SerialID { get; private set; }

    /// <summary>
    /// 定时器执行次数
    /// </summary>

    public int Loop { get; private set; }

    /// <summary>
    ///  定时器定时时间，也就是多少秒之后执行。
    /// </summary>

    public int Time { get; private set; }

    /// <summary>
    /// 定时器执行回调1
    /// </summary>

    public Action ActionArg0 { get; private set; }

    /// <summary>
    /// 定时器执行回调2
    /// </summary>

    public Action<object> ActionArg1 { get; private set; }

    Timer(int serialID, int time, int loop)
    {
        Reset();

        SerialID = serialID;
        Loop = loop;
        Time = time;
    }

    /// <summary>
    /// 实例化定时器
    /// </summary>
    /// <param name="serialID">序列号</param>
    /// <param name="time">时间,单位毫秒</param>
    /// <param name="loop">次数,为负数时为无线次数</param>
    /// <param name="action">执行</param>
    /// <param name="userData">自定义参数</param>

    public Timer(int serialID, int time, int loop, Action action, object userData) : this(serialID, time, loop)
    {
        ActionArg0 = action;
    }

    /// <summary>
    /// 实例化定时器
    /// </summary>
    /// <param name="serialID">序列号</param>
    /// <param name="time">时间,单位毫秒</param>
    /// <param name="loop">次数,为负数时为无线次数</param>
    /// <param name="action">执行</param>
    /// <param name="userData">自定义参数</param>

    public Timer(int serialID, int time, int loop, Action<object> action, object userData) : this(serialID, time, loop)
    {
        ActionArg1 = action;
        m_userData = userData;
    }

    /// <summary>
    /// 重新开始，继承之前的数据
    /// </summary>

    public void Resume()
    {
        m_currentTime = 0;
        m_looped = 0;
    }

    /// <summary>
    /// 重新开始
    /// </summary>
    /// <param name="serialID">序列号</param>
    /// <param name="time">时间,单位毫秒</param>
    /// <param name="loop">次数,为负数时为无线次数</param>
    /// <param name="action">执行</param>

    public void Resume(int serialID, int time, int loop, Action action)
    {
        Reset();

        SerialID = serialID;
        Loop = loop;
        Time = time;
        ActionArg0 = action;
    }

    /// <summary>
    /// 重新开始
    /// </summary>
    /// <param name="serialID">序列号</param>
    /// <param name="time">时间,单位毫秒</param>
    /// <param name="loop">次数,为负数时为无线次数</param>
    /// <param name="action">执行</param>
    /// <param name="userData">自定义参数</param>

    public void Resume(int serialID, int time, int loop, Action<object> action, object userData)
    {
        Reset();

        SerialID = serialID;
        Loop = loop;
        Time = time;
        ActionArg1 = action;
        m_userData = userData;
    }

    /// <summary>
    /// 立即完成一次
    /// </summary>

    public void FnishOnce()
    {
        if (ActionArg0 != null)
        {
            ActionArg0();
        }
        if (ActionArg1 != null)
        {
            ActionArg1(m_userData);
        }
        m_looped++;
        m_currentTime = 0;

        if (m_looped == Loop)
        {
            m_stateFnish = true;
        }
    }

    /// <summary>
    /// 停止
    /// </summary>

    public void Stop()
    {
        Reset();
        m_stateFnish = true;
    }

    /// <summary>
    /// 暂停
    /// </summary>
    /// <param name="time">暂停时间,单位毫秒 为-1时长久暂停,必须主动恢复</param>

    public void Pause(int time)
    {
        if (time == -1)
        {
            m_statePauseAlways = true;
        }

        if (time <= 0)
        {
            return;
        }

        m_pauseTime = time;
        m_statePause = true;
    }

    /// <summary>
    /// 恢复
    /// </summary>

    public void Recover()
    {
        m_pauseTime = 0;
        m_statePause = false;
        m_statePauseAlways = false;
    }

    /// <summary>
    /// 是否完成
    /// </summary>

    public bool IsFnish()
    {
        return m_stateFnish;
    }

    /// <summary>
    /// 轮询
    /// </summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位</param>

    public void Update(float elapseSeconds, float realElapseSeconds)
    {
        if (m_stateFnish || m_statePauseAlways) { return; }

        if (m_statePause)
        {
            m_pauseTimed += elapseSeconds;
            // 暂停中
            if ((m_pauseTime / 1000f) >= m_pauseTimed)
            {
                return;
            }
            // 暂停结束
            else
            {
                m_statePause = false;
                m_pauseTime = 0;
                m_pauseTimed = 0;
                m_statePauseAlways = false;
            }
        }

        m_currentTime += elapseSeconds;

        if (m_currentTime >= (Time / 1000f))
        {
            if (ActionArg0 != null)
            {
                ActionArg0();
            }
            if (ActionArg1 != null)
            {
                ActionArg1(m_userData);
            }
            m_currentTime = 0;
            if (Loop > 0)
            {
                if (m_looped <= Loop)
                {
                    m_looped++;
                    if (m_looped == Loop)
                    {
                        Reset();
                        m_stateFnish = true;
                    }
                }
            }
        }
    }

    private void Reset()
    {
        m_statePauseAlways = false;
        m_statePause = false;
        m_stateFnish = false;
        m_currentTime = 0;
        m_pauseTime = 0;
        m_pauseTimed = 0;
        m_looped = 0;
        Loop = 0;
        Time = 0;
        SerialID = 0;
        ActionArg0 = null;
        ActionArg1 = null;
        m_userData = null;
    }
}