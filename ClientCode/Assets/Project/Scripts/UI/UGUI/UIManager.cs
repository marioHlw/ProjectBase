/**************************
 * 文件名:UIManager.cs
 * 文件描述:UGUI窗口组管理类
 * 创建日期:2019/09/16
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.UGUILibrary
{
    public class UIManager : Singleton<UIManager>
    {
        public Dictionary<string, BLK_UIFormBase> cacheGroupMap = new Dictionary<string, BLK_UIFormBase>();      // 已经缓存的窗口组

        private BLK_UIGroupBase m_currentGroup = null;          // 当前窗口组
        private BLK_UIGroupBase m_nextGroup = null;             // 下一个窗口组
        private BLK_UIGroupBase m_outingGroup = null;           // 正在退出的窗口组
        private BLK_UIGroupBase m_waitEnterGroup = null;        // 等待进入窗口组

        private Dictionary<enUIFormType, Type> m_goupMap = new Dictionary<enUIFormType, Type>();
        private List<string> m_previousGroups = new List<string>();     // 返回列表

        public Canvas RootUI { get; private set; }
        public Transform RootTransform { get; private set; }
        public GameObject RootGameObject { get; private set; }

        public override void Init()
        {
            base.Init();

            m_goupMap.Add(enUIFormType.UIFormLoading, typeof(BLK_UIGroupLoading));

            GameObject _go = GameObject.Find("GameFramework");

            RootUI = _go.GetComponentInChildren<Canvas>();
            RootTransform = RootUI.transform;
            RootGameObject = RootUI.gameObject;

            Log.Info(Ctrl.LogInfos[0] + " - UI窗口管理器");
        }

        public override void UnInit()
        {
            base.UnInit();

            m_currentGroup = null;
            m_nextGroup = null;
            m_outingGroup = null;
            m_waitEnterGroup = null;

            cacheGroupMap.Clear();
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

        public void OnPreviousGroup(enUIFormType formType = enUIFormType.None)
        {
            if (formType != enUIFormType.None)
            {
                if (m_goupMap.ContainsKey(formType))
                {
                    OnOpenGroup(m_goupMap[formType]);
                }
            }
            else if (m_previousGroups.Count > 0)
            {
                string _openName = m_previousGroups[m_previousGroups.Count - 1];
                foreach (KeyValuePair<enUIFormType, Type> key in m_goupMap)
                {
                    if (_openName == key.Value.GetType().FullName)
                    {
                        OnOpenGroup(key.Value);
                    }
                }
            }
            else
            {
                // 退回到主界面
                OnOpenGroup(m_goupMap[enUIFormType.UIFormMain]);
            }
        }

        /// <summary>
        /// 打开指定窗口组
        /// </summary>
        /// <param name="formType">窗口组类型</param>
        /// <param name="playAnimation">是否播放入场动画</param>

        public void OnOpenGroup(enUIFormType formType, bool playAnimation = true)
        {
            if (m_goupMap.ContainsKey(formType))
            {
                OnOpenGroup(m_goupMap[formType], playAnimation);
            }
            else
            {
                Debug.LogError("UI系统加载失败,请在UIManager脚本Init方法中添加初始代码.");
            }
        }

        /// <summary>
        /// 关闭指定窗口组
        /// </summary>
        /// <param name="formType">窗口组类型</param>
        /// <param name="playAnimation">是否播放退场动画</param>

        public void OnCloseGroup(enUIFormType formType, bool playAnimation = true)
        {

        }

        private void OnOpenGroup(Type type, bool playAnimation = true)
        {
            BLK_UIGroupBase _groupBase = Activator.CreateInstance(type) as BLK_UIGroupBase;
            _groupBase.playAnimation = playAnimation;

            OnOpenGroup(_groupBase, playAnimation);
        }

        private void OnOpenGroup(BLK_UIGroupBase group, bool playAnimation = true)
        {
            if (group != null)
            {
                // 当前有窗口已经打开，并且需要打开的窗口是当前已经打开的窗口。
                if (m_currentGroup != null && group.GroupName == m_currentGroup.GroupName)
                {
                    return;
                }

                // 已经存在窗口组正在打开，进入等待
                if (m_nextGroup != null)
                {
                    m_waitEnterGroup = group;
                    return;
                }

                m_waitEnterGroup = null;
                m_nextGroup = group;

                // 当前已经有窗口打开了，关闭当前窗口，进入下一个窗口
                if (m_currentGroup != null)
                {
                    ExitCurrentGroup();
                }
                else if (m_nextGroup != null && m_outingGroup == null)
                {
                    EnterNextGroup(playAnimation);
                }
            }
        }

        // 退出当前窗口组
        private void ExitCurrentGroup()
        {
            if (m_currentGroup == null)
            {
                return;
            }

            // 添加到返回列表
            if (m_currentGroup.BackFlag)
            {
                m_previousGroups.Add(m_currentGroup.GroupName);
            }

            m_outingGroup = m_currentGroup;
            m_currentGroup = null;
            m_outingGroup.OnExit(ExitCurrentGroupEnd);
        }

        // 退出当前窗口组结束回调
        private void ExitCurrentGroupEnd(BLK_UIGroupBase group)
        {
            if (group == null)
            {
                return;
            }

            // 效验是不是正在退出的窗口组
            if (group == m_outingGroup)
            {
                if (m_waitEnterGroup != null && (m_nextGroup == null || m_nextGroup.GroupName != m_waitEnterGroup.GroupName))
                {
                    m_nextGroup = m_waitEnterGroup;
                    m_waitEnterGroup = null;
                }

                if (m_nextGroup != null)
                {
                    m_outingGroup = null;
                    EnterNextGroup();
                }
                else
                {
                    m_outingGroup = null;
                }
            }
        }

        // 进入下一个窗口组
        private void EnterNextGroup(bool playAnimation = true)
        {
            if (m_nextGroup == null)
            {
                return;
            }

            m_currentGroup = m_nextGroup;
            m_currentGroup.playAnimation = playAnimation;
            m_currentGroup.OnOpen(EnterNextGroupEnd);

            // 判断回退列表中是否存在当前显示场景，如果有就当前场景及后面的场景全部删除
            int _index = m_previousGroups.IndexOf(m_currentGroup.GroupName);

            if (_index != -1)
            {
                string _temp = m_previousGroups[m_previousGroups.Count - 1];
                m_previousGroups[m_previousGroups.Count - 1] = m_previousGroups[_index];
                m_previousGroups[_index] = _temp;
                // m_previousGroups.RemoveRange(_index, m_previousGroups.Count - _index);
            }
        }

        // 进入下一个窗口组结束回调
        private void EnterNextGroupEnd(BLK_UIGroupBase group)
        {
            if (group == null)
            {
                return;
            }

            // 校验是不是当前场景执行的
            if (group == m_currentGroup)
            {
                m_nextGroup = null;

                if (m_waitEnterGroup != null && group.GroupName != m_waitEnterGroup.GroupName)
                {
                    BLK_UIGroupBase _group = m_waitEnterGroup;
                    m_waitEnterGroup = null;
                    OnOpenGroup(_group);
                }
            }
            else
            {
                ExitCurrentGroup();
            }
        }
    }
}