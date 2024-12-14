using Newtonsoft.Json;
using Server.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Map.Factory
{
    public abstract class ChunkFactory
    {
        protected readonly EMapObjectType _chunkType;

        public ChunkFactory(EMapObjectType chunkType)
        {
            _chunkType = chunkType;
        }
        public abstract void CreateChunk(string mapFilePath, Dictionary<DtoVector, Dictionary<EMapObjectType, DtoChunk>> chunkMap);

        protected static List<T> LoadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<T>(); // 파일 없으면 빈 리스트 반환

            // 파일에서 JSON 문자열 읽기
            string json = File.ReadAllText(filePath);

            // JSON 문자열을 List<TileSerializeData>로 역직렬화
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}