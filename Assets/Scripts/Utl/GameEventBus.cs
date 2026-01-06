using System;
using UnityEngine;

public static class GameEventBus
{
    public static Action OnGameLoss;
    private static void CutConnectionsBeforeSceneTransition()
    {
        OnGameLoss = null;
    }
}
