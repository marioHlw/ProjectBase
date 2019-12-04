/**************************
 * 文件名:Utility.ZJson.cs
 * 文件描述:JSON相关的实用函数
 * 创建日期:2019/11/11
 * 作者:ZB
 ***************************/



using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;

public static partial class Utility
{
    public static class ZJson
    {
        // 对象转换成字节数组
        public static byte[] ToJsonData(object obj)
        {
            return ZConverter.GetBytes(ToJson(obj));
        }

        // 对象转换成字符串
        // absolutePath：存储绝对路径
        public static string ToJson(object obj, string absolutePath = "")
        {
            string str;

            try
            {
                //str = JsonUtility.ToJson(obj);

                using (var ms = new MemoryStream())
                {
                    new DataContractJsonSerializer(obj.GetType()).WriteObject(ms, obj);
                    str = Encoding.UTF8.GetString(ms.ToArray());
                }

                if (!string.IsNullOrEmpty(absolutePath))
                {
                    if (File.Exists(absolutePath))
                    {
                        File.Delete(absolutePath);
                    }
                    else
                    {
                        string directory = Path.GetDirectoryName(absolutePath);

                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                    }

                    FileStream fileStream = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write);

                    byte[] bytes = ZConverter.GetBytes(str);

                    fileStream.Write(bytes, 0, bytes.Length);

                    fileStream.Close();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(ZText.Format("Can not convert to JSON with exception '{0}'.", exception.ToString()), exception);
            }
            return str;
        }

        // 绝对路径转换成对象
        public static T ToObjectFromPath<T>(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
            {
                return default;
            }

            if (File.Exists(absolutePath))
            {
                return default;
            }

            FileStream fileStream = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write);
            byte[] _bytes = new byte[fileStream.Length];
            fileStream.Read(_bytes, 0, (int)fileStream.Length);
            fileStream.Close();
            return ToObject<T>(_bytes);
        }

        // 字节数组转换成对象
        public static T ToObject<T>(byte[] jsonData)
        {
            return ToObject<T>(ZConverter.GetString(jsonData));
        }

        // 字符串转换成对象
        public static T ToObject<T>(string json)
        {
            T local;

            try
            {
                local = JsonUtility.FromJson<T>(json);
            }
            catch (Exception exception)
            {
                if (exception is Exception)
                {
                    throw;
                }

                throw new Exception(ZText.Format("Can not convert to object with exception '{0}'.", exception.ToString()), exception);
            }

            return local;
        }

        // 字节数组转换成对象
        public static object ToObject(Type objectType, byte[] jsonData)
        {
            return ToObject(objectType, ZConverter.GetString(jsonData));
        }

        // 字符串转换成对象
        public static object ToObject(Type objectType, string json)
        {
            object obj2;

            if (objectType == null)
            {
                throw new Exception("Object type is invalid.");
            }

            try
            {
                obj2 = JsonUtility.FromJson(json, objectType);
            }
            catch (Exception exception)
            {
                if (exception is Exception)
                {
                    throw;
                }

                throw new Exception(ZText.Format("Can not convert to object with exception '{0}'.", exception.ToString()), exception);
            }

            return obj2;
        }
    }
}