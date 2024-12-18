using Runtime.BT.Singleton;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class ObjectManager : MonoSingleton<ObjectManager>
{
    public GameObject characterPrefab;
    Dictionary<int, Character> characters = new Dictionary<int, Character>();


    public Character GetObject(int id)
    {
        return characters[id];
    }
    public Character GenerateEntityData(int id, DtoObjectInfo data)
    {
        if(characters.ContainsKey(id))
        {
            characters[id].SetObjectInfo(data);
        }
        else
        {
            characters[id] = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity).GetComponent<Character>();
            characters[id].SetObjectInfo(data);
        }
        return characters[id];
    }
    public void SetTransformEntityData(int id, DtoTransform data)
    {
        if (PlayerController.Instance.target == null)
            return;

        if (characters.ContainsKey(id))
        {
            characters[id].SetTransform(data);
        }
        else
        {
            characters[id] = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity).GetComponent<Character>();
            characters[id].SetTransform(data);
        }
    }

}