using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    // 캐릭터의 움직임을 담당하는 스크립트
    PlayerMovement characterController;
    // 캐릭터의 물리적 움직임을 제어하는 Rigidbody2D
    Rigidbody2D rgdb2d;
    // 캐릭터가 상호작용할 대상과의 거리
    [SerializeField] float offsetDistance = 1f;
    // 상호작용할 수 있는 범위의 크기
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    // 캐릭터의 정보 (이름, 상태 등)
    Character character;

    public bool Bool = false;

    // 상호작용할 수 있는 대상에 마우스를 올렸을 때 강조 표시를 해주는 스크립트
    [SerializeField] HighlightController highlightController;

    // 스크립트가 실행되기 전에 처음으로 실행되는 함수
    private void Awake()
    {
        // PlayerMovement 컴포넌트를 가져옴 (캐릭터 이동 제어)
        characterController = GetComponent<PlayerMovement>();

        // Rigidbody2D 컴포넌트를 가져옴 (캐릭터의 물리적 이동 제어)
        rgdb2d = GetComponent<Rigidbody2D>();

        // Character 컴포넌트를 가져옴 (캐릭터의 정보)
        character = GetComponent<Character>();
    }

    private void Update()
    {
        // 상호작용 가능한 대상을 체크
        Check();

        // 오른쪽 마우스 버튼이 눌렸을 때 상호작용 시도
        if (Input.GetMouseButton(1) && Bool == false) 
        {
            Interact();
        }
    }

    // 상호작용할 대상을 찾아서 강조 표시하는 함수
    private void Check()
    {
        // 캐릭터 앞쪽에 상호작용할 수 있는 범위를 설정
        Vector2 position = rgdb2d.position + characterController.lastmotionVector * offsetDistance;

        // 설정된 범위 안에 있는 모든 Collider2D를 가져옴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        // 범위 내에 있는 모든 오브젝트를 하나씩 확인
        foreach (Collider2D c in colliders)
        {
            // 상호작용할 수 있는 Interactable 컴포넌트를 가진 오브젝트인지 확인
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                // 상호작용 가능한 오브젝트라면 강조 표시
                highlightController.Highlight(hit.gameObject);
                return; // 상호작용 가능한 대상이 있으면 함수 종료
            }
        }
        // 상호작용 가능한 대상이 없으면 강조 표시 제거
        highlightController.Hide();
    }

    // 실제로 상호작용을 하는 함수
    private void Interact()
    {
        // 캐릭터 앞쪽에 상호작용할 수 있는 범위를 설정
        Vector2 position = rgdb2d.position + characterController.lastmotionVector * offsetDistance;

        // 설정된 범위 안에 있는 모든 Collider2D를 가져옴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        // 범위 내에 있는 모든 오브젝트를 하나씩 확인
        foreach (Collider2D c in colliders)
        {
            // 상호작용할 수 있는 Interactable 컴포넌트를 가진 오브젝트인지 확인
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                // 상호작용 가능한 오브젝트라면 상호작용 실행
                hit.Interact(character);
                break; // 첫 번째로 만난 상호작용 대상과만 상호작용하고 함수 종료
            }
        }
    }
}
