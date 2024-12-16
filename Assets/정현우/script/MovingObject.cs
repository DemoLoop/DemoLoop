// 2024-9-15, ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ �����̴� ����� ó���ϴ� Ŭ����
public class MovingObject : MonoBehaviour
{
    public string characterName;

    // ĳ���Ͱ� �����̴� �ӵ�
    public float speed;
    // �� ���� �� ������ �̵��� ������ ����
    public float walkCount;
    // ���� ���� Ƚ��
    protected int currentWalkCount;

    private bool notCoroutine = false;

    // ĳ���Ͱ� ������ ������ �����ϴ� ����
    protected Vector3 vector;  // protected �θ� �ڽ��� ������ ����������, �ܺο��� �ν����Ϳ��� ������ �Ұ�����

    //���Լ��� ����
    public Queue<string> queue;
    // ĳ���Ͱ� �ε��� �� �ִ� �浹 �ڽ� (�ݶ��̴�)
    public BoxCollider2D boxCollider;
    // ĳ���Ͱ� �浹�� ���̾ ���� (���̳� ��ֹ�)
    public LayerMask layerMask;
    // ĳ������ �ִϸ��̼��� �����ϴ� ������Ʈ
    public Animator animator;

    public void Move(string _dir, int _freqency = 5)  // int _freqency = 5, �μ��� �Ѱܵ� ���� ������ �� �ְ� ����
    {
        queue.Enqueue(_dir);
        if (!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _freqency));
        }
    }

    IEnumerator MoveCoroutine(string _dir, int _freqency)
    {
        while (queue.Count != 0)
        {
            string direction = queue.Dequeue();
            vector.Set(0, 0, vector.z);


            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * (speed), vector.y * (speed), 0);
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }

            currentWalkCount = 0;
            if (_freqency != 5)
                animator.SetBool("Walking", false);
        }
        animator.SetBool("Walking", false);
        notCoroutine = false;
    }

    protected bool CheckCollsion() 
    {
        RaycastHit2D hit;
        Vector2 start = transform.position; // ĳ���� ���� ��ġ
        Vector2 end = start + new Vector2(vector.x * speed * walkCount / 30, vector.y * speed * walkCount / 60);
        // �̵��� ��ǥ ��ġ

        // ĳ������ �ݶ��̴��� ��Ȱ��ȭ (�ڱ� �ڽŰ� �浹���� �ʱ� ����)
        boxCollider.enabled = false;
        // ����ĳ��Ʈ�� �̵��� ��ο� ��ֹ��� �ִ��� Ȯ��
        hit = Physics2D.Linecast(start, end, layerMask);
        // �ݶ��̴��� �ٽ� Ȱ��ȭ
        boxCollider.enabled = true;

        // ���� �浹�ϴ� ��ü�� ������ �̵� �ߴ�
        if (hit.transform != null)
            return true;
        return false;
    }
}
