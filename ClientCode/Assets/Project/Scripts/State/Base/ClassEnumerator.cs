using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ClassEnumerator /* 类枚举器 */
{
    private Type m_attributeType;

    private Type m_interfaceType;

    protected List<Type> results = new List<Type>();

    public ClassEnumerator(Type InAttributeType, Type InInterfaceType, Assembly InAssembly, bool bIgnoreAbstract = true, bool bInheritAttribute = false, bool bShouldCrossAssembly = false)
    {
        m_attributeType = InAttributeType;

        m_interfaceType = InInterfaceType;

        try
        {
            if (bShouldCrossAssembly)
            {
                /* 获得程序所有的Assembly */
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                if (assemblies != null)
                {
                    for (int i = 0; i < assemblies.Length; i++)
                    {
                        Assembly inAssembly = assemblies[i];

                        CheckInAssembly(inAssembly, bIgnoreAbstract, bInheritAttribute);
                    }
                }
            }
            else
            {
                CheckInAssembly(InAssembly, bIgnoreAbstract, bInheritAttribute);
            }
        }
        catch (Exception exception)
        {

#if UNITY_EDITOR
			Debug.LogError("Error in enumerate classes :" + exception.Message);
#endif

		}
    }

    /// <summary>
    /// 在程序集中检测游戏状态类
    /// </summary>
    /// <param name="inAssembly">内部程序集</param>
    /// <param name="inIgnoreAbstract">忽略内部抽象</param>
    /// <param name="inInheritAttribute">继承子属性</param>

    protected void CheckInAssembly(Assembly inAssembly, bool inIgnoreAbstract, bool inInheritAttribute)
    {
        Type[] types = inAssembly.GetTypes();

        if (types != null)
        {
            for (int i = 0; i < types.Length; i++)
            {
                Type c = types[i];

                if (((m_interfaceType == null || m_interfaceType.IsAssignableFrom(c)) && (!inIgnoreAbstract || (inIgnoreAbstract && !c.IsAbstract))) && c.GetCustomAttributes(m_attributeType, inInheritAttribute).Length > 0)
                {
                    results.Add(c);
                }
            }
        }
    }

    public List<Type> Results
    {
        get
        {
            return results;
        }
    }
}