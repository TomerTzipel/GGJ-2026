using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [FormerlySerializedAs("audioSource")] [SerializeField]
    private AudioSource audioSourceSFX;

    [SerializeField] private AudioSource audioSourceMusic;

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

    [SerializeField] private AudioClip musicIntro;
    [SerializeField] private AudioClip musicNoIntro;
    
    private void Start()
    {
        StartCoroutine(PlayMusic());
    }

    void DashSound()
    {
        audioSourceSFX.PlayOneShot(dash);
    }

    void DoorOpenSound()
    {
        audioSourceSFX.PlayOneShot(doorOpen);
    }

    void MaskChangeSound()
    {
        audioSourceSFX.PlayOneShot(maskChange);
    }

    void PlayerDeadSound()
    {
        audioSourceSFX.PlayOneShot(playerDead);
    }

    void EnemyFireSound()
    {
        audioSourceSFX.PlayOneShot(enemyFire);
    }

    void EnemyIceSound()
    {
        audioSourceSFX.PlayOneShot(enemyIce);
    }

    void EnemyLeafSound()
    {
        audioSourceSFX.PlayOneShot(enemyLeaf);
    }

    void ExplosionFireSound()
    {
        audioSourceSFX.PlayOneShot(explosionFire);
    }

    void ExplosionIceSound()
    {
        audioSourceSFX.PlayOneShot(explosionIce);
    }

    void ExplosionLeafSound()
    {
        audioSourceSFX.PlayOneShot(explosionLeaf);
    }

    void PunchFireSound()
    {
        audioSourceSFX.PlayOneShot(punchFire);
    }

    void PunchIceSound()
    {
        audioSourceSFX.PlayOneShot(punchIce);
    }

    void PumchLeafSound()
    {
        audioSourceSFX.PlayOneShot(punchLeaf);
    }

    [ContextMenu("Play Female Grunt")]
    void FemaleGrunt()
    {
        int random = Random.Range(0, 3);
        Debug.Log(random);
        switch (random)
        {
            case 0:
                audioSourceSFX.PlayOneShot(femaleGrunt1);
                break;
            case 1:
                audioSourceSFX.PlayOneShot(femaleGrunt2);
                break;
            case 2:
                audioSourceSFX.PlayOneShot(femaleGrunt3);
                break;
        }
    }

    void MaleGrunt()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                audioSourceSFX.PlayOneShot(maleGrunt1);
                break;
            case 1:
                audioSourceSFX.PlayOneShot(maleGrunt2);
                break;
            case 2:
                audioSourceSFX.PlayOneShot(maleGrunt3);
                break;
        }
    }

    void PlayerGrunt()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                audioSourceSFX.PlayOneShot(playerGrunt1);
                break;
            case 1:
                audioSourceSFX.PlayOneShot(playerGrunt2);
                break;
            case 2:
                audioSourceSFX.PlayOneShot(playerGrunt3);
                break;
        }
    }

    private IEnumerator PlayMusic()
    {
        audioSourceMusic.PlayOneShot(musicIntro);
        yield return new WaitForSecondsRealtime(musicIntro.length - 0.3f);
            audioSourceMusic.clip = musicNoIntro;
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();
    }
    
}
