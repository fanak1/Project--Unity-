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

    public static ScriptablePlayerUnit Character(CharacterCode characterCode)
    {
        if (characterCode != CharacterCode.None)
            return ResourceSystem.Instance.GetCharacterWithCodeName(characterCode);
        else
            return null;
    }

    public static ScriptableEnemyUnit Enemy(EnemyCodeName enemyCodeName)
    {
        return ResourceSystem.Instance.GetEnemyWithCodeName(enemyCodeName);
    }
}
