using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // ToolAction 클래스는 ScriptableObject를 상속받아 툴의 행동을 정의하는 기본 클래스
public class ToolAction : ScriptableObject
{
    public int energyCost = 0;

    // 툴이 특정 월드 포인트에 적용될 때 호출되는 메서드
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }

    // 툴이 타일 맵의 특정 위치에 적용될 때 호출되는 메서드
    public virtual bool OnApplyToTileMap(Vector3Int tileMapPosition, TileMapReadController tileMapReadController, item item)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }

    // 아이템이 사용되었을 때 호출되는 메서드
    public virtual void OnItemUsed(item usedItem, ItemContainer inventory)
    {
        
    }
}
