using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Chunk<TKey>
{
    public TKey Id;
    public abstract void Active();
    public abstract void Deactive();

    public Chunk() { }
}

public abstract class MapChunk<TKey, TObject, TMap> : Chunk<TKey>
{
    public TKey Id;
    public List<TObject> Objects;
    public TMap Map;

    public MapChunk()
    {
        Objects = new List<TObject>();
    }
}