using System;
using System.Collections;

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

    [SheetType(typeof(EInteractionType), "Interaction", "interaction", "InteractionType", "interactionType")]
    public class InteractionTypeGenerator : ISheetType
    {
        public object GenerateType(string data)
        {
            var type = Enum.Parse(typeof(EInteractionType), data);
            return type;
        }
    }
}

