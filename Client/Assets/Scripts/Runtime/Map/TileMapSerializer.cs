using DG.Tweening.Plugins.Core.PathCore;
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
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public static class TileMapSerializer
{
    public static void TileMapSerailize(Tilemap tileMap, Dictionary<string, TileData> tileDataDic, string folderPath, string fileName)
    {
        BoundsInt bounds = GetTilemapPlacedBounds(tileMap);
        List<string> tilesJsonData = new List<string>();


        // 두번째 줄부터 타일의 데이터 넣기
        List<TileSerializeData> tileDatas = GetPlacedTiles(tileMap);
        foreach (TileSerializeData tileData in tileDatas)
        {
            Debug.Log(tileData.id);
            if (tileDataDic.ContainsKey(tileData.id))
            {
                tileData.SetTileData(tileDataDic[tileData.id]);
            }

            tilesJsonData.Add(JsonUtility.ToJson(tileData));
        }

        try
        {
            //string folderPath = PathHelper.GetFolderPath("Json");
            string path = folderPath + '/' + fileName;

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            SaveTilesToJson(tilesJsonData, path);
        }
        catch (Exception e)
        {
            Debug.LogError("타일맵 변환 도중 실패했습니다. " + e.ToString());
        }
    }

    static void SaveTilesToJson(List<string> tilesJsonData, string filePath)
    {
        // TileDataList 객체로 감싸기
        TileDataList tileDataList = new TileDataList(tilesJsonData);

        // List<string>을 JSON 문자열로 변환
        string json = JsonUtility.ToJson(tileDataList, true);

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

    static List<TileSerializeData> GetPlacedTiles(Tilemap tileMap)
    {
        List<TileSerializeData> tiles = new List<TileSerializeData>();
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
                    TileSerializeData tileData = new TileSerializeData();
                    tileData.position = new Vector3Int(x, y, 0);
                    tileData.id = tile.name;
                    tiles.Add(tileData);
                }
            }
        }
        return tiles;
    }
}
