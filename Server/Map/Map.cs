using Newtonsoft.Json;
using Server.Debug;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Map
{
    
    public class Map
    {
        private List<DtoTile> _tileDatas;
        private string _name;
        public string Name => _name;
        Dictionary<DtoVector, DtoChunk> chunkMap = new Dictionary<DtoVector, DtoChunk>();
        public Map(string mapName, List<DtoTile> tileDatas) {
            _name = mapName;

            chunkMap = new Dictionary<DtoVector, DtoChunk>(new VectorCompare());

            _tileDatas = tileDatas;
            ServerDebug.Log(LogType.Log, tileDatas.Count + "개의 타일을 생성 합니다.");

            int maxTileCount = MapUtility.ChunkSize * MapUtility.ChunkSize;
            foreach (var tile in _tileDatas)
            {
                // 타일의 좌표를 사용해 청크 ID 계산
                DtoVector chunkId = MapUtility.GetChunkPosition(tile.position);

                // 해당 청크가 없으면 새로 생성
                if (!chunkMap.ContainsKey(chunkId))
                {
                    chunkMap.Add(chunkId, new DtoChunk());
                    chunkMap[chunkId].chunkID = chunkId;
                    chunkMap[chunkId].dtoTiles = new DtoTileData[maxTileCount];
                }

                // 청크에 타일 추가
                int tileCount = chunkMap[chunkId].tileCount;

                if (tileCount >= maxTileCount)
                {
                    ServerDebug.Log(LogType.Warning, chunkId.x + " " + chunkId.y + "청크의 최대 타일 갯수 : " + maxTileCount + " 를 초과해서 타일을 추가하려했습니다. " +
                        "n타일 좌표 : " + tile.position.x + " ," + tile.position.y);
                    continue;
                }

                DtoTileData tileData = new DtoTileData() { id = tile.id, x = tile.position.x, y = tile.position.y };
                chunkMap[chunkId].dtoTiles[tileCount] = tileData;
                chunkMap[chunkId].tileCount += 1;
            }
        }

        /// <summary>
        /// position에서 부터 surroundSize 거리 내부에 있는 Chunks들을 찾습니다.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="surroundSize"></param>
        /// <returns></returns>

        public List<DtoChunk> GetSurroundChunks(DtoVector position, float surroundDst = 1)
        {
            DtoVector center = MapUtility.GetChunkPosition(position);
            int surroundChunkSize = (int)Math.Max(1, surroundDst / MapUtility.ChunkSize) + 1;

            List<DtoChunk> surroundChunks = new List<DtoChunk>();
            for (int y = (int)(center.y - surroundChunkSize); y < center.y + surroundChunkSize; y++)
            {
                for (int x = (int)(center.x - surroundChunkSize); x < center.x + surroundChunkSize; x++)
                {
                    DtoVector dtoVector = new DtoVector() { x = x, y = y };
                    if (!chunkMap.ContainsKey(dtoVector))
                        continue;

                    DtoChunk searchChunk = chunkMap[dtoVector];

                    if (!MapUtility.IsChunkInLoadChunk(position, searchChunk.chunkID, surroundDst))
                        continue;

                    surroundChunks.Add(chunkMap[dtoVector]);
                }
            }
            return surroundChunks;
        }
        /// <summary>
        /// chunkMap에서 같은 벡터인지 체크하기 위한 컴페어 클래스
        /// </summary>
        private class VectorCompare : IEqualityComparer<DtoVector>
        {
            public bool Equals(DtoVector? x, DtoVector? y)
            {
                if (x.y == y.y && x.x == y.x) return true;
                return false;
            }

            public int GetHashCode([DisallowNull] DtoVector obj)
            {
                return HashCode.Combine(obj.x, obj.y);
            }
        }
    }
}
