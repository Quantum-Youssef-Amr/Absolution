using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Base_AI : MonoBehaviour
{
    [SerializeField] private AIParams @params;
    [SerializeField] private int PathHardLimit = 30;
    private Transform _transform;
    private Coroutine _walking;
    private StageData _SD;

    private void StartSetUp()
    {
        _transform = transform;
        _SD = StageManager.Instance.getStageData();
    }

    public virtual void GetToTile(Vector2 target)
    {
        StartSetUp();
        if (_walking != null)
        {
            StopCoroutine(_walking);
            _walking = null;
        }

        Vector2[] m_path = SolvePath(target);
        if (m_path == null)
            return;

        _walking ??= StartCoroutine(Move(m_path));
    }


    protected Vector2[] SolvePath(Vector2 target)
    {
        List<Vector2> m_solvedPath = new();
        HashSet<Vector2> m_visitedNodes = new();

        Vector2 m_posInGrid = GameMath.FromIsometricToGrid(_transform.position, @params.spriteOffset, _SD.MapStepSize);
        Vector2 m_targetGridPos = GameMath.FromIsometricToGrid(target, @params.spriteOffset, _SD.MapStepSize);

        if (!GameMath.IsInRange(m_targetGridPos, _SD.MapSizeMin, _SD.MapSizeMax))
        {
            print("target outside map");
            return null;
        }

        _targetWorldPos = target;
        _PosWorldPos = _transform.position;

        while ((m_targetGridPos - m_posInGrid).sqrMagnitude != 0)
        {
            if (m_solvedPath.Count > PathHardLimit || m_visitedNodes.Count > PathHardLimit)
                break;

            Dictionary<float, Vector2> m_valuesDict = new()
            {
                {
                    GameMath.IsInRange(m_posInGrid + Vector2.up, _SD.DiffRectMin, _SD.DiffRectMax + Vector2.one)
                    ||
                    m_visitedNodes.Contains(m_posInGrid + Vector2.up)
                    ?
                    Random.Range(1000000,10000000) :
                    (m_targetGridPos - (m_posInGrid + Vector2.up)).sqrMagnitude + Random.value,
                    Vector2.up
                },
                {
                    GameMath.IsInRange(m_posInGrid + Vector2.down, _SD.DiffRectMin, _SD.DiffRectMax + Vector2.one)
                    ||
                    m_visitedNodes.Contains(m_posInGrid + Vector2.down)
                    ?
                    Random.Range(1000000,10000000) :
                    (m_targetGridPos - (m_posInGrid + Vector2.down)).sqrMagnitude + Random.value,
                    Vector2.down
                },
                {
                    GameMath.IsInRange(m_posInGrid + Vector2.left, _SD.DiffRectMin, _SD.DiffRectMax + Vector2.one)
                    ||
                    m_visitedNodes.Contains(m_posInGrid + Vector2.left)
                    ?
                    Random.Range(1000000,10000000) :
                    (m_targetGridPos - (m_posInGrid + Vector2.left)).sqrMagnitude + Random.value,
                    Vector2.left
                },
                {
                    GameMath.IsInRange(m_posInGrid + Vector2.right, _SD.DiffRectMin, _SD.DiffRectMax + Vector2.one)
                    ||
                    m_visitedNodes.Contains(m_posInGrid + Vector2.right)
                    ?
                    Random.Range(1000000,10000000) :
                    (m_targetGridPos - (m_posInGrid + Vector2.right)).sqrMagnitude + Random.value,
                    Vector2.right
                },
            };

            float m_minValue = Mathf.Min(m_valuesDict.Keys.ToArray());
            Vector2 m_dirOfMovement = m_valuesDict[m_minValue];

            m_posInGrid += m_dirOfMovement;
            if (GameMath.IsInRange(m_posInGrid, _SD.DiffRectMin, _SD.DiffRectMax))
            {
                _path = m_solvedPath.ToArray();
                return m_solvedPath.ToArray();
            }

            m_visitedNodes.Add(m_posInGrid);
            m_solvedPath.Add(GameMath.FromGridToIsometric(m_posInGrid, @params.spriteOffset, _SD.MapStepSize));
        }

        _path = m_solvedPath.ToArray();
        return m_solvedPath.ToArray();
    }

    protected virtual IEnumerator Move(Vector2[] steps)
    {
        for (int step = 0; step < steps.Length; step++)
        {
            yield return new WaitForSeconds(1f / @params.StepParSec);
            _transform.position = steps[step];
        }
        _walking = null;
    }

    #region Debugging

    private Vector2 _targetWorldPos, _PosWorldPos;
    private Vector2[] _path;

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_PosWorldPos, 0.1f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_targetWorldPos, 0.1f);

            if (_path == null) return;
            for (int i = 0; i < _path.Length - 1; i++)
            {
                if (i == 0)
                    Gizmos.DrawLine(_PosWorldPos + (Vector2.down * 0.25f), _path[i] + (Vector2.down * 0.25f));
                Gizmos.DrawLine(_path[i] + (Vector2.down * 0.25f), _path[i + 1] + (Vector2.down * 0.25f));
            }
        }
    }

    #endregion

}