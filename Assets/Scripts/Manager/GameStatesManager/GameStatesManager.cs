using UnityEngine;

public abstract class GameStatesManager
{
    protected GameManager gameManager;
    public GameStatesManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public GameStatesManager()
    {
        this.gameManager = GameManager.Instance;
        if (this.gameManager == null)
        {
            Debug.LogError("Stupid game!!!");
        }
    }

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
