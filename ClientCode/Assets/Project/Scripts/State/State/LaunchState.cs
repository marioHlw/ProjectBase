/**************************
 * 文件名:LaunchState.cs
 * 文件描述:游戏启动状态
 * 创建日期:2019/08/20
 * 作者:ZB
 ***************************/



using System;
using TableProto;
using UnityEngine;
using zb.UGUILibrary;

[GameState]
public class LaunchState : BaseState
{
    /// <summary>
    /// 进入状态
    /// </summary>

    public override void OnStateEnter()
    {
        Log.Info("进入 - 游戏状态(LaunchState)");

        Ctrl.uiManager.OnOpenGroup(enUIFormType.UIFormLoading);

        Ctrl.eventRouter.BroadCastEvent<float, string>(UILoadingModule.MS_UPDATE_PROGRESSVALUE, 0, "加载中...");

        Ctrl.eventRouter.AddEventHandler(EventID.MS_TABLE_LOADFINISH, MS_TableLoadFinish);
        Ctrl.eventRouter.BroadCastEvent<float, string>(UILoadingModule.MS_UPDATE_PROGRESSVALUE, 10, "加载中...");
        MonoSingleton<GameFramework>.instance().StartCoroutine(Singleton<DatabinTableManager>.Instance.LoadDatabinTable());
    }

    public override void OnStateLeave()
    {
        base.OnStateLeave();

        Log.Info("退出 - 游戏状态(LaunchState)");
    }

    /// <summary>
    /// 进入下一个状态
    /// </summary>

    private void ToNextState()
    {
        Ctrl.gameStateCtrl.GotoState("LoginState");
    }

    /// <summary>
    /// 表格资源加载完成
    /// </summary>

    private void MS_TableLoadFinish()
    {
        Ctrl.eventRouter.RemoveEventHandler(EventID.MS_TABLE_LOADFINISH, MS_TableLoadFinish);
        Ctrl.eventRouter.BroadCastEvent<float, string>(UILoadingModule.MS_UPDATE_PROGRESSVALUE, 100, "加载中...");
        ToNextState();
    }
}
