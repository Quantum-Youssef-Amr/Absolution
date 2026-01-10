using UnityEngine;

public class SacrificeHero : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    private Hero _currentHero;

    void Awake()
    {
        GameEventBus.OnPreviewHero += hero => _currentHero = hero;
    }

    public void Sacrifice()
    {
        GameEventBus.OnBuildHero?.Invoke(_currentHero);
        inventory.Blood += _currentHero.BloodReturn;
    }
}
