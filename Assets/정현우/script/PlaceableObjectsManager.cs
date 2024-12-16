//작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjects;
    [SerializeField] Tilemap targetTilemap;

    private void Start()
    {
        GameManager.Instance.GetComponent<PlaceableObjectsReferenceManager>().placeableObjectsManager = this;
    }

    // 주어진 아이템을 특정 위치에 배치하는 기능을 수행함
    public void Place(item item, Vector3Int positionOnGrid)
    {
        // 주어진 아이템의 프리팹[자산(asset)]을 인스턴스화하여 새로운 게임 오브젝트를 생성
        GameObject go = Instantiate(item.itemPrefeb);
        // 타일맵의 격자 좌표를 월드 좌표로 변환
        Vector3 position = targetTilemap.CellToWorld(positionOnGrid) + targetTilemap.cellSize/2;
        position -= Vector3.forward * 0.1f;
        go.transform.position = position;
        // 새로운 PlaceableObject 인스턴스를 placeableObjects 컨테이너에 추가합니다.
        // 이 부분은 배치된 객체를 관리하기 위한 것
        placeableObjects.placeableObjects.Add(new PlaceableObject(item, go.transform, positionOnGrid));
    }
}
