using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Seed Tile")]
public class SeedTile : ToolAction
{
    [SerializeField] AudioClip onPlowUsed;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, item item)
    {
    
        if (tileMapReadController.cropsManager.Check(gridPosition) == false)
        { 
            return false;
        }
            
        tileMapReadController.cropsManager.Seed(gridPosition, item.crop);
        
        return true;
    }

    public override void OnItemUsed(item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);
        AudioManager.instance.Play(onPlowUsed);
    }
}
