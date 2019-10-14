using System;
using System.Text;



public static partial class Utility
{
    /// <summary>
    /// 字符相关的实用函数
    /// </summary>

    public static class ZText
    {
        /// <summary>
        /// 创建每个线程的静态变量
        /// </summary>

        [ThreadStatic]
        private static StringBuilder s_CachedStringBuilder = new StringBuilder(0x400); //1024

        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="format">字符串格式</param>
        /// <param name="arg0">字符串参数 0</param>
        /// <returns>格式化后的字符串</returns>

        public static string Format(string format, object arg0)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            s_CachedStringBuilder.Length = 0;

            s_CachedStringBuilder.AppendFormat(format, arg0);

            return s_CachedStringBuilder.ToString();
        }

        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="format">字符串格式</param>
        /// <param name="arg0">字符串参数 0</param>
        /// <param name="arg1">字符串参数 1</param>
        /// <returns>格式化后的字符串</returns>

        public static string Format(string format, object arg0, object arg1)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            s_CachedStringBuilder.Length = 0;

            s_CachedStringBuilder.AppendFormat(format, arg0, arg1);

            return s_CachedStringBuilder.ToString();
        }

        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="format">字符串格式</param>
        /// <param name="arg0">字符串参数 0</param>
        /// <param name="arg1">字符串参数 1</param>
        /// <param name="arg2">字符串参数 2</param>
        /// <returns>格式化后的字符串</returns>

        public static string Format(string format, object arg0, object arg1, object arg2)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            s_CachedStringBuilder.Length = 0;

            s_CachedStringBuilder.AppendFormat(format, arg0, arg1, arg2);

            return s_CachedStringBuilder.ToString();
        }

        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="format">字符串格式</param>
        /// <param name="args">字符串参数</param>
        /// <returns>格式化后的字符串</returns>

        public static string Format(string format, params object[] args)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            if (args == null)
            {
                throw new Exception("Args is invalid.");
            }

            s_CachedStringBuilder.Length = 0;

            s_CachedStringBuilder.AppendFormat(format, args);

            return s_CachedStringBuilder.ToString();
        }

        /// <summary>
        /// 根据类型和名称获取完整名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">名称</param>
        /// <returns>完整名称</returns>

        public static string GetFullName<T>(string name)
        {
            return GetFullName(typeof(T), name);
        }

        /// <summary>
        /// 根据类型和名称获取完整名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="name">名称</param>
        /// <returns>完整名称</returns>

        public static string GetFullName(Type type, string name)
        {
            if (type == null)
            {
                throw new Exception("Type is invalid.");
            }

            string fullName = type.FullName;

            if (!string.IsNullOrEmpty(name))
            {
                return Format("{0}.{1}", fullName, name);
            }

            return fullName;
        }
    }
}