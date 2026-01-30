using System.Collections;
using UnityEngine;



public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    public Quaternion Rotation { get; set; }
    public Vector2 MoveDirection { get; set; }
    public bool IsPlayerProjectile { get; set; }
    public MaskType CurrentMask { get; set; }

    private void Start()
    {
        StartCoroutine(LifeTime(lifeTime));
    }

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * MoveDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !IsPlayerProjectile)
        {
            //Player Takes Damage
        }

        if (collision.CompareTag("Enemy") && IsPlayerProjectile)
        {
            //Create AOE Blast with mask type
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
    }
}
