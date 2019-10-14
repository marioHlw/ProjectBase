using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zb.NGUILibrary;

public class Test : MonoBehaviour
{
    public UILoopGrid loopGrid;
    public int amount = 0;
    public int x = 0;
    public int y = 0;

    // Use this for initialization
    void Start()
    {
        //loopGrid.SetAmount(amount);
        //loopGrid.OnMove(x, y);

        Type[] types = Utility.ZAssembly.GetTypesSubclass(typeof(UIAnimationEntryBase));
        foreach (Type type in types)
        {
            Debug.Log(type.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
