using UnityEngine;

public abstract class ChunkFactory : IFactory<Chunk<Vector2>, DtoChunk>
{
    public abstract string Id { get; }
    public abstract Chunk<Vector2> Create(DtoChunk param);
}
public abstract class ChunkFactory<TChunk> : ChunkFactory
{
    public override string Id { get => typeof(TChunk).Name; }
}
