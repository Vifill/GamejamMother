using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerUps/SlowDownTime")]
public class SlowDownTime : OnClickLogic
{
    public float TimeScale;
    public float SlowdownLastTime;
    public float PitchChange;

    public static float Timer;

    private void OnEnable()
    {
        UsesCoroutine = true;
    }

    public override IEnumerator RunClickCoroutine()
    {
        if (Timer != 0)
        {
            Timer = 0;
        }
        else
        {
            SlowTimeDown();
            while (Timer < SlowdownLastTime)
            {
                Timer += Time.deltaTime;
                yield return null;
            }
            StopTimeSlow();
        }
    }

    private void StopTimeSlow()
    {
        Timer = 0;
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().ChangeMusicPitch(1);
    }

    private void SlowTimeDown()
    {
        Timer = 0;
        Time.timeScale = TimeScale;
        FindObjectOfType<AudioManager>().ChangeMusicPitch(PitchChange);
    }
}
