using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMessege : PersistentSingleton<DebugMessege>
{
    public static TMPro.TextMeshProUGUI text;

    private static Coroutine wait;
    private IEnumerator routine;

    private void Start() {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }
    public void Messege(string mes) {
        text.SetText(mes);
        DisplayMessege();
    }

    private IEnumerator TurnOffMessege() {
        yield return new WaitForSeconds(2f);
        text.SetText("");
    }

    private void DisplayMessege() {
        if(wait != null) {
            CoroutineManager.Instance.StopTrackedCoroutine(routine, ref wait);
        }
        routine = TurnOffMessege();
        wait = CoroutineManager.Instance.StartNewCoroutine(routine);
    }
}
