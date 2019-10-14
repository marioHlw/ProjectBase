/**************************
 * 文件名:EventRouter.cs
 * 文件描述:事件管理类
 * 创建日期:2019/08/20
 * 作者:ZB
 ***************************/




using System;
using System.Collections.Generic;

public class EventRouter : Singleton<EventRouter>
{
    private Dictionary<string, Delegate> m_eventTables = new Dictionary<string, Delegate>();

    public override void Init()
    {
        base.Init();
    }

    public override void UnInit()
    {
        base.UnInit();

        ClearAllEvents();
    }

    public void AddEventHandler(string eventType, Action handler)
    {
        if (OnHandlerAdding(eventType, handler))
        {
            m_eventTables[eventType] = (Action)Delegate.Combine((Action)m_eventTables[eventType], handler);
        }
    }

    public void AddEventHandler<T1>(string eventType, Action<T1> handler)
    {
        if (OnHandlerAdding(eventType, handler))
        {
            m_eventTables[eventType] = (Action<T1>)Delegate.Combine((Action<T1>)m_eventTables[eventType], handler);
        }
    }

    public void AddEventHandler<T1, T2>(string eventType, Action<T1, T2> handler)
    {
        if (OnHandlerAdding(eventType, handler))
        {
            m_eventTables[eventType] = (Action<T1, T2>)Delegate.Combine((Action<T1, T2>)m_eventTables[eventType], handler);
        }
    }

    public void AddEventHandler<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler)
    {
        if (OnHandlerAdding(eventType, handler))
        {
            m_eventTables[eventType] = (Action<T1, T2, T3>)Delegate.Combine((Action<T1, T2, T3>)m_eventTables[eventType], handler);
        }
    }

    public void AddEventHandler<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler)
    {
        if (OnHandlerAdding(eventType, handler))
        {
            m_eventTables[eventType] = (Action<T1, T2, T3, T4>)Delegate.Combine((Action<T1, T2, T3, T4>)m_eventTables[eventType], handler);
        }
    }

    public void RemoveEventHandler(string eventType, Action handler)
    {
        if (OnHandlerRemoving(eventType, handler))
        {
            m_eventTables[eventType] = (Action)Delegate.Remove((Action)m_eventTables[eventType], handler);
        }
    }

    public void RemoveEventHandler<T1>(string eventType, Action<T1> handler)
    {
        if (OnHandlerRemoving(eventType, handler))
        {
            m_eventTables[eventType] = (Action<T1>)Delegate.Remove((Action<T1>)m_eventTables[eventType], handler);
        }
    }

    public void RemoveEventHandler<T1, T2>(string eventType, Action<T1, T2> handler)
    {
        if (OnHandlerRemoving(eventType, handler))
        {
            m_eventTables[eventType] = (Action<T1, T2>)Delegate.Remove((Action<T1, T2>)m_eventTables[eventType], handler);
        }
    }

    public void RemoveEventHandler<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler)
    {
        if (OnHandlerRemoving(eventType, handler))
        {
            m_eventTables[eventType] = (Action<T1, T2, T3>)Delegate.Remove((Action<T1, T2, T3>)m_eventTables[eventType], handler);
        }
    }

    public void RemoveEventHandler<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler)
    {
        if (OnHandlerRemoving(eventType, handler))
        {
            m_eventTables[eventType] = (Action<T1, T2, T3, T4>)Delegate.Remove((Action<T1, T2, T3, T4>)m_eventTables[eventType], handler);
        }
    }

    public void BroadCastEvent(string eventType)
    {
        if (OnBroadCasting(eventType))
        {
            if ((m_eventTables[eventType] as Action) != null)
            {
                (m_eventTables[eventType] as Action).Invoke();
            }
        }
    }

    public void BroadCastEvent<T1>(string eventType, T1 arg1)
    {
        if (OnBroadCasting(eventType))
        {
            if ((m_eventTables[eventType] as Action<T1>) != null)
            {
                (m_eventTables[eventType] as Action<T1>).Invoke(arg1);
            }
        }
    }

    public void BroadCastEvent<T1, T2>(string eventType, T1 arg1, T2 arg2)
    {
        if (OnBroadCasting(eventType))
        {
            if ((m_eventTables[eventType] as Action<T1, T2>) != null)
            {
                (m_eventTables[eventType] as Action<T1, T2>).Invoke(arg1, arg2);
            }
        }
    }

    public void BroadCastEvent<T1, T2, T3>(string eventType, T1 arg1, T2 arg2, T3 arg3)
    {
        if (OnBroadCasting(eventType))
        {
            if ((m_eventTables[eventType] as Action<T1, T2, T3>) != null)
            {
                (m_eventTables[eventType] as Action<T1, T2, T3>).Invoke(arg1, arg2, arg3);
            }
        }
    }

    public void BroadCastEvent<T1, T2, T3, T4>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        if (OnBroadCasting(eventType))
        {
            if ((m_eventTables[eventType] as Action<T1, T2, T3, T4>) != null)
            {
                (m_eventTables[eventType] as Action<T1, T2, T3, T4>).Invoke(arg1, arg2, arg3, arg4);
            }
        }
    }

    private bool OnHandlerAdding(string eventType, Delegate handler)
    {
        bool _result = true;

        if (!m_eventTables.ContainsKey(eventType))
        {
            m_eventTables.Add(eventType, null);
        }

        Delegate _delegate = m_eventTables[eventType];

        if (_delegate != null && _delegate.GetType() != handler.GetType())
        {
            _result = false;
        }

        return _result;
    }

    private bool OnHandlerRemoving(string eventType, Delegate handler)
    {
        bool _result = true;

        if (m_eventTables.ContainsKey(eventType))
        {
            Delegate _delegate = m_eventTables[eventType];

            if (_delegate != null)
            {
                if (_delegate.GetType() != handler.GetType())
                {
                    _result = false;
                }
            }
            else
            {
                _result = false;
            }
        }
        else
        {
            _result = false;
        }
        return _result;
    }

    private bool OnBroadCasting(string eventType)
    {
        return m_eventTables.ContainsKey(eventType);
    }

    public void ClearAllEvents()
    {
        m_eventTables.Clear();
    }
}
