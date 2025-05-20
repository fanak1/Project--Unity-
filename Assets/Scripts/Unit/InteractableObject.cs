using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public Action OnInteractFinish;

    public void InteractFinish()
    {
        OnInteractFinish?.Invoke();
    }

    public abstract void InteractBegin();
}
