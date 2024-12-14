using Newtonsoft.Json;
using Server.Debug;
using Server.Map.Factory;
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
        private string _name;
        public string Name => _name;
        Dictionary<DtoVector, Dictionary<EMapObjectType, DtoChunk>> chunkMap = new Dictionary<DtoVector, Dictionary<EMapObjectType, DtoChunk>>();

        public Map(string mapName) {
            chunkMap = new Dictionary<DtoVector, Dictionary<EMapObjectType, DtoChunk>>(new VectorCompare());
            _name = mapName;
        }

        public void CreateChunk(string mapFilePath, ChunkFactory factory)
        {
            factory.CreateChunk(mapFilePath, chunkMap);
        }

        /// <summary>
        /// position에서 부터 surroundSize 거리 내부에 있는 Chunks들을 찾습니다.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="surroundSize"></param>
        /// <returns></returns>
        public List<T> GetSurroundChunks<T>(EMapObjectType type, DtoVector position, float surroundDst = 1) where T : DtoChunk
        {
            DtoVector center = MapUtility.GetChunkPosition(position);
            int surroundChunkSize = (int)Math.Max(1, surroundDst / MapUtility.ChunkSize) + 1;

            List<T> surroundChunks = new List<T>();
            for (int y = (int)(center.y - surroundChunkSize); y < center.y + surroundChunkSize; y++)
            {
                for (int x = (int)(center.x - surroundChunkSize); x < center.x + surroundChunkSize; x++)
                {
                    DtoVector dtoVector = new DtoVector() { x = x, y = y };
                    if (!chunkMap.ContainsKey(dtoVector))
                        continue;
                    if (!chunkMap[dtoVector].ContainsKey(type))
                        continue;

                    DtoChunk searchChunk = chunkMap[dtoVector][type];

                    if (!MapUtility.IsChunkInLoadChunk(position, searchChunk.chunkPosition, surroundDst))
                        continue;
                    DtoChunk chunk = chunkMap[dtoVector][type];
                    surroundChunks.Add((T)chunk);
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
