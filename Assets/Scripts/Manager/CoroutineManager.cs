using System.Collections.Generic;
using UnityEngine;


public class CoroutineManager : PersistentSingleton<CoroutineManager>
{
    public int coroutineCount = 0;

    public System.Collections.IEnumerator StartTrackedCoroutine(System.Collections.IEnumerator routine) {
        coroutineCount++;
        yield return StartCoroutine(routine);
        coroutineCount--;
    }

    public Coroutine StartNewCoroutine(System.Collections.IEnumerator coroutine) {
        return StartCoroutine(StartTrackedCoroutine(coroutine));
        
    }

    
}
