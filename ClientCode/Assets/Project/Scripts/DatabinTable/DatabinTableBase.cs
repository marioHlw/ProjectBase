/**************************
 * 文件名:DatabinTableBase.cs
 * 文件描述:数据表基类
 * 创建日期:2019/09/03
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DatabinTableBase
{
    public static readonly string DatabinTablePath = "Databin/TableRes/";       // 表格数据路径

    protected string dataName;
    protected string keyName;
    protected bool loaded;                      // 已经加载
    protected bool allowUnLoad = true;          // 允许卸载
    protected Type valueType;                   // 值类型
    protected bool simple;

    protected Dictionary<long, object> dataMap = new Dictionary<long, object>();

    public string Name { get { return dataName; } }
    public int Count { get { Reload(); return dataMap.Count; } }
    public bool Loaded { get { return loaded; } }

    public DatabinTableBase(Type inValueType)
    {
        valueType = inValueType;
    }

    /// <summary>
    /// 设置 - 允许卸载
    /// </summary>

    public void SetAllowUnLoad(bool allowUnLoad)
    {
        this.allowUnLoad = allowUnLoad;
    }

    public Dictionary<long, object>.Enumerator GetEnumerator()
    {
        return dataMap.GetEnumerator();
    }

    /// <summary>
    /// 加载表格数据
    /// </summary>

    public virtual void LoadDatabinTableData(byte[] rawData, Type inValueType)
    {

    }

    /// <summary>
    /// 卸载
    /// </summary>

    public void Unload()
    {
        if (!allowUnLoad)
        {
            return;
        }

        loaded = false;
        simple = false;
        dataMap.Clear();
    }

    /// <summary>
    /// 重新载入
    /// </summary>

    public void Reload()
    {
        if (loaded)
        {
            return;
        }

        LoadResource();
    }

    protected void LoadResource()
    {
        if (Ctrl.resourceComponent.dataTableResourceLoadType == enResourceLoadType.LoadFromResources)
        {
            // 使用异步
            if (Ctrl.resourceComponent.dataTableUseAsync)
            {
                dataName = dataName.Substring(0, dataName.IndexOf('.'));
                string _fullPath = DatabinTableBase.DatabinTablePath + dataName;
                Ctrl.resourceLoader.OnLoadAssetAsync(_fullPath, typeof(TextAsset), false, enResourceLoadType.LoadFromResources, Ctrl.resourceComponent.dataTableResourceLoadPathType, ReadBytesCompleteCallback);
            }
            else
            {
                dataName = dataName.Substring(0, dataName.IndexOf('.'));
                string _fullPath = DatabinTableBase.DatabinTablePath + dataName;
                UnityEngine.Object _textAsset = Ctrl.resourceLoader.OnLoadAsset(_fullPath, typeof(TextAsset), false, enResourceLoadType.LoadFromResources);
                if (_textAsset != null)
                {
                    ReadBytesCompleteCallback((_textAsset as TextAsset).bytes);
                }
            }
        }

        if (Ctrl.resourceComponent.dataTableResourceLoadType == enResourceLoadType.LoadFromAssetBundle)
        {
            // 使用异步
            if (Ctrl.resourceComponent.dataTableUseAsync)
            {
                string _fullPath = "/" + DatabinTableBase.DatabinTablePath + dataName;
                Ctrl.resourceLoader.OnLoadAssetAsync(_fullPath, typeof(TextAsset), false, Ctrl.resourceComponent.dataTableResourceLoadType, Ctrl.resourceComponent.dataTableResourceLoadPathType, ReadBytesCompleteCallback, ReadBytesUpdateCallback, ReadBytesErrorCallback);
            }
            else
            {
                string _fullPath = DatabinTableBase.DatabinTablePath + dataName;
                UnityEngine.Object _textAsset = Ctrl.resourceLoader.OnLoadAsset(_fullPath, typeof(TextAsset), false, Ctrl.resourceComponent.dataTableResourceLoadType, Ctrl.resourceComponent.dataTableResourceLoadPathType);
                if (_textAsset != null)
                {
                    ReadBytesCompleteCallback((_textAsset as TextAsset).bytes);
                }
            }
        }

        if (Ctrl.resourceComponent.dataTableResourceLoadType == enResourceLoadType.LoadFromBytes)
        {
            // 使用异步
            if (Ctrl.resourceComponent.dataTableUseAsync)
            {
                string _fullPath = "/" + DatabinTableBase.DatabinTablePath + dataName;
                Ctrl.resourceLoader.OnReadBytesAsync(_fullPath, Ctrl.resourceComponent.dataTableResourceLoadPathType, ReadBytesCompleteCallback, ReadBytesUpdateCallback, ReadBytesErrorCallback);
            }
            else
            {
                string _fullPath = DatabinTableBase.DatabinTablePath + dataName;
                byte[] _bytes = Ctrl.resourceLoader.OnReadBytes(_fullPath, Ctrl.resourceComponent.dataTableResourceLoadPathType);
                ReadBytesCompleteCallback(_bytes);
            }
        }
    }

    private void ReadBytesCompleteCallback(byte[] bytes)
    {
        OnRecordLoaded(ref bytes);
    }

    private void ReadBytesCompleteCallback(UnityEngine.Object asset)
    {
        byte[] bytes = (asset as TextAsset).bytes;
        OnRecordLoaded(ref bytes);
    }

    private void ReadBytesUpdateCallback(float progress)
    {
        Log.Info(string.Format("表格读取{0}进度：{1}", dataName, progress));
    }

    private void ReadBytesErrorCallback(enLoadResourceStatus status)
    {
        Log.Error(string.Format("表格读取{0}读取错误：{1}", dataName, Language.GetLanguageLoadResourceStatus(status)));
    }

    protected void OnRecordLoaded(ref byte[] rawData)
    {
        LoadDatabinTableData(rawData, valueType);
        loaded = true;
    }

    protected object GetDataByKeyInner(long key)
    {
        object _result;
        if (loaded && dataMap.TryGetValue(key, out _result))
        {
            return _result;
        }
        return null;
    }

    protected long GetDataByKeyInner(object data, Type inValueType)
    {
        Type _type = data.GetType();
        long _result;
        FieldInfo _fieldInfo = _type.GetField(keyName);
        object _val = _fieldInfo.GetValue(data);

        try
        {
            _result = ((_val == null) ? 0L : Convert.ToInt64(_val));
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError("Exception in Databin Get Key :" + ex);
#endif
            _result = 0L;
        }

        return _result;
    }
}