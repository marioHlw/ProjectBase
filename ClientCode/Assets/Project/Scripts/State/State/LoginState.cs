/**************************
 * 文件名:LoginState.cs
 * 文件描述:登陆状态
 * 创建日期:2019/09/20
 * 作者:ZB
 ***************************/




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[GameState]
public class LoginState : BaseState
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();

        Log.Info("进入 - 游戏状态(LoginState)");
    }

    public override void OnStateLeave()
    {
        base.OnStateLeave();

        Log.Info("退出 - 游戏状态(LoginState)");
    }
}
