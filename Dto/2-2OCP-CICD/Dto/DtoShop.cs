using System.Runtime.InteropServices;
using System;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public struct DtoShop
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string npcUID;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public int[] itemIDTable;
}
