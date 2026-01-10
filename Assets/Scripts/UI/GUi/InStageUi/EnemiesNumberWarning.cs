using UnityEngine;
using TMPro;

public class EnemiesNumberWarning : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WarningText;

    void Start()
    {
        GameEventBus.OnEnemiesNumberWarningOff += () => WarningText.enabled = false;
        GameEventBus.OnEnemiesNumberWarningOn += _enemiesNumber =>
        {
            WarningText.enabled = true;
            WarningText.text = _enemiesNumber < 10 ? $"Enemies: 0{_enemiesNumber}" : $"Enemies: {_enemiesNumber}";
        };
    }
}
