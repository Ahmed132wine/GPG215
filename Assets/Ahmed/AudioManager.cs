using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip playerShootClip;
    [SerializeField] private AudioClip playerDamageClip;
    [SerializeField] private AudioClip enemyDeathClip;
    [SerializeField] private AudioClip powerupPickupClip;

    [Header("Volumes")]
    [Range(0f, 1f)][SerializeField] private float musicVolume = 0.5f;
    [Range(0f, 1f)][SerializeField] private float sfxVolume = 0.8f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource != null) musicSource.volume = musicVolume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;
    }

    private void Start()
    {
        PlayMusic();
    }

    // === Music ===
    public void PlayMusic()
    {
        if (musicSource == null || backgroundMusic == null) return;

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;

        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource == null) return;
        musicSource.Stop();
    }

    // === Public SFX wrappers ===
    public void PlayPlayerShoot()
    {
        PlaySFX(playerShootClip);
    }

    public void PlayPlayerDamage()
    {
        PlaySFX(playerDamageClip);
    }

    public void PlayEnemyDeath()
    {
        PlaySFX(enemyDeathClip);
    }

    public void PlayPowerupPickup()
    {
        PlaySFX(powerupPickupClip);
    }

    // === Core SFX function ===
    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }
}
