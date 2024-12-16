// 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class craftingEvent : Interactable
{
    [SerializeField] DialogueContainer dialouge;
    public bool Bool = false;
    private PlayerManager playerManager;
    //private InventoryController inventoryController;
    //private GameObject topPanel = GameManager.Instance.topPanel;
    //private GameObject inventoryPanel = GameManager.Instance.inventoryPanel;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public override void Interact(Character character)
    {
        StartCoroutine(CraftingCoroutine(character));
    }

    public void EndCrafting() 
    {
        GameManager.Instance.inventoryPanel.SetActive(false);
        GameManager.Instance.topPanel.SetActive(false);
        GameManager.Instance.toolbarPanel.SetActive(true);

        playerManager.notMove = false;
        playerManager.canMove = true;
        Bool = false;
    }

    IEnumerator CraftingCoroutine(Character character) 
    {
        Bool = true;
        playerManager.notMove = true;
        playerManager.canMove = false;

        // DialogueSystem 초기화
        GameManager.Instance.dialogueSystem.Initialize(dialouge);

        yield return new WaitUntil(() => GameManager.Instance.dialogueSystem.IsActive == false);

        playerManager = GameManager.Instance.player.GetComponent<PlayerManager>();

        GameManager.Instance.inventoryPanel.SetActive(true);
        GameManager.Instance.topPanel.SetActive(true);
        GameManager.Instance.toolbarPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndCrafting();
        }   
    }
}
