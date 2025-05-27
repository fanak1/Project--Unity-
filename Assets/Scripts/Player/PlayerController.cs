using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerUnit playerUnit; //For stat like spd etc;
    public ProjectileHolder projectileHolder; //For projectile
    public AlbilitiesHolder abilityHolder;
    public Animator animator;

    Rigidbody2D rb;

    public Vector2 mousePos; //Mouse position in screen

    
    public Camera cam; //to get mouse input in screen cam

    RaycastHit2D hit;
    int interactionMask;

    IInteractable interactItem;

    protected bool runAnim = false;

    public int controllerBlockCount = 0;

    public bool IsControllerBlocked => controllerBlockCount > 0;

    public void PushBlock() => controllerBlockCount++;
    public void PopBlock() => controllerBlockCount = Mathf.Max(0, controllerBlockCount-1);

    private void Start() {
        playerUnit = GetComponent<PlayerUnit>(); //UnitBase of Player
        projectileHolder = GetComponent<ProjectileHolder>(); //ProjectileHolder
        abilityHolder = GetComponent<AlbilitiesHolder>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        interactionMask = LayerMask.GetMask("RayCast");

        InitInputInstruction();
        
    }

    void InitInputInstruction()
    {
        InputInstructionUI.Instance.AddInstructions(new KeyValuePair<string, List<string>>("Aim", new List<string> { "mouse"}));
        InputInstructionUI.Instance.AddInstructions(new KeyValuePair<string, List<string>>("Shoot", new List<string> { "lmb" }));
        InputInstructionUI.Instance.AddInstructions(new KeyValuePair<string, List<string>>("Move", new List<string> { "W", "A", "S", "D" }));
        InputInstructionUI.Instance.AddInstructions(new KeyValuePair<string, List<string>>("Skill", new List<string> { "E" }));
    }

    void Update() {
        if (GameManager.Instance.pause || IsControllerBlocked)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (GameManager.Instance.pause)
            return;

        if (IsControllerBlocked) 
            return;

        

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MoveController();
        ShootController();
        AbilityController();


    }

    public virtual void MoveController()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector2 inputDir = new Vector2(movX, movY).normalized;

        Vector2 velocity = inputDir * playerUnit.stats.spd;

        rb.linearVelocity = velocity;

        if (rb.linearVelocity.magnitude > 0.1f)
        {
            if (!runAnim)
            {
                runAnim = true;
                animator.CrossFade("Run", 0f);
            }
        }
        else
        {
            if (runAnim)
            {
                runAnim = false;
                animator.CrossFade("Idle", 0f);
            }
        }
    }

    public virtual void ShootController() { //Shooting (left, right)
        
        if(Input.GetMouseButton(0)) { //Left-mouse
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

    public virtual void AbilityController() {
        foreach (Abilities a in abilityHolder.active) {
            if (Input.GetKeyDown(a.button)) {
                if(abilityHolder.EnoughMana(a, playerUnit.nowMP) && abilityHolder.Usable(a)) {
                    abilityHolder.PerformAbility(a.button);
                }
            }

        }
    }

    public void StopMoving() {
        animator.CrossFade("Idle", 0f);
    }

    public void ContinueMoving() {
    }

}
