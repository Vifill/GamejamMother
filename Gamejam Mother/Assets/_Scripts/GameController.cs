using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool CanClick = false;

    private static List<GameObject> ActivePopups = new List<GameObject>();
    
    public static void AddPopup(GameObject pPopup)
    {
        ActivePopups.Add(pPopup);
    }

    public static void RemovePopup(GameObject pPopup)
    {
        ActivePopups.Remove(pPopup);
    }

    public static List<GameObject> GetActivePopups()
    {
        return ActivePopups;
    }

    public void RunOnClick(OnClickLogic pOnClickLogic)
    {
        if (pOnClickLogic.UsesCoroutine)
        {
            StartCoroutine(pOnClickLogic.RunClickCoroutine());
        }
        else
        {
            pOnClickLogic.RunClickLogic();
        }
    }
}
