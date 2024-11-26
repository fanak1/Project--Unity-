using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public abstract void Interact();

    public abstract void OnClicked();

    public abstract void OnMouseEnterInteract();
}
