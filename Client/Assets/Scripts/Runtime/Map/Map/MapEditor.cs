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

[System.Serializable]
public abstract class MapSerilizer : ISerializer<string>
{
    public abstract void Serialize(string mapName);
}

[System.Serializable]
public class MapObjectSerilizer : MapSerilizer
{
    [SerializeField] Transform _objectsParent;
    public override void Serialize(string mapName)
    {
        Debug.Log(PathHelper.GetPojectParentFolder());
        Entity[] entities = _objectsParent.GetComponentsInChildren<Entity>();

        List<DtoEntityObject> list = new List<DtoEntityObject>();
        foreach (Entity entity in entities)
        {
            list.Add(new DtoEntityObject()
            {
                entityType = entity.ObjectId,
                position = entity.transform.position.ToDtoVector()
            });
        }

        string folderPath = PathHelper.GetPojectParentFolder() + '/' + "Server/Data/" + mapName;
        ObjectSerilizer.Serailize(list, folderPath, EMapObjectType.EntityObject.ToString() + ".json");

        folderPath = PathHelper.GetFolderPath("Json");
        ObjectSerilizer.Serailize(list, folderPath, EMapObjectType.EntityObject.ToString() + ".json");
    }
}
[System.Serializable]
public class MapTileSerilizer : MapSerilizer
{
    [SerializeField] Tilemap _tileMap;
    [SerializeField] SOTileData _soTileData;
    public override void Serialize(string mapName)
    {
        Debug.Log(PathHelper.GetPojectParentFolder());
        List<TileBaseExporter> exporters = new List<TileBaseExporter>();
        exporters.Add(new TilePositionExporter());

        Dictionary<string, TileData> tileDataDic = _soTileData.GetTileTable();
        string folderPath = PathHelper.GetPojectParentFolder() + '/' + "Server/Data/" + mapName;
        TileMapSerializer.TileMapSerailize(_tileMap, tileDataDic, folderPath, EMapObjectType.Tile.ToString() + ".json");

        folderPath = PathHelper.GetFolderPath("Json");
        TileMapSerializer.TileMapSerailize(_tileMap, tileDataDic, folderPath, EMapObjectType.Tile.ToString() + "Tile.json");
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

