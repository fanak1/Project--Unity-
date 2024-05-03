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

    Vector2 mousePos; //Mouse position in screen
    public Camera cam; //to get mouse input in screen cam

    private void Start() {
        playerUnit = GetComponent<PlayerUnit>(); //UnitBase of Player
        projectileHolder = GetComponent<ProjectileHolder>(); //ProjectileHolder
    }

    void Update() {
        MoveController();
        ShootController();
    }

    void MoveController() { //Movement of player
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, vertical, 0) * playerUnit.stats.spd * Time.deltaTime);

    }

    void ShootController() { //Shooting (left, right)
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButton(0)) { //Left-mouse
            projectileHolder.Shoot(0, transform.position, mousePos);
        }
        if (Input.GetMouseButton(1)) { //Right-mouse
            projectileHolder.Shoot(1, transform.position, mousePos);
        }
    }

}
