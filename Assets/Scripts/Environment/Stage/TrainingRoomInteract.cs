using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingRoomInteract : MonoBehaviour, IInteractable
{

    private bool isCollided = false;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player") && !isCollided) {
            isCollided = true;
            PlayerController script = collision.gameObject.GetComponent<PlayerController>();
            if(script != null) {
                playerController = script;
                script.StopMoving();
                Debug.Log("collided");
            }
        }
    }

    public void Exit() {
        playerController.ContinueMoving();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if( collision.gameObject.CompareTag("Player")) {
            isCollided = false;
        }
    }


    //Interactable
    public void Interact() {
        
    }

    public void OnClicked() {
        animator.SetTrigger("Interact");
    }

    public void OnMouseEnterInteract() {
        animator.SetTrigger("Mouseon");
    }
}
