using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zb.NGUILibrary;

// 下面这行不能删除
///<<< BEGIN WRITING YOUR CODE USING

///<<< END WRITING YOUR CODE USING
// 上面这行不能删除

public class BLK_UIFormLogin : BLK_UIFormBase
{
    private UIButton u_btnStartGame;


    private void OnFindChilds()
    {
        u_btnStartGame = transform.Find("bottonWindow/btnStartGame/u_btnStartGame").GetComponent<UIButton>();
    }

    // 下面这行不能删除
    ///<<< BEGIN WRITING YOUR CODE CORE

    public override void OnInit()
    {
        base.OnInit();

        OnFindChilds();

        EventDelegate.Add(u_btnStartGame.onClick, OnClickBtnStartGame);
    }

    public void OnClickBtnStartGame()
    {
        Ctrl.uiManager.OnOpenUIGroup(enUIFormType.UIFormLoading);
        Ctrl.eventRouter.BroadCastEvent<float, string>(LoadingModule.MS_UPDATE_PROGRESSVALUE, 0, "载入数据中...");
    }

    ///<<< END WRITING YOUR CODE CORE
    // 上面这行不能删除
}