using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class ExitDoor : MonoBehaviour
{
    public TextMeshProUGUI message;

    public Action OnDoorEnter;

    private bool playerIn;

    private void Start() {
        message.gameObject.SetActive(false);
    }

    private void Update() {
        if (playerIn && Input.GetKeyDown(KeyCode.K)){
            Debug.Log("K presed");
            OnDoorEnter?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            message.gameObject.SetActive(true);
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            message.gameObject.SetActive(false);
            playerIn = false;
        }

    }
}
