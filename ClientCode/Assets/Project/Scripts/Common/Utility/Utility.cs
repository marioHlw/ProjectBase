using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

/// <summary>
/// 实用函数集
/// 1.服务器传过来的时候，是北京时间，所以从8点开始算
/// </summary>

public static partial class Utility
{
    private static StringBuilder Formater = new StringBuilder(1024);
    private static MD5CryptoServiceProvider MD5Provider;

    public static void Clear()
    {
        Formater.Remove(0, Formater.Length);
    }

    /// <summary>
    /// 获取本地当前时间戳
    /// </summary>
    /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>
    /// <returns>当前时间戳</returns>

    public static long GetTimeStamp(bool bflag = true)
    {
        TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0, 0);

        long ret;

        if (bflag)
        {
            ret = Convert.ToInt64(ts.TotalSeconds);
        }
        else
        {
            ret = Convert.ToInt64(ts.TotalMilliseconds);
        }

        return ret;
    }

    /// <summary>
    /// c#时间转unix时间的毫秒数
    /// </summary>
    /// <param name="dateTime">c#时间</param>
    /// <returns>转unix时间的毫秒数</returns>

    public static long DateTimeToUnixTimestampMilliseconds(DateTime dateTime)
    {
        var start = new DateTime(1970, 1, 1, 8, 0, 0, dateTime.Kind);

        return Convert.ToInt64((dateTime - start).TotalMilliseconds);
    }

    /// <summary>
    /// c#时间转unix时间的秒数
    /// </summary>
    /// <param name="dateTime">c#时间</param>
    /// <returns>unix时间的秒数</returns>

    public static long DateTimeToUnixTimestampSeconds(DateTime dateTime)
    {
        var start = new DateTime(1970, 1, 1, 8, 0, 0, dateTime.Kind);

        return Convert.ToInt64((dateTime - start).TotalSeconds);
    }

    /// <summary>
    /// unix时间戳（毫秒数）转c#时间
    /// </summary>
    /// <param name="milliseconds">unix时间戳（毫秒数）</param>
    /// <returns>c#时间</returns>

    public static DateTime UnixTimestampMillisecondsToDateTime(long milliseconds)
    {
        var start = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Local);

        return start.AddMilliseconds(milliseconds);
    }

    /// <summary>
    /// unix时间戳（秒数）转c#时间 
    /// </summary>
    /// <param name="seconds">unix时间戳（秒数）</param>
    /// <returns>c#时间</returns>

    public static DateTime UnixTimestampSecondsToDateTime(long seconds)
    {
        var start = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Local);

        return start.AddSeconds(seconds);
    }

    public static string GetMd5(string str)
    {
        return BitConverter.ToString(MD5Provider.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", string.Empty);
    }

    public static string ASCIIBytesToString(byte[] data)
    {
        if (data == null)
        {
            return null;
        }
        try
        {
            return Encoding.ASCII.GetString(data).TrimEnd(new char[1]);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string BytesToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes).TrimEnd(new char[1]);
    }

    public static string UTF8BytesToString(ref byte[] str)
    {
        try
        {
            return ((str == null) ? null : Encoding.UTF8.GetString(str).TrimEnd(new char[1]));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static void StringToUTF8Bytes(string str, ref byte[] buffer)
    {
        if ((str != null) && (buffer != null))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);

            if (bytes.Length >= buffer.Length)
            {
                FillErrorCodeToBuf(ref buffer);
            }
            else
            {
                Buffer.BlockCopy(bytes, 0, buffer, 0, bytes.Length);

                buffer[bytes.Length] = 0;
            }
        }
    }

    public static void StringToUTF8Bytes(string str, ref sbyte[] buffer)
    {
        if ((str != null) && (buffer != null))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);

            if (bytes.Length >= buffer.Length)
            {
                FillErrorCodeToSBuf(ref buffer);
            }
            else
            {
                Buffer.BlockCopy(bytes, 0, buffer, 0, bytes.Length);

                buffer[bytes.Length] = 0;
            }
        }
    }

    private static void FillErrorCodeToBuf(ref byte[] buffer)
    {
        try
        {
            buffer[0] = 0x4f;
            buffer[1] = 0x56;
            buffer[2] = 0x45;
            buffer[3] = 0x52;
            buffer[4] = 70;
            buffer[5] = 0x4c;
            buffer[6] = 0x4f;
            buffer[7] = 0x57;
            buffer[8] = 0x30;
            buffer[9] = 0x58;
            buffer[10] = 0x43;
            buffer[11] = 0x43;
            buffer[12] = 0x43;
            buffer[13] = 0x43;
            buffer[14] = 0x43;
            buffer[15] = 0x43;
            buffer[0x10] = 0;
        }
        catch (Exception)
        {
        }
    }

    private static void FillErrorCodeToSBuf(ref sbyte[] buffer)
    {
        try
        {
            buffer[0] = 0x4f;
            buffer[1] = 0x56;
            buffer[2] = 0x45;
            buffer[3] = 0x52;
            buffer[4] = 70;
            buffer[5] = 0x4c;
            buffer[6] = 0x4f;
            buffer[7] = 0x57;
            buffer[8] = 0x30;
            buffer[9] = 0x58;
            buffer[10] = 0x43;
            buffer[11] = 0x43;
            buffer[12] = 0x43;
            buffer[13] = 0x43;
            buffer[14] = 0x43;
            buffer[15] = 0x43;
            buffer[0x10] = 0;
        }
        catch (Exception)
        {
        }
    }
}