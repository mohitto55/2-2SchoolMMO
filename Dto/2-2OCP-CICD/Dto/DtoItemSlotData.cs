using System.Runtime.InteropServices;
using System;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public struct DtoItemSlot
{
    [MarshalAs(UnmanagedType.I4)]
    public int slotIndex;

    public DtoItem item;
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoItemSlotData : DtoBase
{
    [MarshalAs(UnmanagedType.I4)]
    public int slotCount; 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
    public DtoItemSlot[] slotItems;
}
