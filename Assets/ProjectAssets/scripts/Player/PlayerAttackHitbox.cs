using System.Collections;
using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public MaskType CurrentMask { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Deal damage to enemy
        }
    }


}
