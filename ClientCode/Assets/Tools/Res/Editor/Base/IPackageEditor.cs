/**************************
 * 文件名:IPackageEditor.cs
 * 文件描述:资源打包 - 打包类接口
 * 1.所有分类的打包类都必须继承这个接口
 * 创建日期:2019/11/09
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Res
{
    public interface IPackageEditor
    {
        /// <summary>
        /// 更新 - SVN拉取资源
        /// </summary>

        void OnUpdateSvn();

        /// <summary>
        /// 更新 - SVN提交资源
        /// </summary>

        void OnSubmitSvn();

        /// <summary>
        /// 处理 - 全部资源
        /// </summary>

        void OnProcessAll();

        /// <summary>
        /// 打包 - 全部资源
        /// </summary>

        void OnPackageAll();

        /// <summary>
        /// 结束
        /// </summary>

        void OnOver();
    }
}