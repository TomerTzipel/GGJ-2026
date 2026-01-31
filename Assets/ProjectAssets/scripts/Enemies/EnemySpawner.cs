using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _SpawnOffset;
    [SerializeField] private Animator _Animator;
    [SerializeField] private GameObject _MeleeEnemyPrefab;
    [SerializeField] private GameObject _RangeEnemyPrefab;

    private int _spawnAmount;
    private float _spawnInterval;
    private float _spawnIntervalLoweringInterval;
    private float _spawnIntervalLoweringAmount;
    private float _minSpawnInterval;

    private WaitForSeconds _waitForSpawnIntervalLoweringInterval;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _SpawnOffset, 0.5f);
    }

    public void StartSpawning(int spawnAmount, float spawnInterval, float spawnIntervalLoweringInterval,float spawnIntervalLoweringAmount,float minSpawnInterval) 
    {
        _spawnAmount = spawnAmount;
        _spawnInterval = spawnInterval;
        _spawnIntervalLoweringInterval = spawnIntervalLoweringInterval;
        _spawnIntervalLoweringAmount = spawnIntervalLoweringAmount;
        _minSpawnInterval = minSpawnInterval;
        _waitForSpawnIntervalLoweringInterval = new WaitForSeconds(spawnIntervalLoweringInterval);

        StartCoroutine(LowerSpawnIntervalCoroutine());

        if (_spawnAmount < 0) { StartCoroutine(InfiniteSpawnCoroutine()); }
        else { StartCoroutine(SpawnEnemyCoroutine()); }
    }

    private IEnumerator LowerSpawnIntervalCoroutine()
    {
        while (_spawnInterval >= _minSpawnInterval)
        {
            yield return _waitForSpawnIntervalLoweringInterval;
            _spawnInterval -= _spawnIntervalLoweringAmount;
            if (_spawnInterval < _minSpawnInterval) _spawnInterval = _minSpawnInterval;
        }
    }

    private IEnumerator InfiniteSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            _Animator.SetTrigger("ToggleOpen");
            SpawnRandom();    
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        int spawnAmount = _spawnAmount;
        while (spawnAmount > 0)
        {
            yield return new WaitForSeconds(_spawnInterval);
            _Animator.SetTrigger("ToggleOpen");
            SpawnRandom();   
            spawnAmount--;
        }
    }

    private void SpawnRandom()
    {
        int chosen = Random.Range(0, 2);
        EnemyType typeToSpawn = (EnemyType)chosen;
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

    }

}