// 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour
{
    public List<GameObject> panels;

    private PlayerManager playerManager;
    private craftingEvent craftingEvent;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        craftingEvent = FindObjectOfType<craftingEvent>();
    }

    public void Show(int idPanel)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(i == idPanel);
        }
    }

    // 11-25 수정 
    public void EndCrafting()
    {
        GameManager.Instance.inventoryPanel.SetActive(false);
        GameManager.Instance.topPanel.SetActive(false);
        GameManager.Instance.toolbarPanel.SetActive(true);

        playerManager.notMove = false;
        playerManager.canMove = true;
        craftingEvent.Bool = false;
    }
}
