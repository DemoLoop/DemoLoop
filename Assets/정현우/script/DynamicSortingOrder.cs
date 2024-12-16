using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 이 코드의 목적 : 오브젝트의 위치에 따라 화면에서 앞뒤로 표시되는 순서를 자동으로 조정
public class DynamicSortingOrder : MonoBehaviour
{
    // 스프라이트의 렌더링 순서를 설정하는 변수
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // 이 오브젝트에 연결된 SpriteRenderer 컴포넌트를 가져옴
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 매 프레임마다 실행되는 함수
    void Update()
    {
        // 오브젝트의 Y 좌표에 따라 스프라이트의 렌더링 순서를 설정
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}