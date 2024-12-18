using System.Runtime.InteropServices;
using System;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public struct DtoNPC
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string npcUID;
    [MarshalAs(UnmanagedType.I4)]
    public EInteractionType interactionType;
}
