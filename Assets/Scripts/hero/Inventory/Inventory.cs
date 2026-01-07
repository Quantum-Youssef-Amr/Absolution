using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public InventoryCell[] inventoryCells;
}

[Serializable]
public struct InventoryCell
{
    public Hero hero;
    public int num;
}