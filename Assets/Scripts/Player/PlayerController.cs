using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private PlayerUnit playerUnit; //For stat like spd etc;
    private ProjectileHolder projectileHolder; //For projectile
    private AlbilitiesHolder abilityHolder;
    private Animator animator;

    private NavMeshAgent navMeshAgent;


    float spd; //Spd of object


    Vector2 mousePos; //Mouse position in screen
    Vector2 moveToTarget;
    Vector3 moveToTarget3;
    public Camera cam; //to get mouse input in screen cam

    bool isMoving = false;
    bool createParticle = false;

    RaycastHit2D hit;
    int interactionMask;

    IInteractable interactItem;

    bool runAnim = false;

    private void Start() {
        playerUnit = GetComponent<PlayerUnit>(); //UnitBase of Player
        projectileHolder = GetComponent<ProjectileHolder>(); //ProjectileHolder
        abilityHolder = GetComponent<AlbilitiesHolder>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = playerUnit.stats.spd;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        interactionMask = LayerMask.GetMask("RayCast");
    }

    void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MouseRaycast();
        MoveController();
        ShootController();
        AbilityController();

        if(Vector2.Distance(transform.position, moveToTarget) > 0.1f && isMoving) {
            navMeshAgent.SetDestination(moveToTarget3);
        } else {
            animator.SetTrigger("Idle");
            isMoving = false;
            runAnim = false;
        }
        
    }

    void MoveController() { //Movement of player
        if (Input.GetMouseButton(1)) {
            if(interactItem != null) interactItem.OnClicked();
            if (!runAnim) {
                runAnim = true;
                animator.SetTrigger("Run");
            }
            isMoving = true;
            moveToTarget = mousePos;
            moveToTarget3 = (Vector3)moveToTarget + new Vector3(0, 0, transform.position.z);
            if (!createParticle) {
                ClosingCircle.Create(moveToTarget);
                createParticle = true;
            }
        }
        if (Input.GetMouseButtonUp(1)) createParticle = false;
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

    public void MouseRaycast() {
        hit = Physics2D.Raycast(mousePos, Vector2.zero, 2f, interactionMask);

        if(hit.transform != null) {
            Debug.Log("Hit");

            createParticle = true;
            IInteractable temp = hit.transform.gameObject.GetComponent<IInteractable>();

            if (temp != null && interactItem != temp ) interactItem = temp;
            if(interactItem != null) interactItem.OnMouseEnterInteract();
        } else {
            interactItem = null;
        }

        
    }

    public void StopMoving() {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Idle");
    }

    public void ContinueMoving() {
        navMeshAgent.isStopped = false;
    }

}
