using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject toolbarPanel;
    [SerializeField] GameObject topPanel;
    [SerializeField] GameObject Mark;
    [SerializeField] GameObject dialougePanel;
    [SerializeField] PlayerManager playerManager;

    private OrderManager theOrder;

    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) 
            && dialougePanel.activeInHierarchy == false 
            && playerManager.isFlying == false
            && topPanel.activeInHierarchy == false
            )
        {
            theOrder.NotMove();

            Mark.SetActive(!panel.activeInHierarchy);

            panel.SetActive(!panel.activeInHierarchy);
            
            // activeInHierarchy, 현재 활성화 상태를 확인하고, 그 상태를 반전
            toolbarPanel.SetActive(!panel.activeInHierarchy);
            Mark.SetActive(!panel.activeInHierarchy);
        }

        if (panel.activeInHierarchy == false)
        {
            theOrder.Move();
        }
    }
}
