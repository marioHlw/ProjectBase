/**************************
 * 文件名:UIResToolsWin_Base.cs
 * 文件描述:NGUI资源工具-图集类
 *          1.替换图集
 *          2.查看图集使用情况
 * 创建日期:2019/10/14
 * 作者:ZB
 ***************************/



using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIResToolsWin_Atlas : UIResToolsWin_Base
{
    private GameObject m_selectObj = null;
    private GameObject m_replaceObj = null;

    private List<string> m_showInfos = new List<string>();

    public override void OnGUI()
    {
        base.OnGUI();

        EditorGUILayout.LabelField("请选中要检查的纹理集（带UIAtlas的GameObject）");

        EditorGUILayout.BeginHorizontal("box");
        {
            m_selectObj = EditorGUILayout.ObjectField(new GUIContent("原始纹理集："), m_selectObj, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("选择"))
            {
                string url = EditorUtility.OpenFilePanel("请选中要检查的纹理集", Application.dataPath + "/Project/UI", "prefab");
                if (!string.IsNullOrEmpty(url))
                {
                    m_selectObj = AssetDatabase.LoadAssetAtPath(url.Replace(Application.dataPath, "Assets"), typeof(GameObject)) as GameObject;
                    //scrollViewPos = Vector2.zero;
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal("box");
        {
            m_replaceObj = EditorGUILayout.ObjectField(new GUIContent("替换纹理集："), m_replaceObj, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("选择"))
            {
                string url = EditorUtility.OpenFilePanel("请选中要检查的纹理集", Application.dataPath + "/_Project/GUI", "prefab");
                if (!string.IsNullOrEmpty(url))
                {
                    m_replaceObj = AssetDatabase.LoadAssetAtPath(url.Replace(Application.dataPath, "Assets"), typeof(GameObject)) as GameObject;
                }
            }
            if (GUILayout.Button("替换"))
            {
                if (m_replaceObj != null && m_replaceObj.GetComponent<UIAtlas>() != null)
                {
                    if (EditorUtility.DisplayDialog("提示", "是否确认替换以下物体所使用的纹理？？？", "确认", "取消"))
                    {
                        ReplaceAtlas();
                    }
                }
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public override void OnSelectionChange()
    {
        base.OnSelectionChange();


    }

    private void CheckSlelectAtlas()
    {
        m_showInfos.Clear();

        UIAtlas _targetAtlas = m_selectObj.GetComponent<UIAtlas>();

        if (_targetAtlas == null)
        {
            return;
        }

        Dictionary<string, List<string>> _useSprite = new Dictionary<string, List<string>>();
        for (int i = 0; i < _targetAtlas.spriteList.Count; i++)
        {
            _useSprite.Add(_targetAtlas.spriteList[i].name, new List<string>());
        }
    }

    private void ReplaceAtlas()
    {
        UIAtlas _targetAtlas = m_selectObj.GetComponent<UIAtlas>();
        UIAtlas _replaceTargetAtlas = m_replaceObj.GetComponent<UIAtlas>();

        if (m_selectObj == null || m_replaceObj == null || _targetAtlas == null || _replaceTargetAtlas == null)
        {
            return;
        }

        //GameObject obj;
        //bool isSetDirtyAll = false;
        //List<UISprite> allSprites = new List<UISprite>();
        //for (int i = 0; i < showList.Count; i++)
        //{
        //    if (!showToggleList[i]) continue;
        //    obj = AssetDatabase.LoadAssetAtPath(showList[i], typeof(GameObject)) as GameObject;
        //    UISprite sp = obj.GetComponent<UISprite>();
        //    if (sp != null)
        //    {
        //        allSprites.Add(sp);
        //    }
        //    UISprite[] sps = obj.GetComponentsInChildren<UISprite>(true);
        //    if (sps != null && sps.Length > 0)
        //    {
        //        allSprites.AddRange(sps);
        //    }
        //    bool isSetDirty = false;
        //    for (int j = 0; j < allSprites.Count; j++)
        //    {
        //        if (allSprites[j].atlas != null && allSprites[j].atlas.name == _targetAtlas.name)
        //        {
        //            isSetDirty = true;
        //            allSprites[j].atlas = _replaceTargetAtlas;
        //        }
        //    }
        //    if (isSetDirty)
        //    {
        //        isSetDirtyAll = true;
        //        EditorUtility.SetDirty(obj);
        //    }
        //}

        //if (isSetDirtyAll)
        //{
        //    AssetDatabase.SaveAssets();
        //    AssetDatabase.Refresh();
        //    //CheckSelectAtlasUse();
        //}
    }
}