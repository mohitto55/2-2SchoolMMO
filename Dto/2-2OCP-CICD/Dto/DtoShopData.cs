using System.Runtime.InteropServices;
using System;
using System.Reflection;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
public class DtoShopData : DtoBase
{
    [MarshalAs(UnmanagedType.I4)]
    public int itemCount;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public DtoItem[] shopItems;

    public DtoShopData(DtoItem[] shopItems)
    {
        /// 이거 어디 담아놨다가 쓸 수 있게 나중에 변경하면 좋을 것 같다.
        /// Shop이야 플레이어가 어쩌다 한번 패킷보내는 거니 상관없긴한데 다른 경우에선 사용하기 힘들 것 같다.
        int arrSize = GetType().GetField("shopItems").GetCustomAttribute<MarshalAsAttribute>().SizeConst;
        this.shopItems = new DtoItem[arrSize];
        itemCount = shopItems.Length;
        for (int i = 0; i < shopItems.Length; i++)
        {
            this.shopItems[i] = shopItems[i];
        }
    }
}
