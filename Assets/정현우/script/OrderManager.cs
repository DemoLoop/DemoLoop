// �ۼ��� : ������

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    static private OrderManager instance;
    private PlayerManager thePlayer; // �̺�Ʈ ���� Ű�Է� ó�� ����
    private List<MovingObject> characters; // �迭�� �����Ǿ ������� ���� ����Ʈ�� �����. ���� npc ���ڰ� �ٸ��� ������.
    // Add(), Remove(), Clear()

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;  //�����ϴ� ������, ùī�޶� ��� ���!!
        }
    }

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    public void PreLoadCharacteer()
    {
        characters = ToList();
    }

    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>(); // MovingObject�� ���� ��ü�� ��� ã�Ƽ� ��ȯ����

        for (int i = 0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }

        return tempList;
    }

   
    public void NotMove()
    {
        thePlayer.notMove = true;
    }

    public void Move()
    {
        thePlayer.notMove = false;
    }
   

    public void SetUnTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true); // ������Ʈ�� Ȱ��ȭ
            }
        }
    }

    public void SetTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false); // ������Ʈ�� ��Ȱ��ȭ
            }
        }
    }

    public void SetThorought(string _name) // �繰 �н��ϱ�
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = false; // ���δ� ���
            }
        }
    }

    public void SetThorought2(string _name) // �繰 �н� �ٽ� ���ϰ�
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = true; // ���δ� ���
            }
        }
    }

    public void Move(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }

    public void Turn(string _name, string _dir) // ĳ���Ͱ� �Ĵٺ��� ���� ����
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].animator.SetFloat("DirY", 0f);
                characters[i].animator.SetFloat("DirX", 0f);
                switch (_dir)
                {
                    case "UP":
                        characters[i].animator.SetFloat("DirY", 1f);
                        break;
                    case "DOWN":
                        characters[i].animator.SetFloat("DirY", -1f);
                        break;
                    case "LEFT":
                        characters[i].animator.SetFloat("DirX", -1f);
                        break;
                    case "RIGHT":
                        characters[i].animator.SetFloat("DirX", 1f);
                        break;
                }
            }
        }
    }

    void Update()
    {
    }
}