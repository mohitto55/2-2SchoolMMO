using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOItem", menuName = "SOItem")]
public class SOItem : ScriptableObject
{
    public List<ItemData> itemdatas;
}

[System.Serializable]
public struct ItemData
{
    public Sprite sprite;
    public int id;
    public string itemName;
    public string itemDesc;
}
