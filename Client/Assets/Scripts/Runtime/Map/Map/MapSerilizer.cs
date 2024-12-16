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
            Debug.LogError("������Ʈ ��ȯ ���� �����߽��ϴ�. " + e.ToString());
        }
    }

    private static void SaveDatasToJson<T>(ICollection<T> datas, string filePath)
    {
        if (datas == null || !datas.Cast<object>().Any())
        {
            Debug.LogWarning("�����Ͱ� ��� �ֽ��ϴ�.");
            return;
        }
        // List<>�� JSON ���ڿ��� ��ȯ
        string json = JsonConvert.SerializeObject(datas, Formatting.Indented);

        // JSON ���ڿ��� ���Ͽ� ����
        using (StreamWriter outStream = System.IO.File.CreateText(filePath))
        {
            outStream.WriteLine(json);
            outStream.Close();
        }
        Debug.Log("Tiles JSON saved to: " + filePath);
    }
}