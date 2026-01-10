using System;
using TMPro;
using UnityEngine;

public class BloodCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Counter;

    void OnEnable()
    {
        GameEventBus.OnBloodAmountChange += newAmount => ChangeCounter(newAmount);
    }

    void OnDisable()
    {
        GameEventBus.OnBloodAmountChange -= newAmount => ChangeCounter(newAmount);
    }

    private void ChangeCounter(int newAmount)
    {
        Counter.text = $"Blood: {newAmount}";
    }
}
