using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject EndScreenPrefab;
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

    public void LoseGame()
    {
        FindObjectOfType<PopupSpawner>().StopSpawn();
        FindObjectOfType<AudioManager>().StopAllSounds();

        //play end sound
        Instantiate(EndScreenPrefab, FindObjectOfType<Canvas>().transform);
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
