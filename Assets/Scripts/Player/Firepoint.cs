using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firepoint : MonoBehaviour
{
    private Vector3 mousePos;

    public Camera cam;

    private void Start() {
      
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    void LookAround() {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("Mouse: " + mousePos + "; RbPos: " + transform.position);
        Vector3 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rot;
    }

    

    
}