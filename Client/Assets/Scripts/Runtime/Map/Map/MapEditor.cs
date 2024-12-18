using Runtime.Map;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using Utility.Serialize.Reference;

public class MapEditor : SerializedMonoBehaviour
{
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout), SerializeField]
    public Dictionary<EMapObjectType, MapSerilizer> serilizers = new Dictionary<EMapObjectType, MapSerilizer>();
    [SerializeField] string mapName;


    [Button]
    public void GenerateMapTileJsonData()
    {
        foreach (var serilizer in serilizers)
        {
            if (serilizer.Value != null)
                serilizer.Value.Serialize(mapName);
        }
    }
}

public static class VectorExtensions
{
    public static DtoVector ToDtoVector(this Vector3 vector)
    {
        return new DtoVector
        {
            x = vector.x,
            y = vector.y,
            z = vector.z
        };
    }

    public static Vector3 ToUnityVector(this DtoVector dtoVector)
    {
        return new Vector3(dtoVector.x, dtoVector.y, dtoVector.z);
    }
}

