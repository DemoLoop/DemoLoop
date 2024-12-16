using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void BackSceneBtn()
    {
        Debug.Log("옵션 뒤로가기 클릭됨.");
        SceneManager.LoadScene("StartMenu");
    }
}
