using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 下面这行不能删除
///<<< BEGIN WRITING YOUR CODE USING

///<<< END WRITING YOUR CODE USING
// 上面这行不能删除

namespace zb.NGUILibrary
{
    public class UITestModule : Singleton<UITestModule>
    {
        private UITestData m_data = null;
        private UITestProxy m_proxy = null;

        public UITestData Data { get { return m_data; } }
        public UITestProxy Proxy { get { return m_proxy; } }

        public UITestModule()
        {
            m_data = new UITestData();
            m_proxy = new UITestProxy();
        }

        // 下面这行不能删除
        ///<<< BEGIN WRITING YOUR CODE CORE

        ///<<< END WRITING YOUR CODE CORE
        // 上面这行不能删除
    }
}