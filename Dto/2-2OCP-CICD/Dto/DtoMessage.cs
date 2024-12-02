using System.Runtime.InteropServices;


[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoMessage : DtoBase
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string message;
}

