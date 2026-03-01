using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public int Blood
    {
        get { return _initBlood; }
        set { _initBlood = value; GameEventBus.OnBloodAmountChange?.Invoke(_initBlood); }
    }

    [SerializeField] private int _initBlood;
    public List<InventoryCell> inventoryCells;
}

[Serializable]
public struct InventoryCell
{
    public Hero hero;
    public int num;

    public InventoryCell(Hero hero, int num = 0)
    {
        this.hero = hero;
        this.num = num;
    }
}