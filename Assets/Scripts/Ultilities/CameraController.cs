using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : StaticInstance<CameraController> {
    [SerializeField] private float cameraSpeed = 0.05f;

    private CameraState cameraState;

    private Vector3 nextPoint;

    private Vector2 clampOffset;
    private Vector2 clampPosition;

    private bool isResizing = true;

    public float resizeTime;

    private Camera cam;


    private Coroutine coroutine;
    private IEnumerator routine;

    // Start is called before the first frame update
    void Start() {
        nextPoint = transform.position;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if(PlayerUnit.instance != null) StartMovingTo(PlayerUnit.instance.gameObject.transform.position);
        switch (cameraState) {
            case CameraState.Stop:
                
                break;
            case CameraState.Moving:
                
                if (MoveCameraTo(nextPoint)) cameraState = CameraState.Stop;
                break;

            case CameraState.Following:
                if (MoveCameraTo(nextPoint, true)) cameraState = CameraState.Stop;
                break;

        }
    }

    bool MoveCameraTo(Vector3 position, bool follow = false) {
        if (transform.position == position) {
            
            return true;
        } else {
            transform.position = Vector3.MoveTowards(transform.position, position, follow ? 1f : cameraSpeed);
        }
        return false;
    }


    public void StartMovingTo(Vector3 position) {
        float clampX = Mathf.Clamp(position.x, clampPosition.x - clampOffset.x, clampPosition.x + clampOffset.x);
        float clampY = Mathf.Clamp(position.y, clampPosition.y - clampOffset.y, clampPosition.y + clampOffset.y);
       
        Vector3 tempPosition = new Vector3(clampX, clampY, transform.position.z);
        nextPoint = tempPosition;
        if (nextPoint != transform.position) {
            if(cameraState != CameraState.Moving)
                cameraState = CameraState.Following;
        }
    }

    public void SetCameraSize(float size) {
        StartSetSize(size);
    }

    public void ChangeCameraToStage(StageTrigger stage)
    {
        var box = stage.gameObject.GetComponent<BoxCollider2D>();
        var size = box.size;

        // -- Formula from Box Collider size to Camera orthographic size:
        //    height = 2f * orthographic size
        //    width = height * cam.aspect = orthographic size * cam.aspect * 2f;

        // -- Formula from Camera orthographic size to Box Collider size
        //      orthosize = height / 2f (base of height)
        //      orthosize = width / cam.aspect * 2f

        // Base on this formula, we design a formula for camera size:
        // BoxCollider size of stage: (xBox, yBox)
        // We have to keep aspect ratio, despite any size (or aspect ratio of BoxCollider)
        // So we will calculate ortho size base on xBox and ortho size base on yBox and keep the lower one
        // Then we will clamp the camera position to let it not slide outside the BoxCollider

        var orthoSizeY = size.y / 2f;
        var orthoSizeX = (size.x / (cam.aspect * 2f));

        var orthoSize = Mathf.Max(Mathf.Min(Mathf.Min(orthoSizeX, orthoSizeY), 20f), 5f);

        SetCameraSize(orthoSize);

        float clampOffsetX = Mathf.Max(((size.x - orthoSize * cam.aspect * 2f) / 2f) + 1.5f, 1.5f);
        float clampOffsetY = Mathf.Max(((size.y - orthoSize * 2f) / 2f) + 1.5f, 1.5f);

        clampOffset = new Vector2(clampOffsetX, clampOffsetY);
        
        clampPosition = new Vector2(stage.gameObject.transform.position.x, stage.gameObject.transform.position.y);

    }

    private void StartSetSize(float size) {
        if(coroutine != null) CoroutineManager.Instance.StopTrackedCoroutine(routine, ref coroutine);
        routine = SetSize(size);
        coroutine = CoroutineManager.Instance.StartNewCoroutine(routine);
    }

    private IEnumerator SetSize(float size) {
        isResizing = true;
        cameraState = CameraState.Moving;
        float scale = 0f;
        float ogSize = cam.orthographicSize;
        while(scale < 1f) {
            cam.orthographicSize = Mathf.Lerp(ogSize, size, scale);
            scale += Time.deltaTime / resizeTime;
            yield return null;
        }
        isResizing = false;
    }
}

public enum CameraState {
    Stop = 0,
    Moving = 1,
    Following = 2
}
