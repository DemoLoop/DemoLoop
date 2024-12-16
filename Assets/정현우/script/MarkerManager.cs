using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

// MarkerManager 클래스: 타일맵에서 마커를 관리하는 스크립트
public class MarkerManager : MonoBehaviour
{
    // 타일맵과 마커로 사용할 타일을 지정합니다

    [SerializeField] Tilemap targetTilemap; // 마커를 표시할 타일맵
    [SerializeField] TileBase tile; // 사용할 타일 (마커)

    public Vector3Int markedCellPostion; // 현재 마커가 있는 셀의 위치
    Vector3Int oldCellPostion; // 이전 마커 위치
    bool show; // 마커 표시 여부

    // 매 프레임 업데이트되는 함수
    private void Update()
    {
        // 마커가 표시되지 않으면 아무것도 하지 않습니다
        if (show == false)
        {
            return;
        }

        // 이전 위치의 타일을 지웁니다
        targetTilemap.SetTile(oldCellPostion, null);
        // 현재 위치에 마커 타일을 설정합니다
        targetTilemap.SetTile(markedCellPostion, tile);
        // 현재 위치를 이전 위치로 업데이트합니다
        oldCellPostion = markedCellPostion;
    }

    // 마커 표시 여부를 설정하는 함수
    internal void Show(bool selectable)
    {
        show = selectable; // 선택 가능한지 여부를 저장합니다
        targetTilemap.gameObject.SetActive(show); // 타일맵의 활성화 상태를 설정합니다
    }
}