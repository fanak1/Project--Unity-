using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlbilitiesBase : MonoBehaviour
{
    public Animation animation;

    public Renderer renderer;

    public abstract void DoAnimation();

}
