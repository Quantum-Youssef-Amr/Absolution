using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;
    [TextArea] public string description;

    public WeaponRange range;
    public int Damage;
    public int FiringRate;

    public GameObject bullet;
}

public enum WeaponRange
{
    Large = 3,
    medium = 2,
    small = 1
}