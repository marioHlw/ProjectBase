/**************************
 * 文件名:TimerManager.cs
 * 文件描述:定时器管理类
 * 创建日期:2019/08/17
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    private Dictionary<int, Timer> m_timerMap = null;
    private List<int> m_serials = null;

    private LinkedList<Timer> m_timerArg0Pool = null;
    private LinkedList<Timer> m_timerArg1Pool = null;
    private int m_currentSerial = 0;

    public TimerManager()
    {
        m_serials = new List<int>();
        m_timerMap = new Dictionary<int, Timer>();

        m_timerArg0Pool = new LinkedList<Timer>();
        m_timerArg1Pool = new LinkedList<Timer>();

        m_currentSerial = 0;
    }

    public override void Init()
    {
        base.Init();

        Log.Info(Ctrl.LogInfos[0] + " - 定时器管理器");
    }

    public override void UnInit()
    {
        base.UnInit();

        Clear();

        Log.Info(Ctrl.LogInfos[1] + " - 定时器管理器");
    }

    /// <summary>
    /// 添加一个定时器
    /// </summary>
    /// <param name="time">时长</param>
    /// <param name="loop">重复次数 -1表示无限循环</param>
    /// <param name="action">到时执行委托</param>
    /// <param name="userData">自定义数据</param>
    /// <returns>序列号</returns>

    public int AddTimer(int time, int loop, Action action, object userData)
    {
        int _serilaID = ++m_currentSerial;
        Timer _timer = null;

        if (m_timerArg0Pool.Count <= 0)
        {
            _timer = new Timer(_serilaID, time, loop, action, userData);
        }
        else
        {
            LinkedListNode<Timer> _first = m_timerArg0Pool.First;

            m_timerArg0Pool.Remove(_first);

            _timer = _first.Value;
            _timer.Resume(_serilaID, time, loop, action);
        }

        m_timerMap.Add(_serilaID, _timer);
        m_serials.Add(_serilaID);

        return _serilaID;
    }

    /// <summary>
    /// 添加一个定时器
    /// </summary>
    /// <param name="time">时长</param>
    /// <param name="loop">重复次数 -1表示无限循环</param>
    /// <param name="action">到时执行委托</param>
    /// <param name="userData">自定义数据</param>
    /// <returns>序列号</returns>

    public int AddTimer(int time, int loop, Action<object> action, object userData)
    {
        int _serilaID = ++m_currentSerial;
        Timer _timer = null;

        if (m_timerArg0Pool.Count <= 0)
        {
            _timer = new Timer(_serilaID, time, loop, action, userData);
        }
        else
        {
            LinkedListNode<Timer> _first = m_timerArg1Pool.First;

            m_timerArg1Pool.Remove(_first);

            _timer = _first.Value;

            _timer.Resume(_serilaID, time, loop, action, userData);
        }

        m_timerMap.Add(_serilaID, _timer);
        m_serials.Add(_serilaID);

        return _serilaID;
    }

    /// <summary>
    /// 完成定时器一次
    /// </summary>
    /// <param name="serialID">序列号</param>

    public void FinshTimerOnce(int serialID)
    {
        Timer _timer = null;

        if (m_timerMap.TryGetValue(serialID, out _timer))
        {
            _timer.FnishOnce();
        }
    }

    /// <summary>
    /// 暂停定时器
    /// </summary>
    /// <param name="serialID">序列号</param>
    /// <param name="time">暂停时间 为-1时,永久暂停,等待恢复</param>

    public void PauseTimer(int serialID, int time = 0)
    {
        Timer _timer = null;

        if (m_timerMap.TryGetValue(serialID, out _timer))
        {
            _timer.Pause(time);
        }
    }

    /// <summary>
    /// 删除定时器
    /// </summary>
    /// <param name="serialID">序列号</param>

    public bool RemoveTimer(int serialID)
    {
        Timer _timer = null;

        if (m_timerMap.TryGetValue(serialID, out _timer))
        {
            Recycle(serialID);

            m_timerMap.Remove(serialID);
            m_serials.Remove(serialID);
        }

        return true;
    }

    /// <summary>
    /// 重新开始定时器
    /// </summary>
    /// <param name="serialID">序列号</param>

    public bool ResumeTimer(int serialID)
    {
        Timer _timer = null;

        if (m_timerMap.TryGetValue(serialID, out _timer))
        {
            _timer.Resume();
        }

        return true;
    }

    /// <summary>
    /// 清理定时器管理器
    /// </summary>

    public void Clear()
    {
        m_currentSerial = 0;
        int _count = m_serials.Count;

        for (int i = 0; i < _count; i++)
        {
            Recycle(m_serials[i]);
        }

        m_timerMap.Clear();
        m_serials.Clear();
    }

    /// <summary>
    /// 回收定时器
    /// </summary>
    /// <param name="serialID">序列号</param>

    private bool Recycle(int serialID)
    {
        if (m_timerMap.ContainsKey(serialID))
        {
            if (m_timerMap[serialID].ActionArg0 != null)
            {
                m_timerArg0Pool.AddLast(m_timerMap[serialID]);
            }

            if (m_timerMap[serialID].ActionArg1 != null)
            {
                m_timerArg1Pool.AddLast(m_timerMap[serialID]);
            }

            return true;
        }
        else
        {
            Log.Error(string.Format("回收定时器'{0}'失败" + serialID));

            return false;
        }
    }

    /// <summary>
    /// 关闭
    /// </summary>

    public void Shutdown()
    {

    }

    /// <summary>
    /// 轮询
    /// </summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位</param>

    public void CustomUpdate(float elapseSeconds, float realElapseSeconds)
    {
        int _count = m_serials.Count;

        for (int i = 0; i < _count; i++)
        {
            if (!m_timerMap[m_serials[i]].IsFnish())
            {
                m_timerMap[m_serials[i]].Update(elapseSeconds, realElapseSeconds);
            }
            else
            {
                Recycle(m_serials[i]);
            }
        }
    }
}