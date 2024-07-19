using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    public StageScript stageScript;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision != null && collision.gameObject.CompareTag("Player")) {
            stageScript.StageStart();
        }
    }
}
