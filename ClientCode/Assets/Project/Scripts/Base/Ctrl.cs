/**************************
 * 文件名:Ctrl.cs
 * 文件描述:全局静态变量管理类
 * 创建日期:2019/07/31
 * 作者:ZB
 ***************************/

/* 命名空间解释：
 * namespace NGUI.Tools(NGUI工具)
 * namespace zb.NGUILibrary(NGUI)
 */

using System.Collections;
using System.Collections.Generic;
using UI.Common;
using UnityEngine;
using zb.NGUILibrary;

public static class Ctrl
{
    public static readonly string[] LogInfos = new string[] 
    {
        "<color=#0099bc><b>初始化(开启) ► </b></color>",
        "<color=#FF0000><b>初始化(关闭) ► </b></color>",
        "<color=#00FF8B><b>开始读取表格{0}数据 ► </b></color>",
    };

    public static readonly string Language = "Chinese";                 // 语言

#if UNITY_EDITOR
    public static DeviceBase device = new DeviceEditor();
#elif UNITY_ANDROID
    public static DeviceBase device = new DeviceAndroid();
#elif UNITY_IOS
    public static DeviceBase device = new DeviceIOS();
#elif UNITY_STANDALONE_WIN
    public static DeviceBase device = new DeviceWindows();
#else
    public static DeviceBase device = new DeviceWindows();
#endif

    public static GameStateCtrl gameStateCtrl = null;                   // 游戏状态控制器
    public static ObjectPoolManager objectPoolManager = null;           // 对象池管理器
    public static TimerManager timerManager = null;                     // 定时器管理器
    public static EventRouter eventRouter = null;                       // 事件
    public static TweenerManager tweenerManager = null;                 // DOTween扩展管理器

    public static UIRedTipManager uiRedTipManager = null;               // UI红点提示管理器

    public static UIResManager uiResManager = null;                     // NGUI资源管理器
    public static UIManager uiManager = null;                           // NGUI管理器

    public static ResourceComponent resourceComponent = null;           // 资源管理组件
    public static ResourceLoader resourceLoader = null;                 // 资源加载器

    public static DatabinTableManager databinTableManager = null;       // 配置表格数据管理器

    public static void Init()
    {
        gameStateCtrl = Singleton<GameStateCtrl>.Instance;
        timerManager = Singleton<TimerManager>.Instance;
        eventRouter = Singleton<EventRouter>.Instance;
        tweenerManager = Singleton<TweenerManager>.Instance;
        uiResManager = Singleton<UIResManager>.Instance;
        uiManager = Singleton<UIManager>.Instance;
        uiRedTipManager = Singleton<UIRedTipManager>.Instance;
        databinTableManager = Singleton<DatabinTableManager>.Instance;

        resourceLoader = MonoSingleton<ResourceLoader>.GetInstance();

        objectPoolManager = GameFrameworkEntry.GetModule<ObjectPoolManager>();
    }

    public static void SetResourceComponent(ResourceComponent component)
    {
        resourceComponent = component;
    }
}