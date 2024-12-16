using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canMove : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾� �±׿� ���� ��
        {
            StartCoroutine(EnableMovementAfterDelay());
        }
    }

    private IEnumerator EnableMovementAfterDelay()
    {
        // 1�� ���
        yield return new WaitForSeconds(0.35f);

        // �̵� ���� ����
        playerManager.canMove = true;
        playerManager.notMove = false;

        yield return new WaitForSeconds(0.01f);

        // ��ü �ı�
        Destroy(gameObject);
    }
}