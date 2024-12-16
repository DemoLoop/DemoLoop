using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing_Interact : Interactable
{
    private Animator animator;
    private bool Fising_triger = false;
    private bool Blockbool = false;
    public ResourceNode_02 resourceNode_02;
    private void Start()
    {
        animator = GameManager.Instance.player.GetComponent<Animator>();

    }

    
    public override void Interact(Character character)
    {
        if (!Blockbool)
        {
            Blockbool = true;
            Fising_triger = true;
            Fising_game(Fising_triger);
        }
        
    }


    private void Fising_game(bool triger)
    {
        StopAllCoroutines();
        StartCoroutine(FishingCoroutine());
    }

    IEnumerator FishingCoroutine() 
    {
        resourceNode_02.Hit();
        animator.SetTrigger("fishing");
        yield return new WaitForSeconds(0.5f);
        Fising_triger = false;
        Blockbool = false;
    }
}
