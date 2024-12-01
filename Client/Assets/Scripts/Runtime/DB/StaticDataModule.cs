using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class StaticDataModule
{

    public void Initialize()
    {
        var loader = new StaticDataLoader();
    }

    /// <summary>
    /// 기획데이터를 불러올 로더
    /// </summary>
    private class StaticDataLoader
    {
        private string m_path;

        public StaticDataLoader()
        {
            m_path = Application.dataPath;
        }

        public void Load<T>(out List<T> data) where T : StaticData
        {
            var fileName = typeof(T).Name;

            var text = Resources.Load<TextAsset>(fileName);

            data = SerializeHelper.JsonToList<T>(text.ToString());
        }
    }
}
