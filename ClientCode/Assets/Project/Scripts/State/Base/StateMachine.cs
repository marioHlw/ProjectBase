/**************************
 * 文件名:StateMachine.cs
 * 文件描述:游戏状态状态机类
 * 创建日期:2019/08/20
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class StateMachine 
{
    private Dictionary<string, IState> m_registedState = new Dictionary<string, IState>();              // 注册的游戏状态

    private Stack<IState> m_stateStack = new Stack<IState>();

    /// <summary>
    /// 切换游戏状态
    /// </summary>

    public IState ChangeState(IState state)
    {
        if (state == null)
        {
            return null;
        }

        TarState = state;

        IState _state = null;

        if (m_stateStack.Count > 0)
        {
            _state = m_stateStack.Pop();

            _state.OnStateLeave();
        }

        m_stateStack.Push(state);

        state.OnStateEnter();

        return _state;
    }

    /// <summary>
    /// 切换游戏状态
    /// </summary>

    public IState ChangeState(string name)
    {
        IState _state;

        if (name == null)
        {
            return null;
        }

        if (!m_registedState.TryGetValue(name, out _state))
        {
            return null;
        }

        return ChangeState(_state);
    }

    public void Clear()
    {
        while (m_stateStack.Count > 0)
        {
            m_stateStack.Pop().OnStateLeave();
        }
    }

    public IState GetState(string name)
    {
        IState _state;

        if (name == null)
        {
            return null;
        }

        return (!m_registedState.TryGetValue(name, out _state) ? null : _state);
    }

    public string GetStateName(IState state)
    {
        if (state != null)
        {
            Dictionary<string, IState>.Enumerator enumerator = m_registedState.GetEnumerator();

            while (enumerator.MoveNext())
            {
                KeyValuePair<string, IState> current = enumerator.Current;

                if (current.Value == state)
                {
                    return current.Key;
                }
            }
        }

        return null;
    }

    public IState PopState()
    {
        if (m_stateStack.Count <= 0)
        {
            return null;
        }

        IState _state = m_stateStack.Pop();

        _state.OnStateLeave();

        if (m_stateStack.Count > 0)
        {
            m_stateStack.Peek().OnStateResume();
        }

        return _state;
    }

    public void Push(IState state)
    {
        if (state != null)
        {
            if (m_stateStack.Count > 0)
            {
                m_stateStack.Peek().OnStateOverride();
            }

            m_stateStack.Push(state);

            state.OnStateEnter();
        }
    }

    public void Push(string name)
    {
        IState _state;

        if ((name != null) && m_registedState.TryGetValue(name, out _state))
        {
            Push(_state);
        }
    }

    public void RegisterState(string name, IState state)
    {
        if (name != null && state != null && !m_registedState.ContainsKey(name))
        {
            m_registedState.Add(name, state);
        }
    }

    public void RegisterState<TStateImplType>(TStateImplType State, string name) where TStateImplType : IState
    {
        RegisterState(name, State);
    }

    public ClassEnumerator RegisterStateByAttributes<TAttributeType>(Assembly InAssembly) where TAttributeType : Attribute
    {
        ClassEnumerator enumerator = new ClassEnumerator(typeof(TAttributeType), typeof(IState), InAssembly, true, false, false);

        List<Type>.Enumerator enumerator2 = enumerator.Results.GetEnumerator();

        while (enumerator2.MoveNext())
        {
            Type current = enumerator2.Current;

            IState state = (IState)Activator.CreateInstance(current);

            RegisterState<IState>(state, state.Name);
        }
        return enumerator;
    }

    public ClassEnumerator RegisterStateByAttributes<TAttributeType>(Assembly InAssembly, params object[] args) where TAttributeType : Attribute
    {
        ClassEnumerator _enumerator = new ClassEnumerator(typeof(TAttributeType), typeof(IState), InAssembly, true, false, false);

        List<Type>.Enumerator _enumerators = _enumerator.Results.GetEnumerator();

        while (_enumerators.MoveNext())
        {
            Type current = _enumerators.Current;

            IState state = (IState)Activator.CreateInstance(current, args);

            RegisterState<IState>(state, state.Name);
        }
        return _enumerator;
    }

    public IState TopState()
    {
        if (m_stateStack.Count <= 0)
        {
            return null;
        }

        return m_stateStack.Peek();
    }

    public string TopStateName()
    {
        if (m_stateStack.Count <= 0)
        {
            return null;
        }

        IState state = m_stateStack.Peek();

        return GetStateName(state);
    }

    public IState UnregisterState(string name)
    {
        IState state;

        if (name == null)
        {
            return null;
        }

        if (!m_registedState.TryGetValue(name, out state))
        {
            return null;
        }

        m_registedState.Remove(name);

        return state;
    }

    public int Count
    {
        get
        {
            return m_stateStack.Count;
        }
    }

    public IState TarState
    {
        get;

        private set;
    }
}
