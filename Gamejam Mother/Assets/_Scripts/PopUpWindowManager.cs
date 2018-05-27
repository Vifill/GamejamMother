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
    private AudioManager AudioManager;

    private void Start()
    {
        PointsManager = FindObjectOfType<PointsManager>();
        AudioManager = FindObjectOfType<AudioManager>();
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
        if (tag == "Moms")
        {
            PointsManager.AddPoint();
            AudioManager.PlaySFX(AudioManager.Sounds.Noice);
        }
        else if (tag == "Ad")
        {
            AudioManager.PlaySFX(AudioManager.Sounds.Win98Error);
        }
        else if (tag == "Ram")
        {
            AudioManager.PlaySFX(AudioManager.Sounds.Ram);
        }

        FindObjectOfType<GameController>().RunOnClick(ClickLogic);
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
        return gameObject;
    }
}
