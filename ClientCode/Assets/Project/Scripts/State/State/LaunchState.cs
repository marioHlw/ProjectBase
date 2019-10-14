/**************************
 * 文件名:LaunchState.cs
 * 文件描述:游戏启动状态
 * 创建日期:2019/08/20
 * 作者:ZB
 ***************************/



using System;
using TableProto;
using UnityEngine;

[GameState]
public class LaunchState : BaseState
{
	private bool m_isBaseSystemPrepareComplete;
	private bool m_isSplashPlayComplete;

    /// <summary>
    /// 进入状态
    /// </summary>

    public override void OnStateEnter()
    {
        Log.Info("进入 - 游戏状态(LaunchState)");

        /* 启动画面 */
        // Ctrl.resourceManager.LoadScene("Assets/Scene/SplashScene.unity", new LoadSceneCallbacks(OnSplashLoadCompleted));

        m_isSplashPlayComplete = false;
        m_isBaseSystemPrepareComplete = false;

        Ctrl.uiManager.OnOpenUIGroup(enUIFormType.UIFormLoading);
        Ctrl.eventRouter.BroadCastEvent<float, string>(LoadingModule.MS_UPDATE_PROGRESSVALUE, 0, "加载中...");

        // 播放启动画面

        MonoSingleton<GameFramework>.GetInstance().StartPrepareBaseSystem(new GameFramework.DelegateOnBaseSystemPrepareComplete(OnPrepareBaseSystemComplete));

        Ctrl.timerManager.AddTimer(1000, 1, new Action(OnSplashPlayComplete), null);
    }

    public override void OnStateLeave()
    {
        base.OnStateLeave();

        Log.Info("退出 - 游戏状态(LaunchState)");
    }

    /// <summary>
    /// 启动画面播放完成回调
    /// </summary>

    private void OnSplashPlayComplete()
    {
        Ctrl.eventRouter.BroadCastEvent<float, string>(LoadingModule.MS_UPDATE_PROGRESSVALUE, 100, "加载中...");
        m_isSplashPlayComplete = true;

        CheckContionToNextState();
    }

    /// <summary>
    /// 准备基础系统完成回调
    /// </summary>

    private void OnPrepareBaseSystemComplete()
    {
        Ctrl.eventRouter.BroadCastEvent<float, string>(LoadingModule.MS_UPDATE_PROGRESSVALUE, 100, "加载中...");
        m_isBaseSystemPrepareComplete = true;

        CheckContionToNextState();
    }

    /// <summary>
    /// 检查进入下一个状态
    /// </summary>

    private void CheckContionToNextState()
    {
        if (m_isSplashPlayComplete && m_isBaseSystemPrepareComplete)
        {
            Ctrl.gameStateCtrl.GotoState("LoginState");
        }
    }
}
