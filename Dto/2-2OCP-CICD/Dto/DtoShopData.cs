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
        /// �̰� ��� ��Ƴ��ٰ� �� �� �ְ� ���߿� �����ϸ� ���� �� ����.
        /// Shop�̾� �÷��̾ ��¼�� �ѹ� ��Ŷ������ �Ŵ� ��������ѵ� �ٸ� ��쿡�� ����ϱ� ���� �� ����.
        int arrSize = GetType().GetField("shopItems").GetCustomAttribute<MarshalAsAttribute>().SizeConst;
        this.shopItems = new DtoItem[arrSize];
        itemCount = shopItems.Length;
        for (int i = 0; i < shopItems.Length; i++)
        {
            this.shopItems[i] = shopItems[i];
        }
    }
}
