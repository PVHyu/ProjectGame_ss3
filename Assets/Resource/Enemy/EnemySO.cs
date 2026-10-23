using UnityEngine;

public enum EnemyType
{
    NormalPig = 0,
    PigWithBox,
    PigWithBomb,
    PigWithCanon,
    KingPig
}


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/Enemy Data")]

public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int damageContact = 1;
    [SerializeField] private int damageSkill = 1;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    public int MaxHP => maxHP;
    public int DamageSkill => damageSkill;
    public int DamageContact => damageContact;
    public float MoveSpeed => moveSpeed;
}