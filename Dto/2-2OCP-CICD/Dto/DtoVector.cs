using System;
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

    public static float Distance(DtoVector a, DtoVector b)
    {
        float dx = b.x - a.x;
        float dy = b.y - a.y;
        float dz = b.z - a.z;
        float dw = b.w - a.w;

        return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz + dw * dw);
    }
}