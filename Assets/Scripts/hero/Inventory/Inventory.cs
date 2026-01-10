using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public int Blood
    {
        get { return _initBlood; }
        set { _initBlood = value; GameEventBus.OnBloodAmountChange?.Invoke(_initBlood); }
    }

    [SerializeField] private int _initBlood;
    public InventoryCell[] inventoryCells;
}

[Serializable]
public struct InventoryCell
{
    public Hero hero;
    public int num;
}