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

        // 랜덤으로 목적지 선정
        this.destination = this.arrWaypoint[random].position;
        Debug.Log("목적지: " + destination);

        // 코루틴 시작
        if (this.moveStop == null)
        {
            Debug.Log("코루틴시작");
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

            // 목적지와의 거리 계산 및 이동
            if (distance <= 0.2f)
            {
                Debug.Log("목적지 도착");
                this.anim.Play("Idle");

                // 코루틴 정지 후 다른 목적지로 이동
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
                // 방향을 설정하고 NPC 이동
                Vector2 direction = (this.destination - currentPosition).normalized;
                this.rigid.MovePosition(currentPosition + direction * moveSpeed * Time.fixedDeltaTime);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
