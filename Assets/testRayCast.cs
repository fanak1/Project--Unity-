using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRayCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit.collider != null) {
            Debug.Log("Racast: " + hit.collider.gameObject.name);
        }
    }
}
