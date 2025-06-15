using System;
using System.Collections;
using UnityEngine;

public class OrcScript : MobScript
{
    private int controllerBlockCount = 0;
    public bool IsControllerBlocked => controllerBlockCount > 0;

    public void PushBlock() => controllerBlockCount++;
    public void PopBlock() => controllerBlockCount = Mathf.Max(0, controllerBlockCount - 1);

    protected Animator animator;

    bool runAnim = false;

    public ProjectileHitBox hitBox;

    public GameObject hitBoxLocation;

    public Action OnAttackAnimationEnd;

    bool dash;

    public Vector2 dashDirection;

    protected override void Start()
    {
        base.Start();

        moveDuration = 0f;
        moveInterval = 0f;
        animator = GetComponent<Animator>();
    }
    internal override void Move(int index)
    {
        
    }

    internal override void RandomMove()
    {
        if (dash) DashDirection();

        if (IsControllerBlocked) return;

        TurnFace();

        if (Vector3.Distance(PlayerUnit.instance.GetPosition(), gameObject.transform.position) < 1f)
        {
            StartAnimation("Attack");
        } else
        {
            MoveToward();
            if (!runAnim)
            {
                animator.CrossFade("Run", 0f);
                runAnim = true;
            }
        }


    }

    public void StartDashToPlayer()
    {
        if (PlayerUnit.instance != null)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.orcAxe);
            dash = true;
            dashDirection = PlayerUnit.instance.GetPosition() - gameObject.transform.position;
            TurnFace();
        }
    }

    public void EndDash()
    {
        dash = false;
    }

    public void DashDirection()
    {
        transform.position += (Vector3)dashDirection.normalized * Time.deltaTime * spd * 2;
    }

    public void CreateHitBox()
    {
        var hit = Instantiate(hitBox, hitBoxLocation.transform.position, Quaternion.identity);
        hit.transform.SetParent(hitBoxLocation.transform);
        hit.Init(GetComponent<UnitBase>());

        OnAttackAnimationEnd = () => { Destroy(hit.gameObject); };
    }

    public void EndAttackAnimation()
    {
        OnAttackAnimationEnd?.Invoke();
        OnAttackAnimationEnd = null;
    }


    public void StartAnimation(string stateName)
    {
        runAnim = false;
        StartCoroutine(StartAnimationCoroutine(stateName));
    }

    IEnumerator StartAnimationCoroutine(string stateName)
    {
        PushBlock();

        // Start animation immediately (no blending)
        animator.CrossFade(stateName, 0f);

        yield return null; // Allow animator to enter the new state

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Wait until the correct animation state starts
        while (!stateInfo.IsName(stateName))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Wait for animation to finish
        while (stateInfo.normalizedTime < 1f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        runAnim = false;
        PopBlock();
    }

}
