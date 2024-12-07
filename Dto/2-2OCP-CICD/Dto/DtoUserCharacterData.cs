using System.Runtime.InteropServices;


[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoCharacter
{
    [MarshalAs(UnmanagedType.I4, SizeConst = 50)]
    public int uid; // 캐릭터 고유 ID
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string name; // 캐릭터 이름
    [MarshalAs(UnmanagedType.U4, SizeConst = 50)]
    public uint level; // 캐릭터 레벨
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string characterClass; // 직업
    [MarshalAs(UnmanagedType.R4, SizeConst = 50)]
    public float experience; // 경험치
    [MarshalAs(UnmanagedType.R4, SizeConst = 50)]
    public float gold; // 골드
    [MarshalAs(UnmanagedType.I4, SizeConst = 50)]
    public int map; // 맵 ID
    [MarshalAs(UnmanagedType.I4, SizeConst = 50)]
    public DtoVector position; // 맵 ID
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoUserCharacterData : DtoBase
{
    [MarshalAs(UnmanagedType.I4, SizeConst = 100)]
    public int characterCount; // 캐릭터 수
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public DtoCharacter[] characters; // 캐릭터 배열
}

