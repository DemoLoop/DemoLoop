// 2024-09-07 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    //싱글톤의 장점:
    //전역 접근 가능: GameManager.Instance로 어디서든 쉽게 접근 가능.
    //중복 방지: 하나의 인스턴스만 존재하도록 관리.
    //중앙 관리 가능: 중요한 시스템 관리에 유용.

    public static GameManager Instance; 
    private void Awake() 
    {   
        Instance = this;
    }
    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
    public DialogueSystem dialogueSystem;
    public DayTimeController TimeController;
    public GameObject topPanel;
    public GameObject inventoryPanel;
    public GameObject toolbarPanel;
    public PlaceableObjectsReferenceManager placeableObjects;
}
