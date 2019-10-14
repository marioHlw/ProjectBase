/**************************
 * 文件名:ObjectPoolManager.cs
 * 文件描述:对象池管理类
 * 创建日期:2019/08/19
 * 作者:ZB
 ***************************/




using System;
using System.Collections.Generic;

public class ObjectPoolManager : GameFrameworkModule
{
    private const int DefaultCapacity = int.MaxValue;                           // 默认数量
    private const float DefaultExpireTime = float.MaxValue;                     // 默认过期时间
    private const float DefaultAutoReleaseInterval = float.MaxValue;            // 默认自动释放时间
    private const int DefaultPriority = 0;                                      // 默认优先级

    private readonly Dictionary<string, ObjectPoolBase> m_mapObjectPool = null;         // 缓存所有对象池

    /// <summary>
    /// 获取对象池数量
    /// </summary>

    public int Count { get { return m_mapObjectPool.Count; } }

    #region 构造函数

    public ObjectPoolManager()
    {
        m_mapObjectPool = new Dictionary<string, ObjectPoolBase>();
    }

    #endregion

    #region 创建允许多次获取的对象池

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType)
    {
        return InternalCreateObjectPool(objectType, string.Empty, true, DefaultAutoReleaseInterval, DefaultCapacity, DefaultExpireTime, 0);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity)
    {
        return InternalCreateObjectPool(objectType, string.Empty, true, DefaultAutoReleaseInterval, capacity, DefaultExpireTime, 0);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, float expireTime)
    {
        return InternalCreateObjectPool(objectType, string.Empty, true, expireTime, DefaultCapacity, expireTime, 0);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name)
    {
        return InternalCreateObjectPool(objectType, name, true, DefaultAutoReleaseInterval, DefaultCapacity, DefaultExpireTime, 0);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, int priority)
    {
        return InternalCreateObjectPool(objectType, string.Empty, true, DefaultAutoReleaseInterval, capacity, DefaultExpireTime, priority);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, float expireTime)
    {
        return InternalCreateObjectPool(objectType, string.Empty, true, expireTime, capacity, expireTime, 0);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, string.Empty, true, expireTime, DefaultCapacity, expireTime, priority);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity)
    {
        return InternalCreateObjectPool(objectType, name, true, DefaultAutoReleaseInterval, capacity, DefaultExpireTime, 0);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float expireTime)
    {
        return InternalCreateObjectPool(objectType, name, true, expireTime, DefaultCapacity, expireTime, 0);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, string.Empty, true, expireTime, capacity, expireTime, priority);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, int priority)
    {
        return InternalCreateObjectPool(objectType, name, true, DefaultAutoReleaseInterval, capacity, DefaultExpireTime, priority);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, float expireTime)
    {
        return InternalCreateObjectPool(objectType, name, true, expireTime, capacity, expireTime, 0);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, name, true, expireTime, DefaultCapacity, expireTime, priority);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, name, true, expireTime, capacity, expireTime, priority);
    }

    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float autoReleaseInterval, int capacity, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, name, true, autoReleaseInterval, capacity, expireTime, priority);
    }

    #endregion

    #region 创建允许单次获取的对象池
 
    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType)
    {
        return InternalCreateObjectPool(objectType, string.Empty, false, DefaultAutoReleaseInterval, DefaultCapacity, DefaultExpireTime, 0);
    }
  
    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity)
    {
        return InternalCreateObjectPool(objectType, string.Empty, false, DefaultAutoReleaseInterval, capacity, DefaultExpireTime, 0);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, float expireTime)
    {
        return InternalCreateObjectPool(objectType, string.Empty, false, expireTime, DefaultCapacity, expireTime, 0);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name)
    {
        return InternalCreateObjectPool(objectType, name, false, DefaultAutoReleaseInterval, DefaultCapacity, DefaultExpireTime, 0);
    }
   
    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, int priority)
    {
        return InternalCreateObjectPool(objectType, string.Empty, false, DefaultAutoReleaseInterval, capacity, DefaultExpireTime, priority);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, float expireTime)
    {
        return InternalCreateObjectPool(objectType, string.Empty, false, expireTime, capacity, expireTime, 0);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, string.Empty, false, expireTime, DefaultCapacity, expireTime, priority);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity)
    {
        return InternalCreateObjectPool(objectType, name, false, DefaultAutoReleaseInterval, capacity, DefaultExpireTime, 0);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float expireTime)
    {
        return InternalCreateObjectPool(objectType, name, false, expireTime, DefaultCapacity, expireTime, 0);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, string.Empty, false, expireTime, capacity, expireTime, priority);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, int priority)
    {
        return InternalCreateObjectPool(objectType, name, false, DefaultAutoReleaseInterval, capacity, DefaultExpireTime, priority);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, float expireTime)
    {
        return InternalCreateObjectPool(objectType, name, false, expireTime, capacity, expireTime, 0);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, name, false, expireTime, DefaultCapacity, expireTime, priority);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, name, false, expireTime, capacity, expireTime, priority);
    }

    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float autoReleaseInterval, int capacity, float expireTime, int priority)
    {
        return InternalCreateObjectPool(objectType, name, false, autoReleaseInterval, capacity, expireTime, priority);
    }

    #endregion

    #region 删除

    public bool DestroyObjectPool(ObjectPoolBase objectPool)
    {
        if (objectPool == null)
        {
            throw new ObjectPoolException("Object pool is invalid.");
        }

        return InternalDestroyObjectPool(Utility.ZText.GetFullName(objectPool.ObjectType, objectPool.Name));
    }

    public bool DestroyObjectPool<T>() where T : ObjectBase
    {
        return InternalDestroyObjectPool(Utility.ZText.GetFullName<T>(string.Empty));
    }

    public bool DestroyObjectPool<T>(string name) where T : ObjectBase
    {
        return InternalDestroyObjectPool(Utility.ZText.GetFullName<T>(name));
    }

    public bool DestroyObjectPool(Type objectType)
    {
        if (objectType == null)
        {
            throw new ObjectPoolException("Object type is invalid.");
        }

        if (!typeof(ObjectBase).IsAssignableFrom(objectType))
        {
            throw new ObjectPoolException(Utility.ZText.Format("Object type '{0}' is invalid.", objectType.FullName));
        }

        return InternalDestroyObjectPool(Utility.ZText.GetFullName(objectType, string.Empty));
    }

    public bool DestroyObjectPool(Type objectType, string name)
    {
        if (objectType == null)
        {
            throw new ObjectPoolException("Object type is invalid.");
        }

        if (!typeof(ObjectBase).IsAssignableFrom(objectType))
        {
            throw new ObjectPoolException(Utility.ZText.Format("Object type '{0}' is invalid.", objectType.FullName));
        }

        return InternalDestroyObjectPool(Utility.ZText.GetFullName(objectType, name));
    }

    #endregion

    #region 获取

    public ObjectPoolBase GetObjectPool(string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
        {
            throw new ObjectPoolException("Full name is invalid.");
        }

        ObjectPoolBase objectPoolBase = null;

        if (m_mapObjectPool.TryGetValue(fullName, out objectPoolBase))
        {
            return objectPoolBase;
        }

        return null;
    }

    public ObjectPoolBase GetObjectPool(Type objectType)
    {
        if (objectType == null)
        {
            throw new ObjectPoolException("Object type is invalid.");
        }

        if (!typeof(ObjectBase).IsAssignableFrom(objectType))
        {
            throw new ObjectPoolException(Utility.ZText.Format("Object type '{0}' is invalid.", objectType.FullName));
        }

        return GetObjectPool(Utility.ZText.GetFullName(objectType, string.Empty));
    }

    public ObjectPoolBase GetObjectPool(Type objectType, string name)
    {
        if (objectType == null)
        {
            throw new ObjectPoolException("Object type is invalid.");
        }

        if (!typeof(ObjectBase).IsAssignableFrom(objectType))
        {
            throw new ObjectPoolException(Utility.ZText.Format("Object type '{0}' is invalid.", objectType.FullName));
        }

        return GetObjectPool(Utility.ZText.GetFullName(objectType, name));
    }

    public ObjectPoolBase GetObjectPool(Predicate<ObjectPoolBase> condition)
    {
        if (condition == null)
        {
            throw new ObjectPoolException("Condition is invalid.");
        }

        foreach (KeyValuePair<string, ObjectPoolBase> pair in m_mapObjectPool)
        {
            if (condition(pair.Value))
            {
                return pair.Value;
            }
        }

        return null;
    }

    public ObjectPoolBase[] GetAllObjectPools()
    {
        return GetAllObjectPools(false);
    }

    public ObjectPoolBase[] GetAllObjectPools(bool sort)
    {
        if (sort)
        {
            List<ObjectPoolBase> list = new List<ObjectPoolBase>();

            foreach (KeyValuePair<string, ObjectPoolBase> pair in m_mapObjectPool)
            {
                list.Add(pair.Value);
            }

            list.Sort(new Comparison<ObjectPoolBase>(ObjectPoolComparer));

            return list.ToArray();
        }

        int index = 0;

        ObjectPoolBase[] baseArray = new ObjectPoolBase[m_mapObjectPool.Count];

        foreach (KeyValuePair<string, ObjectPoolBase> pair2 in m_mapObjectPool)
        {
            baseArray[index++] = pair2.Value;
        }

        return baseArray;
    }

    public ObjectPoolBase[] GetObjectPools(Predicate<ObjectPoolBase> condition)
    {
        if (condition == null)
        {
            throw new ObjectPoolException("Condition is invalid.");
        }

        List<ObjectPoolBase> list = new List<ObjectPoolBase>();

        foreach (KeyValuePair<string, ObjectPoolBase> pair in m_mapObjectPool)
        {
            if (condition(pair.Value))
            {
                list.Add(pair.Value);
            }
        }

        return list.ToArray();
    }

    public void GetAllObjectPools(List<ObjectPoolBase> results)
    {
        GetAllObjectPools(false, results);
    }

    public void GetAllObjectPools(bool sort, List<ObjectPoolBase> results)
    {
        if (results == null)
        {
            throw new ObjectPoolException("Results is invalid.");
        }

        results.Clear();

        foreach (KeyValuePair<string, ObjectPoolBase> pair in m_mapObjectPool)
        {
            results.Add(pair.Value);
        }

        if (sort)
        {
            results.Sort(new Comparison<ObjectPoolBase>(ObjectPoolComparer));
        }
    }

    public void GetObjectPools(Predicate<ObjectPoolBase> condition, List<ObjectPoolBase> results)
    {
        if (condition == null)
        {
            throw new ObjectPoolException("Condition is invalid.");
        }

        if (results == null)
        {
            throw new ObjectPoolException("Results is invalid.");
        }

        results.Clear();

        foreach (KeyValuePair<string, ObjectPoolBase> pair in m_mapObjectPool)
        {
            if (condition(pair.Value))
            {
                results.Add(pair.Value);
            }
        }
    }

    #endregion

    #region 检查

    public bool HasObjectPool<T>() where T : ObjectBase
    {
        return HasObjectPool(Utility.ZText.GetFullName<T>(string.Empty));
    }

    /// <summary>
    /// 检查是否存在对象池
    /// </summary>
    /// <param name="condition">要检查的条件</param>
    /// <returns>是否存在对象池</returns>

    public bool HasObjectPool(Predicate<ObjectPoolBase> condition)
    {
        if (condition == null)
        {
            throw new ObjectPoolException("Condition is invalid.");
        }

        foreach (KeyValuePair<string, ObjectPoolBase> pair in m_mapObjectPool)
        {
            if (condition(pair.Value))
            {
                return true;
            }
        }
        return false;
    }

    public bool HasObjectPool(string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
        {
            throw new ObjectPoolException("Full name is invalid.");
        }

        return m_mapObjectPool.ContainsKey(fullName);
    }

    public bool HasObjectPool<T>(string name) where T : ObjectBase
    {
        return HasObjectPool(Utility.ZText.GetFullName<T>(name));
    }

    public bool HasObjectPool(Type objectType)
    {
        if (objectType == null)
        {
            throw new ObjectPoolException("Object type is invalid.");
        }

        if (!typeof(ObjectBase).IsAssignableFrom(objectType))
        {
            throw new ObjectPoolException(Utility.ZText.Format("Object type '{0}' is invalid.", objectType.FullName));
        }

        return HasObjectPool(Utility.ZText.GetFullName(objectType, string.Empty));
    }

    public bool HasObjectPool(Type objectType, string name)
    {
        if (objectType == null)
        {
            throw new ObjectPoolException("Object type is invalid.");
        }

        if (!typeof(ObjectBase).IsAssignableFrom(objectType))
        {
            throw new ObjectPoolException(Utility.ZText.Format("Object type '{0}' is invalid.", objectType.FullName));
        }

        return HasObjectPool(Utility.ZText.GetFullName(objectType, name));
    }

    #endregion

    #region 释放

    public void Release()
    {
        ObjectPoolBase[] allObjectPools = GetAllObjectPools(true);

        for (int i = 0; i < allObjectPools.Length; i++)
        {
            allObjectPools[i].Release();
        }
    }

    public void ReleaseAllUnused()
    {
        ObjectPoolBase[] allObjectPools = GetAllObjectPools(true);

        for (int i = 0; i < allObjectPools.Length; i++)
        {
            allObjectPools[i].ReleaseAllUnused();
        }
    }

    #endregion

    /// <summary>
    /// 内部创建对象池
    /// </summary>
    /// <param name="objectType">对象类型</param>
    /// <param name="name">对象池名称</param>
    /// <param name="allowMultiSpawn">是否允许对象被多次获取</param>
    /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数</param>
    /// <param name="capacity">对象池的容量</param>
    /// <param name="expireTime">对象池对象过期秒数</param>
    /// <param name="priority">对象池优先级</param>
    /// <returns>对象池对象</returns>

    private ObjectPoolBase InternalCreateObjectPool(Type objectType, string name, bool allowMultiSpawn, float autoReleaseInterval, int capacity, float expireTime, int priority)
    {
        if (objectType == null)
        {
            throw new ObjectPoolException("Object type is invalid.");
        }

        if (!typeof(ObjectBase).IsAssignableFrom(objectType))
        {
            throw new ObjectPoolException(Utility.ZText.Format("Object type '{0}' is invalid.", objectType.FullName));
        }

        if (HasObjectPool(objectType, name))
        {
            throw new ObjectPoolException(Utility.ZText.Format("Already exist object pool '{0}'.", Utility.ZText.GetFullName(objectType, name)));
        }

        Type objectPoolType = typeof(InObjectPool<>).MakeGenericType(objectType);

        ObjectPoolBase objectPoolBase = (ObjectPoolBase)Activator.CreateInstance(objectPoolType, name, allowMultiSpawn, autoReleaseInterval, capacity, expireTime, priority);

        m_mapObjectPool.Add(Utility.ZText.GetFullName(objectType, name), objectPoolBase);

        return objectPoolBase;
    }

    /// <summary>
    /// 内部删除对象池
    /// </summary>
    /// <param name="fullName">完全名称</param>
    /// <returns>是否删除成功</returns>

    private bool InternalDestroyObjectPool(string fullName)
    {
        ObjectPoolBase objectPoolBase = null;

        if (m_mapObjectPool.TryGetValue(fullName, out objectPoolBase))
        {
            objectPoolBase.Shutdown();

            return m_mapObjectPool.Remove(fullName);
        }

        return false;
    }

    /// <summary>
    /// 对象池优先级比较
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>小于返回-1，等于返回0，大于返回1</returns>

    private int ObjectPoolComparer(ObjectPoolBase a, ObjectPoolBase b)
    {
        return a.Priority.CompareTo(b.Priority);
    }

    /// <summary>
    /// 获取游戏框架模块优先级
    /// </summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行</remarks>

    internal override int Priority { get { return 90; } }

    /// <summary>
    /// 关闭并清理对象池管理器
    /// </summary>

    internal override void Shutdown()
    {
        foreach (KeyValuePair<string, ObjectPoolBase> pair in m_mapObjectPool)
        {
            pair.Value.Shutdown();
        }

        m_mapObjectPool.Clear();
    }

    /// <summary>
    /// 对象池管理器轮询
    /// </summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位</param>

    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
        foreach (KeyValuePair<string, ObjectPoolBase> pair in m_mapObjectPool)
        {
            pair.Value.Update(elapseSeconds, realElapseSeconds);
        }
    }

    /// <summary>
    /// 对象池管理器轮询
    /// </summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位</param>

    internal override void FixedUpdate(float elapseSeconds, float realElapseSeconds)
    {
        foreach (KeyValuePair<string, ObjectPoolBase> pair in m_mapObjectPool)
        {
            pair.Value.FixedUpdate(elapseSeconds, realElapseSeconds);
        }
    }

    /// <summary>
    /// 内部对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>

    private sealed class InObject<T> where T : ObjectBase
    {
        private readonly T m_object;
        private int m_spawnCount;

        /// <summary>
        /// 获取对象名称
        /// </summary>

        public string Name { get { return m_object.Name; } }

        /// <summary>
        /// 获取对象是否被加锁
        /// </summary>

        public bool Locked { get { return m_object.Locked; } internal set { m_object.Locked = value; } }

        /// <summary>
        /// 获取自定义释放检查标记
        /// </summary>

        public bool CustomCanReleaseFlag { get { return m_object.CustomCanReleaseFlag; } }

        /// <summary>
        /// 获取对象的优先级
        /// </summary>

        public int Priority { get { return m_object.Priority; } internal set { m_object.Priority = value; } }

        /// <summary>
        /// 获取对象上次使用时间
        /// </summary>

        public DateTime LastUseTime { get { return m_object.LastUseTime; } }

        /// <summary>
        /// 获取对象是否正在使用
        /// </summary>

        public bool IsInUse { get { return m_spawnCount > 0; } }

        /// <summary>
        /// 获取对象的获取计数
        /// </summary>

        public int SpawnCount { get { return m_spawnCount; } }

        /// <summary>
        /// 实例化内部对象的新实例
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="spawned">对象是否已被获取</param>

        public InObject(T obj, bool spawned)
        {
            if (obj == null)
            {
                throw new ObjectPoolException("Object is invalid.");
            }

            m_object = obj;

            m_spawnCount = spawned ? 1 : 0;

            if (spawned)
            {
                m_object.OnSpawn();
            }
        }

        /// <summary>
        /// 查看对象
        /// </summary>
        /// <returns>对象</returns>

        public T Peek()
        {
            return m_object;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns>对象</returns>

        public T Spawn()
        {
            m_spawnCount++;

            m_object.LastUseTime = DateTime.Now;

            m_object.OnSpawn();

            return m_object;
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="isShutdown">是否是关闭对象池时触发</param>

        public void Release(bool isShutdown)
        {
            m_object.Release(isShutdown);
        }

        /// <summary>
        /// 回收对象
        /// </summary>
 
        public void Unspawn()
        {
            m_object.OnUnspawn();

            m_object.LastUseTime = DateTime.Now;

            m_spawnCount--;

            if (SpawnCount < 0)
            {
                throw new ObjectPoolException("Spawn count is less than 0.");
            }
        }
    }

    /// <summary>
    /// 内部对象池
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>

    private sealed class InObjectPool<T> : ObjectPoolBase where T : ObjectBase
    {
        private readonly LinkedList<InObject<T>> m_objects;
        private readonly List<T> m_cachedCanReleaseObjects;
        private readonly List<T> m_cachedToReleaseObjects;

        private readonly bool m_allowMultiSpawn;            // 是否允许对象被多次获取
        private float m_autoReleaseInterval;                // 对象池自动释放可释放对象的间隔秒数
        private float m_autoReleaseTime;                    // 对象池自动释放间隔累计秒数
        private int m_capacity;                             // 对象池的容量
        private float m_expireTime;                         // 对象池对象过期秒数
        private int m_priority;                             // 对象池的优先级

        /// <summary>
        /// 获取是否允许对象被多次获取
        /// </summary>

        public override bool AllowMultiSpawn { get { return m_allowMultiSpawn; } }

        /// <summary>
        /// 获取对象池中能被释放的对象的数量
        /// </summary>

        public override int CanReleaseCount { get { return m_cachedCanReleaseObjects.Count; } }

        /// <summary>
        /// 获取对象池中对象的数量
        /// </summary>

        public override int Count { get { return m_objects.Count; } }

        /// <summary>
        /// 获取对象池对象类型
        /// </summary>

        public override Type ObjectType { get { return typeof(T); } }

        /// <summary>
        /// 获取对象池自动释放可释放对象的间隔秒数
        /// </summary>

        public override float AutoReleaseInterval { get { return m_autoReleaseInterval; } }

        /// <summary>
        /// 获取对象池的优先级
        /// </summary>

        public override int Priority { get { return m_priority; } }

        /// <summary>
        /// 获取对象池的容量
        /// </summary>

        public override int Capacity { get { return m_capacity; } }

        /// <summary>
        /// 获取对象池对象过期秒数
        /// </summary>

        public override float ExpireTime { get { return m_expireTime; } }

        /// <summary>
        /// 初始化对象池的新实例
        /// </summary>
        /// <param name="name">对象池名称</param>
        /// <param name="allowMultiSpawn">是否允许对象被多次获取</param>
        /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数</param>
        /// <param name="capacity">对象池的容量</param>
        /// <param name="expireTime">对象池对象过期秒数</param>
        /// <param name="priority">对象池的优先级</param>

        public InObjectPool(string name, bool allowMultiSpawn, float autoReleaseInterval, int capacity, float expireTime, int priority) : base(name)
        {
            m_objects = new LinkedList<InObject<T>>();
            m_cachedCanReleaseObjects = new List<T>();
            m_cachedToReleaseObjects = new List<T>();

            m_allowMultiSpawn = allowMultiSpawn;
            m_autoReleaseInterval = autoReleaseInterval;
            m_priority = priority;
            m_autoReleaseTime = 0f;

            SetCapacity(capacity);
            SetExpireTime(expireTime);
        }

        /// <summary>
        /// 检查对象
        /// </summary>
        /// <returns>要检查的对象是否存在</returns>

        public bool CanSpawn()
        {
            return CanSpawn(string.Empty);
        }

        /// <summary>
        /// 检查对象
        /// </summary>
        /// <param name="name">对象名称</param>
        /// <returns>要检查的对象是否存在</returns>

        public bool CanSpawn(string name)
        {
            foreach (InObject<T> obj in m_objects)
            {
                if (obj.Name != name)
                {
                    continue;
                }

                if (m_allowMultiSpawn || !obj.IsInUse)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取所有对象信息
        /// </summary>
        /// <returns>所有对象信息</returns>

        public override ObjectInfo[] GetAllObjectInfos()
        {
            int index = 0;

            ObjectInfo[] infoArray = new ObjectInfo[m_objects.Count];

            foreach (InObject<T> tempObject in m_objects)
            {
                infoArray[index++] = new ObjectInfo(tempObject.Name, tempObject.Locked, tempObject.Priority, tempObject.LastUseTime, tempObject.SpawnCount);
            }

            return infoArray;
        }

        /// <summary>
        /// 释放对象池中的可释放对象
        /// </summary>

        public override void Release()
        {
            Release(m_objects.Count - m_capacity, new ReleaseObjectFilterCallback<T>(DefaultReleaseObjectFilterCallback));
        }

        /// <summary>
        /// 释放对象池中的可释放对象
        /// </summary>
        /// <param name="toReleaseCount">尝试释放对象数量</param>

        public override void Release(int toReleaseCount)
        {
            Release(toReleaseCount, new ReleaseObjectFilterCallback<T>(DefaultReleaseObjectFilterCallback));
        }

        /// <summary>
        /// 释放对象池中的所有未使用对象
        /// </summary>

        public override void ReleaseAllUnused()
        {
            LinkedListNode<InObject<T>> current = m_objects.First;

            while (current != null)
            {
                if (current.Value.IsInUse || current.Value.Locked || !current.Value.CustomCanReleaseFlag)
                {
                    current = current.Next;

                    continue;
                }

                LinkedListNode<InObject<T>> next = current.Next;

                m_objects.Remove(current);

                current.Value.Release(false);

                Log.Debug("Object pool '{0}' release '{1}'.", Utility.ZText.GetFullName<T>(Name), current.Value.Name);

                current = next;
            }
        }

        /// <summary>
        /// 释放对象池中的可释放对象
        /// </summary>
        /// <param name="releaseObjectFilterCallback">释放对象筛选函数</param>

        public void Release(ReleaseObjectFilterCallback<T> releaseObjectFilterCallback)
        {
            Release(m_objects.Count - m_capacity, releaseObjectFilterCallback);
        }

        /// <summary>
        /// 释放对象池中的可释放对象
        /// </summary>
        /// <param name="toReleaseCount">尝试释放对象数量</param>
        /// <param name="releaseObjectFilterCallback">释放对象筛选函数</param>

        public void Release(int toReleaseCount, ReleaseObjectFilterCallback<T> releaseObjectFilterCallback)
        {
            if (releaseObjectFilterCallback == null)
            {
                throw new ObjectPoolException("Release object filter callback is invalid.");
            }

            m_autoReleaseTime = 0f;

            if (toReleaseCount < 0)
            {
                toReleaseCount = 0;
            }

            DateTime expireTime = DateTime.MinValue;

            if (m_expireTime < float.MaxValue)
            {
                expireTime = DateTime.Now.AddSeconds((double)-m_expireTime);
            }

            GetCanReleaseObjects(m_cachedCanReleaseObjects);
            List<T> toReleaseObjects = releaseObjectFilterCallback(m_cachedCanReleaseObjects, toReleaseCount, expireTime);

            if (toReleaseObjects == null || toReleaseObjects.Count <= 0)
            {
                return;
            }

            foreach (ObjectBase toReleaseObject in toReleaseObjects)
            {
                if (toReleaseObject == null)
                {
                    throw new ObjectPoolException("Can not release null object.");
                }

                bool found = false;

                foreach (InObject<T> obj in m_objects)
                {
                    if (obj.Peek() != toReleaseObject)
                    {
                        continue;
                    }

                    m_objects.Remove(obj);

                    obj.Release(false);

                    Log.Debug("Object pool '{0}' release '{1}'.", Utility.ZText.GetFullName<T>(Name), toReleaseObject.Name);

                    found = true;

                    break;
                }

                if (!found)
                {
                    throw new ObjectPoolException("Can not release object which is not found.");
                }
            }
        }

        #region Set

        /// <summary>
        /// 设置 - 对象池自动释放可释放对象的间隔秒数
        /// </summary>
        /// <param name="autoReleaseInterval">间隔秒数</param>

        public void SetAutoReleaseInterval(float autoReleaseInterval)
        {
            m_autoReleaseInterval = autoReleaseInterval;
        }

        /// <summary>
        /// 设置 - 对象池的优先级
        /// </summary>
        /// <param name="priority">优先级</param>

        public void SetPriority(int priority)
        {
            m_priority = priority;
        }

        /// <summary>
        /// 设置 - 对象池对象过期秒数
        /// </summary>
        /// <param name="expireTime">过期秒数</param>

        public void SetExpireTime(float expireTime)
        {
            if (expireTime < 0f)
            {
                throw new ObjectPoolException("ExpireTime is invalid.");
            }

            if (ExpireTime == expireTime)
            {
                return;
            }

            Log.Debug("Object pool '{0}' expire time changed from '{1}' to '{2}'.", Utility.ZText.GetFullName<T>(Name), m_expireTime.ToString(), expireTime.ToString());

            m_expireTime = expireTime;
            Release();
        }

        /// <summary>
        /// 设置 - 对象池的容量
        /// </summary>
        /// <param name="capacity">容量</param>

        public void SetCapacity(int capacity)
        {
            if (capacity < 0)
            {
                throw new ObjectPoolException("Capacity is invalid.");
            }

            if (m_capacity == capacity)
            {
                return;
            }

            Log.Debug("Object pool '{0}' capacity changed from '{1}' to '{2}'.", Utility.ZText.GetFullName<T>(Name), m_capacity.ToString(), capacity.ToString());

            m_capacity = capacity;

            Release();
        }

        /// <summary>
        /// 设置 - 对象是否被加锁
        /// </summary>
        /// <param name="obj">要设置被加锁的内部对象</param>
        /// <param name="locked">是否被加锁</param>

        public void SetLocked(T obj, bool locked)
        {
            if (obj == null)
            {
                throw new ObjectPoolException("Object is invalid.");
            }

            SetLocked(obj.Target, locked);
        }

        /// <summary>
        /// 设置 - 对象是否被加锁
        /// </summary>
        /// <param name="target">要设置被加锁的对象</param>
        /// <param name="locked">是否被加锁</param>

        public void SetLocked(object target, bool locked)
        {
            if (target == null)
            {
                throw new ObjectPoolException("Target is invalid.");
            }

            InObject<T> obj = GetObject(target);

            if (obj != null)
            {
                Log.Debug("Object pool '{0}' set locked '{1}' to '{2}.", Utility.ZText.GetFullName<T>(Name), obj.Peek().Name, locked.ToString());

                obj.Locked = locked;
            }
            else
            {
                throw new ObjectPoolException(Utility.ZText.Format("Can not find target in object pool '{0}'.", Utility.ZText.GetFullName<T>(Name)));
            }
        }

        /// <summary>
        /// 设置 - 对象的优先级
        /// </summary>
        /// <param name="obj">要设置优先级的内部对象</param>
        /// <param name="priority">优先级</param>

        public void SetPriority(T obj, int priority)
        {
            if (obj == null)
            {
                throw new ObjectPoolException("Object is invalid.");
            }

            SetPriority(obj.Target, priority);
        }

        /// <summary>
        /// 设置 - 对象的优先级
        /// </summary>
        /// <param name="target">要设置优先级的对象</param>
        /// <param name="priority">优先级</param>

        public void SetPriority(object target, int priority)
        {
            if (target == null)
            {
                throw new ObjectPoolException("Target is invalid.");
            }

            InObject<T> obj = GetObject(target);

            if (obj != null)
            {
                Log.Debug("Object pool '{0}' set priority '{1}' to '{2}.", Utility.ZText.GetFullName<T>(Name), obj.Peek().Name, priority.ToString());

                obj.Priority = priority;
            }
            else
            {
                throw new ObjectPoolException(Utility.ZText.Format("Can not find target in object pool '{0}'.", Utility.ZText.GetFullName<T>(Name)));
            }
        }

        #endregion

        #region 对象操作

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns>要获取的对象</returns>

        public T Spawn()
        {
            return Spawn(string.Empty);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="name">对象名称</param>
        /// <returns>要获取的对象</returns>

        public T Spawn(string name)
        {
            foreach (InObject<T> obj in m_objects)
            {
                if (obj.Name != name)
                {
                    continue;
                }

                if (m_allowMultiSpawn || !obj.IsInUse)
                {
                    Log.Debug("Object pool '{0}' spawn '{1}'.", Utility.ZText.GetFullName<T>(Name), obj.Peek().Name);

                    return obj.Spawn();
                }
            }

            return null;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj">要回收的内部对象</param>

        public void Unspawn(T obj)
        {
            if (obj == null)
            {
                throw new ObjectPoolException("Object is invalid.");
            }

            Unspawn(obj.Target);
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="target">要回收的对象</param>

        public void Unspawn(object target)
        {
            if (target == null)
            {
                throw new ObjectPoolException("Target is invalid.");
            }

            InObject<T> obj = GetObject(target);

            if (obj != null)
            {
                Log.Debug("Object pool '{0}' unspawn '{1}'.", Utility.ZText.GetFullName<T>(Name), obj.Peek().Name);

                obj.Unspawn();

                Release();
            }
            else
            {
                throw new ObjectPoolException(Utility.ZText.Format("Can not find target in object pool '{0}'.", Utility.ZText.GetFullName<T>(Name)));
            }
        }

        /// <summary>
        /// 注册对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="spawned">对象是否已被获取</param>

        public void Register(T obj, bool spawned)
        {
            if (obj == null)
            {
                throw new ObjectPoolException("Object is invalid.");
            }

            Log.Debug(spawned ? "Object pool '{0}' create and spawned '{1}'." : "Object pool '{0}' create '{1}'.", Utility.ZText.GetFullName<T>(Name), obj.Name);

            m_objects.AddLast(new InObject<T>(obj, spawned));

            Release();
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="target">对象的目标体</param>
        /// <returns>对象</returns>

        private InObject<T> GetObject(object target)
        {
            foreach (InObject<T> obj in m_objects)
            {
                if (obj.Peek().Target == target)
                {
                    return obj;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取可以释放的对象集
        /// </summary>
        /// <param name="results">可以释放的对象集</param>

        private void GetCanReleaseObjects(List<T> results)
        {
            if (results == null)
            {
                throw new ObjectPoolException("Results is invalid.");
            }

            results.Clear();

            foreach (InObject<T> obj in m_objects)
            {
                if (obj.IsInUse || obj.Locked || !obj.CustomCanReleaseFlag)
                {
                    continue;
                }

                results.Add(obj.Peek());
            }
        }

        #endregion

        /// <summary>
        /// 默认释放对象过滤回调
        /// </summary>
        /// <param name="candidateObjects">候选释放的对象</param>
        /// <param name="toReleaseCount">需要释放的数量</param>
        /// <param name="expireTime">过期时间</param>
        /// <returns>需要释放的对象集</returns>

        private List<T> DefaultReleaseObjectFilterCallback(List<T> candidateObjects, int toReleaseCount, DateTime expireTime)
        {
            m_cachedToReleaseObjects.Clear();

            /* 释放已经过时的对象 */
            if (expireTime > DateTime.MinValue)
            {
                for (int i = candidateObjects.Count - 1; i >= 0; i--)
                {
                    if (candidateObjects[i].LastUseTime <= expireTime)
                    {
                        m_cachedToReleaseObjects.Add(candidateObjects[i]);

                        candidateObjects.RemoveAt(i);

                        continue;
                    }
                }

                toReleaseCount -= m_cachedToReleaseObjects.Count;
            }

            /* 如果需要释放的数量还有多余，从优先级最低并且过时最早的开始释放 */
            for (int i = 0; toReleaseCount > 0 && i < candidateObjects.Count; i++)
            {
                for (int j = i + 1; j < candidateObjects.Count; j++)
                {
                    if (candidateObjects[i].Priority > candidateObjects[j].Priority
                        || (candidateObjects[i].Priority == candidateObjects[j].Priority && candidateObjects[i].LastUseTime > candidateObjects[j].LastUseTime))
                    {
                        T temp = candidateObjects[i];

                        candidateObjects[i] = candidateObjects[j];

                        candidateObjects[j] = temp;
                    }
                }

                m_cachedToReleaseObjects.Add(candidateObjects[i]);

                toReleaseCount--;
            }

            return m_cachedToReleaseObjects;
        }

        internal override void Shutdown()
        {
            LinkedListNode<InObject<T>> current = m_objects.First;

            while (current != null)
            {
                LinkedListNode<InObject<T>> next = current.Next;

                m_objects.Remove(current);

                current.Value.Release(true);

                Log.Debug("Object pool '{0}' release '{1}'.", Utility.ZText.GetFullName<T>(Name), current.Value.Name);

                current = next;
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_autoReleaseTime += realElapseSeconds;

            if (m_autoReleaseTime < m_autoReleaseInterval)
            {
                return;
            }

            Log.Debug("Object pool '{0}' auto release start.", Utility.ZText.GetFullName<T>(Name));

            Release();

            Log.Debug("Object pool '{0}' auto release complete.", Utility.ZText.GetFullName<T>(Name));
        }

        internal override void FixedUpdate(float elapseSeconds, float realElapseSeconds)
        {
            
        }
    }
}

/// <summary>
/// 释放对象筛选方法
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
/// <param name="candidateObjects">要筛选的对象集合</param>
/// <param name="toReleaseCount">需要释放的对象数量</param>
/// <param name="expireTime">对象过期参考时间</param>
/// <returns>经筛选需要释放的对象集合</returns>

public delegate List<T> ReleaseObjectFilterCallback<T>(List<T> candidateObjects, int toReleaseCount, DateTime expireTime) where T : ObjectBase;