/**************************
 * 文件名:BLK_UIGroupBase.cs
 * 文件描述:UGUI系统窗口组，一个UI系统可以由多个窗口组成。
 * 创建日期:2019/09/16
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace zb.UGUILibrary
{
    public class BLK_UIGroupBase
    {
        public bool playAnimation = true;

        protected string groupName;
        protected bool backFlag = true;                                                                         // 添加到返回列表标识

        protected List<string> formPaths = new List<string>();                                                  // 该窗口组所持有的窗口资源路径
        protected Dictionary<string, BLK_UIFormBase> formMap = new Dictionary<string, BLK_UIFormBase>();        // 该窗口组所持有的窗口
        protected Dictionary<string, bool> formStateMap = new Dictionary<string, bool>();                       // 该窗口组所持有的窗口状态

        public Action<BLK_UIGroupBase> enterCallback = null;        // 进入完成回调
        public Action<BLK_UIGroupBase> exitCallback = null;         // 退出完成回调

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

        public virtual void OnOpen(Action<BLK_UIGroupBase> action)
        {
            enterCallback = action;

            int _depth = 10;
            for (int i = 0; i < formPaths.Count; i++)
            {
                formStateMap[formPaths[i]] = false;
            }

            for (int i = 0; i < formPaths.Count; i++)
            {
                AddForm(formPaths[i], _depth * (i + 1));
            }
        }

        public virtual void OnExit(Action<BLK_UIGroupBase> action)
        {
            exitCallback = action;

            foreach (BLK_UIFormBase temp in formMap.Values)
            {
                if (temp != null)
                {
                    temp.exitCallback = CloseFormCallback;
                    temp.OnClose();
                }
            }
        }
   
        /// <summary>
        /// 添加一个窗口
        /// </summary>
        /// <param name="path">资源路径(Resources目录下)</param>
        /// <param name="depth">UIPanel深度</param>

        private void AddForm(string path, int depth)
        {
            if (!formMap.ContainsKey(path))
            {
                BLK_UIFormBase _formBase = null;

                if (UIManager.Instance.cacheGroupMap.TryGetValue(path, out _formBase))
                {
                    formMap.Add(path, _formBase);

                    _formBase.gameObject.SetActive(true);
                    _formBase.enterCallback = OpenFormCallback;
                    _formBase.OnOpen();
                }
                else
                {
                    LoadAssetCallback(Resources.Load(path) as GameObject, path);
                }
            }
            else
            {
                BLK_UIFormBase _formBase = formMap[path];

                _formBase.enterCallback = OpenFormCallback;
                _formBase.OnOpen();
            }
        }

        private void LoadAssetCallback(GameObject obj, string path)
        {
            BLK_UIFormBase _formBase = null;

            GameObject _go = GameObject.Instantiate(obj);

            _formBase = _go.GetComponent<BLK_UIFormBase>();
            _formBase.Name = path;
            _formBase.PlayAnimation = playAnimation;
            _formBase.OnInit();

            _go.transform.SetParent(UIManager.Instance.RootTransform);
            _go.transform.localPosition = Vector3.zero;
            _go.transform.localScale = Vector3.one;
            _go.transform.rotation = Quaternion.identity;
            _go.SetActive(true);

            RectTransform _rectTransform = _go.transform as RectTransform;
            _rectTransform.sizeDelta = Vector2.zero;

            if (_formBase != null && _formBase.openCaches)
            {
                UIManager.Instance.cacheGroupMap.Add(path, _formBase);
            }
            else if (_formBase == null)
            {
                Log.Error(_go.name + "未绑定BLK_UIFormBase组件");
            }

            if (_formBase != null)
            {
                formMap.Add(path, _formBase);

                _formBase.enterCallback = OpenFormCallback;
                _formBase.OnOpen();
            }
        }

        /// <summary>
        /// 窗口打开完成回调
        /// </summary>

        private void OpenFormCallback(BLK_UIFormBase formBase)
        {
            formStateMap[formBase.Name] = true;

            bool _isFinish = true;
            foreach (KeyValuePair<string, bool> temp in formStateMap)
            {
                if (!temp.Value)
                {
                    _isFinish = false;
                }
            }
            if (_isFinish)
            {
                if (enterCallback != null)
                {
                    enterCallback(this);
                }
            }
        }

        /// <summary>
        /// 窗口关闭完成回调
        /// </summary>

        private void CloseFormCallback(BLK_UIFormBase formBase)
        {
            formStateMap[formBase.Name] = false;

            bool _isFinish = true;
            foreach (KeyValuePair<string, bool> temp in formStateMap)
            {
                if (temp.Value)
                {
                    _isFinish = false;
                }
            }
            if (_isFinish)
            {
                if (exitCallback != null)
                {
                    exitCallback(this);
                }
            }
        }
    }
}