using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoTileChunk : DtoChunk
{
    [MarshalAs(UnmanagedType.I4)]
    public int tileCount;
    /// <summary>
    /// 클래스는 참조 유형이므로 Marshal.PtrToStructure를 사용하면 Subdata 위치에 포인터와 값을 복사하지 않습니다.
    //Subdata를 struct로 선언하면 subdata의 실제 값이 복사됩니다.
    //따라서 Marshalling을 수행할 때는 struct를 사용해야 합니다.생성자에서 struct 버전을 사용하는 클래스도 있을 수 있습니다.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public DtoTileData[] dtoTiles;
}
