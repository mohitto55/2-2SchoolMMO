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
        // Ÿ�ϸ� ������ ��� �� Ž��
        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                // ���� ��ġ�� Ÿ�� ��������
                TileBase currentTile = tilemap.GetTile(position);

                tilemap.SetTile(position, null);
            }
        }
    }

    public void DestroyChunk(Tilemap tilemap)
    {

    }
}
