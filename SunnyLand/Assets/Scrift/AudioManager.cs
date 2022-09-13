using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSourcer;
    private float musicvolume = 1f;

    private void Start()
    {
        audioSourcer.Play();
    }
    private void Update()
    {
        audioSourcer.volume = musicvolume;
    }
    public void updateVolume(float volume)
    {
        musicvolume = volume;
    }


}
