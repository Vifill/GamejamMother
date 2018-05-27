using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour 
{
    public List<AudioClip> AudioClips = new List<AudioClip>();
    private List<AudioSource> Sources = new List<AudioSource>();
    private Dictionary<Sounds, AudioClip> Clips = new Dictionary<Sounds, AudioClip>();
    private AudioSource MusicSource;
    public AudioMixerGroup SfxMixer;
    public AudioMixerGroup MusicMixer;

    private void Start() 
	{
        int clipsIndex = 0;
        foreach (var clip in AudioClips)
        {
            var aSource = gameObject.AddComponent<AudioSource>();
            Sources.Add(aSource);


            Clips.Add((Sounds)clipsIndex, clip);
            clipsIndex++;

        }
        PlaySFX(Sounds.StartUpSound);
    }

    public void StartGameAudio()
    {
        StartCoroutine(StartingGame());
    }

    private IEnumerator StartingGame()
    {
        var aErrorSource = gameObject.AddComponent<AudioSource>();
        aErrorSource.outputAudioMixerGroup = SfxMixer;
        float time = 0;
        float interval = 0;
        while (time <= 3)
        {
            time += Time.deltaTime;
            interval += Time.deltaTime;
            if (interval >= 0.1f)
            {
                aErrorSource.PlayOneShot(Clips[Sounds.ErrorSound]);
                interval = 0;
            }
            yield return null;
        }

        var aSource = Sources.FirstOrDefault(a => !a.isPlaying);
        aSource.outputAudioMixerGroup = MusicMixer;
        aSource.PlayOneShot(Clips[Sounds.ComputerErrorSong]);
        aSource.loop = true;
        MusicSource = aSource;
    }

    public void PlaySFX(Sounds pSoundEnum)
    {
        var availableSource = Sources.Where(a => !a.isPlaying).ToList();
        if (availableSource.Any())
        {
            var source = availableSource.FirstOrDefault();
            source.outputAudioMixerGroup = SfxMixer;
            source.PlayOneShot(Clips[pSoundEnum]);
        }
        else
        {
            var newSource = gameObject.AddComponent<AudioSource>();
            StartCoroutine(RemoveExtraAudioSource(newSource));
        }
        
    }

    private IEnumerator RemoveExtraAudioSource(AudioSource pSource)
    {
        while (pSource.isPlaying)
        {
            yield return null;
        }
        Destroy(pSource); 
    }

    /// <summary>
    /// Slow down or Speed up music
    /// </summary>
    public void ChangeMusicPitch(float pPitch)
    {
        MusicSource.pitch = pPitch;
    }

    public enum Sounds
    {
        ComputerErrorSong,
        ShutDownSound,
        StartUpSound,
        MouseClickDown,
        MouseClickUp,
        DialUp,
        ErrorSound,
        BalloonSound,
        CongratsYouWon,
        Noice
    }
}
