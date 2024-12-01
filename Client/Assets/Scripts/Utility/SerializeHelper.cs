using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public static class SerializeHelper
{
    public static byte[] StructureToByte(object structure)
    {
        int size = Marshal.SizeOf(structure);
        byte[] arr = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.StructureToPtr(structure, ptr, true);
        Marshal.Copy(ptr, arr, 0, size);
        Marshal.FreeHGlobal(ptr);
        return arr;
    }
    public static T ByteToStructure<T>(byte[] buffer)
    {
        int size = Marshal.SizeOf(typeof(T));

        if (size > buffer.Length)
        {
            throw new Exception($"[Warning] Buffer size is more than {typeof(T).Name} size");
        }

        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.Copy(buffer, 0, ptr, size);

        object? obj = Marshal.PtrToStructure(ptr, typeof(T));

        if (obj == null)
        {
            throw new Exception($"[Warning] obj is null reference");
        }

        Marshal.FreeHGlobal(ptr);

        return (T)obj;
    }

    public static string ToJson<T>(T data)
    {
        return JsonConvert.SerializeObject(data);
    }
    public static T FromJson<T>(string data)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
        catch (Exception ex)
        {
            throw new Exception("직렬화 오류! : " + ex);
        }
    }
    public static List<T> JsonToList<T>(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log(json);
            return null;
        }
    }

    public static string ByteToString(byte[] data)
    {
        return Encoding.UTF8.GetString(data);
    }
    public static byte[] StringToByte(string data)
    {
        return Encoding.UTF8.GetBytes(data);
    }
    public static byte[] DataToByte<T>(T data)
    {
        string json = ToJson(data);

        return Encoding.UTF8.GetBytes(json);
    }
    public static T ByteToData<T>(byte[] data)
    {
        string json = ByteToString(data);

        return FromJson<T>(json);
    }


}