using System.Runtime.InteropServices;
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoObjectInfo : DtoEntityBase
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string entityType;

    public DtoVector position;
}