using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Reward : StaticInstance<Reward>
{

    private Animator animator;
    public AbilityPackage thunderAbilityPrefab;
    public AbilityPackage thunderAbilityPrefab1;
    public AbilityPackage abilityPrefab;
    public AbilityPackage abilityPrefab1;

    public GameObject father;

    private void Start() {
        animator = GetComponent<Animator>();

        father.SetActive(false);
    }

    public void SetFatherActive(bool active)
    {
        father.SetActive(active);
    }

    public void DisplayReward(List<ScriptableAlbilities> list, Action<ScriptableAlbilities> onRewardChoose) {
        if (PlayerUnit.instance != null)
            PlayerUnit.instance.mouseBlock = true;
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(var ability in list)
        {
            var a = CreateAndAddAbilityToTransform(ability, this.gameObject.transform);
            a.OnRewardChoose += onRewardChoose;
        }
        if (animator != null)
        {
            animator.CrossFade("Init Anim", 0f);
        }
    }

    private AbilityPackage CreateAndAddAbilityToTransform(ScriptableAlbilities ability, Transform parent, bool playerHave = false)
    {
        AbilityPackage a;
        switch (ability.characterCode)
        {
            case CharacterCode.Thunder:
                a = playerHave ? Instantiate(thunderAbilityPrefab) : Instantiate(thunderAbilityPrefab1);
                a.Init(ability, playerHave);
                a.gameObject.transform.SetParent(parent);
                break;
            default:
                a = playerHave ? Instantiate(abilityPrefab) : Instantiate(abilityPrefab1);
                a.Init(ability, playerHave);
                a.gameObject.transform.SetParent(parent);
                break;
        }
        return a;
    }

    public void ResetButton()
    {
        if (gameObject != null)
        {
            if(GameManager.Instance.currentMoney >= 5)
            {
                RewardManager.Instance.ReInitReward();
                GameManager.Instance.DecreaseMoney(5);
            }
                
        }
    }
}
