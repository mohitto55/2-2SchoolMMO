using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
namespace Utility.Serilize
{    public class SaveData {
        public SaveData() {
            type = DataType.None;
        }
        public SaveData(int value) {
            type = DataType.Int;
            _pdi = value;
        }
        public SaveData(float value) {
            type = DataType.Float;
            _pdf = value;
        }
        public SaveData(string value) {
            type = DataType.String;
            _pds = value;
        }
        public SaveData(string key, SaveData value) {
            type = DataType.Dic;
            _dic.Add(key, value);
        }

        public int GetInt() {
            return _pdi;
        }
        public float GetFloat() {
            return _pdf;
        }
        public string GetString() {
            return _pds;
        }
        public SaveData[] GetList() {
            return _list.ToArray();
        }
        public SaveData GetData(string key) {
            if (_dic.ContainsKey(key))
                return _dic[key];
            else
                return null;
        }
        public object GetSerilizedData() {
            switch (type) {
                case DataType.Dic:
                    DI data = new DI();
                    List<KV> KVList = new List<KV>(_dic.Count);
                    foreach (KeyValuePair<string, SaveData> savedata in _dic) {
                        KV kv = new KV();
                        kv.k = savedata.Key;
                        kv.v = savedata.Value.GetSerilizedData();
                        KVList.Add(kv);
                    }
                    data.kv = KVList.ToArray();
                    return data;
                case DataType.List:
                    List<object> dataList = new List<object>(_list.Count);
                    foreach (SaveData listdata in _list) {
                        dataList.Add(listdata.GetSerilizedData());
                    }
                    return dataList.ToArray();
                case DataType.String:
                    return _pds;
                case DataType.Float:
                    return _pdf;
                case DataType.Int:
                    return _pdi;
                default:
                    return null;
            }
        }
        public void LoadFromSerilizedData(object serilizedData) {
            if (serilizedData is Dictionary<string, object>) {
                type = DataType.Dic;
                Dictionary<string, object> dictionary = (Dictionary<string, object>)serilizedData;
                _dic = new Dictionary<string, SaveData>(dictionary.Count);
                using (Dictionary<string, object>.Enumerator enumerator = dictionary.GetEnumerator()) {
                    while (enumerator.MoveNext()) {
                        string key = enumerator.Current.Key;
                        SaveData data = new SaveData();
                        data.LoadFromSerilizedData(enumerator.Current.Value);
                        _dic.Add(key, data);
                    }
                    return;
                }
            } else if (serilizedData is List<object>) {
                type = DataType.List;
                List<object> list = (List<object>)serilizedData;
                using (List<object>.Enumerator enumerrator = list.GetEnumerator()) {
                    while (enumerrator.MoveNext()) {
                        SaveData data = new SaveData();
                        data.LoadFromSerilizedData(enumerrator.Current);
                    }
                    return;
                }
            } else if (serilizedData is object[]) {
                type = DataType.List;
                object[] array = (object[])serilizedData;
                _list = new List<SaveData>(array.Length);
                foreach (object arraydata in array) {
                    SaveData data = new SaveData();
                    data.LoadFromSerilizedData(arraydata);
                    _list.Add(data);
                }
            } else if (serilizedData is DI) {
                type = DataType.Dic;
                DI di = (DI)serilizedData;
                _dic = new Dictionary<string, SaveData>(di.kv.Length);
                foreach (KV kv in di.kv) {
                    SaveData data = new SaveData();
                    data.LoadFromSerilizedData(kv.v);
                    _dic.Add(kv.k, data);
                }
            } else if (serilizedData is string) {
                type = DataType.String;
                _pds = (string)serilizedData;
            } else if (serilizedData is float) {
                type = DataType.Float;
                _pdf = (float)serilizedData;
            } else if (serilizedData is int) {
                type = DataType.Int;
                _pdi = (int)serilizedData;
            } else if (serilizedData == null) {
                type = DataType.None;
            }
            switch (type) {
                case DataType.Dic:
                    break;
                case DataType.List:
                    break;
                case DataType.String:
                    break;
                case DataType.Float:
                    break;
                case DataType.Int:
                    break;
                default:
                    break;
            }
        }
        [Serializable]
        class DI {
            public KV[] kv;
        }
        [Serializable]
        class KV {
            public string k;
            public object v;
        }
        public void AddData(string key, SaveData value) {
            type = DataType.Dic;
            _dic.Add(key, value);
        }
        DataType type = DataType.None;
        int _pdi;
        float _pdf;
        string _pds;
        Dictionary<string, SaveData> _dic = new Dictionary<string, SaveData>();
        List<SaveData> _list = new List<SaveData>();
        public enum DataType {
            None, Int, Float, String, Dic, List
        }
    }
}