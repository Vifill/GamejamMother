using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerDowns/SpeedUpTime")]
public class SpeedUpTime : OnClickLogic 
{
    public float TimeScale;
    public float SpeedUpLastTime;
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
            SpeedTimeUp();
            while (Timer < SpeedUpLastTime)
            {
                Timer += Time.deltaTime;
                Debug.Log(Timer);
                yield return null;
            }
            StopTimeSlow();
        }
    }

    private void SpeedTimeUp()
    {
        Timer = 0;
        Time.timeScale = TimeScale;
        FindObjectOfType<AudioManager>().ChangeMusicPitch(PitchChange);
    }

    private void StopTimeSlow()
    {
        Timer = 0;
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().ChangeMusicPitch(1);
    }
}
