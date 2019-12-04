using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 下面这行不能删除
///<<< BEGIN WRITING YOUR CODE USING

///<<< END WRITING YOUR CODE USING
// 上面这行不能删除

namespace zb.UGUILibrary
{
    public class UILoadingModule : Singleton<UILoadingModule>
    {
        private UILoadingData m_data = null;
        private UILoadingProxy m_proxy = null;

        public UILoadingData Data { get { return m_data; } }
        public UILoadingProxy Proxy { get { return m_proxy; } }

        public UILoadingModule()
        {
            m_data = new UILoadingData();
            m_proxy = new UILoadingProxy();
        }

        // 下面这行不能删除
        ///<<< BEGIN WRITING YOUR CODE CORE

        public static string MS_UPDATE_PROGRESSVALUE = "MS_UPDATE_PROGRESSVALUE";               // 消息 - 更新进度条值

        ///<<< END WRITING YOUR CODE CORE
        // 上面这行不能删除
    }
}