/**************************
 * 文件名:BaseState.cs
 * 文件描述:游戏状态基类
 * 创建日期:2019/08/20
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : IState
{
    /// <summary>
    /// 获取当前状态名称
    /// </summary>

    public virtual string Name
    {
        get
        {
            return base.GetType().Name;
        }
    }

    protected BaseState()
    {
    }

    /// <summary>
    /// 进入当前状态
    /// </summary>

    public virtual void OnStateEnter()
    {
    }

    /// <summary>
    /// 离开当前状态
    /// </summary>

    public virtual void OnStateLeave()
    {
    }

    /// <summary>
    /// 重写当前状态
    /// </summary>

    public virtual void OnStateOverride()
    {
    }

    /// <summary>
    /// 重新开始当前状态
    /// </summary>

    public virtual void OnStateResume()
    {
    }
}

