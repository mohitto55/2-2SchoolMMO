using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

interface ISheatData
{
    public ISheatData Desearilize(string[] column);
}
public static class GoogleSheatParser
{
    public static void GetDataFormSheat(string url, Queue<List<string[]>> threadQueue)
    {
        Thread thread = new Thread(() => DataThread(url, threadQueue));
        thread.Start();
    }
    
    private static void DataThread(string url, Queue<List<string[]>> threadQueue) {
        Debug.Log("시트 데이터 받아오는 중");
        WebClient wc = new WebClient();
        wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
        wc.Headers.Add("DNT", "1");
        wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        wc.Headers.Add("Accept-Encoding", "deflate");
        wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");

        var data = wc.DownloadString(url);
        string[] row = data.Split('\n');
        List<string[]> sheatDatas = new List<string[]>();
        for (int i = 1; i < row.Length; i++) {
            string[] column = row[i].Split(',');
            sheatDatas.Add(column);        
        }
        threadQueue.Enqueue(sheatDatas);
    }
}
