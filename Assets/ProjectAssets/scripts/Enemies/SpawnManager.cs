using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _EnemySpawners;

    private void OnEnable()
    {
        //make type precentage based
        ActivateSpawners(EnemyType.Melee);
    }

    public void ActivateSpawners(EnemyType typeToSpawn)
    {
        foreach (EnemySpawner spawner in _EnemySpawners) { spawner.SpawnEnemy(typeToSpawn); }
    }

}