using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PopupSpawner : MonoBehaviour
{
    public SpawnModelConfig SpawnModelConfig;
    public InitialSpawnConfig InitialSpawnConfig;
    public RectTransform PopUpSpawn;
    public GameObject NotepadPrefab;

    private SpawnModel CurrentSpawnModel;
    private List<PopupChanceModel> CurrentPopupChanceModels;
    private Queue<SpawnModelTimerModel> SpawnModelQueue;
    private AudioManager AudioManager;
    private bool IsInitialized;
    private Coroutine CurrentSpawnCoroutine;

    public void Initialize()
    {
        if (!IsInitialized)
        {
            IsInitialized = true;
            AudioManager = FindObjectOfType<AudioManager>();
            SpawnModelQueue = new Queue<SpawnModelTimerModel>(SpawnModelConfig.TimeModels);
            AudioManager.StartGameAudio();

            GameController.CanClick = false;
            StartCoroutine(PopulateInitialPopups());
            GameController.CanClick = true;
        }
        
    }

    private IEnumerator PopulateInitialPopups()
    {
        if (InitialSpawnConfig.InitialSpawnModel != null && InitialSpawnConfig.InitialSpawnModel.PopupChanceModel.Any())
        {
            var chanceModel = GetChanceModel(InitialSpawnConfig.InitialSpawnModel);
            for (int i = 0; i < InitialSpawnConfig.NumberOfInitialPopups; i++)
            {
                var prefab = GetPrefabToSpawn(chanceModel);
                SpawnPopup(prefab);
                yield return new WaitForSeconds(InitialSpawnConfig.RateOfSpawn);
            }
            StartGame();
        }
    }

    public void StartGame()
    {
        StartCoroutine(SpawnModelSwitchCoroutine());
        CurrentSpawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnModelSwitchCoroutine()
    {
        while (SpawnModelQueue.Any())
        {
            var modelTimerModel = SpawnModelQueue.Dequeue();
            CurrentSpawnModel = modelTimerModel.SpawnModel;
            StartNewSpawnModel(modelTimerModel.SpawnModel);
            yield return new WaitForSeconds(modelTimerModel.TimeToNextModel);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnPopup(GetPrefabToSpawn());
            yield return new WaitForSeconds(CurrentSpawnModel.SpawnRate);
        }
    }

    private void StartNewSpawnModel(SpawnModel pModel)
    {
        InitializeSpawnModel(pModel);
    }

    private void InitializeSpawnModel(SpawnModel pModel)
    {
        CurrentPopupChanceModels = GetChanceModel(pModel);
    }

    public List<PopupChanceModel> GetChanceModel(SpawnModel pSpawnModel)
    {
        CurrentSpawnModel = pSpawnModel;
        var popupChanceModels = pSpawnModel.PopupChanceModel.Where(a => a.PopupPrefab != null).OrderBy(a => a.ChanceOfSpawn);

        List<PopupChanceModel> tmpList = new List<PopupChanceModel>();
        float totalChance = 0;
        foreach (var model in popupChanceModels)
        {
            totalChance += model.ChanceOfSpawn;
            tmpList.Add(new PopupChanceModel { ChanceOfSpawn = totalChance, PopupPrefab = model.PopupPrefab });
        }

        return tmpList;
    }

    private GameObject GetPrefabToSpawn()
    {
        return GetPrefabToSpawn(CurrentPopupChanceModels);
    }

    public GameObject GetPrefabToSpawn(List<PopupChanceModel> pSpawnModel)
    {
        var totalChance = pSpawnModel.Last().ChanceOfSpawn;
        var roll = Random.Range(0, totalChance);

        return pSpawnModel.FirstOrDefault(a => roll <= a.ChanceOfSpawn).PopupPrefab;
    }

    public void SpawnPopup(GameObject pPopupPrefab, bool pCountAsPopup = true)
    {
        var realPrefab = pPopupPrefab.GetComponent<PopUpWindowManager>().GetPrefab();
        Rect size = realPrefab.transform.Find("Border").GetComponent<RectTransform>().rect;
        realPrefab.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.width);
        realPrefab.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.height);
        var popup = Instantiate(realPrefab, PopUpSpawn);
        popup.transform.localPosition = GetSpawnLocation(size);
        AudioManager.PlaySFX(AudioManager.Sounds.BalloonSound);
        GameController.AddPopup(popup);
    }

    public Vector2 GetSpawnLocation(Rect pBorderRect)
    {
        var backgroundRect = PopUpSpawn.rect;

        Vector2 limits = new Vector2((backgroundRect.width - pBorderRect.width)/2, (backgroundRect.height - pBorderRect.height)/2);
        //return new Vector2(-limits.x, -limits.y);
        //return new Vector2(limits.x, limits.y);
        //return new Vector2(limits.x, 0);
        //return new Vector2(0, limits.y);

        return new Vector2(Random.Range(-limits.x, limits.x), Random.Range(-limits.y, limits.y));
    }

    public void StopSpawn()
    {
        if (CurrentSpawnCoroutine != null)
        {
            StopCoroutine(CurrentSpawnCoroutine);
        }
    }

    public void SpawnNotepad()
    {
        Instantiate(NotepadPrefab, PopUpSpawn);
    }
}

[Serializable]
public class InitialSpawnConfig
{
    public SpawnModel InitialSpawnModel;
    public int NumberOfInitialPopups = 75;
    public float RateOfSpawn = 0.1f;
}