using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class ShopNPC : InteractableObject
{
    bool playerIn = false;

    public List<ScriptableAlbilities> shopItems;

    bool inShop = false;

    public GameObject tooltip;

    private void Start() {
        tooltip.SetActive(false);
        shopItems = ResourceSystem.Instance.GetClaimableAbilitiesForPlayer();
    }

    public override void InteractBegin()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerIn = true;
        tooltip.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if(inShop)
        {
            Shop.Instance.StartDeInit();
            InteractFinish();
            playerIn = false;
            inShop = false;
        }
        tooltip.SetActive(false);

    }

    private void Update()
    {
        if (GameManager.Instance.pause)
            return;
        if (playerIn)
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                inShop = true;
                Shop.Instance.Init(shopItems, (ScriptableAlbilities a) => Buy(a));
            }
                
            
        } else
        {
            
        }
    }

    public void Buy(ScriptableAlbilities a) {
        shopItems.Remove(a);
    }
}
