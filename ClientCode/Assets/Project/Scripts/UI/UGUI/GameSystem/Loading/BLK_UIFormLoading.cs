using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 下面这行不能删除
///<<< BEGIN WRITING YOUR CODE USING

///<<< END WRITING YOUR CODE USING
// 上面这行不能删除

namespace zb.UGUILibrary
{
    public class BLK_UIFormLoading : BLK_UIFormBase
    {
        private Text u_txtProgress;
        private Text u_txtInfo;
        private Slider u_sldSlider;

        private void OnFindChilds()
        {
            u_txtProgress = transform.Find("u_sldSlider/u_txtProgress").GetComponent<Text>();
            u_txtInfo = transform.Find("u_sldSlider/u_txtInfo").GetComponent<Text>();
            u_sldSlider = transform.Find("u_sldSlider").GetComponent<Slider>();
        }

        // 下面这行不能删除
        ///<<< BEGIN WRITING YOUR CODE CORE

        public override void OnInit()
        {
            base.OnInit();

            OnFindChilds();
        }

        public override void OnOpen()
        {
            base.OnOpen();

            u_sldSlider.value = 0;
            u_txtInfo.text = "";
        }

        public override void OnClose()
        {
            base.OnClose();

            Ctrl.eventRouter.RemoveEventHandler<float, string>(UILoadingModule.MS_UPDATE_PROGRESSVALUE, MS_UpdateProgressValue);
        }

        public override void OnHide()
        {
            base.OnHide();

            Ctrl.eventRouter.RemoveEventHandler<float, string>(UILoadingModule.MS_UPDATE_PROGRESSVALUE, MS_UpdateProgressValue);
        }

        public override void OnActivate()
        {
            base.OnActivate();

            Ctrl.eventRouter.AddEventHandler<float, string>(UILoadingModule.MS_UPDATE_PROGRESSVALUE, MS_UpdateProgressValue);
        }

        private void MS_UpdateProgressValue(float val, string info)
        {
            if (val > 100) val = 100;

            u_sldSlider.value = val / 100f;
            u_txtInfo.text = info;
            u_txtProgress.text = (int)val + "%";
        }

        ///<<< END WRITING YOUR CODE CORE
        // 上面这行不能删除
    }
}