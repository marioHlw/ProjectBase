/**************************
 * 文件名:ObjectPoolBase.cs
 * 文件描述:对象池抽象类
 * 创建日期:2019/08/19
 * 作者:ZB
 ***************************/



using System;

public abstract class ObjectPoolBase
{
    private readonly string m_Name;

    public string Name { get { return m_Name; } }
    public string FullName { get { return Utility.ZText.GetFullName(ObjectType, m_Name); } }

    /// <summary>
    /// 获取对象类型
    /// </summary>

    public abstract Type ObjectType { get; }

    /// <summary>
    /// 获取对象优先级
    /// </summary>

    public abstract int Priority { get; }

    /// <summary>
    /// 获取是否允许对象被多次获取
    /// </summary>

    public abstract bool AllowMultiSpawn { get; }

    /// <summary>
    /// 获取或设置对象池自动释放可释放对象的间隔秒数
    /// </summary>

    public abstract float AutoReleaseInterval { get; }

    /// <summary>
    /// 获取或设置对象池对象过期秒数
    /// </summary>

    public abstract float ExpireTime { get; }

    /// <summary>
    /// 获取对象池中能被释放的对象的数量
    /// </summary>

    public abstract int CanReleaseCount { get; }

    /// <summary>
    /// 获取或设置对象池的容量
    /// </summary>

    public abstract int Capacity { get; }

    /// <summary>
    /// 获取对象池中对象的数量
    /// </summary>
    public abstract int Count { get; }

    /// <summary>
    /// 初始化对象池基类的新实例
    /// </summary>

    public ObjectPoolBase() : this(null)
    {
    }

    /// <summary>
    /// 初始化对象池基类的新实例
    /// </summary>
    /// <param name="name">对象池名称</param>

    public ObjectPoolBase(string name)
    {
        m_Name = name ?? string.Empty;
    }

    /// <summary>
    /// 获取所有对象信息
    /// </summary>
    /// <returns>所有对象信息</returns>

    public abstract ObjectInfo[] GetAllObjectInfos();

    /// <summary>
    /// 释放对象池中的可释放对象
    /// </summary>

    public abstract void Release();

    /// <summary>
    /// 释放对象池中的可释放对象
    /// </summary>
    /// <param name="toReleaseCount">尝试释放对象数量</param>

    public abstract void Release(int toReleaseCount);

    /// <summary>
    /// 释放对象池中的所有未使用对象
    /// </summary>

    public abstract void ReleaseAllUnused();

    internal abstract void Shutdown();

    internal abstract void Update(float elapseSeconds, float realElapseSeconds);

    internal abstract void FixedUpdate(float elapseSeconds, float realElapseSeconds);
}
