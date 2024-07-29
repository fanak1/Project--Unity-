using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : StaticInstance<CameraController> {
    [SerializeField] private float cameraSpeed = 100f;

    private CameraState cameraState;

    private Vector3 nextPoint;

    private float time;

    // Start is called before the first frame update
    void Start() {
        nextPoint = transform.position;
        time = 0f;
    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        switch (cameraState) {
            case CameraState.Stop:
                
                break;
            case CameraState.Moving:
                if (MoveCameraTo(nextPoint)) cameraState = CameraState.Stop;
                break;
        }
    }

    bool MoveCameraTo(Vector3 position) {
        if (transform.position == position) {
            
            return true;
        } else {
            transform.position = Vector3.MoveTowards(transform.position, position, cameraSpeed);
        }
        return false;
    }

    public void StartMovingTo(Vector3 position) {
        time = 0f;
        Vector3 tempPosition = new Vector3(position.x, position.y, transform.position.z);
        nextPoint = tempPosition;
        if (nextPoint != transform.position) {
            cameraState = CameraState.Moving;
        }
    }
}

public enum CameraState {
    Stop = 0,
    Moving = 1
}
