using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CipherManager : PersistentSingleton<CipherManager> {


    [SerializeField] private GameObject[] options;

    [SerializeField] private GameObject question;

    private TextMeshProUGUI questionText;

    private TextMeshProUGUI[] optionText;

    private ScriptableCipher cipher;

    [SerializeField] private GameObject ui;

    // Event --------------------------------------------------------------------------------------------------------------------------------------------

    public event Action OnCorrectAnswer;
    public event Action OnWrongAnswer;

    public event Action OnCipherFinish;

    private void Start() {
        optionText = new TextMeshProUGUI[options.Length];
        for (int i = 0; i < options.Length; i++) {
            optionText[i] = options[i].GetComponent<TextMeshProUGUI>();
        }
        questionText = question.GetComponent<TextMeshProUGUI>();

    }
    public void InitiateQuestion(Difficulty d) {
        ui.SetActive(true);
        cipher = GenerateCipher(d);
        Debug.Log(cipher.question);
        questionText.SetText(cipher.question);

        for (int i = 0; i < cipher.answers.Length; i++) {
            optionText[i].SetText(cipher.answers[i]);
        }
    }

    public void Choose(int answer) { 
        if( answer == cipher.correctAnswer) {
            OnCorrectAnswer?.Invoke();
            

        } else {
            OnWrongAnswer?.Invoke();
            
        }
        Debug.Log("good");
        OnCipherFinish?.Invoke();
        ui.gameObject.SetActive(false);
    }
        
        
    public ScriptableCipher GenerateCipher(Difficulty d) => ResourceSystem.Instance.GetRandomCipherWithDifficulty(d);
}
