// 2024-09-24 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;
    public CropsManager cropsManager;
    public PlaceableObjectsReferenceManager objectsManager;


    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        if (tileMap == null) 
        {
            //"BaseTilemap"이라는 이름을 가진 게임 오브젝트를 찾아서 그 오브젝트의 Tilemap 컴포넌트를 tileMap 변수에 할당하는 역할
            tileMap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        // 타일맵이 존재하지 않을 경우 기본값으로 (0, 0, 0)을 반환하여 이후의 코드에서 오류를 방지하는 역할
        if (tileMap == null) { return Vector3Int.zero; }

        Vector3 worldPostion;

        if (mousePosition)
        {
            worldPostion = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPostion = position;
        }

        // 화면 좌표(2D)를 월드 좌표(3D)로 변환하는 메서드
        // WorldToCell: 월드 좌표를 타일맵의 그리드 좌표로 변환하는 메서드
        Vector3Int gridPosition = tileMap.WorldToCell(worldPostion);
       
        return gridPosition;
    }

    public TileBase GetTileBase(Vector3Int gridPosition, bool mousePosition = false)
    {
        if (tileMap == null)
        {
            
            tileMap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        if (tileMap == null) { return null; }


        TileBase tile = tileMap.GetTile(gridPosition);
        // 이 코드는 tileMap에서 특정 위치에 있는 타일을 가져오는 역할을 합니다.

        //Debug.Log("Tile in position =" + gridPosition + " is " + tile); 테스트용 

        if (tile == null)
        {
            return null;  // 타일이 없으면 null을 반환
        }

        return tile;  // 타일이 있으면 그 타일을 반환
    }
}