using Newtonsoft.Json;
using Server.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Map
{
    public class MapManager
    {
        private static string mapFolderPath = "C:\\Users\\admin\\git\\Unity\\2-2SchoolMMO\\Server\\Data";
        public static Dictionary<string, Map> mapTable = new Dictionary<string, Map>();


        // 테스트 코드
        public static void Init()
        {
            List<string> mapFiles = GetFolderFilesName(mapFolderPath);
            foreach (string mapFile in mapFiles)
            {
                if (mapFile == "")
                    continue;
                string mapName = mapFile.Split('.')[0];
                ServerDebug.Log(LogType.Log, mapName + "맵 생성 시작");

                string mapPath = mapFolderPath + "\\"+ mapFile;
                List<DtoTile> tileDataList = LoadFromJson(mapPath);

                if (!mapTable.ContainsKey(mapName))
                {
                    mapTable.Add(mapName, new Map(tileDataList));
                }
                else { 
                    ServerDebug.Log(LogType.Error, $"{mapName}맵은 이미 존재합니다.");
                }
            }
        }

        public static List<DtoTile> GetMapTiles(string map)
        {
            if (mapTable.ContainsKey(map))
            {
                return mapTable[map].Tiles;
            }
            return null;
        }

        private static List<string> GetFolderFilesName(string folderPath)
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
        private static List<DtoTile> LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<DtoTile>(); // 파일 없으면 빈 리스트 반환

            // 파일에서 JSON 문자열 읽기
            string json = File.ReadAllText(filePath);

            // JSON 문자열을 List<TileSerializeData>로 역직렬화
            return JsonConvert.DeserializeObject<List<DtoTile>>(json);
        }
    }
}
