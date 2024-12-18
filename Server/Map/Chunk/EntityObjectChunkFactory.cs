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
    /// 맵에 존재하는 정적? 오브젝트 관리하는 청크를 만드는 클래스
    /// </summary>
    public class EntityObjectChunkFactory : ChunkFactory
    {
        private List<DtoObjectInfo> _objectDatas;

        public EntityObjectChunkFactory(EMapObjectType chunkType) : base(chunkType)
        {
        }

        public override void CreateChunk(string mapFilePath, Dictionary<DtoVector, Dictionary<EMapObjectType, DtoChunk>> chunkMap)
        {
            _objectDatas = LoadFromJson<DtoObjectInfo>(mapFilePath);

            ServerDebug.Log(LogType.Log, _objectDatas.Count + "개의 오브젝트를 생성합니다.");
            int maxTileCount = MapUtility.ChunkSize * MapUtility.ChunkSize;
            foreach (var entity in _objectDatas)
            {
                // 타일의 좌표를 사용해 청크 ID 계산
                DtoVector chunkPosition = MapUtility.GetChunkPosition(entity.position);

                if ((EEntityType)Enum.Parse(typeof(EEntityType), entity.entityType) == EEntityType.Npc)
                {
                    ObjectManager.CreateNPC(entity.position, entity.entityID);
                }

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

                // 청크에 오브젝트 추가
                int tileCount = chunk.tileCount;

                if (tileCount >= maxTileCount)
                {
                    ServerDebug.Log(LogType.Warning, chunk.chunkPosition.x + " " + chunk.chunkPosition.y + "청크의 최대 타일 갯수 : " + maxTileCount + " 를 초과해서 타일을 추가하려했습니다. " +
                        "n타일 좌표 : " + entity.position.x + " ," + entity.position.y);
                    continue;
                }

                //chunk.dtoTiles[tileCount] = new DtoTileData() { id = tile.id, x = tile.position.x, y = tile.position.y };
                chunk.tileCount += 1;
            }
        }
    }
}
