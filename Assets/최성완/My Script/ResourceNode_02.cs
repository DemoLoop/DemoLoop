using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class ResourceNode_02 : ToolHit
{
    //public GameObject positionForFish;
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 1f;
    [SerializeField] item[] item;
    [SerializeField] int itemCountInOneDrop;
    [SerializeField] int dropCount = 1; // �����Ǵ� ������ ����; 
    [SerializeField] ResourceNodeType nodeType;

    public override void Hit()    
    {

        StartCoroutine(HandleHit());
    }

    private IEnumerator HandleHit()
    {
        yield return new WaitForSeconds(0.5f); // 0.5�� ����


        while (dropCount > 0)
        {
            dropCount -= 1;
            // ������ ��ġ�� ������ ����;
            Vector3 Player_Position = GameManager.Instance.player.transform.position;

            Player_Position.x += spread;
            Player_Position.y += spread;

            Debug.Log(Player_Position.x + "   " + Player_Position.y);

            int fish = UnityEngine.Random.Range(0, 4);   
            
            itemSpawnManager.instance.SpawnItem(Player_Position, item[fish], itemCountInOneDrop);
        }
        dropCount += 1;
        //Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
