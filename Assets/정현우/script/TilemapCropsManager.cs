using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCropsManager : TimeAgent
{
    // 밭을 간 타일 이미지
    [SerializeField] TileBase plowed;

    // 씨앗이 심어진 타일 이미지
    [SerializeField] TileBase seeded;

    // 작물이 표시될 타일맵
    Tilemap targetTilemap;

    // 작물의 이미지를 표시할 오브젝트(prefab)
    [SerializeField] GameObject cropsSpritePrefab; // 오타 수정: cropsSpritePerfab -> cropsSpritePrefab

    [SerializeField] CropsContainer container;

    private void Start()
    {
        GameManager.Instance.GetComponent<CropsManager>().cropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        Init();
        VisualizeMap();
    }

    private void VisualizeMap()
    {
        for (int i = 0; i < container.crops.Count; i++)
        {
            VisualizeTile(container.crops[i]);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < container.crops.Count; i++)
        {
            container.crops[i].renderer = null;
        }
    }

    // 시간이 흐를 때마다 작물 상태를 갱신하는 함수
    public void Tick()
    {
        if (targetTilemap == null) { return; }

        // 모든 작물 타일을 하나씩 확인
        foreach (CropTile cropTile in container.crops)
        {
            // 만약 타일에 심어진 작물이 없다면 다음으로 넘어감
            if (cropTile.crop == null) { continue; }

            // 시간이 지나면서 작물이 손상됨
            cropTile.damage += 0.02f;

            // 손상이 1을 넘으면 작물이 망가지므로 수확 처리
            if (cropTile.damage > 1f)
            {
                cropTile.Harvested();  // 수확
                targetTilemap.SetTile(cropTile.position, plowed);  // 다시 밭으로 변경
                continue;
            }

            // 작물이 완전히 자랐으면 아무것도 하지 않음
            if (cropTile.Complete)
            {
                Debug.Log("i am done growing");
                continue;
            }

            // 성장 시간을 1씩 더함
            cropTile.growTimer += 1;

            // 현재 단계의 성장 시간이 다 되면 다음 단계로 넘어감
            if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);  // 작물 이미지를 화면에 표시
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];  // 해당 단계의 이미지를 설정

                cropTile.growStage += 1;  // 다음 단계로 이동
            }
        }
    }

    internal bool Check(Vector3Int position)
    {
        // 데이터가 존재하면 true, 존재하지 않으면 false를 반환
        return container.Get(position) != null;
    }

    // 밭을 가는 함수
    public void Plow(Vector3Int position)
    {
        if (Check(position) == true)
        {
            return;
        }

        // 새로운 밭 타일을 생성
        CreatePlowedTile(position);
    }

    // 씨앗을 심는 함수
    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropTile tile = container.Get(position);

        if (tile == null) { return; }

        // 씨앗이 심어진 타일 이미지로 변경
        targetTilemap.SetTile(position, seeded);

        // 해당 위치에 씨앗 정보를 저장
        tile.crop = toSeed;
    }

    public void VisualizeTile(CropTile cropTile)
    {
        targetTilemap.SetTile(cropTile.position, cropTile.crop != null ? seeded : plowed);

        if (cropTile.renderer == null)
        {
            GameObject go = Instantiate(cropsSpritePrefab, transform);  // 작물 이미지를 표시할 오브젝트 생성

            // 타일맵 좌표를 월드 좌표로 변환하여 위치 설정
            go.transform.position = targetTilemap.CellToWorld(cropTile.position);
            go.transform.position -= Vector3.forward * 0.01f;
            cropTile.renderer = go.GetComponent<SpriteRenderer>();  // SpriteRenderer 설정

            // 프리팹이 SpriteRenderer를 포함하지 않는 경우 예외 처리
            if (cropTile.renderer == null)
            {
                Debug.LogError("The prefab does not have a SpriteRenderer component.");
            }
        }

        bool growing = cropTile.crop != null && cropTile.growTimer >= cropTile.crop.growthStageTime[0];

        cropTile.renderer.gameObject.SetActive(true);  // 작물 이미지를 화면에 표시

        if (growing)
        {
            cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage - 1];  // 해당 단계의 이미지를 설정
        }
    }

    // 새로운 밭 타일을 생성하는 함수
    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();  // 새로운 CropTile 객체 생성
        container.Add(crop);  // 딕셔너리에 추가

        crop.position = position;  // 타일 위치 설정

        VisualizeTile(crop);

        targetTilemap.SetTile(position, plowed);  // 해당 타일을 밭으로 변경
    }

    // 작물을 수확하는 함수
    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        CropTile tile = container.Get(gridPosition);
        if (tile == null) { return; }

        // 만약 작물이 완전히 자랐다면
        if (tile.Complete)
        {
            // 아이템을 생성하여 해당 위치에 떨어뜨림
            Vector3 spawnPosition = targetTilemap.CellToWorld(gridPosition) + new Vector3(0.5f, 0.5f, 0);
            itemSpawnManager.instance.SpawnItem
                (
                spawnPosition,
                tile.crop.yield,  // 작물 수확물
                tile.crop.count  // 수확된 아이템 개수
                );

            tile.Harvested();  // 수확 후 초기화
            VisualizeTile(tile);
        }
    }

    /* 수정 전 코드 
    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        CropTile tile = container.Get(gridPosition);
        if (tile == null) { return; }


        // 만약 작물이 완전히 자랐다면
        if (tile.Complete)
        {
            // 아이템을 생성하여 해당 위치에 떨어뜨림
            itemSpawnManager.instance.SpawnItem
                (
                targetTilemap.CellToWorld(gridPosition),
                tile.crop.yield,  // 작물 수확물
                tile.crop.count  // 수확된 아이템 개수
                );

            tile.Harvested();  // 수확 후 초기화
            VisualizeTile(tile);
        }
        */
    }