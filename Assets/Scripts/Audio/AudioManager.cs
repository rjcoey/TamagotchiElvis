using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip levelMusic;

    void OnEnable()
    {
        AudioEventBus.OnPlayMusic += PlayMusic;
        AudioEventBus.OnPlaySFX += PlaySFX;
    }

    void OnDisable()
    {
        AudioEventBus.OnPlayMusic -= PlayMusic;
        AudioEventBus.OnPlaySFX -= PlaySFX;
    }

    void Start()
    {
        PlayMusic(levelMusic);
    }

    private void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    private void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }
}
