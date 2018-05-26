using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerUps/SlowDownTime")]
public class SlowDownTime : OnClickLogic
{
    public float TimeScale;
    public float SlowdownLastTime;
    public float PitchChange;

    private void OnEnable()
    {
        UsesCoroutine = true;
    }

    public override IEnumerator RunClickCoroutine()
    {
        SlowTimeDown();
        yield return new WaitForSecondsRealtime(SlowdownLastTime);
        Time.timeScale = 1;
    }

    public void SlowTimeDown()
    {
        Time.timeScale = TimeScale;
        var audio = FindObjectOfType<AudioManager>();
        audio.ChangeMusicPitch(SlowdownLastTime, PitchChange);
    }
}
