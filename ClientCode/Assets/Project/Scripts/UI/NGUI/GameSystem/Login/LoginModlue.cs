using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 下面这行不能删除
///<<< BEGIN WRITING YOUR CODE USING

///<<< END WRITING YOUR CODE USING
// 上面这行不能删除

public class LoginModule : Singleton<LoginModule>
{
    private LoginData m_data = null;
    private LoginProxy m_proxy = null;

    public LoginData Data { get { return m_data; } }
    public LoginProxy Proxy { get { return m_proxy; } }

    public LoginModule()
    {
        m_data = new LoginData();
        m_proxy = new LoginProxy();
    }

    // 下面这行不能删除
    ///<<< BEGIN WRITING YOUR CODE CORE

    ///<<< END WRITING YOUR CODE CORE
    // 上面这行不能删除
}