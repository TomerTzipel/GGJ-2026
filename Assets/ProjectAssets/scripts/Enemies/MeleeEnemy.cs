using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    protected override void UpdateAttack()
    {
        Vector3 direction = (playerPosition.PlayerDelayedPosition - transform.position).normalized;

        transform.position += direction * _moveSpeed * Time.deltaTime;
    }
}
