using System;
using System.IO;



public static partial class Utility
{
    /// <summary>
    /// 路径相关的实用函数
    /// </summary>
    public static class ZPath
    {
        /// <summary>
        /// 获取连接后的路径
        /// </summary>
        /// <param name="path">路径片段</param>
        /// <returns>连接后的路径</returns>

        public static string GetCombinePath(params string[] path)
        {
            if ((path == null) || (path.Length < 1))
            {
                return null;
            }

            string str = path[0];

            for (int i = 1; i < path.Length; i++)
            {
                str = Path.Combine(str, path[i]);
            }

            return GetRegularPath(str);
        }

        /// <summary>
        /// 获取规范的路径
        /// </summary>
        /// <param name="path">要规范的路径</param>
        /// <returns>规范的路径</returns>

        public static string GetRegularPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            else
            {
                return path.Replace('\\', '/');
            }
        }

        /// <summary>
        /// 获取远程格式的路径(带有file:// 或 http:// 前缀)
        /// </summary>
        /// <param name="path">原始路径</param>
        /// <returns>远程格式路径</returns>

        public static string GetRemotePath(params string[] path)
        {
            string combinePath = GetCombinePath(path);

            if (combinePath == null)
            {
                return null;
            }

            if (!combinePath.Contains("://"))
            {
                return ("file:///" + combinePath).Replace("file:////", "file:///");
            }

            return combinePath;
        }

        /// <summary>
        /// 获取带有 CRC32 和后缀的资源名
        /// </summary>
        /// <param name="resourceName">原始资源名</param>
        /// <param name="hashCode">CRC32 哈希值</param>
        /// <returns>带有 CRC32 和后缀的资源名</returns>

        public static string GetResourceNameWithCrc32AndSuffix(string resourceName, int hashCode)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                throw new Exception("Resource name is invalid.");
            }

            /* {1:x8}表示第二个参数(hashCode) 8位用16进制表示  例:hashCode = 32 00000020.dat */
            return Utility.ZText.Format("{0}.{1:x8}.dat", resourceName, hashCode);
        }

        /// <summary>
        /// 获取带有后缀的资源名
        /// </summary>
        /// <param name="resourceName">原始资源名</param>
        /// <returns>带有后缀的资源名</returns>

        public static string GetResourceNameWithSuffix(string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                throw new Exception("Resource name is invalid.");
            }

            return Utility.ZText.Format("{0}.dat", resourceName);
        }

        /// <summary>
        /// 移除空文件夹
        /// </summary>
        /// <param name="directoryName">要处理的文件夹名称</param>
        /// <returns>是否移除空文件夹成功</returns>

        public static bool RemoveEmptyDirectory(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
            {
                throw new Exception("Directory name is invalid.");
            }

            try
            {
                if (!Directory.Exists(directoryName))
                {
                    return false;
                }

                string[] directories = Directory.GetDirectories(directoryName, "*");

                int length = directories.Length;

                string[] strArray = directories;

                for (int i = 0; i < strArray.Length; i++)
                {
                    if (RemoveEmptyDirectory(strArray[i]))
                    {
                        length--;
                    }
                }

                if (length > 0)
                {
                    return false;
                }

                if (Directory.GetFiles(directoryName, "*").Length != 0)
                {
                    return false;
                }

                Directory.Delete(directoryName);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}