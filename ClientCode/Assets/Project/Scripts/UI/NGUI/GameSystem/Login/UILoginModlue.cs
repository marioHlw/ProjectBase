using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 下面这行不能删除
///<<< BEGIN WRITING YOUR CODE USING

///<<< END WRITING YOUR CODE USING
// 上面这行不能删除

public class UILoginModule : Singleton<UILoginModule>
{
    private UILoginData m_data = null;
    private UILoginProxy m_proxy = null;

    public UILoginData Data { get { return m_data; } }
    public UILoginProxy Proxy { get { return m_proxy; } }

    public UILoginModule()
    {
        m_data = new UILoginData();
        m_proxy = new UILoginProxy();
    }

    // 下面这行不能删除
    ///<<< BEGIN WRITING YOUR CODE CORE

    ///<<< END WRITING YOUR CODE CORE
    // 上面这行不能删除
}