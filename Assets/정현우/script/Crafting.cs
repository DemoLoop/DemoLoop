// 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] AudioClip onOpenAudio;

    public void Craft(CraftingRecipe recipe)
    {
        if (inventory.CheckFreeSpace() == false) 
        {
            Debug.Log("Not enough space");
            return;
        }

        for (int i = 0; i < recipe.elements.Count; i++)
        {
            if (inventory.CheckItem(recipe.elements[i]) == false)
            { 
                Debug.Log("no elements");
                return; 
            }    
        }

        for (int i = 0; i < recipe.elements.Count; i++) // 11월 7일
        {
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count);
        }

        inventory.Add(recipe.output.item, recipe.output.count);
        AudioManager.instance.Play(onOpenAudio);
    }

}
