using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Utility.Data
{
    [CreateAssetMenu(fileName = "GoogleSheetTableConfig", menuName = "GoogleSheetTable/Config", order = 100)]
    //[CanEditMultipleObjects]
    [System.Serializable]
    public class GoogleSheetLoader : ScriptableObject
    {
        [TextArea]
        public string m_googleSheetURL;
        string m_sheetData;
        Dictionary<string, ISheetType> m_typeDic
            = new Dictionary<string, ISheetType>();


        Dictionary<string, string> m_guidDic = new Dictionary<string, string>();


        public void GetAllGuid()
        {
            m_guidDic.Clear();
            var allSheetType = Assembly.GetExecutingAssembly().GetTypes().
                Where(t => t.IsClass && t.IsSubclassOf(typeof(StaticData))).ToList();

            foreach (var sheetType in allSheetType)
            {
                var attribute =
                    (GoogleSheetGID)Attribute.GetCustomAttribute(
                    sheetType,
                    typeof(GoogleSheetGID));

                m_guidDic.Add(sheetType.Name, attribute.m_gid);
                Debug.Log(sheetType.Name + " " + attribute.m_type);
            }
        }
        public void GetAllSheetType()
        {
            m_typeDic.Clear();

            var allSheetType = Assembly.GetExecutingAssembly().GetTypes().
                Where(t => t.IsClass && t.GetInterface("ISheetType") != null).ToList();

            foreach (var sheetType in allSheetType)
            {
                var attribute =
                    (SheetTypeAttribute)Attribute.GetCustomAttribute(
                    sheetType,
                    typeof(SheetTypeAttribute)
                    );

                foreach (var name in attribute.m_typeNames)
                {
                    if (m_typeDic.ContainsKey(name))
                    {

                        Debug.LogWarning(
                            $"Already Dictionary has sheetType key " +
                            $"# {attribute.GetType().Name} : {name}");

                        continue;
                    }

                    m_typeDic.Add(name, (ISheetType)Activator.CreateInstance(sheetType));
                }
            }

        }

        public void Initialize()
        {
            GetAllSheetType();
            GetAllGuid();
        }

        public void Load<T>(out List<T> table) where T :  new()
        {
            table = new List<T>();

            UnityWebRequest www = UnityWebRequest.Get($"{m_googleSheetURL}/export?format=tsv&sheet=&gid={m_guidDic[typeof(T).Name]}");
            Debug.Log($"{m_googleSheetURL}/export?format=tsv&sheet=&gid={m_guidDic[typeof(T).Name]}");

            www.SendWebRequest();

            while (!www.isDone && (int)www.result <= 1) { }
            m_sheetData = www.downloadHandler.text;

            Debug.Log(m_sheetData);


            string[] rows = m_sheetData.Split("\r\n");

 
            string[] fieldNames = rows[0].Split('\t');
            string[] typeNames = rows[1].Split('\t');
            string[] columns;

            FieldInfo field; 

            for (int i = 2; i < rows.Length; i++)
            {
                columns = rows[i].Split('\t');
                var value = new T();

                
                for (int j = 0; j < columns.Length; j++)
                {
                    if (fieldNames[j].Length < 1 || typeNames[j].Length < 1 || columns[j].Length < 1) continue;
                    
                    if(!m_typeDic.ContainsKey(typeNames[j]))
                    {
                        Debug.LogWarning("didn't find typename ## " + typeNames[j]);
                        continue;
                    }

                    field = typeof(T).GetField(fieldNames[j]);
                    if (field == null)
                    {
                        Debug.LogWarning("didn't find fieldName ## " + fieldNames[j]);
                        continue;
                    }
                    Debug.Log(typeNames[j] + " " + m_typeDic[typeNames[j]]);
                    field
                        .SetValue(value, m_typeDic[typeNames[j]]
                        .GenerateType(columns[j]));

                }

                table.Add(value);
            }
            
            
        }
    }
}