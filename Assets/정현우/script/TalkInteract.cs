// 2024-09-19 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : Interactable
{
    [SerializeField] DialogueContainer dialouge;
    public override void Interact(Character character)
    {
        GameManager.Instance.dialogueSystem.Initialize(dialouge);
    }

}
