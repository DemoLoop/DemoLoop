using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectsReferenceManager : MonoBehaviour
{
    public PlaceableObjectsManager placeableObjectsManager;

    public void Place(item item, Vector3Int pos)
    { 

        if (placeableObjectsManager == null) 
        {
            Debug.LogWarning("PlaceableObjectsManager°¡ ÇÒ´ç ¾È µÊ");
            return;
        }


        placeableObjectsManager.Place(item, pos);
    }
}
