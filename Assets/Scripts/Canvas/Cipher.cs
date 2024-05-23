using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cipher : PersistentSingleton<Cipher>
{
    public GameObject test;

    public TextMeshProUGUI[] options;

    public TextMeshProUGUI question;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public void DisplayCipher(ScriptableCipher cipher) {
        animator.SetTrigger("Init");
        question.SetText(cipher.question);

        for (int i = 0; i < cipher.answers.Length; i++) {
            options[i].SetText(cipher.answers[i]);
        }
    }
}
