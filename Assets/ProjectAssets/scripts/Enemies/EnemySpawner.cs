using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _SpawnOffset;
    [SerializeField] private int _SpawnAmount = 5;
    [SerializeField] private float _SpawnInterval = 1.25f;
    [SerializeField] private Animator _Animator;
    [SerializeField] private GameObject _MeleeEnemyPrefab;
    [SerializeField] private GameObject _RangeEnemyPrefab;

    private WaitForSeconds _waitForSpawnInterval;

    private void Awake() { _waitForSpawnInterval = new WaitForSeconds(_SpawnInterval); }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _SpawnOffset, 0.5f);
    }

    public void SpawnEnemy(EnemyType typeToSpawn) { StartCoroutine(SpawnEnemyCoroutine(typeToSpawn)); }

    private IEnumerator SpawnEnemyCoroutine(EnemyType typeToSpawn)
    {
        int spawnAmount = _SpawnAmount;
        while (spawnAmount > 0)
        {
            _Animator.SetTrigger("ToggleOpen");
            switch (typeToSpawn)
            {
                case EnemyType.Melee:
                    Instantiate(_MeleeEnemyPrefab, transform.position + _SpawnOffset, Quaternion.identity);
                    break;
                case EnemyType.Ranged:
                    Instantiate(_RangeEnemyPrefab, transform.position + _SpawnOffset, Quaternion.identity);
                    break;
                default:
                    Debug.LogWarning("Unknown enemy type, no spawn");
                    break;
            }

            yield return _waitForSpawnInterval;
            spawnAmount--;
        }
    }

}