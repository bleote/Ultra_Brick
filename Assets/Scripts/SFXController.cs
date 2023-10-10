using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static void PlaySound(string sfx)
    {
        GameObject soundObject = GameObject.FindGameObjectWithTag(sfx);
        AudioSource soundAudioSource = soundObject.GetComponent<AudioSource>();
        soundAudioSource.Play();
    }

    public static void StopSound(string sfx)
    {
        GameObject soundObject = GameObject.FindGameObjectWithTag(sfx);
        AudioSource soundAudioSource = soundObject.GetComponent<AudioSource>();
        soundAudioSource.Stop();
    }
}
