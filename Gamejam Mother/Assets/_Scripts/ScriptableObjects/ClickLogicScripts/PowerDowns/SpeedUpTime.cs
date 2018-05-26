using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerDowns/SpeedUpTime")]
public class SpeedUpTime : OnClickLogic 
{
    public float TimeScale;
    public float SpeedUpLastTime;
    public float PitchChange;

    private void OnEnable()
    {
        UsesCoroutine = true;
    }

    public override IEnumerator RunClickCoroutine()
    {
        SpeedTimeUp();
        yield return new WaitForSecondsRealtime(SpeedUpLastTime);
        Time.timeScale = 1;
    }

    public void SpeedTimeUp()
    {
        Time.timeScale = TimeScale;
        var audio = FindObjectOfType<AudioManager>();
        audio.ChangeMusicPitch(SpeedUpLastTime, PitchChange);
    }
}
