using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[System.Serializable]
public class TestMove // 커스텀 클래스
    {
    public string name;
    public string direction;
    }

public class Test01 : MonoBehaviour
{
    [SerializeField]
    public TestMove[] move;

    private OrderManager theOrder;

    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theOrder.PreLoadCharacteer();
            for(int i = 0; i< move.Length; i++)
                theOrder.Move(move[i].name, move[i].direction);
        }
    }
}
