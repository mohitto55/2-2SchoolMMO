using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoEntityObject : DtoBase
{
    [MarshalAs(UnmanagedType.I4)]
    public int entityID;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string entityType;

    public DtoVector position;
}

