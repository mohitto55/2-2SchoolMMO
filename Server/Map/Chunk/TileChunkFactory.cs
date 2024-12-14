using Server.Debug;
using Server.Map.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Map.Chunk
{
    /// <summary>
    /// 타일맵을 관리하는 청크를 만드는 클래스
    /// </summary>
    [System.Serializable]
    public class TileChunkFactory : ChunkFactory
    {
        private List<DtoTile> _tileDatas;

        public TileChunkFactory(EMapObjectType chunkType) : base(chunkType)
        {
        }
        public override void CreateChunk(string mapFilePath, Dictionary<DtoVector, Dictionary<EMapObjectType, DtoChunk>> chunkMap)
        {
            _tileDatas = LoadFromJson<DtoTile>(mapFilePath);
            ServerDebug.Log(LogType.Log, _tileDatas.Count + "개의 타일을 생성 합니다.");
            int maxTileCount = MapUtility.ChunkSize * MapUtility.ChunkSize;
            foreach (var tile in _tileDatas)
            {
                // 타일의 좌표를 사용해 청크 ID 계산
                DtoVector chunkPosition = MapUtility.GetChunkPosition(tile.position);

                // 해당 청크가 없으면 새로 생성
                if (!chunkMap.ContainsKey(chunkPosition))
                {
                    chunkMap.Add(chunkPosition, new Dictionary<EMapObjectType, DtoChunk>());
                }

                if (!chunkMap[chunkPosition].ContainsKey(_chunkType))
                {
                    chunkMap[chunkPosition].Add(_chunkType, new DtoTileChunk() { dtoTiles = new DtoTileData[maxTileCount] });
                }

                DtoTileChunk chunk = (DtoTileChunk)(chunkMap[chunkPosition][_chunkType]);
                chunk.chunkPosition = chunkPosition;

                // 청크에 타일 추가
                int tileCount = chunk.tileCount;

                if (tileCount >= maxTileCount)
                {
                    ServerDebug.Log(LogType.Warning, chunk.chunkPosition.x + " " + chunk.chunkPosition.y + "청크의 최대 타일 갯수 : " + maxTileCount + " 를 초과해서 타일을 추가하려했습니다. " +
                        "n타일 좌표 : " + tile.position.x + " ," + tile.position.y);
                    continue;
                }

                chunk.dtoTiles[tileCount] = new DtoTileData() { id = tile.id, x = tile.position.x, y = tile.position.y };
                chunk.tileCount += 1;
            }
        }

    }
}
