// 2024-09-07 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    // virtual: 메서드를 가상으로 선언해, 자식 클래스에서 재정의할 수 있도록 허용하는 키워드
    public virtual void Hit()
    {
    
    }

    public virtual bool CanBeHit(List<ResourceNodeType> canBehit)
    {
        return true;
    }
}

