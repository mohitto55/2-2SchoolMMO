using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapObjectSerilizer : MapSerilizer
{
    [SerializeField] Transform _objectsParent;
    public override void Serialize(string mapName)
    {
        Debug.Log(PathHelper.GetPojectParentFolder());
        Entity[] entities = _objectsParent.GetComponentsInChildren<Entity>();

        List<DtoObjectInfo> list = new List<DtoObjectInfo>();
        foreach (Entity entity in entities)
        {
            list.Add(new DtoObjectInfo()
            {
                entityID = entity.ObjectId,
                entityType = entity.EntityType.ToString(),
                position = entity.transform.position.ToDtoVector()
            });
        }

        string folderPath = PathHelper.GetPojectParentFolder() + '/' + "Server/Data/" + mapName;
        ObjectSerilizer.Serailize(list, folderPath, EMapObjectType.EntityObject.ToString() + ".json");

        folderPath = PathHelper.GetFolderPath("Json");
        ObjectSerilizer.Serailize(list, folderPath, EMapObjectType.EntityObject.ToString() + ".json");
    }
}