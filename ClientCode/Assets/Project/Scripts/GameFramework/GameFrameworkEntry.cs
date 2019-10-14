/**************************
 * 文件名:GameFrameworkEntry.cs
 * 文件描述:游戏框架入口
 * 创建日期:2019/08/17
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFrameworkEntry
{
    private static readonly LinkedList<GameFrameworkModule> gameFrameworkModuleMap = new LinkedList<GameFrameworkModule>();             // 缓存所有游戏模块容器

    /// <summary>
    /// 获取游戏框架模块
    /// </summary>
    /// <typeparam name="T">要获取的游戏框架模块类型</typeparam>
    /// <returns>要获取的游戏框架模块。如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块</returns>

    public static T GetModule<T>() where T : class
    {
        Type type = typeof(T);

        string typeName = type.Name;

        if (!type.IsInterface)
        {
            typeName = type.ToString();
        }

        Type moduleType = Type.GetType(typeName);

        if (moduleType == null)
        {
            throw new LogException(string.Format("Can not find Game Framework module type '{0}'.", typeName));
        }

        return (GetModule(moduleType) as T);
    }

    /// <summary>
    /// 获取游戏框架模块
    /// </summary>
    /// <param name="moduleType">要获取的游戏框架模块类型</param>

    private static GameFrameworkModule GetModule(Type moduleType)
    {
        foreach (GameFrameworkModule module in gameFrameworkModuleMap)
        {
            if (module.GetType() == moduleType)
            {
                return module;
            }
        }

        return CreateModule(moduleType);
    }

    /// <summary>
    /// 创建游戏框架模块
    /// </summary>
    /// <param name="moduleType">要游戏框架模块类型</param>
    /// <returns>游戏框架模块</returns>

    private static GameFrameworkModule CreateModule(Type moduleType)
    {
        GameFrameworkModule module = (GameFrameworkModule)Activator.CreateInstance(moduleType);

        if (module == null)
        {
            throw new LogException(string.Format("Can not create module '{0}'.", moduleType.FullName));
        }

        LinkedListNode<GameFrameworkModule> first = gameFrameworkModuleMap.First;

        while (first != null)
        {
            if (module.Priority > first.Value.Priority)
            {
                break;
            }
            first = first.Next;
        }

        if (first != null)
        {
            gameFrameworkModuleMap.AddBefore(first, module);
            return module;
        }

        gameFrameworkModuleMap.AddLast(module);
        return module;
    }


    /// <summary>
    /// 关闭并清理所有游戏框架模块
    /// </summary>

    public static void Shutdown()
    {
        for (LinkedListNode<GameFrameworkModule> node = gameFrameworkModuleMap.Last; node != null; node = node.Previous)
        {
            node.Value.Shutdown();
        }
        gameFrameworkModuleMap.Clear();
    }

    /// <summary>
    /// 所有游戏框架模块轮询(Update)
    /// </summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位</param>

    public static void Update(float elapseSeconds, float realElapseSeconds)
    {
        using (LinkedList<GameFrameworkModule>.Enumerator enumerator = gameFrameworkModuleMap.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                enumerator.Current.Update(elapseSeconds, realElapseSeconds);
            }
        }
    }

    /// <summary>
    /// 所有游戏框架模块轮询(FixedUpdate)
    /// </summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位</param>

    public static void FixedUpdate(float elapseSeconds, float realElapseSeconds)
    {
        using (LinkedList<GameFrameworkModule>.Enumerator enumerator = gameFrameworkModuleMap.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                enumerator.Current.FixedUpdate(elapseSeconds, realElapseSeconds);
            }
        }
    }
}