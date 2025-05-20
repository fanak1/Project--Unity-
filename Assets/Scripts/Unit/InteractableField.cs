using UnityEngine;

public class InteractableField : InteractableObject
{


    public override void InteractBegin()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        InteractFinish();
        Destroy(gameObject);
    }
}
