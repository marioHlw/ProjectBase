/**************************
 * 文件名:GameFrameworkModule.cs
 * 文件描述:游戏框架模块抽象类
 * 创建日期:2019/08/17
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameFrameworkModule
{
    public GameFrameworkModule()
    {

    }

    /// <summary>
    /// 获取游戏框架模块优先级,优先级较高的模块会优先轮询，并且关闭操作会后进行
    /// </summary>

    internal virtual int Priority { get { return 0; } }

    /// <summary>
    /// 关闭并清理游戏框架模块
    /// </summary>

    internal abstract void Shutdown();

    /// <summary>
    /// 游戏框架模块轮询(Update)
    /// </summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位</param>

    internal abstract void Update(float elapseSeconds, float realElapseSeconds);

    /// <summary>
    /// 游戏框架模块轮询(FixedUpdate)
    /// </summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位</param>

    internal abstract void FixedUpdate(float elapseSeconds, float realElapseSeconds);
}