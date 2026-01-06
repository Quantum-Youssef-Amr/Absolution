using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public int StageID;
    public string StageName;

    [Header("Map")]
    public Vector2Int MapSize;
    public Vector2 MapStepSize;

    public Vector2Int MapSizeMax, MapSizeMin;
    [Space(16), Header("Diff rect")]
    public Vector2Int DiffRectCenter = new();

    [Tooltip("Full width and length")]
    public Vector2Int DiffRectSize;

    public Vector2Int DiffRectMax;
    public Vector2Int DiffRectMin;

    [Space(16), Header("Spawner Setting")]
    public int MaxStageWave;
    public int OpenNewEnemyEvery;
    public int SpawnRate;

    public AnimationCurve StageProgression;

    [Range(0.2f, 1f)] public float HardnessFactor;
    public GameObject[] StageEnemies;
}
