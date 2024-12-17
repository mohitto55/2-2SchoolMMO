using System.Runtime.InteropServices;
using System;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public struct DtoInventoryItem
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

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoInventoryItemData : DtoBase
{
    [MarshalAs(UnmanagedType.I4, SizeConst = 100)]
    public int slotCount; 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public DtoInventoryItem[] slotItems;
}
