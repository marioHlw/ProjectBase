/**************************
 * 文件名:ObjectBase.cs
 * 文件描述:对象抽象基类
 * 创建日期:2019/08/19
 * 作者:ZB
 ***************************/



using System;

public abstract class ObjectBase
{
    /// <summary>
    /// 获取对象
    /// </summary>

    public object Target
    {
        get;
        private set;
    }

    /// <summary>
    /// 获取对象名称
    /// </summary>

    public string Name
    {
        get;
        private set;
    }

    /// <summary>
    /// 获取自定义释放检查标记
    /// </summary>

    public virtual bool CustomCanReleaseFlag { get { return true; } }

    /// <summary>
    /// 获取对象上次使用时间
    /// </summary>

    public DateTime LastUseTime { get; internal set; }

    /// <summary>
    /// 获取或设置对象是否被加锁
    /// </summary>

    public bool Locked { get; set; }

    /// <summary>
    /// 获取或设置对象的优先级
    /// </summary>

    public int Priority { get; set; }

    /// <summary>
    /// 初始化对象基类的新实例
    /// </summary>
    /// <param name="target">对象</param>

    public ObjectBase(object target) : this(null, target, false, 0)
    {
    }

    /// <summary>
    /// 初始化对象基类的新实例
    /// </summary>
    /// <param name="name">对象名称</param>
    /// <param name="target">对象</param>

    public ObjectBase(string name, object target) : this(name, target, false, 0)
    {
    }

    /// <summary>
    /// 初始化对象基类的新实例
    /// </summary>
    /// <param name="name">对象名称</param>
    /// <param name="target">对象</param>
    /// <param name="locked">对象是否被加锁</param>

    public ObjectBase(string name, object target, bool locked) : this(name, target, locked, 0)
    {
    }

    /// <summary>
    /// 初始化对象基类的新实例
    /// </summary>
    /// <param name="name">对象名称</param>
    /// <param name="target">对象</param>
    /// <param name="priority">对象的优先级</param>

    public ObjectBase(string name, object target, int priority) : this(name, target, false, priority)
    {
    }

    /// <summary>
    /// 初始化对象基类的新实例
    /// </summary>
    /// <param name="name">对象名称</param>
    /// <param name="target">对象</param>
    /// <param name="locked">对象是否被加锁</param>
    /// <param name="priority">对象的优先级</param>

    public ObjectBase(string name, object target, bool locked, int priority)
    {
        if (target == null)
        {
            throw new ObjectPoolException(Utility.ZText.Format("Target '{0}' is invalid.", name));
        }

        Name = name ?? string.Empty;
        Target = target;
        Locked = locked;
        Priority = priority;
        LastUseTime = DateTime.Now;
    }

    /// <summary>
    /// 获取对象时的事件
    /// </summary>

    protected internal virtual void OnSpawn()
    {
    }

    /// <summary>
    /// 回收对象时的事件
    /// </summary>

    protected internal virtual void OnUnspawn()
    {
    }

    /// <summary>
    /// 释放对象
    /// </summary>
    /// <param name="isShutdown">是否是关闭对象池时触发</param>

    protected internal abstract void Release(bool isShutdown);
}