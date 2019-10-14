using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 下面这行不能删除
///<<< BEGIN WRITING YOUR CODE USING

///<<< END WRITING YOUR CODE USING
// 上面这行不能删除

public class UIMainModule : Singleton<UIMainModule>
{
    private UIMainData m_data = null;
    private UIMainProxy m_proxy = null;

    public UIMainData Data { get { return m_data; } }
    public UIMainProxy Proxy { get { return m_proxy; } }

    public UIMainModule()
    {
        m_data = new UIMainData();
        m_proxy = new UIMainProxy();
    }

    // 下面这行不能删除
    ///<<< BEGIN WRITING YOUR CODE CORE

    ///<<< END WRITING YOUR CODE CORE
    // 上面这行不能删除
}