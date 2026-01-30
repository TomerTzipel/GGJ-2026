using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private EnemyType _myEnemyType;
    [SerializeField] private PlayerPosition playerPosition;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _Speed = 3f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackCooldown = 0.75f;
    [SerializeField] private int _attackDamage = 9;

    public enum EnemyType { Melee, Ranged }
    public enum EnemyState { Idle, Chasing, PreparingToAttack }

    private EnemyState _myCurrentState;

    private EnemyState DetermineMyState()
    {
        if (playerPosition != null) { _myCurrentState = EnemyState.Idle; return EnemyState.Idle; }

        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition.PlayerDelayedPosition);

        if (distanceToPlayer < 2f)
        {
            return EnemyState.PreparingToAttack;
        }
        else if (distanceToPlayer < 10f)
        {
            return EnemyState.Chasing;
        }
        else
        {
            return EnemyState.Idle;
        }
    }

    private void Update()
    {
        switch (DetermineMyState())
        {
            case EnemyState.Idle:
                // Idle behavior
                break;
            case EnemyState.Chasing:
                // Chasing behavior
                break;
            case EnemyState.PreparingToAttack:
                // Preparing to attack behavior
                break;
        }
    }

    protected virtual void Attack()
    {
        // Attack logic here
    }

    protected virtual void Die() { Destroy(gameObject); }

}