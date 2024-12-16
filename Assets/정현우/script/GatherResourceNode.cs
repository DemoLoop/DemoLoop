// 2024-09-30, 작성자 : 정현우

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


// enum: 열거형은 관련된 상수 값을 그룹화하여 사용하기 위한 데이터 타입
public enum ResourceNodeType
{ 
    Undefind,
    Tree,
    Ore,
    Hunt,
    Water
}

// GatherResourceNode 클래스는 자원 수집 기능을 구현하는 Unity 스크립트입니다.

// 플레이어가 특정 위치에서 자원을 수집할 수 있도록 도와주는 동작을 정의합니다. 
[CreateAssetMenu(menuName = "Data/Tool Action/Gather Resource Node")]  
public class GatherResourceNode : ToolAction
{
    [SerializeField] AudioClip onOpenAudio;

    // 플레이어가 상호작용할 수 있는 영역의 크기
    [SerializeField] float sizeOfInteractableArea = 1.0f;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;

    // OnApply 메서드는 지정된 위치에서 자원 수집을 시도합니다.
    public override bool OnApply(Vector2 worldPoint)
    {
        // 지정된 위치와 크기 내에 있는 모든 2D 충돌체(콜라이더)를 감지합니다.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

        // 감지된 모든 충돌체를 반복합니다.
        foreach (Collider2D c in colliders)
        {
            // 각 충돌체에서 ToolHit 컴포넌트를 가져옵니다.
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null) // ToolHit 컴포넌트가 존재하는 경우
            {
                if (hit.CanBeHit(canHitNodesOfType) == true)
                {
                    AudioManager.instance.Play(onOpenAudio);
                    // Hit 메서드를 호출하여 자원을 수집합니다.

                    hit.Hit();
                    return true; // 작업이 성공적으로 수행되었음을 반환합니다.    
                }
            

            }
        }
        return false; // 감지된 ToolHit 객체가 없으면 false를 반환합니다.
    }
}
