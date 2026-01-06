using UnityEngine;

public class MoveToIso : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private Vector2Int GridPos;

    [ContextMenu("Move")]
    private void MoveToIsoFunc()
    {
        transform.position = GameMath.FromGridToIsometric(GridPos, Vector2.zero, stageData.MapStepSize);
    }
}
