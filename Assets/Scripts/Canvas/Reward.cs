using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reward : PersistentSingleton<Reward>
{
    [SerializeField] private TextMeshProUGUI[] options;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public void DisplayReward(List<ScriptableAlbilities> list) {
        animator.SetTrigger("Init");
        for(int i=0; i<options.Length; i++) {
            options[i].SetText(list[i].description);
        }
    }
}
