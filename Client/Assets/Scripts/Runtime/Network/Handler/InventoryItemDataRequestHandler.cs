using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class InventoryItemDataRequestHandler : PacketHandler<DtoMessage>
{
    public InventoryItemDataRequestHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoMessage data)
    {

    }

    protected override void OnSuccess(DtoMessage data)
    {
    }
}