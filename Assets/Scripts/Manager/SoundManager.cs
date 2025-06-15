using UnityEngine;

public class SoundManager : PersistentSingleton<SoundManager>
{
    public AudioSource backgroundSource;
    public AudioSource SFXSource;
    [Header("---------- Background Music ----------")]
    public AudioClip combat;
    public AudioClip idle;
    public AudioClip mainMenu;
    public AudioClip loading;
    public AudioClip finishLevel;

    [Header("---------- SFX ------------")]
    public AudioClip hitEnemy;
    public AudioClip projectileOut;
    public AudioClip killEnemy;
    public AudioClip killed;

    public AudioClip rewardBegin;
    public AudioClip rewardEnd;

    public AudioClip popUpOpen;
    public AudioClip popUpClose;

    public AudioClip levelBegin;
    public AudioClip passLevel;
    public AudioClip exitDoor;

    public AudioClip spawnPoint;

    public AudioClip buttonClick;

    public AudioClip orcAxe;
    public AudioClip stoneBossLaser;

    

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBackground(AudioClip clip)
    {
        if(backgroundSource.clip == null ||  backgroundSource.clip != clip)
        {
            backgroundSource.loop = true;
            backgroundSource.clip = clip;
            backgroundSource.Play();
        }
        
    }
}
