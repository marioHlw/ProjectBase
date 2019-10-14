/**************************
 * 文件名:GameStateCtrl.cs
 * 文件描述:游戏状态控制器类
 * 创建日期:2019/08/20
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateCtrl : Singleton<GameStateCtrl>
{
    private StateMachine m_gameState = new StateMachine();

    public override void Init()
    {
        base.Init();

        // 注册游戏状态
        m_gameState.RegisterStateByAttributes<GameStateAttribute>(typeof(GameStateAttribute).Assembly);

        Log.Info(Ctrl.LogInfos[0] + " - 游戏状态控制器");
    }

    public override void UnInit()
    {
        base.UnInit();

        GetCurrentState().OnStateLeave();

        Log.Info(Ctrl.LogInfos[1] + " - 游戏状态控制器");
    }

    public IState GetCurrentState()
    {
        return m_gameState.TopState();
    }

    public void GotoState(string name)
    {
        m_gameState.ChangeState(name);
    }

    public void Uninitialize()
    {
        m_gameState.Clear();

        m_gameState = null;
    }

    public string CurrentStateName
    {
        get
        {
            IState currentState = GetCurrentState();

            return currentState == null ? "unkown state" : currentState.Name;
        }
    }
}
