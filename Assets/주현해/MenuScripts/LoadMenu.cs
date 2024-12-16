using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{

    public void BackSceneBtn()
    {
        Debug.Log("불러옴 뒤로가기 클릭됨.");
        SceneManager.LoadScene("StartMenu");
    }
}
