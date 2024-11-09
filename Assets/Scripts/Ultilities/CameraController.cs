using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : StaticInstance<CameraController> {
    [SerializeField] private float cameraSpeed = 100f;

    private CameraState cameraState;

    private Vector3 nextPoint;

    private Camera cam;

    private float time;

    private Coroutine coroutine;

    // Start is called before the first frame update
    void Start() {
        nextPoint = transform.position;
        time = 0f;
        cam = GetComponent<Camera>();
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

    public void SetCameraSize(float size) {
        StartSetSize(size);
    }

    private void StartSetSize(float size) {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(SetSize(size));
    }

    private IEnumerator SetSize(float size) {
        float scale = 0f;
        float ogSize = cam.orthographicSize;
        while(scale < 1f) {
            cam.orthographicSize = Mathf.Lerp(ogSize, size, scale);
            scale += Time.deltaTime;
            yield return null;
        }
    }
}

public enum CameraState {
    Stop = 0,
    Moving = 1
}
