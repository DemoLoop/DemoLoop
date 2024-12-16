using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class NPCMove
{
    [Tooltip("NPCMove�� äũ�ϸ� NPC�� ������")]
    public bool NPCmove;
    public string[] direction; // NPC�� �����̴� ���� ����;
    [Range(1, 5)] // ��ũ�ѹ� 1-5���� ����
    public int freqency; // NPC�� �󸶳� ���� �ӵ��� ������ ���ΰ�?


}

public class NPCManager : MovingObject
{
    [SerializeField]
    public NPCMove npc;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        StartCoroutine(MoveCoroutine());
    }

    public void SetMove()
    {
        
    }

    public void SetNotMove()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveCoroutine() 
    {
        if (npc.direction.Length != 0) 
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                switch (npc.freqency)
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        break;
                }

                yield return new WaitUntil(() =>    queue.Count < 2);

                base.Move(npc.direction[i], npc.freqency);

                if(i == npc.direction.Length -1)
                {
                    i = -1;
                }           
            }
        }
    }
}
