using UnityEngine;

public static class Registry
{
    public static GameStatesManager States(GameStates gameState)
    {
        return gameState switch
        {
            GameStates.MENU => null,
            GameStates.GAMEPLAY => new GamePlayStatesManager(),
            GameStates.ENDING => null,
            _ => null
        };
    }
}
