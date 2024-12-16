using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    void GenerateData(){
        talkData.Add(1000, new string[] {"정상화의 신, 신창섭"});
        
        talkData.Add(100, new string[] {"팩트는, 정상화되고 있다는거임"});

        talkData.Add(200, new string[] {"나무 보관함에 담아, 돈 다발"});
    }

    public string GetTalk(int id, int talkIndex)
    {   
        return talkData[id][talkIndex];
    }
}
