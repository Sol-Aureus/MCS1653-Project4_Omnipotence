using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;

    // Method to set the master volume level
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);
    }

    // Method to set the sound effects volume level
    public void SetSoundVolume(float level)
    {
        audioMixer.SetFloat("SoundVolume", level);
    }

    // Method to set the music volume level
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
    }
}
