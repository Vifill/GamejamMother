using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PopUpWindowManager : MonoBehaviour
{
    public OnClickLogic ClickLogic;
    public List<GameObject> Prefabs;

    public void CloseWindow()
    {
        OnClose();
        Destroy(gameObject);
    }

    private void OnClose()
    {
        GameController.RemovePopup(gameObject);
    }

    public void AdButtonClick()
    {
        FindObjectOfType<GameController>().RunOnClick(ClickLogic);
    }

    public GameObject GetPrefab()
    {
        if (Prefabs != null && Prefabs.Any())
        {
            return Prefabs[Random.Range(0, Prefabs.Count)];
        }
        return gameObject;
    }
}
