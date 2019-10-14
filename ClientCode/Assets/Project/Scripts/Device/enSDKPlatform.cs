using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enSDKPlatform
{
    /// <summary>
    /// 内部开发平台
    /// </summary>

    Develop_Android = 10001,
    Develop_IOS = 10002,
    Develop_Windows = 10003,
    Develop_Editor = 10004,

    /// <summary>
    /// 应用宝平台
    /// </summary>

    Tencent_Android = 11111,
    Tencent_IOS = 11112,

    /// <summary>
    /// 应用宝对外发布平台
    /// </summary>

    TencentRelease_Android = 11115,
    TencentRelease_IOS = 11116,

    /// <summary>
    /// 玩家测试平台
    /// </summary>

    Pionner_Android = 11113,
    Pionner_IOS = 11114,
}