/**************************
 * 文件名:DatabinTableManager.cs
 * 文件描述:数据表管理类
 * 创建日期:2019/09/03
 * 作者:ZB
 ***************************/




using System;
using System.Collections;
using System.Collections.Generic;
using TableProto;
using UnityEngine;

public class DatabinTableManager : Singleton<DatabinTableManager>
{
    public static DatabinTable<config_info_ARRAY, config_info> TableConfigInfo = null;

    public override void Init()
    {
        base.Init();
    }

    public override void UnInit()
    {
        base.UnInit();
    }

    public IEnumerator LoadDatabinTable()
    {
        yield return new LoadDatabinTableIterator();
    }

    private sealed class LoadDatabinTableIterator : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        object IEnumerator<object>.Current { get { return current; } }
        object IEnumerator.Current { get { return current; } }

        public void Dispose()
        {
            PC = -1;
        }

        public void Reset()
        {

        }

        public bool MoveNext()
        {
            int _num = PC;

            PC = -1;

            switch (_num)
            {
                case 0:
                    TableConfigInfo = new DatabinTable<config_info_ARRAY, config_info>("config_info.bytes", "ID");
                    Ctrl.eventRouter.BroadCastEvent<float, string>(LoadingModule.MS_UPDATE_PROGRESSVALUE, 10, "加载中...");
                    current = null;
                    PC = 1;
                    return true;
                case 1:
                    current = null;
                    PC = 2;
                    return true;
                case 2:
                    PC = -1;
                    break;
            }
            return false;
        }
    }
}