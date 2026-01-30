using UnityEngine;

public class RangeEnemy : BaseEnemy
{
    protected override void UpdateAttack()
    {
        Vector3 direction = (playerPosition.PlayerDelayedPosition - transform.position).normalized;
        //Shoot a projectile towards the player
    }
}