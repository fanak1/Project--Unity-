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

    public static void CreateParticle(ParticlesType particles, Vector3 position)
    {
        switch (particles)
        {
            case ParticlesType.BlueBloom:
                BlueBloomParticles.Create(position);
                break;
            case ParticlesType.FireballExplosion:
                FireballHitParticles.Create(position);
                break;
            case ParticlesType.DieParticles:
                DieParticles.Create(position);
                break;
            case ParticlesType.RedOrangeCircleExplode:
                RedOrangeCircleExplode.Create(position);
                break;
            case ParticlesType.ElectricExplode:
                ElectricExplode.Create(position);
                break;
            case ParticlesType.Normal:
                NormalParticles.Create(position);
                break;
            default:
                Debug.LogError("Unknown particle type: " + particles);
                break;
        }
    }

    public static Color AbilityRarityColor(Rarity rarity)
    {
        return rarity switch
        {
            Rarity.Normal => Color.white,
            Rarity.Rare => Color.green,
            Rarity.Epic => Color.red,
            Rarity.Legendary => new Color(252, 186, 3),
            _ => Color.gray // Default color for unknown rarity
        };
    }
}
