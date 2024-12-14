using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoAccount : DtoBase
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string username;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string id;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string password;

    public DtoAccount()
    {
        this.username = null;
        this.id = null;
        this.password = null;
    }
    public DtoAccount(string username, string id, string password)
    {
        this.id = id;
        this.password = password;
        this.username = username;
    }
}

