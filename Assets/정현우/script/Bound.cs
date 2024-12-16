// 작성자 : 정현우, 날짜 : 2024-09-06 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bound : MonoBehaviour
{
    private CameraManager theCamera;
    private BoxCollider2D bound;

    void Start()
    {
        bound = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraManager>();
        theCamera.SetBound(bound);
    }
}
