using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoTransform : DtoEntityBase
{
    public DtoVector dtoPosition;
}