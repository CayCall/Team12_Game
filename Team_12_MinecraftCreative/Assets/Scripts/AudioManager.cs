using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   
    [System.Serializable]
    public struct AudioClipInfo
    {
        public string name;
        public AudioClip clip;
    }
    public List<AudioClipInfo> audioClips;

    private Dictionary<string, AudioClip> audioDictionary;

    public AudioSource audioSource;

    void Awake()
    {
        audioDictionary = new Dictionary<string, AudioClip>();
        foreach (var audioClipInfo in audioClips)
        {
            audioDictionary[audioClipInfo.name] = audioClipInfo.clip;
        }
    }

    // Play sound with optional looping
    public void PlaySound(string soundName, bool loop = false)
    {
        if (audioDictionary.ContainsKey(soundName))
        {
            if (loop)
            {
                audioSource.clip = audioDictionary[soundName];
                audioSource.loop = true;
                audioSource.Play();
            }
            else
            {
                audioSource.PlayOneShot(audioDictionary[soundName]);
            }
        }
        else
        {
            Debug.LogWarning("Sound clip not found: " + soundName);
        }
    }

    // Stop sound if it's playing
    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }
    }
}