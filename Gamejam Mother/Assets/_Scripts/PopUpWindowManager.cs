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
    public string Tag;

    private PointsManager PointsManager;

    private void Start()
    {
        PointsManager = FindObjectOfType<PointsManager>();
    }

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

        if (tag == "Moms")
        {
            PointsManager.AddPoint();
        }
    }

    public GameObject GetPrefab()
    {
        if (Prefabs != null && Prefabs.Any())
        {
            var prefabToSpawn = Prefabs[Random.Range(0, Prefabs.Count)];

            if (!string.IsNullOrEmpty(Tag))
            {
                prefabToSpawn.tag = Tag;
            }

            return prefabToSpawn;

            
        }
        else if (!string.IsNullOrEmpty(Tag))
        {
            gameObject.tag = Tag;
        }

        return gameObject;
    }
}
