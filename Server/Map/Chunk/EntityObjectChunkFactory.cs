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
        private List<DtoTile> _tileDatas;

        public EntityObjectChunkFactory(EMapObjectType chunkType) : base(chunkType)
        {
        }

        public override void CreateChunk(string mapFilePath, Dictionary<DtoVector, Dictionary<EMapObjectType, DtoChunk>> chunkMap)
        {
            _tileDatas = LoadFromJson<DtoTile>(mapFilePath);
        }
    }
}
