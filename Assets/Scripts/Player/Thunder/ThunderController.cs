using System.Collections;
using UnityEngine;

public class ThunderController : PlayerController
{

    public override void AbilityController()
    {
        foreach (Abilities a in abilityHolder.active)
        {
            if (Input.GetKeyDown(a.button))
            {
                if (abilityHolder.EnoughMana(a, playerUnit.nowMP) && abilityHolder.Usable(a))
                {
                    TurnFace((Vector3)mousePos);
                    StartAnimation("Cast");
                    abilityHolder.PerformAbility(a.button);
                }
            }

        }
    }

    public override void ShootController()
    {
        if (Input.GetMouseButton(0))
        { //Left-mouse
            if (projectileHolder.EnoughMana(0, playerUnit.nowMP))
            {
                TurnFace((Vector3)mousePos);
                StartAnimation("Cast");
                projectileHolder.Shoot(0, transform.position, mousePos);

            }

        }
    }

    public void StartAnimation(string stateName)
    {
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

        animator.CrossFade("Idle", 0f);
        runAnim = false;
        PopBlock();
    }
}
