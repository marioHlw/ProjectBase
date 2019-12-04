/*****************************************************
 * 文件名:Setting.cs
 * 文件描述:游戏内存设置
 * 1.保存PlayerPrefs
 * 创建日期:2019/11/17
 * 作者:ZB
 *****************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting
{
    public static void SetBool(string name, bool val)
    {
        PlayerPrefs.SetInt(name, val ? 1 : 0);
    }

    public static void SetInt(string name, int val)
    {
        PlayerPrefs.SetInt(name, val);
    }

    public static void SetString(string name, string val)
    {
        PlayerPrefs.SetString(name, val);
    }

    public static void SetFloat(string name, float val)
    {
        PlayerPrefs.SetFloat(name, val);
    }

    public static void SetColor(string name, Color color)
    {
        SetString(name, color.r + " " + color.g + " " + color.b + " " + color.a);
    }

    public static bool GetBool(string name)
    {
        return PlayerPrefs.GetInt(name, 0) == 1;
    }

    public static int GetInt(string name)
    {
        return PlayerPrefs.GetInt(name, int.MinValue);
    }

    public static string GetString(string name)
    {
        return PlayerPrefs.GetString(name, "");
    }

    public static float GetFloat(string name)
    {
        return PlayerPrefs.GetFloat(name, float.MinValue);
    }

    public static Color GetColor(string name, Color color)
    {
        string _tempStr = GetString(name);

        if (string.IsNullOrEmpty(_tempStr))
        {
            return color;
        }

        string[] _parts = _tempStr.Split(' ');

        if (_parts.Length == 4)
        {
            float.TryParse(_parts[0], out color.r);
            float.TryParse(_parts[1], out color.g);
            float.TryParse(_parts[2], out color.b);
            float.TryParse(_parts[3], out color.a);
        }

        return color;
    }
}