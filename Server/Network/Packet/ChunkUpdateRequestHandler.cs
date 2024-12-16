using Server.Debug;
using Server.Map;

public class ChunkUpdateRequestHandler : PacketHandler<DtoChunkRequest>
{
    public ChunkUpdateRequestHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoChunkRequest data)
    {

    }

    protected override void OnSuccess(DtoChunkRequest data)
    {
        string mapName = data.mapName;
        DtoVector position = data.position;
        float surroundDst = data.surroundDst;

        List<DtoTileChunk> chunks = MapManager.GetSurroundChunks<DtoTileChunk>(EMapObjectType.Tile, mapName, position, surroundDst);
        
        foreach (var chunk in chunks)
        {
            if (chunk == null)
            {
                ServerDebug.Log(LogType.Log, mapName);
                continue;
            }
            var handler = PacketHandlerPoolManager.GetPacketHandler(EHandleType.ChunkUpdateResponse);
            handler.Init(chunk, m_id);
            IOCPServer.SendClient(m_id, handler);
        }
    }
}