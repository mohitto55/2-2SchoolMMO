using System.Runtime.InteropServices;


[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoCharacter
{
    [MarshalAs(UnmanagedType.I4, SizeConst = 50)]
    public int uid; // ĳ���� ���� ID
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string name; // ĳ���� �̸�
    [MarshalAs(UnmanagedType.U4, SizeConst = 50)]
    public uint level; // ĳ���� ����
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string characterClass; // ����
    [MarshalAs(UnmanagedType.R4, SizeConst = 50)]
    public float experience; // ����ġ
    [MarshalAs(UnmanagedType.R4, SizeConst = 50)]
    public float gold; // ���
    [MarshalAs(UnmanagedType.I4, SizeConst = 50)]
    public int map; // �� ID
    [MarshalAs(UnmanagedType.I4, SizeConst = 50)]
    public DtoVector position; // �� ID
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoUserCharacterData : DtoBase
{
    [MarshalAs(UnmanagedType.I4, SizeConst = 100)]
    public int characterCount; // ĳ���� ��
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public DtoCharacter[] characters; // ĳ���� �迭
}

