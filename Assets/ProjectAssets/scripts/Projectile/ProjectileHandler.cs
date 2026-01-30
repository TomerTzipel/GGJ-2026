using System.Collections;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private MaskType MaskType;
    [SerializeField] private AOEHandler aoePrefab;
    [SerializeField] private bool isPlayerProjectile;

    public Vector2 MoveDirection { get; set; }
    public int Damage { get; set; }

    private void Start()
    {
        float angle = Mathf.Atan2(MoveDirection.y, MoveDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        StartCoroutine(LifeTime(lifeTime));
    }

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isPlayerProjectile)
        {
            if(collision.TryGetComponent(out IHealthComponent health))
            {
                health.TakeDamage(Damage);
            }
       
        }

        if (collision.CompareTag("Enemy") && isPlayerProjectile)
        {
            GenerateAOE();        
        }
    }
    private IEnumerator LifeTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        GenerateAOE();
    }

    private void GenerateAOE()
    {
        Destroy(gameObject);
        AOEHandler aoe =Instantiate(aoePrefab, transform.position,Quaternion.identity);
        aoe.Damage = Damage;
    }
}
