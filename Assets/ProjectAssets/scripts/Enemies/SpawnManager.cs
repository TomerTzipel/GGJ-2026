using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _EnemySpawners;

    private void Start()
    {
        ActivateSpawners();
    }

    public void ActivateSpawners()
    {
        foreach (EnemySpawner spawner in _EnemySpawners) { spawner.StartSpawning(); }
    }

}