using ProtoBuf;
using System;
using System.IO;



public static partial class Utility
{
    /// <summary>
    /// Protobuf 相关的使用函数
    /// </summary>
    public static class ZProtobuf
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="absolutePath">完整保存文件路径</param>
        public static void Serialize<T>(T data, string absolutePath)
        {
            try
            {
                if (null == data) return;

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

                Serializer.Serialize(fileStream, data);

                fileStream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 文件路径反序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="absolutePath">文件完整路径</param>
        /// <returns></returns>
        public static T Deserialize<T>(string absolutePath)
        {
            if (!File.Exists(absolutePath))
            {
                return default(T);
            }
            try
            {
                FileStream fileStream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                T data = Serializer.Deserialize<T>(fileStream);

                fileStream.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 字节数组数据反序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="bytes">数据字节数组</param>
        /// <returns></returns>
        public static T DeserializeBinary<T>(byte[] bytes)
        {
            if (null == bytes)
            {
                return default(T);
            }

            try
            {
                Stream steam = new MemoryStream(bytes);

                T data = Serializer.Deserialize<T>(steam);

                steam.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}