using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [field: SerializeField] public MaskType StartingMask { get; private set; }
    [field: SerializeField] public int MaxHP { get; private set; }

    [Header("Movement Settings")]
    [field: SerializeField] public float MoveSpeed {  get; private set; }
    [field: SerializeField] public float DashSpeed { get; private set; }
    [field: SerializeField] public float DashDuration { get; private set; }
    [field: SerializeField] public float DashCD { get; private set; }

    [Header("Attack Settings")]
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public float AttackCD { get; private set; }
    [field: SerializeField] public float AttackDuration { get; private set; }
    [field: SerializeField] public float AbilityCD { get; private set; }
    [field: SerializeField] public float AbilityDuration { get; private set; }
}
