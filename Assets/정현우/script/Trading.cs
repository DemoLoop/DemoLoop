// 작성자 : 정현우
// 12-11 스크립트 일부 수정
// 12-14일 상점 구입 기능 추가

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel; // 12-11
    [SerializeField] GameObject toolbarPanel; // 12-11
    [SerializeField] PlayerManager playerManager;
    public bool Bool = false;

    Store store;

    Currency money;

    // 12-14
    public itemStorePanel itemStorePanel; //public으로 오브젝트 접근(수정)
    [SerializeField] ItemContainer playerInventory;
    [SerializeField] ItemPanel inventoryItemPanel;

    private void Awake()
    {
        money = GetComponent<Currency>();
        //itemStorePanel = storePanel.GetComponent<itemStorePanel>(); // 12-14, 작동이 안 됨
    }

    public void BeginTraing(Store store) 
    {
        this.store = store;

        Bool = true;
        playerManager.notMove = true;
        playerManager.canMove = false;

        //itemStorePanel.Clear(); 버그남
        itemStorePanel.SetInventory(store.storeContent); // 12-14

        storePanel.SetActive(true);
        inventoryPanel.SetActive(true); // 12-11
        toolbarPanel.SetActive(false); // 12-11

    }

    // 12-14
    internal void BuyItem(int id)
    {
        item itemToBuy = store.storeContent.slots[id].item;
        int totalPrice = (int)(itemToBuy.price * store.sellFromPlayerMultip);
        if (money.Check(totalPrice)) 
        {
            money.Decrease(totalPrice);
            playerInventory.Add(itemToBuy);
            inventoryItemPanel.Show();
        }
    }

    public void StopTrading()
    {
        store = null;

        storePanel.SetActive(false);
        inventoryPanel.SetActive(false); // 12-11
        toolbarPanel.SetActive(true); // 12-11

        playerManager.notMove = false;
        playerManager.canMove = true;
        Bool = false;
    }

    public void SellItem()
    {
        if (GameManager.Instance.dragAndDropController.CheckForSale() == true)
        {
            ItemSlot itemToSell = GameManager.Instance.dragAndDropController.itemSlot;
            int moneyGain = itemToSell.item.stackable == true ?
            // 개수 있는 아이템, 아닌 것 구별
            (int)(itemToSell.item.price * itemToSell.count * store.buyFromPlayerMultip) : 
            (int)(itemToSell.item.price * store.buyFromPlayerMultip); 
            money.Add(moneyGain);
            itemToSell.Clear();
            GameManager.Instance.dragAndDropController.UpdateIcon();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopTrading();
        }
    }
}
