using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject talkPanel;
    public Text text;
    int clickCount = 0;
    bool getGem = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(clickCount == 0){
                text.text = "푸른 빛을 품은 그대를 찾아서";
                clickCount++;
            }
            else if(!getGem && clickCount ==1){
                talkPanel.SetActive(false);
            }
        }
        Debug.Log(getGem);
        Debug.Log(clickCount);
    }

}
