using Google.Protobuf.WellKnownTypes;
using Server.Debug;
using Server.Utility.Sheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Utility.Data
{
    public class GoogleSheetLoader
    {
        public string m_googleSheetURL;
        string m_sheetData;
        Dictionary<string, ISheetType> m_typeDic
            = new Dictionary<string, ISheetType>();


        public static Dictionary<string, string> m_guidDic = new Dictionary<string, string>();


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
            }
        }
        public void AddGuid<T>(string gid)
        {
            var sheetType = typeof(T);
            m_guidDic.Add(sheetType.Name, gid);
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

                        ServerDebug.Log(LogType.Warning,
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

        /// <summary>
        /// 구글 스프레드시트를 HttpClient로 가져오려면 비동기 함수를 써야한다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheetURL"></param>
        /// <returns></returns>
        public async IAsyncEnumerator<T>? Load<T>(string sheetURL) where T :  new()
        {
            string m_googleSheetURL = sheetURL;
            string url = "";
            try
            {
                url = $"{m_googleSheetURL}/export?format=tsv&sheet=&gid={m_guidDic[typeof(T).Name]}";
            }
            catch (Exception e)
            {
                ServerDebug.Log(LogType.Error, e.ToString());
            }


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    m_sheetData = await response.Content.ReadAsStringAsync();

                }
                catch (Exception e)
                {
                    ServerDebug.Log(LogType.Error, e.ToString());
                }
                ServerDebug.Log(LogType.Log, m_sheetData);


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

                        if (!m_typeDic.ContainsKey(typeNames[j]))
                        {
                            ServerDebug.Log(LogType.Warning, "didn't find typename ## " + typeNames[j]);
                            continue;
                        }

                        field = typeof(T).GetField(fieldNames[j]);
                        if (field == null)
                        {
                            ServerDebug.Log(LogType.Warning, "didn't find fieldName ## " + fieldNames[j]);
                            continue;
                        }
                        //ServerDebug.Log(LogType.Log, typeNames[j] + " " + m_typeDic[typeNames[j]]);

                        // SetValue가 아닌 SetValueDirect로 바꾼이유
                        // T가 구조체라면 SetValue를 하면 value가 값 타입을 넘겨줘서 실질적인 값에 할당할 수 없다.
                        // 그래서 SetValueDirect로 참조해서 넣어준다.
                        // 당장은 별 문제는 없어보인다.
                        field
                            .SetValueDirect(__makeref(value), m_typeDic[typeNames[j]]
                            .GenerateType(columns[j]));

                        var fields = typeof(T).GetFields(); // 모든 필드 정보 가져오기
                    }
                    yield return value;
                }
            }

            T? t = default;
            yield return t;
        }
    }
}