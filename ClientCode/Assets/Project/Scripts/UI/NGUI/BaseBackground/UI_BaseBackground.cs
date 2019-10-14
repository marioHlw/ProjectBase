/**************************
 * 文件名:UI_BaseBackground.cs
 * 文件描述:底图背景父类
 * 创建日期:2019/07/27
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    public class UI_BaseBackground : MonoBehaviour
    {
        public UISprite u_iconBg;
        public UILabel u_txtTilte;
        public UIButton u_btnClose;

        private void Awake()
        {
            EventDelegate.Add(u_btnClose.onClick, OnClickBtnClose);
        }

        private void OnDestroy()
        {
            EventDelegate.Remove(u_btnClose.onClick, OnClickBtnClose);
        }

        public virtual void OnInit()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnClickBtnClose()
        {

        }
    }
}