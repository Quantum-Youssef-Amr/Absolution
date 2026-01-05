using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { private set; get; }

    [SerializeField] private StageData stageData;


    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("there is more than 1 stage manager");
            return;
        }
        Instance = this;
        CalculateStageMinMax();
    }

    [ContextMenu("Recalculate MIN MAX")]
    private void CalculateStageMinMax()
    {
        stageData.DiffRectMax = new(Mathf.RoundToInt(stageData.DiffRectCenter.x + (stageData.DiffRectSize.x - 1)), Mathf.RoundToInt(stageData.DiffRectCenter.y + ((stageData.DiffRectSize.y - 1) / 2)));

        stageData.DiffRectMin = new(Mathf.RoundToInt(stageData.DiffRectCenter.x - (stageData.DiffRectSize.x - 1)), Mathf.RoundToInt(stageData.DiffRectCenter.y - ((stageData.DiffRectSize.y - 1) / 2)));

        stageData.MapSizeMax = stageData.DiffRectCenter + ((stageData.MapSize / 2) - Vector2Int.one);
        stageData.MapSizeMin = stageData.DiffRectCenter - stageData.MapSize / 2;
    }

    public StageData getStageData()
    {
        return stageData;
    }

    #region Debugging
    void OnDrawGizmosSelected()
    {
        for (int x = stageData.MapSizeMin.x; x <= stageData.MapSizeMax.x; x++)
        {
            for (int y = stageData.MapSizeMin.x; y <= stageData.MapSizeMax.y; y++)
            {
                Gizmos.color = GameMath.IsInRange(new(x, y), stageData.DiffRectMin, stageData.DiffRectMax) ? Color.red : Color.blue;
                Gizmos.DrawWireSphere(GameMath.FromGridToIsometric(new(x, y), new(0, 0.25f), stageData.MapStepSize), 0.1f);
            }
        }
    }
    #endregion
}
