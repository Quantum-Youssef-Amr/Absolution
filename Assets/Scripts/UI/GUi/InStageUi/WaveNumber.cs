using UnityEngine;
using TMPro;

public class WaveNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WaveText;

    void Start()
    {
        GameEventBus.OnWaveNumberChange += _waveNumber =>
            {
                WaveText.text = _waveNumber < 10 ? $"Wave: 0{_waveNumber}" : $"Wave: {_waveNumber}";
            };
    }
}
