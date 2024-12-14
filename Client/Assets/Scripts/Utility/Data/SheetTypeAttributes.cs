using System;
using System.Collections;
using UnityEngine;

namespace Utility.Data
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class GoogleSheetGID : Attribute
    {
        public Type m_type;

        public string m_gid;

        public GoogleSheetGID(string gid)
        {
            m_gid = gid;
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class SheetTypeAttribute : Attribute
    {
        public Type m_type; 
    
        public string[] m_typeNames;
    
        public SheetTypeAttribute(Type type, params string[] typeNames)
        {
            m_type = type;
            m_typeNames = typeNames;
        }
    }

    public interface ISheetType
    {
        public object GenerateType(string data);
    }

    [SheetType(typeof(Int32), "int", "Int", "INT", "int32", "Int32", "INT32")]
    public class Int32TypeGenerator : ISheetType
    {
        public object GenerateType(string data)
        {
            data = data.Replace(" ", "");
            Int32 retval = 0;
            retval = Int32.Parse(data);
            return retval;

        }
    }
    [SheetType(typeof(float), "float", "Float", "FLOAT")]
    public class FloatTypeGenerator : ISheetType
    {
        public object GenerateType(string data)
        {
            data = data.Replace(" ", "");
            return float.Parse(data);
        }
    }
    [SheetType(typeof(string), "string", "String", "STRING")]
    public class StringTypeGenerator : ISheetType
    {
        public object GenerateType(string data)
        {
            return data;
        }
    }
    [SheetType(typeof(Vector3), "vector3", "Vector3", "VECTOR3")]
    public class Vector3TypeGenerator : ISheetType
    {
        public object GenerateType(string data)
        {
            data = data.Replace(" ", "");
            var split = data.Split(',');

            if(split.Length != 3)
            {
                Debug.LogWarning("wrong vector data");
                return null;
            }

            return new Vector3(
                float.Parse(split[0]),
                float.Parse(split[1]),
                float.Parse(split[2]));
        }
    }
    [SheetType(typeof(int[]), "int[]", "array<int>", "int arr", "INT[]", "ARRAY<INT>")]
    public class ArrayIntTypeGenerator : ISheetType
    {
        public object GenerateType(string data)
        {
            data = data.Replace(" ", "");

            var split = data.Split(',');
            var container = new int[split.Length];

            try
            {
                for (int i = 0; i < split.Length; i++)
                {
                    container[i] = int.Parse(split[i]);
                }
            }
            catch
            {
                Debug.LogError(data);
            }

            return container;
        }
    }
    [SheetType(typeof(string[]), "string[]", "array<string>", "string arr", "STRING[]", "ARRAY<STRING>")]
    public class StringArrayTypeGenerator : ISheetType
    {
        public object GenerateType(string data)
        {
            var split = data.Split(',');

            var container = new string[split.Length];

            for (int i = 0; i < split.Length; i++)
            {
                container[i] = split[i];
            }
            return container;
        }
    }
}

