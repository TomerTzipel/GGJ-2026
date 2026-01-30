using System.Collections;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private MaskType MaskType;
    [SerializeField] private AOEHandler aoePrefab;
    public Vector2 MoveDirection { get; set; }
    public bool IsPlayerProjectile { get; set; }

    private void Start()
    {
        float angle = Mathf.Atan2(MoveDirection.y, MoveDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        StartCoroutine(LifeTime(lifeTime));
    }

    private void Update()
    {
        Debug.Log(moveSpeed);
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !IsPlayerProjectile)
        {
            //Player Takes Damage
        }

        if (collision.CompareTag("Enemy") && IsPlayerProjectile)
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
        Instantiate(aoePrefab, transform.position,Quaternion.identity);
    }
}
