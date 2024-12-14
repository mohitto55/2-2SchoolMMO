using UnityEngine.Tilemaps;
using UnityEngine;

public class TileMapChunk : MapChunk<Vector2, ChunkTile, Tilemap>
{
    public override void Active()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            Vector3Int tilePos = Objects[i].position;
            TileBase tileBase = Objects[i].tileBase;

            if (Map != null)
                Map.SetTile(tilePos, tileBase);
        }
    }


    public override void Deactive()
    {
        foreach (var obj in Objects)
        {
            Map.SetTile(new Vector3Int((int)obj.position.x, (int)obj.position.y), null);
        }
    }
}

public class ChunkTile
{
    public TileBase tileBase;
    public Vector3Int position;
}