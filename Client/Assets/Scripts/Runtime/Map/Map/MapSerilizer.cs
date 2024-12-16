using DG.Tweening.Plugins.Core.PathCore;
using Newtonsoft.Json;
using NUnit.Framework;
using Runtime.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;


public static class ObjectSerilizer 
{
    public static void Serailize<T>(ICollection<T> datas, string folderPath, string fileName)
    {
        try
        {
            string path = folderPath + '/' + fileName;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            SaveDatasToJson(datas, path);
        }
        catch (Exception e)
        {
            Debug.LogError("오브젝트 변환 도중 실패했습니다. " + e.ToString());
        }
    }

    private static void SaveDatasToJson<T>(ICollection<T> datas, string filePath)
    {
        if (datas == null || !datas.Cast<object>().Any())
        {
            Debug.LogWarning("데이터가 비어 있습니다.");
            return;
        }
        // List<>을 JSON 문자열로 변환
        string json = JsonConvert.SerializeObject(datas, Formatting.Indented);

        // JSON 문자열을 파일에 저장
        using (StreamWriter outStream = System.IO.File.CreateText(filePath))
        {
            outStream.WriteLine(json);
            outStream.Close();
        }
        Debug.Log("Tiles JSON saved to: " + filePath);
    }
}