/**************************
 * 文件名:Optimize.cs
 * 文件描述:优化相关
 * 创建日期:2019/11/16
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Optimize
{
    // 降低分辨率：解决发热、游戏卡顿
    // 一般情况下大型游戏苹果分辨率将为原来的百分之八十
    // 降低率按游戏具体在手机上测试为准
    public static void LowerResolution()
    {
        int _width = (int)(Screen.currentResolution.width * 0.5f);
        int _height = (int)(Screen.currentResolution.height * 0.5f);
        Screen.SetResolution(_width, _height, true);
    }
}