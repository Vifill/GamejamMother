using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopIconDoubleClick : MonoBehaviour 
{
    public float DoubleclickThreshhold;
    private int ClickCount;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerClick();
        }
    }

    private void OnPointerClick()
    {
        ClickCount++;
        if (ClickCount == 2)
        {
            print("double click!");
            ClickCount = 0;
        }
        else
        {
            StartCoroutine(TickDown());
        }
    }

    private IEnumerator TickDown()
    {
        yield return new WaitForSeconds(DoubleclickThreshhold);
        if (ClickCount > 0)
        {
            ClickCount--;
        }
    }
}
