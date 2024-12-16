using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class IconHighlight : MonoBehaviour
{
    // Vector3: 물리적 계산, 부드러운 움직임 및 방향 설정에 적합
    // Vector3Int: 그리드 기반 시스템이나 정수 좌표가 필요한 경우에 적합

    // CellPosition은 타일맵 내의 특정 셀의 위치를 나타내는 변수
    public Vector3Int CellPosition;
    Vector3 targetPosition;
    [SerializeField] Tilemap targetTilemap;
    SpriteRenderer spriteRenderer;

    bool canSelect;
    bool show;


    // CanSelect와 Show 프로퍼티는 게임 오브젝트의 활성화 상태를 제어하는 데 사용
    //  canSelect와 show가 모두 true일 때만 게임 오브젝트를 활성화합니다.
    //  즉, 아이콘이 선택 가능하고 보여야 할 때만 화면

    public bool CanSelect
    { 
        set
        {
            canSelect = value;
            gameObject.SetActive(canSelect && show);
        }
    }

    public bool Show
    {
        set 
        {
            show = value;
            gameObject.SetActive(canSelect && show);
        }
    }

    private void Update()
    {
        // CellToWorld 메서드는 주어진 셀의 그리드 좌표를 월드 공간의 좌표로 변환
        targetPosition = targetTilemap.CellToWorld(CellPosition);

        // cellSize를 2로 나누는 것은 셀의 중심을 찾기 위한 계산.
        // 타일의 크기를 반으로 나누면, 타일의 중심 좌표를 얻을 수 있음.
        // 예를 들어, 셀 크기가 (1, 1)이라면, (0.5, 0.5)가 됨
        // targetPosition에 셀의 중심을 더하여, 게임 오브젝트가 타일의 중앙에 위치하도록 조정하는 것
        transform.position = targetPosition + targetTilemap.cellSize/2;
    }

    internal void Set(Sprite icon)
    {
        if (spriteRenderer == null) { spriteRenderer = GetComponent<SpriteRenderer>(); }

        spriteRenderer.sprite = icon;   
    }
}
