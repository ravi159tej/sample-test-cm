using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public AudioSource sound;
    public AudioClip FlipSound, Levelcomplete, WrongMatch;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnLevelComplete += Playlcsound;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelComplete -= Playlcsound;
    }

    public void Playlcsound()
    {
        sound.clip = Levelcomplete;
        sound.Play();
    }
    public void PlayFlipsound()
    {
        sound.clip = FlipSound;
        sound.Play();
    }
    public void PlayWorngMatchsound()
    {
        sound.clip = WrongMatch;
        sound.Play();
    }
}
