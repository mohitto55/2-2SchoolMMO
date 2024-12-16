using Newtonsoft.Json;
using Server.Debug;
using Server.Map.Chunk;
using Server.Map.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.Map
{
    public class MapManager
    {
        private static string mapFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Data");
        public static Dictionary<string, Map> mapTable = new Dictionary<string, Map>();
        public static Dictionary<EMapObjectType, ChunkFactory> chunkFactoryTable = new Dictionary<EMapObjectType, ChunkFactory>();

        public static void Init()
        {
            ChunkFactoryInit();
            ServerDebug.Log(LogType.Error, mapFolderPath);

            /// Data폴더/맵이름폴더/청크타입json 
            List<string> mapNames = GetFoldersNameInFolder(mapFolderPath);
            foreach (string mapName in mapNames)
            {
                if (mapName == "")
                    continue;

                // 맵이 이미 생성되었는가
                if (!mapTable.ContainsKey(mapName))
                {
                    mapTable.Add(mapName, new Map(mapName));
                }
                else
                {
                    ServerDebug.Log(LogType.Error, $"{mapName}맵은 이미 존재합니다.");
                }

                // 맵 폴더안에 청크타입 파일들 이름 가져오기
                List<string> mapObjectTypeFiles = GetFileNamesInFolder(mapFolderPath + "\\" + mapName);

                ServerDebug.Log(LogType.Log, mapName + " 맵 생성 시작");
                foreach (string mapFile in mapObjectTypeFiles)
                {
                    string mapType = mapFile.Split('.')[0];
                    ServerDebug.Log(LogType.Log, mapName + " 맵 타입 : " + mapType);

                    // type에 맞는 청크 오브젝트 데이터가 있는지 확인
                    if (Enum.TryParse<EMapObjectType>(mapType, out EMapObjectType type))
                    {
                        // 있다면 적절한 청크 팩토리를 가져와서 맵 초기화하기
                        string mapPath = mapFolderPath + "\\" + mapName + "\\" + mapFile;
                        if (chunkFactoryTable.ContainsKey(type))
                        {
                            mapTable[mapName].CreateChunk(mapPath, chunkFactoryTable[type]);
                        }
                    }
                }
                ServerDebug.Log(LogType.Log, mapName + " 맵 생성 완료");
            }
        }

        public static void ChunkFactoryInit()
        {
            chunkFactoryTable = new Dictionary<EMapObjectType, ChunkFactory>();
            chunkFactoryTable.Add(EMapObjectType.Tile, new TileChunkFactory(EMapObjectType.Tile));
            chunkFactoryTable.Add(EMapObjectType.EntityObject, new EntityObjectChunkFactory(EMapObjectType.EntityObject));
        }

        public static List<T> GetSurroundChunks<T>(EMapObjectType type, string map, DtoVector position, float surroundDst = 1) where T : DtoChunk
        {
            if (mapTable.ContainsKey(map))
            {
                return mapTable[map].GetSurroundChunks<T>(type, position, surroundDst);
            }
            ServerDebug.Log(LogType.Warning, map + "이라는 이름의 맵은 존재하지 않습니다. 청크를 반환할 수 있습니다.");
            return null;
        }

        private static List<string> GetFileNamesInFolder(string folderPath)
        {
            List<string> folderFiles = new List<string>();
            if (!Directory.Exists(folderPath))
            {
                ServerDebug.Log(LogType.Error, "폴더가 존재하지 않습니다.");
                return folderFiles;
            }

            // 폴더 내 모든 파일 경로 가져오기
            string[] files = Directory.GetFiles(folderPath);

            // 파일 이름만 출력
            foreach (string file in files)
            {
                folderFiles.Add(Path.GetFileName(file));
            }
            return folderFiles;
        }
        private static List<string> GetFoldersNameInFolder(string folderPath)
        {
            List<string> folderNames = new List<string>();
            if (!Directory.Exists(folderPath))
            {
                ServerDebug.Log(LogType.Error, "폴더가 존재하지 않습니다.");
                return folderNames;
            }

            // 폴더 내 모든 파일 경로 가져오기
            string[] folders = Directory.GetDirectories(folderPath);

            // 파일 이름만 출력
            foreach (string folder in folders)
            {
                folderNames.Add(Path.GetFileName(folder));
            }
            return folderNames;
        }
        private static List<T> CreateChunk<T>(string filePath)
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
