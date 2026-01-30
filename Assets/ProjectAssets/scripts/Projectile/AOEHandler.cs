using System.Collections;
using UnityEngine;

public class AOEHandler : MonoBehaviour
{
    [SerializeField] MaskType maskType;
    [SerializeField] float duration;

    private void Awake()
    {
        StartCoroutine(Duration());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        //ENEMY TAKE DAMAGE
    }

    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
