using System.Collections;
using UnityEngine;

public abstract class BossScript : MonoBehaviour
{
    private int controllerBlockCount = 0;
    public bool IsControllerBlocked => controllerBlockCount > 0;

    public void PushBlock() => controllerBlockCount++;
    public void PopBlock() => controllerBlockCount = Mathf.Max(0, controllerBlockCount - 1);

    protected Animator animator;

    public EnemyBase b;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        b = GetComponent<EnemyBase>();
    }

    protected virtual void Update()
    {
        if (b.dead) return;
        if (GameManager.Instance.pause) return;
        if (IsControllerBlocked) return;

        TurnFace();
        RandomMove();
    }

    public void StartAnimation(string stateName)
    {
        StartCoroutine(StartAnimationCoroutine(stateName));
    }

    internal void TurnFace()
    {
        if (PlayerUnit.instance == null) return;

        if (PlayerUnit.instance.GetPosition().x - this.transform.position.x < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
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

        animator.CrossFade("Idle", 0f);
        PopBlock();
    }

    public abstract void RandomMove();
}
