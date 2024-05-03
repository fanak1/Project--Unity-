using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cipher : PersistentSingleton<Cipher>
{
    public GameObject test;

    public TextMeshProUGUI[] options;

    public TextMeshProUGUI question;

    public void DisplayCipher(ScriptableCipher cipher) {
        question.SetText(cipher.question);

        for (int i = 0; i < cipher.answers.Length; i++) {
            options[i].SetText(cipher.answers[i]);
        }
    }
}
