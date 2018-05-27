using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerUps/AutoClicker")]
public class AutoClicker : OnClickLogic
{
    public int NumberOfAutomaticClicks = 7;
    public float TimeBetweenCloses = 0.05f;

    private void OnEnable()
    {
        UsesCoroutine = true;
    }

    public override IEnumerator RunClickCoroutine()
    {
        var tmpPopups = GameController.GetActivePopups().Where(a => !a.name.Contains("SingleMom")).ToList();
        List<PopUpWindowManager> popups;
        if (tmpPopups.Count() <= NumberOfAutomaticClicks)
        {
            popups = tmpPopups.Select(a => a.GetComponent<PopUpWindowManager>()).Reverse().ToList();
        }
        else
        {
            popups = tmpPopups.GetRange(tmpPopups.Count() - NumberOfAutomaticClicks, NumberOfAutomaticClicks).Select(a => a.GetComponent<PopUpWindowManager>()).Reverse().ToList();
        }

        int size = popups.Count();
        for (int i = 0; i < size; i++)
        { 
            popups[i].CloseWindow();
            yield return new WaitForSeconds(TimeBetweenCloses);
        }
    }
}
