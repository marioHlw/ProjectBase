/**************************
 * 文件名:IState.cs
 * 文件描述:游戏状态接口
 * 创建日期:2019/08/20
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// 进入当前状态
    /// </summary>

    void OnStateEnter();

    /// <summary>
    /// 离开当前状态
    /// </summary>

    void OnStateLeave();

    /// <summary>
    /// 重写当前状态
    /// </summary>

    void OnStateOverride();

    /// <summary>
    /// 重新开始当前状态
    /// </summary>

    void OnStateResume();

    /// <summary>
    /// 获取当前状态名称
    /// </summary>

    string Name { get; }
}
