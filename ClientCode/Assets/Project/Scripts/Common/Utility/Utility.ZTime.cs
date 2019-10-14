/**************************
 * 文件名:Utility.ZTime.cs
 * 文件描述:时间实用相关类
 * 创建日期:2019/08/21
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Utility
{
    public static class ZTime
    {
        /// <summary>
        /// 获取 - 时间的00:00:00显示
        /// </summary>
        /// <param name="second">秒数</param>

        public static string GetTimeString(int second)
        {
            int hour = 0;
            int minute = 0;
            hour = second / 3600;
            second = second % 3600;
            minute = second / 60;
            second = second % 60;

            return string.Format("{0:D2}", hour) + ":" + string.Format("{0:D2}", minute) + ":" + string.Format("{0:D2}", second);
        }
    }
}