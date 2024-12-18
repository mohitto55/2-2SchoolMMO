using NUnit.Framework;
using Runtime.DB.ViewModel;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOItem", menuName = "SOItem")]
public class SOItem : ScriptableObject
{
    public List<ItemSpriteData> itemdatas;
}

[System.Serializable]
public struct ItemSpriteData
{
    public Sprite sprite;
    public int id;
}