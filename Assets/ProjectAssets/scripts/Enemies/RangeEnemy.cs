using UnityEngine;

public class RangeEnemy : BaseEnemy
{
    [SerializeField] ProjectileHandler projectilePrefab;
    [SerializeField] MaskTypeEventChannel rangedEnemyAttackEC;
    protected override void ChangeToAttack()
    {
        Vector3 direction = (playerPosition.PlayerDelayedPosition - transform.position).normalized;

        ProjectileHandler projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.MoveDirection = direction;
        projectile.Damage = _attackDamage;
        rangedEnemyAttackEC.RaiseEvent(_myMaskType);
        ChangeState(EnemyState.Idle);
    }
}