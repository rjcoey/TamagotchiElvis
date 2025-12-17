using System;
using UnityEngine;

public class AudioEventBus : MonoBehaviour
{
    public static event Action<AudioClip> OnPlayMusic;
    public static event Action<AudioClip> OnPlaySFX;

    public static void RaisePlayMusic(AudioClip musicClip) => OnPlayMusic?.Invoke(musicClip);
    public static void RaisePlaySFX(AudioClip sfxClip) => OnPlaySFX?.Invoke(sfxClip);
}
