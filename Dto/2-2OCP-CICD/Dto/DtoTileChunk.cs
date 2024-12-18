using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoTileChunk : DtoChunk
{
    [MarshalAs(UnmanagedType.I4)]
    public int tileCount;
    /// <summary>
    /// Ŭ������ ���� �����̹Ƿ� Marshal.PtrToStructure�� ����ϸ� Subdata ��ġ�� �����Ϳ� ���� �������� �ʽ��ϴ�.
    //Subdata�� struct�� �����ϸ� subdata�� ���� ���� ����˴ϴ�.
    //���� Marshalling�� ������ ���� struct�� ����ؾ� �մϴ�.�����ڿ��� struct ������ ����ϴ� Ŭ������ ���� �� �ֽ��ϴ�.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public DtoTileData[] dtoTiles;
}
