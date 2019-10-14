using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 下面这行不能删除
///<<< BEGIN WRITING YOUR CODE USING

///<<< END WRITING YOUR CODE USING
// 上面这行不能删除

public class LoadingModule : Singleton<LoadingModule>
{
    private LoadingData m_data = null;
    private LoadingProxy m_proxy = null;

    public LoadingData Data { get { return m_data; } }
    public LoadingProxy Proxy { get { return m_proxy; } }

    public LoadingModule()
    {
        m_data = new LoadingData();
        m_proxy = new LoadingProxy();
    }

    // 下面这行不能删除
    ///<<< BEGIN WRITING YOUR CODE CORE

    public static string MS_UPDATE_PROGRESSVALUE = "MS_UPDATE_PROGRESSVALUE";               // 消息 - 更新进度条值

    ///<<< END WRITING YOUR CODE CORE
    // 上面这行不能删除
}