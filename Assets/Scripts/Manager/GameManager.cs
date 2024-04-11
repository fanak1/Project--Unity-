using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private GameObject stageManagerGameObject;
    [SerializeField] private GameObject cipherManagerGameObject;

    private StageManager stageManager;
    private CipherManager cipherManager;

    private Difficulty difficulty;

    private void Start() {
        stageManager = stageManagerGameObject.GetComponent<StageManager>();
        cipherManager = cipherManagerGameObject.GetComponent<CipherManager>();

        InitiateManager();
    }

    public void BeginCipherUI() {
        cipherManager.InitiateQuestion(difficulty);
    }

    public void CloseCipherUI() {
        stageManager.CipherStateEnd();
    }

    private void InitiateManager() {
        stageManager.OnCipherBegin += BeginCipherUI;
        cipherManager.OnCipherFinish += CloseCipherUI;
    }

}
