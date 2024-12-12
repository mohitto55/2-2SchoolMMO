using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoTile : DtoBase
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]  // ���� ������ ���ڿ�
    public string id;

    public DtoVector position;

    [MarshalAs(UnmanagedType.Bool)]  // ���� boolean ��
    public bool moveable;
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public struct DtoTileData
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string id;
    [MarshalAs(UnmanagedType.R4)]
    public float x;
    [MarshalAs(UnmanagedType.R4)]
    public float y;
    [MarshalAs(UnmanagedType.Bool)]
    public bool moveable;
}
