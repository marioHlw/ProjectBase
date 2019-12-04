/**************************
 * 文件名:UIRedTipManager.cs
 * 文件描述:UI红点提示管理，用来管理游戏所有UI上的红点提示。
 * 创建日期:2019/07/25
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Common
{
    public class UIRedTipManager : Singleton<UIRedTipManager>
    {
        /// <summary>
        /// 红点提示类型树
        /// </summary>

        private class RedTipTypeTree
        {
            static Dictionary<enRedTipType, RedTipTypeTree> treeMap = new Dictionary<enRedTipType, RedTipTypeTree>();
            static List<enRedTipType> withIds = new List<enRedTipType>();

            private enRedTipType parentType;

            private RedTipTypeTree(enRedTipType parentType)
            {
                this.parentType = parentType;
            }

            public static void SetParent(enRedTipType tipType, enRedTipType partentTipType, bool withId = false)
            {
                if (tipType == enRedTipType.None)
                {
                    return;
                }

                if (treeMap.ContainsKey(tipType) && treeMap[tipType].parentType == partentTipType)
                {
                    return;
                }

                if (treeMap.ContainsKey(tipType))
                {
                    treeMap[tipType].parentType = partentTipType;
                }
                else
                {
                    treeMap.Add(tipType, new RedTipTypeTree(partentTipType));
                }

                if (withId)
                {
                    withIds.Add(tipType);
                }
            }

            public static enRedTipType GetParent(enRedTipType tipType)
            {
                if (treeMap.ContainsKey(tipType))
                {
                    return treeMap[tipType].parentType;
                }
                else
                {
                    return enRedTipType.None;
                }
            }

            public static List<enRedTipType> GetChilds(enRedTipType parentType)
            {
                List<enRedTipType> _childs = new List<enRedTipType>();

                foreach (KeyValuePair<enRedTipType, RedTipTypeTree> item in treeMap)
                {
                    if (item.Value.parentType == parentType)
                    {
                        _childs.Add(item.Key);
                    }
                }

                return _childs;
            }

            /// <summary>
            /// 通知父节点是否绑定ID
            /// </summary>

            public static bool IsWithId(enRedTipType tipType)
            {
                return withIds.Contains(tipType);
            }
        }

        private Dictionary<enRedTipType, List<UIRedTipComponent>> m_redTipComponentMap = new Dictionary<enRedTipType, List<UIRedTipComponent>>();               // 红点类型对应的所有UI组件
        private Dictionary<enRedTipType, List<int>> m_redTipDataMap = new Dictionary<enRedTipType, List<int>>();                                                // 红点类型对应所有UI组件的唯一ID

        /// <summary>
        /// 初始化 - 游戏启动时调用
        /// </summary>

        public override void Init()
        {
            base.Init();

            Log.Info(Ctrl.LogInfos[0] + " - UI红点提示管理器");
        }

        /// <summary>
        /// 销毁 - 游戏退出时调用
        /// </summary>

        public override void UnInit()
        {
            base.UnInit();

            m_redTipComponentMap.Clear();
            m_redTipDataMap.Clear();

            Log.Info(Ctrl.LogInfos[1] + " - UI红点提示管理器");
        }

        /// <summary>
        /// 添加 - 红点组件
        /// </summary>

        public void AddRedTip(UIRedTipComponent component)
        {
            if (component == null)
            {
                return;
            }

            if (m_redTipComponentMap.ContainsKey(component.RedTipType))
            {
                if (!m_redTipComponentMap[component.RedTipType].Contains(component))
                {
                    m_redTipComponentMap[component.RedTipType].Add(component);
                }
            }
            else
            {
                m_redTipComponentMap.Add(component.RedTipType, new List<UIRedTipComponent>());
                m_redTipComponentMap[component.RedTipType].Add(component);
            }


        }

        /// <summary>
        /// 删除 - 红点组件
        /// </summary>

        public void RemoveRedTip(UIRedTipComponent component)
        {
            if (component == null)
            {
                return;
            }

            if (m_redTipComponentMap.ContainsKey(component.RedTipType))
            {
                if (m_redTipComponentMap[component.RedTipType].Contains(component))
                {
                    m_redTipComponentMap[component.RedTipType].Remove(component);
                }
            }
        }

        /// <summary>
        /// 通知 - 发送红点状态
        /// soleID:唯一ID，当一个红点状态由多个子红点状态控制时，如果子类红点中存在这个ID的红点组件，那么这个红点显示。反之这个红点不显示(其他的子红点不做考虑)。
        /// </summary>

        public void OnNotice(enRedTipType tipType, bool active, int soleID = 0)
        {
            if (soleID < 0)
            {
                soleID = 0;
            }

            if (!active)
            {
                List<enRedTipType> _childs = RedTipTypeTree.GetChilds(tipType);

                if (_childs != null)
                {
                    for (int i = 0, length = _childs.Count; i < length; i++)
                    {
                        if (soleID == 0 && m_redTipDataMap.ContainsKey(_childs[i]) && m_redTipDataMap[_childs[i]].Count > 0)
                        {
                            active = true;
                            break;
                        }
                        else if (m_redTipDataMap.ContainsKey(_childs[i]) && m_redTipDataMap[_childs[i]].Count > 0)
                        {
                            for (int j = 0, count = m_redTipDataMap[_childs[i]].Count; j < count; j++)
                            {
                                if (m_redTipDataMap[_childs[i]][j] == soleID)
                                {
                                    active = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (m_redTipDataMap.ContainsKey(tipType))
            {
                if (m_redTipDataMap[tipType].Contains(soleID))
                {
                    if (!active)
                    {
                        m_redTipDataMap[tipType].Remove(soleID);
                    }
                }
                else if (active)
                {
                    m_redTipDataMap[tipType].Add(soleID);
                }
            }
            else if (active)
            {
                m_redTipDataMap.Add(tipType, new List<int>() { soleID });
            }

            // 应用在当前UI上
            if (m_redTipComponentMap.ContainsKey(tipType))
            {
                for (int i = 0, length = m_redTipComponentMap[tipType].Count; i < length; i++)
                {
                    if (m_redTipComponentMap[tipType][i].soleID == soleID)
                    {
                        if (m_redTipComponentMap[tipType][i] != null)
                        {
                            m_redTipComponentMap[tipType][i].SetActive(active);
                        }
                    }
                }
            }

            // 通知父节点
            if (RedTipTypeTree.GetParent(tipType) != enRedTipType.None)
            {
                OnNotice(RedTipTypeTree.GetParent(tipType), false, RedTipTypeTree.IsWithId(tipType) ? soleID : 0);
            }
        }

        /// <summary>
        /// 判断 - 是否激活
        /// </summary>

        public bool IsActive(enRedTipType tipType, int soleID)
        {
            return m_redTipDataMap.ContainsKey(tipType) && m_redTipDataMap[tipType].Contains(soleID);
        }

        /// <summary>
        /// 判断 - 是否激活
        /// </summary>

        public bool IsActive(enRedTipType tipType)
        {
            return m_redTipDataMap.ContainsKey(tipType) && m_redTipDataMap[tipType].Count > 0;
        }
    }
}