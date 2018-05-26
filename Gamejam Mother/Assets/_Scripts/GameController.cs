using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool CanClick = false;

    private static List<GameObject> ActivePopups = new List<GameObject>();
    private MemoryManager MemoryManager;

    private static GameController GC;

    public static GameController instance
    {
        get
        {
            if (!GC)
            {
                GC = FindObjectOfType(typeof(GameController)) as GameController;

                if (!GC)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
            }

            return GC;
        }
    }

    private void Start()
    {
        MemoryManager = GetComponent<MemoryManager>();
    }

    public static void AddPopup(GameObject pPopup)
    {
        ActivePopups.Add(pPopup);
        instance.MemoryManager.AddMemoryUsage(1);
    }

    public static void RemovePopup(GameObject pPopup)
    {
        ActivePopups.Remove(pPopup);
        instance.MemoryManager.RemoveMemoryUsage(1);
    }

    public static List<GameObject> GetActivePopups()
    {
        return ActivePopups;
    }

    public void RunOnClick(OnClickLogic pOnClickLogic)
    {
        pOnClickLogic.Initialize();
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
