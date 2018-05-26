using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWindowManager : MonoBehaviour
{
    public OnClickLogic ClickLogic;

    public void CloseWindow()
    {
        OnClose();
        Destroy(gameObject);
    }

    private void OnClose()
    {
        //throw new NotImplementedException();
    }

    public void AdButtonClick()
    {
        ClickLogic.RunClickLogic();
    }
}
