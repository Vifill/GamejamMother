using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindowManager : MonoBehaviour 
{
	private void Start() 
	{
		
	}
	
	private void Update() 
	{
		
	}

    public void CloseWindow()
    {
        OnClose();
        Destroy(gameObject);
    }

    private void OnClose()
    {
        throw new NotImplementedException();
    }

    public void AdButton()
    {

    }
}
