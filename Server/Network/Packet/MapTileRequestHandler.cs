using Server.Debug;
using Server.Map;

public class MapTileRequestHandler : PacketHandler<DtoMessage>
{
    public MapTileRequestHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoMessage data)
    {

    }

    protected override void OnSuccess(DtoMessage data)
    {
        List<DtoTile> tiles = MapManager.GetMapTiles(data.message);
        ServerDebug.Log(LogType.Log, tiles.Count.ToString());
        var handler = PacketHandlerPoolManager.GetPacketHandler(EHandleType.MapTileResponse);
        DtoMapTile dtoTile = new DtoMapTile();
        handler.Init(dtoTile, m_id);
        List<DtoTileData> tileDatas = new List<DtoTileData>();
        foreach (var tile in tiles)
        {
            DtoTileData tileData = new DtoTileData();
            tileData.id = tile.id;
            tileData.x = tile.position.x;
            tileData.y = tile.position.y;
            tileData.moveable = tile.moveable;
            tileDatas.Add(tileData);
        }
        dtoTile.count = tileDatas.Count;
        dtoTile.dtoTiles = tileDatas;
        IOCPServer.SendClient(m_id, handler);
    }
}