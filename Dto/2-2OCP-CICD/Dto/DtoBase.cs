using System.Runtime.InteropServices;
using System;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoBase
{
    [MarshalAs(UnmanagedType.U2)]
    public UInt16 errorCode;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string errorMessage;
}