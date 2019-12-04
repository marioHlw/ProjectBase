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
    public static DatabinTable<goods_info_ARRAY, goods_info> TableGoodsInfo = null;
    public static DatabinTable<systemaddress_ARRAY, systemaddress> TableSystemAddress = null;

    private int m_tableCount = 3;

    public override void Init()
    {
        base.Init();
    }

    public override void UnInit()
    {
        base.UnInit();
    }

    public void LoadFinish()
    {
        m_tableCount--;

        // 所有表格数据加载完成
        if (m_tableCount == 0)
        {
            Ctrl.eventRouter.BroadCastEvent(EventID.MS_TABLE_LOADFINISH);
        }
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

                    TableGoodsInfo = new DatabinTable<goods_info_ARRAY, goods_info>("goods_info.bytes", "ID");

                    TableSystemAddress = new DatabinTable<systemaddress_ARRAY, systemaddress>("systemaddress.bytes", "ID");

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