using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoAccount : DtoBase
{
    [MarshalAs(UnmanagedType.U2)]
    public string id;
    [MarshalAs(UnmanagedType.U2)]
    public string password;

    public DtoAccount(string id, string password)
    {
        this.id = id;
        this.password = password;
    }
}
