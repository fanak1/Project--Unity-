using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CipherManager : PersistentSingleton<CipherManager> {

    private ScriptableCipher cipher;

    [SerializeField] private Cipher cipherUI;

    // Event --------------------------------------------------------------------------------------------------------------------------------------------

    public event Action OnCorrectAnswer;
    public event Action OnWrongAnswer;

    public event Action OnCipherFinish;

    private void Start() {
        

    }
    public void InitiateQuestion(Difficulty d) {
        cipherUI.gameObject.SetActive(true);
        cipher = GenerateCipher(d);
        
        cipherUI.DisplayCipher(cipher);
    }

    public void Choose(int answer) { 
        if( answer == cipher.correctAnswer) {
            OnCorrectAnswer?.Invoke();
            

        } else {
            OnWrongAnswer?.Invoke();
            
        }
        
        OnCipherFinish?.Invoke();
        cipherUI.gameObject.SetActive(false);
    }
        
        
    public ScriptableCipher GenerateCipher(Difficulty d) => ResourceSystem.Instance.GetRandomCipherWithDifficulty(d);
}
