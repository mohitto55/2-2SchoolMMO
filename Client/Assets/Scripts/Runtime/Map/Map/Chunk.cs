using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk
{
    public BoundsInt bounds;

    public void Active(Tilemap tilemap)
    {

    }
    public void Deactive(Tilemap tilemap)
    {
        // 타일맵 영역의 모든 셀 탐색
        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                // 현재 위치의 타일 가져오기
                TileBase currentTile = tilemap.GetTile(position);

                tilemap.SetTile(position, null);
            }
        }
    }

    public void DestroyChunk(Tilemap tilemap)
    {

    }
}
