using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    public StageScript stageScript;

    private bool clear = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision != null && collision.gameObject.CompareTag("Player")) {
            Debug.Log(this.transform.position);
            CameraController.Instance.StartMovingTo(this.transform.position);
            if (!clear) stageScript.StageStart();
        }
    }

    public void Clear() => clear = true;
}
