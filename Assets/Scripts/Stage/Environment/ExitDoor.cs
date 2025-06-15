using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class ExitDoor : MonoBehaviour
{
    //public TextMeshProUGUI message;

    public Action OnDoorEnter;

    private bool playerIn;

    public GameObject tooltip;

    private void Start() {
        //message.gameObject.SetActive(false);
        tooltip.SetActive(false);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.exitDoor);
    }

    private void Update() {
        if (GameManager.Instance.pause)
            return;
        if (playerIn && Input.GetKeyDown(KeyCode.F)){
            SoundManager.Instance.PlaySFX(SoundManager.Instance.exitDoor);
            OnDoorEnter?.Invoke();
            Destroy(gameObject);
            if(LevelManager.Instance.isLastLevel)
            {
                Statistic.Instance.Open(true);
            } else
            {
                GameManager.Instance.NextLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
           // message.gameObject.SetActive(true);
            playerIn = true;
            tooltip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
           // message.gameObject.SetActive(false);
            playerIn = false;
            tooltip.SetActive(false);
        }

    }
}
