using UnityEngine.Tilemaps;
using UnityEngine;

public class MapObjectChunk : MapChunk<Vector2, Entity, Transform>
{
    public override void Active()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            if(Objects[i] != null)
                Objects[i].gameObject.SetActive(true);
        }
    }


    public override void Deactive()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            if (Objects[i] != null)
                Objects[i].gameObject.SetActive(false);
        }
    }
}