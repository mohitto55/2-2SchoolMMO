using System.Linq;

public class MapTileResponseHandler : PacketHandler<DtoMapTile>
{
    public MapTileResponseHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoMapTile data)
    {

    }

    protected override void OnSuccess(DtoMapTile data)
    {
        Debug.Log("Ä«¿îÆ® " + data.count);
        MapGenerator.Instance.GenerateMap(data.dtoTiles.ToList<DtoTileData>());
    }
}