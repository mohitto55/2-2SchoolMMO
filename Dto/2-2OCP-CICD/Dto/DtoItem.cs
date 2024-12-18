using System.Runtime.InteropServices;
using System;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public struct DtoItem
{
    [MarshalAs(UnmanagedType.I4)]
    public int itemId;
    [MarshalAs(UnmanagedType.I4)]
    public int maxCount;
    [MarshalAs(UnmanagedType.I4)]
    public int count;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string name;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string desc;
    [MarshalAs(UnmanagedType.I4)]
    public int cost;
}
