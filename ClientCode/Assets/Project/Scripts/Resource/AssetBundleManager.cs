using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager
{
    private static Dictionary<string, AssetBundle> s_dataMap = new Dictionary<string, AssetBundle>();
    private static Dictionary<string, int> s_referenceCountMap = new Dictionary<string, int>();

    public static void Add(string path, AssetBundle assetBundle)
    {
        if (!s_dataMap.ContainsKey(path))
        {
            s_dataMap.Add(path, assetBundle);
            s_referenceCountMap.Add(path, 1);
        }
    }
}