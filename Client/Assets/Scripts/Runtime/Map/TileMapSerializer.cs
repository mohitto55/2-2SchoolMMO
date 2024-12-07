using DG.Tweening.Plugins.Core.PathCore;
using Runtime.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public static class TileMapSerializer
{
    public static void TileMapSerailize(Tilemap tileMap, string fileName, TileBaseExporter idExporter, List<TileBaseExporter> exporters)
    {
        exporters.Insert(0, idExporter);

        BoundsInt bounds = GetTilemapPlacedBounds(tileMap);
        List<List<string>> tilesData = new List<List<string>>();

        // 첫번째 줄에 데이터의 타입 이름 적기
        tilesData.Add(new List<string>());
        foreach (TileBaseExporter exporter in exporters)
        {
            string data = exporter.DataType +",";
            tilesData[0].Add(data);
        }

        // 두번째 줄부터 타일의 데이터 넣기
        for (int y = bounds.yMin; y <= bounds.yMax; y++)
        {          
            for (int x = bounds.xMin; x <= bounds.xMax; x++)
            {
                tilesData.Add(new List<string>());
                foreach (TileBaseExporter exporter in exporters)
                {
                    Vector3Int cellPosition = new Vector3Int(x, y, 0);
                    TileBase tile = tileMap.GetTile(cellPosition);

                    string data = exporter.Export(tile, cellPosition);

                    data += ",";

                    tilesData[tilesData.Count - 1].Add(data);
                }            
            }
        }

        // 타일 데이터를 csv로 변환
        string delimiter = ",";
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < tilesData.Count; y++)
        {
            for (int x = 0; x < tilesData[y].Count; x++)
            {
                sb.Append(string.Join(delimiter, tilesData[y][x]));
            }
            sb.AppendLine();
        }

        try
        {
            string folderPath = GetFolderPath("CSV");
            string path = GetFilePath("CSV", fileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            Debug.Log(path);
            using (StreamWriter outStream = System.IO.File.CreateText(path))
            {
                outStream.WriteLine(sb);
            }
        }
        catch (Exception e)
        {
            {
                Debug.LogError("타일맵 변환 도중 실패했습니다. " + e.ToString());
            }
        }
    }
    public static void TileMapGenerate(Tilemap tileMap, string fileName, TileBaseExporter idExporter, List<TileBaseExporter> exporters)
    {
        string folderPath = GetFolderPath("CSV");
        string path = GetFilePath("CSV", fileName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        using (StreamReader stream = System.IO.File.OpenText(path))
        {
            Debug.Log(stream);
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
    static string GetApplicationDataPath
    {
        get { return Application.dataPath; }
    }
    static string GetFolderPath(string saveFolder)
    {
        return GetApplicationDataPath + '/' + saveFolder;
    }
    static string GetFilePath(string saveFolder, string fileName)
    {
        return GetFolderPath(saveFolder) + '/' + fileName + ".csv";
    }
}
