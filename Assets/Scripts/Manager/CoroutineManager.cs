using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoroutineManager : PersistentSingleton<CoroutineManager>
{
    public int coroutineCount = 0;
    private List<IEnumerator> activeRoutines = new List<IEnumerator>();

    private IEnumerator StartTrackedCoroutine(IEnumerator routine)
    {
        coroutineCount++;
        activeRoutines.Add(routine);
        yield return routine;
        activeRoutines.Remove(routine);
        coroutineCount--;
    }

    public Coroutine StartNewCoroutine(IEnumerator routine)
    {
        return StartCoroutine(StartTrackedCoroutine(routine));
    }

    public void StopTrackedCoroutine(IEnumerator routine, ref Coroutine coroutine)
    {
        if (routine != null && coroutine != null && activeRoutines.Contains(routine))
        {
            StopCoroutine(coroutine);
            activeRoutines.Remove(routine);
            coroutineCount--;
            coroutine = null;
        }
    }

    public bool IsCoroutineRunning(IEnumerator routine)
    {
        return activeRoutines.Contains(routine);
    }
}



