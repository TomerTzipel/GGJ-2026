using UnityEngine;

public class RangeEnemy : BaseEnemy
{
    [SerializeField] ProjectileHandler projectilePrefab;

    protected override void UpdateAttack()
    {
        Vector3 direction = (playerPosition.PlayerDelayedPosition - transform.position).normalized;

        ProjectileHandler projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.MoveDirection = direction;
        projectile.Damage = _attackDamage;
    }
}