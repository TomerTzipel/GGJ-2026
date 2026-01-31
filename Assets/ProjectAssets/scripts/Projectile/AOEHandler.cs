using System.Collections;
using UnityEngine;


public class AOEHandler : MonoBehaviour
{
    [SerializeField] MaskTypeEventChannel aoeSpawnedEC;
    [SerializeField] MaskType maskType;
    [SerializeField] float duration;
    [SerializeField] private Collider2D hitbox;
    [SerializeField] private PlayerSettings settings;
    public int Damage { get; set; }

    private void Awake()
    {
        StartCoroutine(HitboxLifetime(settings.AbilityHitboxDuration));
        StartCoroutine(Duration());
    }

    private void Start()
    {
        aoeSpawnedEC.RaiseEvent(maskType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        if (collision.TryGetComponent(out IHealthComponent health))
        {
            health.TakeDamage(Damage,maskType);
        }
    }
    private IEnumerator HitboxLifetime(float duration)
    {
        yield return new WaitForSeconds(duration);
        hitbox.enabled = false;
    }

    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
