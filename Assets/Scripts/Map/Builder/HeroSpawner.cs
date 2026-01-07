using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private GameObject HeroObject;
    [SerializeField] private GameObject HeroRangeIndicator;
    [SerializeField] private Tilemap Map;

    private Hero _currentSelectedHero;
    private newInputSystem _inputs;
    private Camera _mainCamera;
    private StageData _sd;
    private Transform _t;

    private Vector2 m_MouseCurrentPos;
    void Awake()
    {
        _inputs = new();


        GameEventBus.OnSelectingHeroSec += Hero =>
        {
            _currentSelectedHero = Hero;

            HeroObject.GetComponent<SpriteRenderer>().sprite = _currentSelectedHero.sprite;
            HeroObject.GetComponent<HeroAI>().SetHeroParams(_currentSelectedHero);

            ShowHeroObject();
        };

        _inputs.Player.MousePos.performed += MouseScreenPosition =>
        {
            if (_currentSelectedHero == null)
            {
                HideHeroObject();
                return;
            }

            m_MouseCurrentPos = CalculateWorldGridPos(MouseScreenPosition.ReadValue<Vector2>());
            HeroObject.transform.position = GameMath.IsInRange(m_MouseCurrentPos, _sd.DiffRectMin + Vector2.one, _sd.DiffRectMax - Vector2.one) ?
            GameMath.FromGridToIsometric(m_MouseCurrentPos, _currentSelectedHero.spriteOffset, _sd.MapStepSize) : HeroObject.transform.position;
        };

        _inputs.Player.Attack.performed += (_) =>
        {
            if (_currentSelectedHero == null || !GameMath.IsInRange(m_MouseCurrentPos, _sd.DiffRectMin + Vector2.one, _sd.DiffRectMax - Vector2.one))
                return;

            GameObject m_hero = Instantiate(HeroObject, GameMath.FromGridToIsometric(m_MouseCurrentPos, _currentSelectedHero.spriteOffset, _sd.MapStepSize), Quaternion.identity, _t);

            removeIndicators(HeroObject.transform);
            removeIndicators(m_hero.transform);

            m_hero.GetComponent<HeroAI>().SetHeroParams(_currentSelectedHero);
            m_hero.GetComponent<HeroAI>().BeginDefending();

            GameEventBus.OnBuildHero?.Invoke(_currentSelectedHero);

            _currentSelectedHero = null;
        };
    }

    void OnEnable()
    {
        _inputs.Enable();
    }

    void OnDisable()
    {
        _inputs.Disable();
    }

    void Start()
    {
        _mainCamera = Camera.main;
        _sd = StageManager.Instance.getStageData();
        _t = transform;
    }

    private void HideHeroObject()
    {
        HeroObject.GetComponent<SpriteRenderer>().enabled = false;
        HeroObject.GetComponent<HeroAI>().enabled = false;
    }

    private void ShowHeroObject()
    {
        HeroObject.GetComponent<SpriteRenderer>().enabled = true;
        HeroObject.GetComponent<HeroAI>().enabled = true;

        ConstructHeroObjectRangeIndicators();
    }

    private void ConstructHeroObjectRangeIndicators()
    {
        removeIndicators(HeroObject.transform);

        for (int x = -(int)_currentSelectedHero.weapon.range; x <= (int)_currentSelectedHero.weapon.range; x++)
        {
            for (int y = -(int)_currentSelectedHero.weapon.range; y <= (int)_currentSelectedHero.weapon.range; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                GameObject m_indicator = Instantiate(HeroRangeIndicator, HeroObject.transform);
                m_indicator.transform.SetLocalPositionAndRotation(
                    GameMath.FromGridToIsometric(new(x, y), _currentSelectedHero.spriteOffset + (Vector2.down * 0.25f), _sd.MapStepSize)
                    , Quaternion.identity
                );
            }
        }
    }

    private void removeIndicators(Transform transform)
    {
        for (int child = 0; child < transform.childCount; child++)
        {
            Destroy(transform.GetChild(child).gameObject);
        }
    }

    private Vector2 CalculateWorldGridPos(Vector2 MouseScreenPos)
    {
        var m_cellPos = Map.WorldToCell(_mainCamera.ScreenToWorldPoint(MouseScreenPos));
        return new(m_cellPos.x, m_cellPos.y);
    }

    #region Debugging

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.color = Color.aquamarine;
        for (int x = _sd.DiffRectMin.x + 1; x <= _sd.DiffRectMax.x - 1; x++)
        {
            for (int y = _sd.DiffRectMin.y + 1; y <= _sd.DiffRectMax.y - 1; y++)
            {
                Gizmos.DrawCube(GameMath.FromGridToIsometric(new(x, y), Vector2.zero, _sd.MapStepSize), 0.2f * Vector3.one);
            }
        }
    }
    #endregion
}
