using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;
    
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip enemyFire;
    [SerializeField] private AudioClip enemyIce;
    [SerializeField] private AudioClip enemyLeaf;
    [SerializeField] private AudioClip explosionFire;
    [SerializeField] private AudioClip explosionIce;
    [SerializeField] private AudioClip explosionLeaf;
    [SerializeField] private AudioClip femaleGrunt1;
    [SerializeField] private AudioClip femaleGrunt2;
    [SerializeField] private AudioClip femaleGrunt3;
    [SerializeField] private AudioClip maleGrunt1;
    [SerializeField] private AudioClip maleGrunt2;
    [SerializeField] private AudioClip maleGrunt3;
    [SerializeField] private AudioClip playerGrunt1;
    [SerializeField] private AudioClip playerGrunt2;
    [SerializeField] private AudioClip playerGrunt3;
    [SerializeField] private AudioClip maskChange;
    [SerializeField] private AudioClip playerDead;
    [SerializeField] private AudioClip punchFire;
    [SerializeField] private AudioClip punchIce;
    [SerializeField] private AudioClip punchLeaf;
    [SerializeField] private AudioClip spellFire;
    [SerializeField] private AudioClip spellIce;
    [SerializeField] private AudioClip spellLeaf;

    void DashSound()
    {
        audioSource.PlayOneShot(dash);
    }
    
    void DoorOpenSound()
    {
        audioSource.PlayOneShot(doorOpen);
    }

    void MaskChangeSound()
    {
        audioSource.PlayOneShot(maskChange);
    }

    void PlayerDeadSound()
    {
        audioSource.PlayOneShot(playerDead);
    }

    void EnemyFireSound()
    {
        audioSource.PlayOneShot(enemyFire);
    }

    void EnemyIceSound()
    {
        audioSource.PlayOneShot(enemyIce);
    }

    void EnemyLeafSound()
    {
        audioSource.PlayOneShot(enemyLeaf);
    }
    
    void ExplosionFireSound()
    {
        audioSource.PlayOneShot(explosionFire);
    }

    void ExplosionIceSound()
    {
        audioSource.PlayOneShot(explosionIce);
    }

    void ExplosionLeafSound()
    {
        audioSource.PlayOneShot(explosionLeaf);
    }
    
    void PunchFireSound()
    {
        audioSource.PlayOneShot(punchFire);
    }

    void PunchIceSound()
    {
        audioSource.PlayOneShot(punchIce);
    }

    void PumchLeafSound()
    {
        audioSource.PlayOneShot(punchLeaf);
    }
    
    [ContextMenu("Play Female Grunt")]
    void FemaleGrunt()
    {
        int random = Random.Range(0,3);
        Debug.Log(random);
        switch (random)
        {
            case 0 :
                audioSource.PlayOneShot(femaleGrunt1);
                break;
            case 1 :
                audioSource.PlayOneShot(femaleGrunt2);
                break;
            case 2 :
                audioSource.PlayOneShot(femaleGrunt3);
                break;
        }
        //audioSource.PlayOneShot();
    }

    void MaleGrunt()
    {
        int random = Random.Range(0,3);
        switch (random)
        {
            case 0 :
                audioSource.PlayOneShot(maleGrunt1);
                break;
            case 1 :
                audioSource.PlayOneShot(maleGrunt2);
                break;
            case 2 :
                audioSource.PlayOneShot(maleGrunt3);
                break;
        }
        //audioSource.PlayOneShot();
    }

    void PlayerGrunt()
    {
        int random = Random.Range(0,3);
        switch (random)
        {
            case 0 :
                audioSource.PlayOneShot(playerGrunt1);
                break;
            case 1 :
                audioSource.PlayOneShot(playerGrunt2);
                break;
            case 2 :
                audioSource.PlayOneShot(playerGrunt3);
                break;
        }
        //audioSource.PlayOneShot();
    }
}
