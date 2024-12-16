//작성자 : 정현우, 2024-12-01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Interactable
{
    [SerializeField] DialogueContainer dialouge;
    public ItemContainer storeContent; // 상점 기능추가
    public float buyFromPlayerMultip = 0.5f;
    public float sellFromPlayerMultip = 1.5f;

    public override void Interact(Character character)
    {
        StartCoroutine(StoreCoroutine(character)); // character를 인자로 전달
    }

    IEnumerator StoreCoroutine(Character character)
    {
        // 1. DialogueSystem 초기화
        GameManager.Instance.dialogueSystem.Initialize(dialouge);

        yield return new WaitUntil(() => GameManager.Instance.dialogueSystem.IsActive == false);

        // 2. Trading 컴포넌트 확인
        Trading trading = character.GetComponent<Trading>();

        if (trading == null)
        {
            yield break; // Trading이 없으면 코루틴 종료
        }

        // 3. Trading 시작
        trading.BeginTraing(this);
    }
}