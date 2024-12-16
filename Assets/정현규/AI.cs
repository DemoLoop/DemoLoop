using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;

    public Transform[] arrWaypoint;
    private Vector2 destination;
    private Coroutine moveStop;
    public float moveSpeed = 3.0f;

    void Start()
    {
        this.rigid = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();

        Invoke("AiMove", 2);
    }

    private void AiMove()
    {
        int random = Random.Range(0, arrWaypoint.Length);
        Debug.LogFormat("random : {0}", random);

        // �������� ������ ����
        this.destination = this.arrWaypoint[random].position;
        Debug.Log("������: " + destination);

        // �ڷ�ƾ ����
        if (this.moveStop == null)
        {
            Debug.Log("�ڷ�ƾ����");
            this.moveStop = this.StartCoroutine(this.crAiMove());
        }

        this.anim.Play("Walk");
    }

    IEnumerator crAiMove()
    {
        while (true)
        {
            Vector2 currentPosition = this.transform.position;
            float distance = Vector2.Distance(currentPosition, this.destination);

            // ���������� �Ÿ� ��� �� �̵�
            if (distance <= 0.2f)
            {
                Debug.Log("������ ����");
                this.anim.Play("Idle");

                // �ڷ�ƾ ���� �� �ٸ� �������� �̵�
                if (this.moveStop != null)
                {
                    this.StopCoroutine(this.moveStop);
                    this.moveStop = null;
                    Invoke("AiMove", 1.5f);
                    break;
                }
            }
            else
            {
                // ������ �����ϰ� NPC �̵�
                Vector2 direction = (this.destination - currentPosition).normalized;
                this.rigid.MovePosition(currentPosition + direction * moveSpeed * Time.fixedDeltaTime);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
