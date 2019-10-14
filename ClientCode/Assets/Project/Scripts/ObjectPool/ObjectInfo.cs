/**************************
 * 文件名:ObjectInfo.cs
 * 文件描述:对象信息类
 * 创建日期:2019/08/19
 * 作者:ZB
 ***************************/



using System;

public struct ObjectInfo
{
    private readonly string m_name;                     // 名字
    private readonly bool m_locked;                     // 上锁状态
    private readonly int m_priority;                    // 优先级
    private readonly DateTime m_lastUseTime;            // 上次使用时间
    private readonly int m_spawnCount;                  // 被使用次数

    /// <summary>
    /// 获取对象名称
    /// </summary>

    public string Name { get { return m_name; } }

    /// <summary>
    /// 获取对象是否被加锁
    /// </summary>

    public bool Locked { get { return m_locked; } }

    /// <summary>
    /// 获取对象的优先级
    /// </summary>

    public int Priority { get { return m_priority; } }

    /// <summary>
    /// 获取对象上次使用时间
    /// </summary>

    public DateTime LastUseTime { get { return m_lastUseTime; } }

    /// <summary>
    /// 获取对象是否正在使用
    /// </summary>

    public bool IsInUse { get { return (m_spawnCount > 0); } }

    /// <summary>
    /// 获取对象的获取计数
    /// </summary>

    public int SpawnCount { get { return m_spawnCount; } }

    /// <summary>
    /// 初始化对象信息的新实例
    /// </summary>
    /// <param name="name">对象名称</param>
    /// <param name="locked">对象是否被加锁</param>
    /// <param name="priority">对象的优先级</param>
    /// <param name="lastUseTime">对象上次使用时间</param>
    /// <param name="spawnCount">对象的获取计数</param>

    public ObjectInfo(string name, bool locked, int priority, DateTime lastUseTime, int spawnCount)
    {
        m_name = name;
        m_locked = locked;
        m_priority = priority;
        m_lastUseTime = lastUseTime;
        m_spawnCount = spawnCount;
    }
}
