using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Runtime.BT.Singleton;
using System;
using Sirenix.OdinInspector;

public class MapGenerator : MonoSingleton<MapGenerator>
{
    [SerializeField] private string _mapName;
    [SerializeField, EnableIf("")] private float _chunkSurroundDst;
    private Dictionary<Vector2, Dictionary<string, Chunk<Vector2>>> mapChunk = new Dictionary<Vector2, Dictionary<string, Chunk<Vector2>>>();

    private Vector2 Center {
        get
        {
            return Camera.main.transform.position;
        }
    }

    float _updateDuration = 1;

    protected void Awake()
    {
        Camera mainCamera = Camera.main;

        // 카메라의 z 값에 따라 월드 깊이를 명확히 설정 (예: z = 0 또는 다른 원하는 깊이)
        float cameraDepth = Mathf.Abs(mainCamera.transform.position.z);

        // 좌측과 우측 경계 월드 좌표 구하기
        Vector3 leftWorldPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, cameraDepth));
        Vector3 rightWorldPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, cameraDepth));

        // 좌우의 거리
        float visibleWidth = Vector3.Distance(leftWorldPos, rightWorldPos);
        _chunkSurroundDst = visibleWidth / 2 + MapUtility.ChunkSize / 2;
    }
    public void Update()
    {
        _updateDuration -= Time.deltaTime;
        if (_updateDuration <= 0)
        {
            RequestMapChunk();
            _updateDuration = 1;
        }
        ChunkUpdate();
    }
    public void RequestMapChunk()
    {
        DtoChunkRequest chunkRequest = new DtoChunkRequest();
        chunkRequest.mapName = _mapName;
        chunkRequest.position = new DtoVector() { x = Center.x, y = Center.y };
        chunkRequest.surroundDst = _chunkSurroundDst;
        NetworkManager.Instance.SendPacket(EHandleType.ChunkUpdateRequest, chunkRequest);
    }

    private void ChunkUpdate()
    {
        List<Vector2> removeChunk = new List<Vector2>();
        foreach (var chunkTypes in mapChunk)
        {

            DtoVector centerVector = new DtoVector() { x = Center.x, y = Center.y };
            DtoVector chunkVector = new DtoVector() { x = chunkTypes.Key.x, y = chunkTypes.Key.y };

            Debug.DrawLine(Center, new Vector3(chunkVector.x, chunkVector.y));

            // 청크가 로드 범위 밖으로 벗어나면 비활성화 한다.
            if (!MapUtility.IsChunkInLoadChunk(centerVector, chunkVector, _chunkSurroundDst))
            {
                foreach (var chunk in chunkTypes.Value)
                {
                    chunk.Value.Deactive();
                }
                removeChunk.Add(chunkTypes.Key);
            }

        }
        foreach (var vec in removeChunk)
        {
            if (!mapChunk.ContainsKey(vec))
                continue;
            mapChunk.Remove(vec);
        }
    }

    public void GenerateChunk(Chunk<Vector2> chunk, DtoChunk chunkData)
    {
        if (chunk == null)
        {
            Debug.LogWarning("청크가 존재하지 않습니다.");
            return;
        }
        Vector2 position = new Vector2(chunkData.chunkPosition.x, chunkData.chunkPosition.y);

        string chunkType = chunk.GetType().Name;

        if (!mapChunk.ContainsKey(position))
        {
            mapChunk.Add(position, new Dictionary<string, Chunk<Vector2>>());
        }

        if (!mapChunk[position].ContainsKey(chunkType))
        {
            mapChunk[position].Add(chunkType, chunk);
        }
        chunk.Active();
    }

}
