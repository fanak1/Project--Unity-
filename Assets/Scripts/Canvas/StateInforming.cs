using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StateInforming : StaticInstance<StateInforming>
{
    private const string ROUND_START = "Round Start";
    private const string QUESTION = "Question";
    private const string REWARD = "Reward";
    private const string END_ROUND = "End Round";

    //Event----------------------------------------------------------------------

    public Action OnDisplayBegin;

    public Action OnDisplayEnd;


    //----------------------------------------------------------------------------

    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private float interval = 0.1f;


    private void Start() {
        gameObject.SetActive(false);
    }

    public void DisplayBegin(string text) {
        OnDisplayBegin?.Invoke();
        tmp.SetText(text);
        this.gameObject.SetActive(true);
    }

    public void DisplayEnd() {
        tmp.SetText("");
        OnDisplayEnd?.Invoke();
    }

    IEnumerator DisplayStart(string text) {

        OnDisplayBegin?.Invoke();
        string txt = "";

        foreach(char letter in text) {
            txt += letter;
            tmp.SetText(txt);

            yield return new WaitForSecondsRealtime (interval);
        }

        yield return new WaitForSecondsRealtime (0.5f);

        DisplayEnd();
    }

    public void Display(string text) {
        CoroutineManager.Instance.StartNewCoroutine(DisplayStart(text));
    }

}

