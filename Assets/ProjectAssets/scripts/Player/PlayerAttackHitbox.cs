using System.Collections;
using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public MaskType CurrentMask { get; set; }
    [SerializeField] private PlayerSettings settings;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out IHealthComponent health))
            {
                health.TakeDamage(settings.Damage);
            }
        }
    }


}
