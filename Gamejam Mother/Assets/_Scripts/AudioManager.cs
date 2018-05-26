using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour 
{
    public List<AudioClip> AudioClips = new List<AudioClip>();
    private List<AudioSource> Sources = new List<AudioSource>();
    private AudioSource MusicSource;
    public AudioMixerGroup SfxMixer;
    public AudioMixerGroup MusicMixer;

    private void Start() 
	{
        foreach (var clip in AudioClips)
        {
            var aSource = gameObject.AddComponent<AudioSource>();
            Sources.Add(aSource);
            aSource.clip = clip;
        }
        PlayMainSong();
	}

    private void PlayMainSong()
    {
        MusicSource = Sources.FirstOrDefault();
        MusicSource.outputAudioMixerGroup = MusicMixer;
        MusicSource.Play();
        MusicSource.loop = true;
    }

    public void PlaySFX(AudioClip pAudioClip)
    {
        var availableSource = Sources.Where(a => !a.isPlaying).FirstOrDefault();
        availableSource.outputAudioMixerGroup = SfxMixer;
        availableSource.PlayOneShot(pAudioClip);
    }

    /// <summary>
    /// Slow down or Speed up music
    /// </summary>
    /// <param name="pSlowTime"></param>
    /// <param name="pPitch"></param>
    public void ChangeMusicPitch(float pSlowTime, float pPitch)
    {
        StartCoroutine(PitchMusic(pSlowTime, pPitch));
    }

    private IEnumerator PitchMusic(float pSlowTime, float pPitch)
    {
        float time = 0;
        MusicSource.pitch = pPitch;
        while (time < pSlowTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        MusicSource.pitch = 1;
    }
}
