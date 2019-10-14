using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IModule
{
    /// <summary>
    /// 模块初始化
    /// </summary>

    void Init();

    /// <summary>
    /// 进入战斗
    /// </summary>

    void EnterBattle();

    /// <summary>
    /// 退出战斗
    /// </summary>

    void ExitBattle();

    /// <summary>
    /// 网络断开
    /// </summary>

    void NetDisConnected();

    /// <summary>
    /// 网络重连接
    /// </summary>

    void NetConnected();
}