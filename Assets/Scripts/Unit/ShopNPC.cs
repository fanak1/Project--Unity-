using System.Resources;
using UnityEngine;

public class ShopNPC : InteractableObject
{
    bool playerIn = false;

    public override void InteractBegin()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerIn = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Shop.Instance.DeInit();
        playerIn = false;
    }

    private void Update()
    {
        if (GameManager.Instance.pause)
            return;
        if (playerIn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Shop.Instance.Init(ResourceSystem.Instance.GetAllAbilities());
            }
                
            
        }
    }
}
