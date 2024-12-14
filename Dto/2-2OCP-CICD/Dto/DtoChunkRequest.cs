using System.Runtime.InteropServices;


[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoChunkRequest : DtoBase
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string mapName;

    public DtoVector position;

    [MarshalAs(UnmanagedType.R4)]
    public float surroundDst = 1;
}

