using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zb.NGUILibrary;

// 下面这行不能删除
///<<< BEGIN WRITING YOUR CODE USING

///<<< END WRITING YOUR CODE USING
// 上面这行不能删除

public class BLK_UIFormLoading : BLK_UIFormBase
{
    private UISlider u_sdrLoad;
    private UILabel u_txtInfo;
    private UILabel u_txtProgress;
    private GameObject u_objWindow;

    private void OnFindChilds()
    {
        u_sdrLoad = transform.Find("u_objWindow/u_sdrLoad").GetComponent<UISlider>();
        u_txtInfo = transform.Find("u_objWindow/info/u_txtInfo").GetComponent<UILabel>();
        u_txtProgress = transform.Find("u_objWindow/u_txtProgress").GetComponent<UILabel>();
        u_objWindow = transform.Find("u_objWindow").gameObject;
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

        UIReset();

        Ctrl.eventRouter.AddEventHandler<float,string>(LoadingModule.MS_UPDATE_PROGRESSVALUE, MS_UpdateProgressValue);
    }

    public override void OnClose()
    {
        base.OnClose();

        Ctrl.eventRouter.RemoveEventHandler<float, string>(LoadingModule.MS_UPDATE_PROGRESSVALUE, MS_UpdateProgressValue);
    }

    private void UIReset()
    {
        u_sdrLoad.value = 0;
        u_txtInfo.text = "";
        u_txtProgress.text = "";
    }

    private void MS_UpdateProgressValue(float val, string info)
    {
        if (val > 100f) { val = 1; }

        u_sdrLoad.value = val / 100f;
        u_txtProgress.text = val + "%";
        u_txtInfo.text = info;
    }

    ///<<< END WRITING YOUR CODE CORE
    // 上面这行不能删除
}