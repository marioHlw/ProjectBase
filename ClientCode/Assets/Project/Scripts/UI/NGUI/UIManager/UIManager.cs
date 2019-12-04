/**************************
 * 文件名:UIManager.cs
 * 文件描述:NGUI管理类
 * 创建日期:2019/08/20
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace zb.NGUILibrary
{
    public class UIManager : Singleton<UIManager>
    {
        public Dictionary<string, BLK_UIFormBase> cacheUIFormMap = new Dictionary<string, BLK_UIFormBase>();      // 已经缓存的窗口

        private BLK_UIGroupBase m_currentUIGroup = null;        // 当前
        private BLK_UIGroupBase m_nextUIGroup = null;           // 下一个
        private BLK_UIGroupBase m_outingUIGroup = null;         // 正在退出的
        private BLK_UIGroupBase m_waitEnterUIGroup = null;      // 等待进入

        private Dictionary<enUIFormType, Type> m_uigoupMap = new Dictionary<enUIFormType, Type>();
        private List<string> m_previousGroups = new List<string>();     // 返回列表

        public UIRoot RootUI { get; private set; }
        public Transform RootTransform { get; private set; }
        public GameObject RootGameObject { get; private set; }

        public override void Init()
        {
            base.Init();

            GameObject _go = GameObject.Find("GameFramework");

            RootUI = _go.GetComponentInChildren<UIRoot>();
            RootTransform = RootUI.transform;
            RootGameObject = RootUI.gameObject;

            Log.Info(Ctrl.LogInfos[0] + " - UI窗口管理器");
        }

        public override void UnInit()
        {
            base.UnInit();

            m_currentUIGroup = null;
            m_nextUIGroup = null;
            m_outingUIGroup = null;
            m_waitEnterUIGroup = null;

            cacheUIFormMap.Clear();
            m_previousGroups.Clear();

            Log.Info(Ctrl.LogInfos[1] + " - UI窗口管理器");
        }

        /// <summary>
        /// 清空历史记录
        /// </summary>

        public void OnClearPreviousGroups()
        {
            m_previousGroups.Clear();
        }

        /// <summary>
        /// 返回上一个窗口
        /// </summary>
        /// <param name="formType">退回到指定窗口</param>

        public void OnPreviousUIGroup(enUIFormType formType = enUIFormType.None)
        {
            if (formType != enUIFormType.None)
            {
                if (m_uigoupMap.ContainsKey(formType))
                {
                    OnOpenUIGroup(m_uigoupMap[formType]);
                }
            }
            else if (m_previousGroups.Count > 0)
            {
                string _openName = m_previousGroups[m_previousGroups.Count - 1];
                foreach (KeyValuePair<enUIFormType, Type> key in m_uigoupMap)
                {
                    if (_openName == key.Value.GetType().FullName)
                    {
                        OnOpenUIGroup(key.Value);
                    }
                }
            }
            else
            {
                // 退回到主界面
                OnOpenUIGroup(m_uigoupMap[enUIFormType.UIFormMain]);
            }
        }

        public void OnOpenUIGroup(enUIFormType formType, bool playAnimation = true)
        {
            if (m_uigoupMap.ContainsKey(formType))
            {
                OnOpenUIGroup(m_uigoupMap[formType], playAnimation);
            }
            else
            {
                Debug.LogError("UI系统加载失败,请在UIManager脚本Init方法中添加初始代码.");
            }
        }

        public void OnOpenUIGroup(Type type, bool playAnimation = true)
        {
            BLK_UIGroupBase _groupBase = Activator.CreateInstance(type) as BLK_UIGroupBase;
            OnOpenUIGroup(_groupBase, playAnimation);
        }

        public void OnOpenUIGroup(BLK_UIGroupBase group, bool playAnimation = true)
        {
            if (group != null)
            {
                // 当前有窗口已经打开，并且需要打开的窗口时当前已经打开的窗口。
                if (m_currentUIGroup != null && group.GroupName == m_currentUIGroup.GroupName)
                {
                    return;
                }

                // 已经存在窗口组正在打开，进入等待
                if (m_nextUIGroup != null)
                {
                    m_waitEnterUIGroup = group;
                    return;
                }

                m_waitEnterUIGroup = null;
                m_nextUIGroup = group;

                if (m_nextUIGroup == null)
                {
                    OnClearPreviousGroups();
                }

                // 当前已经有窗口打开了，关闭当前窗口，进入下一个窗口
                if (m_currentUIGroup != null)
                {
                    ExitCurrentUIGroup();
                }
                else if (m_nextUIGroup != null && m_outingUIGroup == null)
                {
                    EnterNextUIGroup(playAnimation);
                }
            }
        }

        public void OnCloseUIGroup(enUIFormType formType, bool playAnimation = true)
        {

        }

        private void ExitCurrentUIGroup()
        {
            if (m_currentUIGroup == null)
            {
                return;
            }

            // 添加到返回列表
            if (m_currentUIGroup.BackFlag)
            {
                m_previousGroups.Add(m_currentUIGroup.GroupName);
            }

            m_outingUIGroup = m_currentUIGroup;
            m_currentUIGroup = null;
            m_outingUIGroup.OnPlayAnimationExit(ExitCurrentUIGroupEnd);
        }

        private void ExitCurrentUIGroupEnd(BLK_UIGroupBase group)
        {
            if (group == null)
            {
                return;
            }

            // 效验是不是正在退出的窗口组
            if (group == m_outingUIGroup)
            {
                if (m_waitEnterUIGroup != null && (m_nextUIGroup == null || m_nextUIGroup.GroupName != m_waitEnterUIGroup.GroupName))
                {
                    m_nextUIGroup = m_waitEnterUIGroup;
                    m_waitEnterUIGroup = null;
                }

                if (m_nextUIGroup != null)
                {
                    m_outingUIGroup.OnExitForms();
                    m_outingUIGroup = null;
                    EnterNextUIGroup();
                }
                else
                {
                    m_outingUIGroup.OnExitForms();
                    m_outingUIGroup = null;
                    OnClearPreviousGroups();
                }
            }
        }

        private void EnterNextUIGroup(bool playAnimation = true)
        {
            if (m_nextUIGroup == null)
            {
                return;
            }

            m_currentUIGroup = m_nextUIGroup;
            m_currentUIGroup.OnLoadForms();

            if (playAnimation)
            {
                m_currentUIGroup.OnPlayAnimationEnter(EnterNextUIGroupEnd);
            }
            else
            {
                EnterNextUIGroupEnd(m_currentUIGroup);
            }

            // 判断回退列表中是否存在当前显示场景，如果有就当前场景及后面的场景全部删除
            int _index = m_previousGroups.IndexOf(m_currentUIGroup.GroupName);

            if (_index != -1)
            {
                m_previousGroups.RemoveRange(_index, m_previousGroups.Count - _index);
            }
        }

        private void EnterNextUIGroupEnd(BLK_UIGroupBase group)
        {
            if (group == null)
            {
                return;
            }

            // 校验是不是当前场景执行的
            if (group == m_currentUIGroup)
            {
                m_nextUIGroup = null;

                if (m_waitEnterUIGroup != null && group.GroupName != m_waitEnterUIGroup.GroupName)
                {
                    BLK_UIGroupBase _group = m_waitEnterUIGroup;
                    m_waitEnterUIGroup = null;
                    OnOpenUIGroup(_group);
                }
                else
                {
                    
                }
            }
            else
            {
                ExitCurrentUIGroup();
            }
        }
    }
}