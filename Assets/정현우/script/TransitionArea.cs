using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionArea : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    private bool Bool = false;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }


    // OnTriggerEnter2D�� Unity���� 2D ���� �ý����� ����� ��, Ʈ���� �浹�� �߻����� �� ȣ��Ǵ� �޼���
    // Ư�� �� ������Ʈ �� �ϳ��� Ʈ���ŷ� �����Ǿ� ���� �� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Bool)
        {
            Bool = true;
            playerManager.canMove = false;
            playerManager.notMove = true;

            if (collision.transform.CompareTag("Player"))
            {
                // �浹�� ������Ʈ�� �θ� ������Ʈ���� Transition ������Ʈ�� �����ɴϴ�
                transform.parent.GetComponent<Transition>().InitiateTransition(collision.transform);
                Bool = false;
            }
        }
        
    }
}
