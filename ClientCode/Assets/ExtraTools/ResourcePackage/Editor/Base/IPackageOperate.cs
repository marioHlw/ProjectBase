using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Package
{
    public interface IPackageOperate
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