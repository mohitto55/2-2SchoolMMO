using DG.Tweening.Plugins.Core.PathCore;
using Newtonsoft.Json;
using NUnit.Framework;
using Runtime.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public static class TileMapSerializer
{
    public static void TileMapSerailize(Tilemap tileMap, Dictionary<string, TileData> tileDataDic, string folderPath, string fileName)
    {
        BoundsInt bounds = GetTilemapPlacedBounds(tileMap);
        // 두번째 줄부터 타일의 데이터 넣기
        List<DtoTile> tileDatas = GetPlacedTiles(tileMap);
        foreach (DtoTile tileSerializeData in tileDatas)
        {
            if (tileDataDic.ContainsKey(tileSerializeData.id))
            {
                TileData tileData = tileDataDic[tileSerializeData.id];
                tileSerializeData.id = tileData._tile.name;
                tileSerializeData.moveable = tileData._moveable;
            }

        }

        try
        {
            string path = folderPath + '/' + fileName;

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            SaveTilesToJson(tileDatas, path); 
        }
        catch (Exception e)
        {
            Debug.LogError("타일맵 변환 도중 실패했습니다. " + e.ToString());
        }
    }

    static void SaveTilesToJson(List<DtoTile> tilesJsonData, string filePath)
    {

        // List<string>을 JSON 문자열로 변환
        string json = JsonConvert.SerializeObject(tilesJsonData, Formatting.Indented);

        // JSON 문자열을 파일에 저장
        using (StreamWriter outStream = System.IO.File.CreateText(filePath))
        {
            outStream.WriteLine(json);
            outStream.Close();
        }
        Debug.Log("Tiles JSON saved to: " + filePath);
    }
    public static void TileMapGenerate(Tilemap tileMap, Dictionary<string, Tile> tileDic, string fileName)
    {
        string folderPath = PathHelper.GetFolderPath("CSV");
        string path = PathHelper.GetFilePath("CSV", fileName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        List<string> lines = new List<string>();

        // StreamReader를 사용하여 파일 읽기
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // 읽은 각 라인을 리스트에 추가
                string[] split = line.Split(',');
                TileBase tile = null;
                for (int i = 0; i < split.Length; i++)
                {
                    //Vector3Int cellPosition = new Vector3Int(x, y, 0);
                    //TileBase tile = tileMap.GetTile(cellPosition);

                    //tile = exporters[i].Import(tileMap, tileDic, tile, split[i]);
                }
                lines.Add(line);
                Debug.Log(line);
            }
        }
    }
    static BoundsInt GetTilemapPlacedBounds(Tilemap tileMap)
    {
        int minX = int.MaxValue;
        int maxX = int.MinValue;
        int minY = int.MaxValue;
        int maxY = int.MinValue;
        BoundsInt bounds = tileMap.cellBounds;

        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                // 해당 위치에 타일이 있는지 확인
                TileBase tile = tileMap.GetTile(cellPosition);

                if (tile != null) // 타일이 존재하는 경우
                {
                    // 최소, 최대 X 및 Y 값 업데이트
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                }
            }
        }
        bounds.xMin = minX;
        bounds.xMax = maxX;
        bounds.yMin = minY;
        bounds.yMax = maxY;
        return bounds;
    }

    static List<DtoTile> GetPlacedTiles(Tilemap tileMap)
    {
        List<DtoTile> tiles = new List<DtoTile>();
        BoundsInt bounds = tileMap.cellBounds;
        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                // 해당 위치에 타일이 있는지 확인
                TileBase tile = tileMap.GetTile(cellPosition);
                if (tile != null) // 타일이 존재하는 경우
                {
                    DtoTile tileData = new DtoTile();
                    DtoVector position = new DtoVector();
                    position.x = x;
                    position.y = y;
                    tileData.position = position;
                    tileData.id = tile.name;
                    tiles.Add(tileData);
                }
            }
        }
        return tiles;
    }
}
