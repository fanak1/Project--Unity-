using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerUnit playerUnit; //For stat like spd etc;
    private ProjectileHolder projectileHolder; //For projectile


    float horizontal; //Horizontal Input
    float vertical; //Vertical Input

    float spd; //Spd of object

    Vector3 mousePos; //Mouse position in screen
    public Camera cam; //to get mouse input in screen cam

    private void Start() {
        playerUnit = GetComponent<PlayerUnit>();
        projectileHolder = GetComponent<ProjectileHolder>();
    }

    void Update() {
        MoveController();
    }

    void MoveController() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, vertical, 0) * playerUnit.stats.spd * Time.deltaTime);
    }

    void ShootController() {
        mousePos = cam.ScreenToWorldPoint(mousePos);
        if(Input.GetMouseButton(0)) {
            projectileHolder.Shoot(0, transform.position, mousePos);
        }
    }

}
