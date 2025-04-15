using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerUnit playerUnit; //For stat like spd etc;
    private ProjectileHolder projectileHolder; //For projectile
    private AlbilitiesHolder abilityHolder;
    private Animator animator;

    Vector2 mousePos; //Mouse position in screen
    
    public Camera cam; //to get mouse input in screen cam

    bool isMoving = false;

    RaycastHit2D hit;
    int interactionMask;

    IInteractable interactItem;

    bool runAnim = false;

    private void Start() {
        playerUnit = GetComponent<PlayerUnit>(); //UnitBase of Player
        projectileHolder = GetComponent<ProjectileHolder>(); //ProjectileHolder
        abilityHolder = GetComponent<AlbilitiesHolder>();
        animator = GetComponent<Animator>();

        interactionMask = LayerMask.GetMask("RayCast");
    }

    void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MoveController();
        ShootController();
        AbilityController();


    }

    void MoveController() { //Movement of player
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector2 mov = new Vector2(movX, movY).normalized;


        if (mov != Vector2.zero)
        {
            if (!runAnim)
            {
                runAnim = true;
                animator.SetTrigger("Run");
            }

            transform.Translate(mov * playerUnit.stats.spd * Time.deltaTime);

        } else
        {
            animator.SetTrigger("Idle");
            isMoving = false;
            runAnim = false;
        }
    }

    void ShootController() { //Shooting (left, right)
        
        if(Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.A)) { //Left-mouse
            if(projectileHolder.EnoughMana(0, playerUnit.nowMP)) {
                projectileHolder.Shoot(0, transform.position, mousePos);
                
            }
                
        }
        /*
        if (Input.GetMouseButton(1)) { //Right-mouse
            projectileHolder.Shoot(1, transform.position, mousePos);
        }
        */
    }

    void AbilityController() {
        foreach (Abilities a in abilityHolder.active) {
            if (Input.GetKeyDown(a.button)) {
                if(abilityHolder.EnoughMana(a, playerUnit.nowMP) && abilityHolder.Usable(a)) {
                    abilityHolder.PerformAbility(a.button);
                }
            }

        }
    }

    public void StopMoving() {
        animator.SetTrigger("Idle");
    }

    public void ContinueMoving() {
    }

}
