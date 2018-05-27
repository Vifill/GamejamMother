using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpStartSound : MonoBehaviour 
{
    private AudioManager AudioManager;
    public AudioManager.Sounds SpawnSound;

    private void Start()
    {
        AudioManager = FindObjectOfType<AudioManager>();
        AudioManager.PlaySFX(SpawnSound);
    }
}
