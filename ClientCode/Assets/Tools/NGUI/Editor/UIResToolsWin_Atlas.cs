/**************************
 * 文件名:UIResToolsWin_Base.cs
 * 文件描述:NGUI - UI资源工具 - 图集使用信息类
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
    private string m_UIPrefabUseInfo = "";

    public override void OnGUI()
    {
        base.OnGUI();

        EditorGUILayout.LabelField("请选中要检查的纹理集（带UIAtlas的GameObject）");

        EditorGUILayout.BeginHorizontal("box");
        {
            m_selectObj = EditorGUILayout.ObjectField(new GUIContent("原始纹理集："), m_selectObj, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("选择"))
            {
                string _url = EditorUtility.OpenFilePanel("请选中要检查的纹理集", Application.dataPath + "/Project/UI", "prefab");
                if (!string.IsNullOrEmpty(_url))
                {
                    m_selectObj = AssetDatabase.LoadAssetAtPath(_url.Replace(Application.dataPath, "Assets"), typeof(GameObject)) as GameObject;
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
            EditorGUILayout.LabelField("没有使用的图片资源:", GUILayout.Width(145));
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
            EditorGUILayout.LabelField("筛选:", GUILayout.Width(30));
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

                    if (GUILayout.Button("显示详情", GUILayout.Width(80)))
                    {
                        if (m_UIPrefabUseInfo == m_showInfos[i])
                        {
                            m_UIPrefabUseInfo = "";
                        }
                        else
                        {
                            m_UIPrefabUseInfo = m_showInfos[i];
                        }
                    }

                    GUILayout.TextField(m_showInfos[i]);
                }
                EditorGUILayout.EndHorizontal();

                if (m_UIPrefabUseInfo == m_showInfos[i])
                {
                    OnGUI_UIPrefabUseInfo();
                }
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
    /// 绘制UI的Prefab使用选中了图集中图片的物体名称
    /// </summary>

    private void OnGUI_UIPrefabUseInfo()
    {
        if (string.IsNullOrEmpty(m_UIPrefabUseInfo))
        {
            return;
        }

        GameObject _obj = AssetDatabase.LoadAssetAtPath(m_UIPrefabUseInfo, typeof(GameObject)) as GameObject;

        List<UISprite> _uisprites = new List<UISprite>();

        UISprite _uisprite = _obj.GetComponent<UISprite>();
        if (_uisprite != null)
        {
            _uisprites.Add(_uisprite);
        }

        UISprite[] _uispriteArray = _obj.GetComponentsInChildren<UISprite>(true);
        if (_uispriteArray != null)
        {
            _uisprites.AddRange(_uispriteArray);
        }

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical("Box");
            {
                for (int i = 0, max = _uisprites.Count; i < max; i++)
                {
                    if (_uisprites[i].atlas == m_selectObj.GetComponent<UIAtlas>())
                    {
                        string _str = _uisprites[i].name;
                        Transform _tran = _uisprites[i].transform.parent;
                        while (_tran != _obj.transform)
                        {
                            _str = _tran.name + "/" + _str;
                            _tran = _tran.parent;
                        }
                        GUILayout.TextField(_str);
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
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

    /// <summary>
    /// 替换图集
    /// </summary>

    private void ReplaceAtlas()
    {
        UIAtlas _selectAtlas = m_selectObj.GetComponent<UIAtlas>();         // 选中的图集
        UIAtlas _replaceAtlas = m_replaceObj.GetComponent<UIAtlas>();       // 替换成的图集

        if (m_selectObj == null || m_replaceObj == null || _selectAtlas == null || _replaceAtlas == null)
        {
            return;
        }

        bool _isSetDirtyAll = false;
        List<UISprite> _sprites = new List<UISprite>();

        for (int i = 0, count = m_showInfos.Count; i < count; i++)
        {
            if (!m_selectStates[i])
            {
                continue;
            }

            GameObject _obj = AssetDatabase.LoadAssetAtPath(m_showInfos[i], typeof(GameObject)) as GameObject;

            UISprite _sprite = _obj.GetComponent<UISprite>();
            if (_sprite != null)
            {
                _sprites.Add(_sprite);
            }
            UISprite[] _spriteArray = _obj.GetComponentsInChildren<UISprite>(true);
            if (_spriteArray != null && _spriteArray.Length > 0)
            {
                _sprites.AddRange(_spriteArray);
            }

            bool _isSetDirty = false;
            for (int j = 0, max = _sprites.Count; j < max; j++)
            {
                if (_sprites[j].atlas != null && _sprites[j].atlas.name == _selectAtlas.name)
                {
                    _isSetDirty = true;
                    _sprites[j].atlas = _replaceAtlas;
                }
            }
            if (_isSetDirty)
            {
                _isSetDirtyAll = true;
                // 标记目标物体已改变，标记之后unity会自动保存。
                EditorUtility.SetDirty(_obj);
            }
        }

        if (_isSetDirtyAll)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            CheckSlelectAtlas();
        }
    }
}