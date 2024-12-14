
using System.Numerics;

[System.Serializable]
public enum EMapObjectType
{
    Tile,
    EntityObject
}


public static class MapUtility
{
    public const int ChunkSize = 4;


    /// <summary>
    /// position이 활성화 가능한 청크 위치 안에 존재하는지 확인하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool IsPositionInLoadChunk(DtoVector center, DtoVector target, float surroundDst = 1)
    {
        float centerX = GetGridPosition(center).x;
        float centerY = GetGridPosition(center).y;
        int surroundChunkSize = (int)System.Math.Max(1, surroundDst / ChunkSize);

        float minX = centerX - surroundChunkSize;
        float maxX = centerX + surroundChunkSize;
        float minY = centerY - surroundChunkSize;
        float maxY = centerY + surroundChunkSize;

        float targetX = GetChunkPosition(target).x;
        float targetY = GetChunkPosition(target).y;

        if (minX <= targetX && targetX <= maxX && minY <= targetY && targetY <= maxY)
            return true;
        return false;
    }

    /// <summary>
    /// position이 활성화 가능한 청크 위치 안에 존재하는지 확인하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool IsChunkInLoadChunk(DtoVector center, DtoVector chunk, float surroundDst = 1)
    {
        float centerX = GetGridPosition(center).x;
        float centerY = GetGridPosition(center).y;
        int surroundChunkSize = (int)System.Math.Max(1, surroundDst / ChunkSize);

        float minX = centerX - surroundChunkSize;
        float maxX = centerX + surroundChunkSize;
        float minY = centerY - surroundChunkSize;
        float maxY = centerY + surroundChunkSize;

        DtoVector chunkCenter = GetGridPosition(GetChunkCenter(chunk));

        if (minX <= chunkCenter.x && chunkCenter.x <= maxX && minY <= chunkCenter.y && chunkCenter.y <= maxY)
            return true;
        return false;
    }
    /// <summary>
    /// 청크의 그리드 사이즈가 아닌 실제 크기의 중앙 위치 값을 반환합니다.
    /// </summary>
    /// <param name="chunk"></param>
    /// <returns></returns>
    public static DtoVector GetChunkCenter(DtoVector chunkPosition)
    {
        // 청크 중앙을 구합니다.
        return new DtoVector()
        {
            x = (chunkPosition.x * ChunkSize) + (ChunkSize / 2),
            y = (chunkPosition.y * ChunkSize) + (ChunkSize / 2)
        };
    }

    /// <summary>
    /// 위치가 어떤 청크 내부에 있는지 반환한다.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static DtoVector GetChunkPosition(DtoVector pos)
    {
        return new DtoVector() {
            x = (int)System.Math.Floor(pos.x / ChunkSize),
            y = (int)System.Math.Floor(pos.y / ChunkSize) 
        };
    }

    /// <summary>
    /// 위치가 청크로 나눈 그리드 위에서 어떤 위치에 있는지 반환한다.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static DtoVector GetGridPosition(DtoVector pos)
    {
        return new DtoVector()
        {
            x = (float)System.Math.Floor(pos.x / ChunkSize),
            y = (float)System.Math.Floor(pos.y / ChunkSize)
        };
    }
}

