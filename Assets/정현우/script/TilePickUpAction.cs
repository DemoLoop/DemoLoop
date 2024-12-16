using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Data/Tool Action/Harvest")]
public class TilePickUpAction : ToolAction
{
    [SerializeField] AudioClip onPlowUsed;
    // override 키워드는 C#에서 상속받은 클래스에서 부모 클래스의 메서드를 재정의
    // OnApplyToTileMap 메서드는 ToolAction 클래스에서 정의된 기본 메서드를 재정의
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, item item)
    {
        tileMapReadController.cropsManager.PickUp(gridPosition);
        //AudioManager.instance.Play(onPlowUsed);
        return true;
    }
}
