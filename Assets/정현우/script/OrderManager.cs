// 작성자 : 정현우

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    static private OrderManager instance;
    private PlayerManager thePlayer; // 이벤트 도중 키입력 처리 방지
    private List<MovingObject> characters; // 배열을 고정되어서 사용하지 말고 리스트를 사용함. 마을 npc 숫자가 다르기 때문에.
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
            instance = this;  //공유하는 변수값, 첫카메라를 계속 사용!!
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
        MovingObject[] temp = FindObjectsOfType<MovingObject>(); // MovingObject를 가진 객체를 모두 찾아서 반환해줌

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
                characters[i].gameObject.SetActive(true); // 오브젝트를 활성화
            }
        }
    }

    public void SetTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false); // 오브젝트를 비활성화
            }
        }
    }

    public void SetThorought(string _name) // 사물 패스하기
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = false; // 전부다 통과
            }
        }
    }

    public void SetThorought2(string _name) // 사물 패스 다시 못하게
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = true; // 전부다 통과
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

    public void Turn(string _name, string _dir) // 캐릭터가 쳐다보는 방향 설정
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