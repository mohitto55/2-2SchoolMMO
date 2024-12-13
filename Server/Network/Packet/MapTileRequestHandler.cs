using Server.Debug;
using Server.Map;

public class MapTileRequestHandler : PacketHandler<DtoChunkRequest>
{
    public MapTileRequestHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoChunkRequest data)
    {

    }

    protected override void OnSuccess(DtoChunkRequest data)
    {
        string mapName = data.mapName;
        DtoVector position = data.position;
        int surroundDst = data.surroundDst;

        List<DtoChunk> chunks = MapManager.GetSurroundChunks(mapName, position, surroundDst);
        ServerDebug.Log(LogType.Log, chunks.Count.ToString() + "청크 총 갯수");
        foreach (var chunk in chunks)
        {
            if (chunk == null)
            {
                ServerDebug.Log(LogType.Log, mapName);
                continue;
            }

            var handler = PacketHandlerPoolManager.GetPacketHandler(EHandleType.MapTileResponse);
            ServerDebug.Log(LogType.Log, chunk.tileCount + "Tile 총 갯수");

            handler.Init(chunk, m_id);
            IOCPServer.SendClient(m_id, handler);
        }
    }
}