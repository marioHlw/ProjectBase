/**************************
 * 文件名:AssetBundleManager.cs
 * 文件描述:资源管理类
 * 1.所有加载资源都从这个类调用
 * 创建日期:2019/11/02
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Res
{
    public class ResManager : Singleton<ResManager>
    {
        private ResLoaderResources m_loaderResources;
        private ResLoaderByte m_loaderByte;
        private ResLoaderAssetBundle m_loaderAssetBundle;

        public ResLoaderResources LoaderResources { get { return m_loaderResources; } }
        public ResLoaderByte LoaderByte { get { return m_loaderByte; } }
        public ResLoaderAssetBundle LoaderAssetBundle { get { return m_loaderAssetBundle; } }

        public override void Init()
        {
            base.Init();

            m_loaderResources = new ResLoaderResources();
            m_loaderByte = new ResLoaderByte();
            m_loaderAssetBundle = new ResLoaderAssetBundle();
        }
    }
}