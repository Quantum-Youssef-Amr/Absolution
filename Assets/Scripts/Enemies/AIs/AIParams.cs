using UnityEngine;

[CreateAssetMenu(fileName = "AIParams", menuName = "Scriptable Objects/AIParams")]
public class AIParams : ScriptableObject
{
    public int StepParSec;
    public float DamageParSec;
    public float MaxHealth;

    public Vector2 spriteOffset = new(0, 0.25f);
}
