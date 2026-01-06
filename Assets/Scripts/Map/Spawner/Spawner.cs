using System;
using System.Collections;
using System.Data.SqlTypes;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    private Transform _t;
    private int _waveNumber;
    private int _openEnemies;
    private StageData _sd;

    private Coroutine _spawning;

    public Action OnLose;
    public Action OnStageStart;

    void Start()
    {
        _sd = StageManager.Instance.getStageData();
        _t = transform;

        OnLose += () =>
        {
            StopAllCoroutines();
            GameEventBus.OnGameLoss?.Invoke();
        };

        OnStageStart += () => StartCoroutine(SpawnStage());

        OnStageStart?.Invoke();

    }

    private IEnumerator SpawnStage()
    {
        for (int WavesInStage = 0; WavesInStage < _sd.MaxStageWave; WavesInStage++)
        {
            if (_spawning != null)
            {
                StopCoroutine(_spawning);
                _spawning = null;
            }

            _waveNumber++;
            _openEnemies += _waveNumber % _sd.OpenNewEnemyEvery == 0 ? 1 : 0;

            _spawning ??= StartCoroutine(SpawnWave());

            yield return new WaitUntil(() =>
            {
                return _spawning == null;
            });
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int Enemy = 0; Enemy < Mathf.CeilToInt(_sd.StageProgression.Evaluate(_waveNumber)); Enemy++)
        {
            Vector2 m_SpawnLocation = GetRandomSpawnLocation();
            Vector2 m_HeadingLocation = GetRandomHeadingLocation();

            GameObject m_Enemy = _sd.StageEnemies[UnityEngine.Random.Range(0, Math.Clamp(_openEnemies, 0, _sd.StageEnemies.Length))];
            m_Enemy = Instantiate(m_Enemy, m_SpawnLocation, Quaternion.identity, _t);
            m_Enemy.GetComponent<Base_AI>().GetToTile(m_HeadingLocation);

            yield return new WaitForSeconds(1f / _sd.SpawnRate * _waveNumber * _sd.HardnessFactor);
        }
        _spawning = null;
    }

    private Vector2 GetRandomSpawnLocation()
    {
        bool
        m_isAxisZero = UnityEngine.Random.value > 0.5f,
        m_isAxisMax = UnityEngine.Random.value > 0.5f;

        int
        x = UnityEngine.Random.Range(_sd.MapSizeMin.x + 1, _sd.MapSizeMax.x + 1),
        y = UnityEngine.Random.Range(_sd.MapSizeMin.y + 1, _sd.MapSizeMax.y + 1);

        x = m_isAxisZero ? (m_isAxisMax ? _sd.MapSizeMin.x + 1 : _sd.MapSizeMax.x + 1) : x;
        y = !m_isAxisZero ? (!m_isAxisMax ? _sd.MapSizeMin.y + 1 : _sd.MapSizeMax.y + 1) : y;

        return GameMath.FromGridToIsometric(
            new(x, y),
            Vector2.zero, _sd.MapStepSize
        );
    }

    private Vector2 GetRandomHeadingLocation()
    {
        bool
        m_isAxisZero = UnityEngine.Random.value > 0.5f,
        m_isAxisMax = UnityEngine.Random.value > 0.5f;

        int
        x = UnityEngine.Random.Range(_sd.DiffRectMin.x, _sd.DiffRectMax.x + 2),
        y = UnityEngine.Random.Range(_sd.DiffRectMin.y, _sd.DiffRectMax.y + 2);

        x = m_isAxisZero ? (m_isAxisMax ? _sd.DiffRectMin.x : _sd.DiffRectMax.x + 2) : x;
        y = !m_isAxisZero ? (!m_isAxisMax ? _sd.DiffRectMin.y : _sd.DiffRectMax.y + 2) : y;

        return GameMath.FromGridToIsometric(
            new(x, y),
            Vector2.zero, _sd.MapStepSize
        );
    }
}
