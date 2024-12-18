using Runtime.DB.Model;
using UnityEngine;

public class ShopItemResponseHandler : PacketHandler<DtoShopData>
{
    public ShopItemResponseHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoShopData data)
    {
    }

    protected override void OnSuccess(DtoShopData data)
    {
        ShopView shopView;
        
        if (UIView.TryGetView("Shop", out shopView)){
            for (int i = 0; i < data.itemCount; i++)
            {
                Debug.Log("›o »ý¼º " + data.shopItems[i].name);
                shopView.SetSlotModel(i, data.shopItems[i]);
            }
        }
    }
}
