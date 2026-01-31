using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttackHitbox : MonoBehaviour
{
    public MaskType CurrentMask { get; set; }
    public float LifeTime { get; set; }
    [SerializeField] private PlayerSettings settings;
  

    private void Start()
    {
        StartCoroutine(LifeTimeCoroutine(LifeTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out IHealthComponent health))
            {
                health.TakeDamage(settings.Damage,CurrentMask);
            }
        }
    }

    private IEnumerator LifeTimeCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
  
}
