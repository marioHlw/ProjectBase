/**************************
 * 文件名:DatabinTable.cs
 * 文件描述:数据表类
 * 创建日期:2019/09/03
 * 作者:ZB
 ***************************/



using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class DatabinTable<T, K> : DatabinTableBase
{
    public DatabinTable(string inName, string inKey)
        : base(typeof(T))
    {
        dataName = inName;
        keyName = inKey;
        dataMap.Clear();
        loaded = false;

        LoadResource();
    }

    private void ReadBytesCompleteCallback(byte[] bytes)
    {
        base.OnRecordLoaded(ref bytes);
    }

    private void ReadBytesCompleteCallback(UnityEngine.Object asset)
    {
        byte[] bytes = (asset as TextAsset).bytes;
        base.OnRecordLoaded(ref bytes);
    }

    private void ReadBytesUpdateCallback(float progress)
    {
        Log.Info(string.Format("表格读取{0}进度：{1}", typeof(K).ToString(), progress));
    }

    private void ReadBytesErrorCallback(enLoadResourceStatus status)
    {
        Log.Error(string.Format("表格读取{0}读取错误：{1}", typeof(K).ToString(), Language.GetLanguageLoadResourceStatus(status)));
    }

    public override void LoadDatabinTableData(byte[] rawData, Type inValueType)
    {
        base.LoadDatabinTableData(rawData, inValueType);

        try
        {
            using (Stream stream = new MemoryStream(rawData))
            {
                // 用ProtoBuf进行序列化
                T _list = Serializer.Deserialize<T>(stream);
                PropertyInfo _listProp = _list.GetType().GetProperty("items");
                List<K> _recordList = (List<K>)_listProp.GetGetMethod().Invoke(_list, null);
                if (_recordList != null)
                {
                    Log.Info(string.Format(Ctrl.LogInfos[2], typeof(K).ToString()));

                    for (int i = 0; i < _recordList.Count; i++)
                    {
                        K item = (K)_recordList[i];
                        PropertyInfo itemProp = _recordList[i].GetType().GetProperty(keyName);
                        int id = Convert.ToInt32(itemProp.GetGetMethod().Invoke(_recordList[i], null));
                        if (!dataMap.ContainsKey(id))
                        {
                            dataMap.Add(id, item as IExtensible);
                        }
                        else
                        {
#if UNITY_EDITOR
                            Debug.LogError(string.Format("加载表{0}时出现重复的资源ID：{1}", typeof(T).ToString(), id));
#endif
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError("LoadError type is: " + typeof(T).ToString());
            Debug.LogError(ex);
#endif
        }
    }

    /// <summary>
    /// 循环所有数据
    /// </summary>
    /// <param name="inVisitor">循环参数委托</param>

    public void Accept(Action<K> inVisitor)
    {
        // 确保数据加载完成
        Reload();
        if (loaded)
        {
            Dictionary<long, object>.Enumerator _enumerator = dataMap.GetEnumerator();
            while (_enumerator.MoveNext())
            {
                KeyValuePair<long, object> _current = _enumerator.Current;
                inVisitor((K)((object)_current.Value));
            }
        }
    }

    public K FindIf(Func<K, bool> InFunc)
    {
        // 确保数据加载完成
        Reload();
        if (loaded)
        {
            Dictionary<long, object>.Enumerator enumerator = dataMap.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<long, object> _current1 = enumerator.Current;
                if (InFunc((K)((object)_current1.Value)))
                {
                    KeyValuePair<long, object> _current2 = enumerator.Current;
                    return (K)((object)_current2.Value);
                }
            }
        }

        return default(K);
    }

    /// <summary>
    /// 获取 - 根据键值获取数据
    /// </summary>

    public K GetDataByKey(uint key)
    {
        return GetDataByKey((long)(ulong)key);
    }

    /// <summary>
    /// 获取 - 根据键值获取数据
    /// </summary>

    public K GetDataByKey(long key)
    {
        K k = (K)((object)GetDataByKeyInner(key));
        if (k == null && simple && key != 0L)
        {
            Unload();
            Reload();
            simple = false;
            k = (K)(object)GetDataByKeyInner(key);
        }

        return k;
    }

    /// <summary>
    /// 获取 - 数据(索引值，第几个数据)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>

    public K GetDataByIndex(int index)
    {
        // 确保数据加载完成
        Reload();
        if (loaded)
        {
            Dictionary<long, object>.Enumerator _enumerator = dataMap.GetEnumerator();
            int _index = 0;
            while (_enumerator.MoveNext())
            {
                if (_index == index)
                {
                    KeyValuePair<long, object> _current = _enumerator.Current;
                    return ((K)((object)_current.Value));
                }
                _index++;
            }
        }
        return default(K);
    }
}