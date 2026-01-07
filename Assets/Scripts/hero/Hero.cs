using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "Scriptable Objects/Hero")]
public class Hero : ScriptableObject
{
    public HeroRank Rank;
    public new string name;
    [TextArea] public string description;
    public int BloodReturn;

    [Space(16), Header("Engine")]
    public Sprite sprite;
    public Vector2 spriteOffset;

    [Space(16), Header("Weapon")]
    public Weapon weapon;
}

public enum HeroRank
{
    C,
    B,
    A,
    S
}