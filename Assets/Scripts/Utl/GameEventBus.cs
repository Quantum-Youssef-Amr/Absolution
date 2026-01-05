using System;
using UnityEngine;

public static class GameEventBus
{
    public static Action<Vector3> OnMoveToCell;

    private static void CutConnectionsBeforeSceneTransition()
    {
        OnMoveToCell = null;
    }
}
