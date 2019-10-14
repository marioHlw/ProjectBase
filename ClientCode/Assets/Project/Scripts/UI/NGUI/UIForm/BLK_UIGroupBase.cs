/**************************
 * 文件名:BLK_UIGroupBase.cs
 * 文件描述:NGUI系统窗口组，一个UI系统可以由多个窗口组成。
 * 创建日期:2019/09/16
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace zb.NGUILibrary
{
    public class BLK_UIGroupBase
    {
        protected string groupName;
        protected bool backFlag = true;                                                                         // 添加到返回列表标识
        protected List<string> uiFormPaths = new List<string>();                                                // 该UI场景所持有的窗口资源路径
        protected Dictionary<string, BLK_UIFormBase> uiFormMap = new Dictionary<string, BLK_UIFormBase>();      // 该UI场景所持有的窗口

        private int m_animationCount = 0;
        private Action<BLK_UIGroupBase> m_enterEndCallback = null;    // 入场结束回调
        private Action<BLK_UIGroupBase> m_exitEndCallback = null;     // 退场结束回调

        public string GroupName { get { return groupName; } }
        public bool BackFlag { get { return backFlag; } }

        public BLK_UIGroupBase()
        {
            OnAwake();
        }

        /// <summary>
        /// 初始化，只会调用一次。
        /// </summary>

        public virtual void OnAwake()
        {
            groupName = GetType().FullName;
        }        

        /// <summary>
        /// 加载所有窗口，创建对象
        /// </summary>

        public virtual void OnLoadForms()
        {
            int _depth = 10;
            for (int i = 0; i < uiFormPaths.Count; i++)
            {
                AddForm(uiFormPaths[i], _depth * (i + 1));
            }
        }

        /// <summary>
        /// 退出所有窗口，删除对象
        /// </summary>

        public virtual void OnExitForms()
        {
            foreach (BLK_UIFormBase temp in uiFormMap.Values)
            {
                if (temp != null)
                {
                    temp.OnRecycle();
                }
            }
        }

        /// <summary>
        /// 播放入场动画
        /// </summary>

        public virtual void OnPlayAnimationEnter(Action<BLK_UIGroupBase> enterEndCallback = null)
        {
            m_enterEndCallback = enterEndCallback;
            m_animationCount = 0;

            foreach (BLK_UIFormBase temp in uiFormMap.Values)
            {
                if (temp != null)
                {
                    temp.OnPlayAnimationEnter(OnPlayAnimationEnterEnd);
                }
            }
        }

        /// <summary>
        /// 播放退场动画
        /// </summary>

        public virtual void OnPlayAnimationExit(Action<BLK_UIGroupBase> exitEndCallback = null)
        {
            m_exitEndCallback = exitEndCallback;
            m_animationCount = uiFormMap.Count;
            foreach (BLK_UIFormBase temp in uiFormMap.Values)
            {
                if (temp != null)
                {
                    temp.OnPlayAnimationExit(OnPlayAnimationExitEnd);
                }
            }
        }

        /// <summary>
        /// 播放入场动画结束
        /// </summary>

        private void OnPlayAnimationEnterEnd(BLK_UIFormBase form)
        {
            m_animationCount++;
            if (m_animationCount >= uiFormMap.Count)
            {
                if (m_enterEndCallback != null)
                {
                    m_enterEndCallback.Invoke(this);
                    m_enterEndCallback = null;
                }
            }
        }

        /// <summary>
        /// 播放退场动画结束
        /// </summary>

        private void OnPlayAnimationExitEnd(BLK_UIFormBase form)
        {
            m_animationCount--;
            if (m_animationCount <= 0)
            {
                if (m_exitEndCallback != null)
                {
                    m_exitEndCallback.Invoke(this);
                    m_exitEndCallback = null;
                }
            }
        }

        /// <summary>
        /// 添加一个窗口
        /// </summary>
        /// <param name="path">资源路径(Resources目录下)</param>
        /// <param name="depth">UIPanel深度</param>
        /// <param name="playAnimationEntry">是否播放入场动画</param>
        /// <returns>窗口对象</returns>

        private BLK_UIFormBase AddForm(string path, int depth, bool playAnimationEntry = false)
        {
            if (!uiFormMap.ContainsKey(path))
            {
                BLK_UIFormBase _formBase = null;
                bool _isCache = true;

                if (UIManager.Instance.cacheUIFormMap.TryGetValue(path, out _formBase))
                {
                    _isCache = true;

                    _formBase.gameObject.SetActive(true);
                }
                else
                {
                    _isCache = false;

                    GameObject _go = UIResManager.Instance.OnLoad(path);
                    Vector3 _position = _go.transform.localPosition;
                    Vector3 _scale = _go.transform.localScale;
                    _go = GameObject.Instantiate(_go) as GameObject;
                    _go.transform.SetParent(UIManager.Instance.RootTransform);
                    _go.transform.localPosition = _position;
                    _go.transform.localScale = _scale;
                    _go.transform.rotation = Quaternion.identity;

                    _formBase = _go.GetComponent<BLK_UIFormBase>();

                    if (_formBase != null && _formBase.openCaches)
                    {
                        UIManager.Instance.cacheUIFormMap.Add(path, _formBase);
                    }
                    else if (_formBase == null)
                    {
                        Log.Error(_go.name + "未绑定BLK_UIFormBase组件");
                    }
                }

                if (_formBase != null)
                {
                    SetFormDepth(_formBase, depth);
                    uiFormMap.Add(path, _formBase);
                    if (!_isCache)
                    {
                        _formBase.OnInit();
                    }
                    _formBase.OnOpen();
                    if (playAnimationEntry)
                    {
                        _formBase.OnPlayAnimationEnter(null);
                    }
                }

                return _formBase;
            }
            else
            {
                BLK_UIFormBase _formBase = uiFormMap[path];
                SetFormDepth(_formBase, depth);
                uiFormMap[path].OnOpen();
                return _formBase;
            }
        }

        /// <summary>
        /// 删除一个窗口
        /// </summary>
        /// <param name="path">资源路径(Resources目录下)</param>
        /// <param name="playAnimationExit">是否播放退场动画</param>

        private void RemoveForm(string path, bool playAnimationExit = false)
        {
            if (uiFormMap.ContainsKey(path))
            {
                if (playAnimationExit)
                {
                    uiFormMap[path].OnPlayAnimationExit(null);
                }
                else
                {
                    uiFormMap[path].OnClose();
                }

                uiFormMap.Remove(path);
                uiFormPaths.Remove(path);
            }
        }

        private void SetFormDepth(BLK_UIFormBase form, int depth)
        {
            UIPanel _panel = form.GetComponent<UIPanel>();

            if (_panel != null)
            {
                _panel.depth = depth;
            }
        }
    }
}