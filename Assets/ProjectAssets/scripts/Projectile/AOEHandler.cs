using System.Collections;
using UnityEngine;

public class AOEHandler : MonoBehaviour
{
    [SerializeField] MaskTypeEventChannel aoeSpawnedEC;
    [SerializeField] MaskType maskType;
    [SerializeField] float duration;

    public int Damage { get; set; }

    private void Awake()
    {
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

    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
