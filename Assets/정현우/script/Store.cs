//�ۼ��� : ������, 2024-12-01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Interactable
{
    [SerializeField] DialogueContainer dialouge;
    public ItemContainer storeContent; // ���� ����߰�
    public float buyFromPlayerMultip = 0.5f;
    public float sellFromPlayerMultip = 1.5f;

    public override void Interact(Character character)
    {
        StartCoroutine(StoreCoroutine(character)); // character�� ���ڷ� ����
    }

    IEnumerator StoreCoroutine(Character character)
    {
        // 1. DialogueSystem �ʱ�ȭ
        GameManager.Instance.dialogueSystem.Initialize(dialouge);

        yield return new WaitUntil(() => GameManager.Instance.dialogueSystem.IsActive == false);

        // 2. Trading ������Ʈ Ȯ��
        Trading trading = character.GetComponent<Trading>();

        if (trading == null)
        {
            yield break; // Trading�� ������ �ڷ�ƾ ����
        }

        // 3. Trading ����
        trading.BeginTraing(this);
    }
}