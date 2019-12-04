/**************************
 * 文件名:UIResToolsWin_Sprite.cs
 * 文件描述:NGUI - UI资源工具 - 图片使用信息类
 * 创建日期:2019/10/21
 * 作者:ZB
 ***************************/




using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIResToolsWin_Sprite : UIResToolsWin_Base
{
    private string m_searchPathAtals = "Assets/Project/UI/Common1/Atlas";

    public override void OnGUI()
    {
        base.OnGUI();


    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        GetAtalsPath();
    }

    public override void OnSelectionChange()
    {
        base.OnSelectionChange();
    }

    private List<string> GetAtalsPath()
    {
        List<string> _results = new List<string>();

        //m_searchPathAtals = Application.dataPath + m_searchPathAtals;
        //Debug.Log(m_searchPathAtals);

        /* 不明白可查看官方文档:AssetDatabase.FindAssets */
        HashSet<string> tempGuids = new HashSet<string>();
        /* 把传送为参数的集合中的所有元素添加到集合中 */
        // "t:Scene t:Prefab t:Shader t:Model t:Material t:Texture t:AudioClip t:AnimationClip t:AnimatorController t:Font t:TextAsset t:ScriptableObject";
        tempGuids.UnionWith(AssetDatabase.FindAssets("t:Prefab"));
        string[] assetGuids = new List<string>(tempGuids).ToArray();
        for (int i = 0, count = assetGuids.Length; i < count; i++)
        {
            string fullPath = AssetDatabase.GUIDToAssetPath(assetGuids[i]);
            if (AssetDatabase.IsValidFolder(fullPath))
            {
                continue;
            }

            Debug.Log(fullPath);
        }
        return _results;
    }
}
