using System;
using System.Collections.Generic;
using System.Reflection;



public static partial class Utility
{
    /// <summary>
    /// 程序集相关的实用函数
    /// </summary>

    public static class ZAssembly
    {
        private static readonly Assembly[] s_Assemblies = null;

        private static readonly Dictionary<string, Type> s_CachedTypes = new Dictionary<string, Type>();

        static ZAssembly()
        {
            s_Assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        /// <summary>
        /// 获取已加载的程序集
        /// </summary>
        /// <returns>已加载的程序集</returns>

        public static Assembly[] GetAssemblies()
        {
            return s_Assemblies;
        }

        /// <summary>
        /// 获取已加载的程序集中的指定类型
        /// </summary>
        /// <param name="typeName">要获取的类型名</param>
        /// <returns>已加载的程序集中的指定类型</returns>

        public static Type GetType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new Exception("Type name is invalid.");
            }

            Type type = null;

            if (s_CachedTypes.TryGetValue(typeName, out type))
            {
                return type;
            }

            type = Type.GetType(typeName);

            if (type != null)
            {
                s_CachedTypes.Add(typeName, type);

                return type;
            }

            foreach (Assembly assembly in s_Assemblies)
            {
                type = Type.GetType(Utility.ZText.Format("{0}, {1}", typeName, assembly.FullName));

                if (type != null)
                {
                    s_CachedTypes.Add(typeName, type);

                    return type;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取已加载的程序集中的所有类型
        /// </summary>
        /// <returns>已加载的程序集中的所有类型</returns>

        public static Type[] GetTypes()
        {
            List<Type> list = new List<Type>();

            foreach (Assembly assembly in s_Assemblies)
            {
                list.AddRange(assembly.GetTypes());
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获取同一程序集中继承自某个类的所有类型
        /// </summary>
        /// <param name="baseType">父类类型</param>
        /// <returns>已加载的程序集中继承自某个类的所有类型</returns>

        public static Type[] GetTypesSubclass(Type baseType)
        {
            List<Type> list = new List<Type>();

            foreach (Assembly assembly in s_Assemblies)
            {
                if (baseType.Assembly == assembly)
                {
                    Type[] types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        if (type.IsSubclassOf(baseType))
                        {
                            list.Add(type);
                        }
                    }
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获取已加载的程序集中的所有类型
        /// </summary>
        /// <param name="results">已加载的程序集中的所有类型</param>

        public static void GetTypes(List<Type> results)
        {
            if (results == null)
            {
                throw new Exception("Results is invalid.");
            }

            results.Clear();

            foreach (Assembly assembly in s_Assemblies)
            {
                results.AddRange(assembly.GetTypes());
            }
        }
    }
}