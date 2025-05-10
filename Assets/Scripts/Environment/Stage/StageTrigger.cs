using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    public StageScript stageScript;

    private bool clear = false;

    private bool init = false;

    private bool enter = false;

    private (float, float) boxSize;

    private float sizeScale;

    const float CAM_RATIO = 3.5f;

    private void Start() {
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        boxSize = (bc.size.x, bc.size.y);
        sizeScale = boxSize.Item1 / CAM_RATIO;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision != null && collision.gameObject.CompareTag("Player") && !enter) {

            CameraController.Instance.ChangeCameraToStage(this);
            if (!clear && !init) {
                stageScript.StageStart();
                init = true;
            }
            enter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision != null && collision.gameObject.CompareTag("Player")) {
            enter = false;
        }
    }

    public void Clear() => clear = true;

    public void ResetStage() {
        enter = false;
        clear = false;
    }
}
