using System.Runtime.InteropServices;
using System;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoInventoryItem : DtoBase
{
    [MarshalAs(UnmanagedType.I4)]
    public int inventoryId;
    [MarshalAs(UnmanagedType.I4)]
    public int inventorySlot;
    [MarshalAs(UnmanagedType.I4)]
    public int userUid;
    [MarshalAs(UnmanagedType.I4)]
    public int itemId;
    [MarshalAs(UnmanagedType.I4)]
    public int count;
}