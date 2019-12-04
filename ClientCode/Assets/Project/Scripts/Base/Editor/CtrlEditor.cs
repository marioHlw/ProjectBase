/**************************
 * 文件名:CtrlEditor.cs
 * 文件描述:编辑器全局静态变量管理类
 * 创建日期:2019/11/20
 * 作者:ZB
 ***************************/



using UnityEngine;
using System.Collections;
using UnityEditor;

public static class CtrlEditor
{
    private static Texture m_txrAssets;
    private static Texture m_txrPrefab;
    private static Texture m_txrSpriteatlas;
    private static Texture m_txrBytes;

    public static Texture TxrAssets
    {
        get
        {
            if (m_txrAssets == null)
            {
                m_txrAssets = AssetDatabase.GetCachedIcon("Assets");
            }

            return m_txrAssets;
        }
    }

    public static Texture TxrPrefab
    {
        get
        {
            if (m_txrPrefab == null)
            {
                m_txrPrefab = EditorGUIUtility.IconContent("Prefab Icon").image;
            }

            return m_txrPrefab;
        }
    }

    public static Texture TxrSpriteatlas
    {
        get
        {
            if (m_txrSpriteatlas == null)
            {
                m_txrSpriteatlas = EditorGUIUtility.IconContent("Spriteatlas Icon").image;
            }

            return m_txrSpriteatlas;
        }
    }

    public static Texture TxrBytes
    {
        get
        {
            if (m_txrBytes == null)
            {
                m_txrBytes = EditorGUIUtility.IconContent("TextAsset Icon").image;
            }

            return m_txrBytes;
        }
    }

    public static Texture GetTextureFormExtension(string extension)
    {
        switch (extension)
        {
            case "": return TxrAssets;
            case ".prefab": return TxrPrefab;
            case ".bytes": return TxrBytes;
            case ".spriteatlas": return TxrSpriteatlas;
        }

        return TxrAssets;
    }

    public static bool Toggle(string label, bool value, int labelWidth = 0, int valueWidth = 100)
    {
        GUILayout.BeginHorizontal("box");
        int _labelLen = label.Length;
        EditorGUILayout.LabelField(label, labelWidth == 0 ? GUILayout.Width(_labelLen * 12) : GUILayout.Width(labelWidth));
        value = EditorGUILayout.Toggle(value, valueWidth == 0 ? GUILayout.Width(100) : GUILayout.Width(valueWidth));
        GUILayout.EndHorizontal();
        return value;
    }

    public static string TextField(string label, string value, int labelWidth = 0, int valueWidth = 100)
    {
        GUILayout.BeginHorizontal("box");
        int _labelLen = label.Length;
        EditorGUILayout.LabelField(label, labelWidth == 0 ? GUILayout.Width(_labelLen * 12) : GUILayout.Width(labelWidth));
        value = EditorGUILayout.TextField(value, valueWidth == 0 ? GUILayout.Width(100) : GUILayout.Width(valueWidth));
        GUILayout.EndHorizontal();
        return value;
    }
}