/**************************
 * 文件名:UIResToolsWin_Base.cs
 * 文件描述:NGUI资源工具-图集类
 *          1.替换图集
 *          2.查看图集使用情况
 * 创建日期:2019/10/14
 * 作者:ZB
 ***************************/



using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class UIResToolsWin_Atlas : UIResToolsWin_Base
{
    private GameObject m_selectObj = null;
    private GameObject m_replaceObj = null;

    private List<string> m_refInfos = new List<string>();               // 引用了选中图集的所有UI预设路径
    private List<string> m_showInfos = new List<string>();              // 需要显示出来的所有UI预设路径
    private List<string> m_unUseIcons = new List<string>();             // 选中的图集中没有使用过的所有图片

    private string m_selectName = "";                       // 选中的物体名称
    private string m_searchInfo = "";                       // 搜索过滤信息
    private Vector2 m_viewPosition = Vector2.zero;          // 显示所有UI预设的ScrollView位置
    private List<bool> m_selectStates = new List<bool>();   // UI预设选中状态

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
                    m_viewPosition = Vector2.zero;
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

        if (m_selectObj != null && m_selectObj.GetComponent<UIAtlas>() == null)
        {
            m_selectObj = null;
        }
        if (m_selectObj == null)
        {
            return;
        }
        if (m_selectObj.gameObject.name != m_selectName)
        {
            CheckSlelectAtlas();
            m_selectName = m_selectObj.gameObject.name;
        }

        EditorGUILayout.BeginHorizontal("box");
        {
            string _str = "";
            for (int i = 0, count = m_unUseIcons.Count; i < count; i++)
            {
                _str += (m_unUseIcons[i] + ";");
            }
            EditorGUILayout.LabelField("没有使用的图片资源:", GUILayout.Width(150));
            GUI.color = Color.red;
            EditorGUILayout.TextField("", _str);
            GUI.color = Color.white;

            if (GUILayout.Button("刷新", GUILayout.Width(100)))
            {
                CheckSlelectAtlas_UnUseIcon();
            }
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("纹理被引用情况:");
            string _search = EditorGUILayout.TextField(m_searchInfo);
            if (_search != m_searchInfo)
            {
                m_searchInfo = _search;
                CheckShowInfo();
            }
        }
        EditorGUILayout.EndHorizontal();

        m_viewPosition = GUILayout.BeginScrollView(m_viewPosition);
        {
            for (int i = 0, count = m_showInfos.Count; i < count; i++)
            {
                EditorGUILayout.BeginHorizontal("Box");
                {
                    m_selectStates[i] = GUILayout.Toggle(m_selectStates[i], "选择", GUILayout.Width(50));

                    if (GUILayout.Button("跳转", GUILayout.Width(100)))
                    {
                        Selection.activeObject = AssetDatabase.LoadAssetAtPath(m_showInfos[i], typeof(UnityEngine.Object));
                    }

                    GUILayout.TextField(m_showInfos[i]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        GUILayout.EndScrollView();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public override void OnSelectionChange()
    {
        base.OnSelectionChange();


    }

    /// <summary>
    /// 检测选中的图集
    /// </summary>

    private void CheckSlelectAtlas()
    {
        m_showInfos.Clear();
        m_refInfos.Clear();

        UIAtlas _selectAtlas = m_selectObj.GetComponent<UIAtlas>();

        if (_selectAtlas == null)
        {
            return;
        }

        Dictionary<string, List<string>> _useSprite = new Dictionary<string, List<string>>();
        for (int i = 0; i < _selectAtlas.spriteList.Count; i++)
        {
            _useSprite.Add(_selectAtlas.spriteList[i].name, new List<string>());
        }

        List<UISprite> _selectSprites = new List<UISprite>();             // 选中的所有UISprite
        string[] _pathArray = Directory.GetFiles(Application.dataPath + "/Project/UI", "*.prefab", SearchOption.AllDirectories);
        for (int i = 0, count = _pathArray.Length; i < count; i++)
        {
            _selectSprites.Clear();

            string _path = _pathArray[i].Replace(Application.dataPath, "Assets");
            GameObject _obj = AssetDatabase.LoadAssetAtPath(_path, typeof(GameObject)) as GameObject;
            if (_obj != null)
            {
                // 自身的UISprite
                UISprite _sprite = _obj.GetComponent<UISprite>();
                if (_sprite != null)
                {
                    _selectSprites.Add(_sprite);
                }
                // 子物体上所有的UISprite
                UISprite[] _spriteArray = _obj.GetComponentsInChildren<UISprite>(true);
                if (_spriteArray != null && _spriteArray.Length > 0)
                {
                    _selectSprites.AddRange(_spriteArray);
                }
            }

            for (int j = 0, max = _selectSprites.Count; j < max; j++)
            {
                if (_selectSprites[j].atlas != null && _selectSprites[j].atlas.name == _selectAtlas.name)
                {
                    m_refInfos.Add(_path.Replace('\\', '/'));
                    break;
                }
            }
        }

        CheckShowInfo();
    }

    /// <summary>
    /// 检测选中的图集中没有使用过的图片
    /// </summary>

    private void CheckSlelectAtlas_UnUseIcon()
    {
        m_unUseIcons.Clear();

        UIAtlas _selectAtlas = m_selectObj.GetComponent<UIAtlas>();

        if (_selectAtlas == null)
        {
            return;
        }

        for (int i = 0, count = m_showInfos.Count; i < count; i++)
        {
            GameObject _obj = AssetDatabase.LoadAssetAtPath(m_showInfos[i], typeof(GameObject)) as GameObject;

            UISprite _uisprite = _obj.GetComponent<UISprite>();
            if (_uisprite != null && _uisprite.atlas == _selectAtlas && !string.IsNullOrEmpty(_uisprite.spriteName))
            {
                m_unUseIcons.Add(_uisprite.spriteName);
            }

            UISprite[] _uispriteArray = _obj.GetComponentsInChildren<UISprite>(true);
            for (int j = 0; j < _uispriteArray.Length; j++)
            {
                _uisprite = _uispriteArray[j];

                if (_uisprite != null && _uisprite.atlas == _selectAtlas && !string.IsNullOrEmpty(_uisprite.spriteName))
                {
                    m_unUseIcons.Add(_uisprite.spriteName);
                }
            }
        }
    }

    /// <summary>
    /// 检测需要显示信息(UI预设路径)
    /// </summary>

    private void CheckShowInfo()
    {
        m_selectStates.Clear();
        m_showInfos.Clear();

        if (string.IsNullOrEmpty(m_searchInfo))
        {
            m_showInfos.AddRange(m_refInfos);
        }
        else
        {
            for (int j = 0, max = m_refInfos.Count; j < max; j++)
            {
                if (m_refInfos[j].IndexOf(m_searchInfo, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    m_showInfos.Add(m_refInfos[j]);
                }
            }
        }

        for (int i = 0, count = m_showInfos.Count; i < count; i++)
        {
            if (i >= m_selectStates.Count)
            {
                m_selectStates.Add(true);
            }
            else
            {
                m_selectStates[i] = true;
            }
        }
        if (m_selectStates.Count > m_showInfos.Count)
        {
            m_selectStates.RemoveRange(m_selectStates.Count, m_showInfos.Count - m_selectStates.Count);
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