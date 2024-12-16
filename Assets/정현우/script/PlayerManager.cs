using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MovingObject
{
    static public PlayerManager instance;

    //public int Money;

    public TextMeshProUGUI MeneyPanel;

    public GameObject TopPanel;

    // �뽬�� ���� �ӵ�
    public float runSpeed = 4;
    // ���� ����Ǵ� �뽬 �ӵ�
    private float applyRunSpeed;
    // �뽬 ������ ���θ� �����ϴ� ����
    private bool applyRunFlag = false;
    // ĳ���Ͱ� ������ �� �ִ��� ���θ� �����ϴ� ����
    public bool canMove = true;

    public bool notMove = false;

    // ���� ���� ����
    public bool isFlying = false;

    public GameObject toolBarPanel;
    public GameObject inventory;

    public Image flyImage;


    // ���� ���� �� ȣ��Ǵ� �Լ� (�ʱ�ȭ)
    private void Start()
    {
        queue = new Queue<string>();
        // ĳ������ �ݶ��̴��� �ִϸ����͸� ������
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // ĳ���Ͱ� �����̴� ������ ó���ϴ� �Լ�
    IEnumerator MoveCoroutine()
    {
        // �÷��̾ �����̴� ����Ű�� ������ ���� �� ����
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0 && !notMove)
        {
            // ���� ���� ShiftŰ�� ������ �뽬 �ӵ��� ����
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed; // �뽬 �ӵ� ����
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0; // �뽬 ����
                applyRunFlag = false;
            }

            // �÷��̾��� �̵� ������ ������ (x, y ��ǥ)
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

            // ����, �¿� ���ÿ� ������ �� ���� ������ ���� (�̻��� ������ ����)
            if (vector.x != 0)
                vector.y = 0;

            // �ִϸ��̼ǿ��� ������ ���� (x��, y��)
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            if (isFlying == false)
            {
                bool checkCollsionFlag = base.CheckCollsion();
                if (checkCollsionFlag) { break; } // �ڽ��ݶ��̴��� ������ �������� �ʴ´�.
            }
           

            // �̵� ���̸� �ִϸ��̼� ����
            animator.SetBool("Walking", true);

            // �̵��� �Ÿ���ŭ ���� ������ �ݺ�
            while (currentWalkCount < walkCount)
            {
                // x������ �̵�
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed) / 100, 0, 0);
                }
                // y������ �̵�
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed) / 100, 0);
                }

                // �뽬 ���� ��� �� ������ �ȵ��� ī��Ʈ ����
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;

                // ��� ���߰� �ٽ� ���� (0.01�ʸ���)
                yield return new WaitForSeconds(0.01f);
            }

            // �̵��� ������ ���� ���� ���� �ʱ�ȭ
            currentWalkCount = 0;
        }

        // �̵��� ������ �ȱ� �ִϸ��̼� ����
        animator.SetBool("Walking", false);
        // �ٽ� ������ �� �ְ� ����
        canMove = true;
    }

    // �� �����Ӹ��� ȣ��Ǵ� �Լ� (Ű �Է� üũ)
    private void Update()
    {
        //MeneyPanel.text = "$"+Money.ToString();

        // ĳ���Ͱ� ������ �� �ִ� ���¿���
        if (canMove && !notMove)
        {
            // �÷��̾ �̵� Ű�� ������ �̵� �ڷ�ƾ ����
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false; // �����̴� ������ �ٸ� �Է��� ���� ����
                StartCoroutine(MoveCoroutine()); // �̵� ����
            }
        }


        if (Input.GetKeyDown(KeyCode.F1) && !inventory.activeInHierarchy && !TopPanel.activeInHierarchy)
        {
            isFlying = !isFlying; // ���� ���� ���
            animator.SetBool("fly", isFlying); // ���� �ִϸ��̼� ����

            if (isFlying)
            {
                flyImage.color = new Color32(255, 255, 255, 255);
                toolBarPanel.SetActive(false);  
                boxCollider.enabled = false; // ���� �߿��� �ݶ��̴� ��Ȱ��ȭ
                runSpeed = 12; // ���� �� �ӵ��� 12�� ����
                SetSortingLayer("fly");
            }
            else
            {
                flyImage.color = new Color32(125, 125, 125, 255);
                toolBarPanel.SetActive(true);
                boxCollider.enabled = true; // ������ �ƴ� �� �ݶ��̴� Ȱ��ȭ
                runSpeed = 4; // ������ �ƴ� �� �ӵ��� 4�� ����
                SetSortingLayer("object_1");
            }
        }
    }

    private void SetSortingLayer(string layerName)
    {
        // ��������Ʈ �������� �����ɴϴ�.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = layerName; // ���� ���̾� ����
        }
    }
}
