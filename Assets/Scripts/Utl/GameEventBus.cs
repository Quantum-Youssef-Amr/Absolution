using System;
using UnityEngine;

public static class GameEventBus
{
    public static Action OnGameLoss;
    public static Action<Hero> OnSelectingHeroSec;
    public static Action<Hero> OnBuildHero;
    public static Action<Hero> OnRemoveHero;

    public static Action<InventoryCell[]> OnUpdateInventoryIU;
    public static Action<int> OnSelectingHero;
    public static Action<bool, Hero> OnIsHeroAvailable;

    private static void CutConnectionsBeforeSceneTransition()
    {
        OnGameLoss = null;
        OnBuildHero = null;

        OnUpdateInventoryIU = null;
        OnSelectingHero = null;
        OnIsHeroAvailable = null;
    }
}
