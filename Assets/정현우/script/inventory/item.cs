using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")] 

public class item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;
    public ToolAction onAction;
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
    public Crop crop;
    public bool iconHighlight;
    public GameObject itemPrefeb;
    public int price = 100;
    public bool canBeSold = true;
}
