using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode), System.Serializable]
public class DtoVector : DtoBase
{
    [MarshalAs(UnmanagedType.R4)]
    public float x;
    [MarshalAs(UnmanagedType.R4)]
    public float y;
    [MarshalAs(UnmanagedType.R4)]
    public float z;
    [MarshalAs(UnmanagedType.R4)]
    public float w;
}