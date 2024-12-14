using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoChunk : DtoBase
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string chunkType;

    public DtoVector chunkPosition;
}

