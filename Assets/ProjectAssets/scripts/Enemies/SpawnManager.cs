using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _EnemySpawners;
    [SerializeField] private int _SpawnAmount = 5;
    [SerializeField] private float _SpawnInterval = 1.25f;
    [SerializeField] private float _SpawnIntervalLoweringInterval = 5;
    [SerializeField] private float _SpawnIntervalLoweringAmount = 0.1f;
    [SerializeField] private float _MinSpawnInterval = 1;
    private void Start()
    {
        Invoke("ActivateSpawners",5f);
    }

    public void ActivateSpawners()
    {
        foreach (EnemySpawner spawner in _EnemySpawners) 
        { 
           spawner.StartSpawning(
            _SpawnAmount,_SpawnInterval,
            _SpawnIntervalLoweringInterval,
            _SpawnIntervalLoweringAmount,
            _MinSpawnInterval); 
        }
    }

}