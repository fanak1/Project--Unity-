using UnityEngine;

public abstract class GameStatesManager
{
    public virtual GameStates StateName { get; protected set; }

    public abstract void Start();

    public abstract void Update();
}

public enum GameStates
{
    MENU = 0,
    GAMEPLAY = 1,
    ENDING = 2
}
