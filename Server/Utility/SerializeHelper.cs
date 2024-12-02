using System;
using System.Runtime.InteropServices;

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

}