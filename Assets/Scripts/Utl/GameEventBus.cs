using System;
using UnityEngine;

public static class GameEventBus
{
    // Builder
    public static Action OnGameLoss;
    public static Action<Hero> OnSelectingHeroSec;
    public static Action<Hero> OnBuildHero;
    public static Action<Hero> OnRemoveHero;

    // Inventory
    public static Action<InventoryCell[]> OnUpdateInventoryIU;
    public static Action<int> OnSelectingHero;
    public static Action<bool, Hero> OnIsHeroAvailable;
    public static Action<int> OnBloodAmountChange;

    // UI
    public static Action<int> OnWaveNumberChange;
    public static Action<int> OnEnemiesNumberWarningOn;
    public static Action OnEnemiesNumberWarningOff;
    public static Action<Hero> OnPreviewHero;

    public static void CutConnectionsBeforeSceneTransition()
    {
        OnGameLoss = null;
        OnBuildHero = null;

        OnUpdateInventoryIU = null;
        OnSelectingHero = null;
        OnIsHeroAvailable = null;
        OnBloodAmountChange = null;

        OnWaveNumberChange = null;
        OnEnemiesNumberWarningOff = null;
        OnEnemiesNumberWarningOn = null;
        OnPreviewHero = null;
    }
}
