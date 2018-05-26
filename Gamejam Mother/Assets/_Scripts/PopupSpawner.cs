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

    private SpawnModel CurrentSpawnModel;
    private List<PopupChanceModel> CurrentPopupChanceModels;
    private Queue<SpawnModelTimerModel> SpawnModelQueue;
    private Canvas Canvas;
    private Image Background;
    private AudioManager AudioManager;
    private bool IsInitialized;

    public List<GameObject> SpawnedPopups = new List<GameObject>();

    public void Initialize()
    {
        if (!IsInitialized)
        {
            IsInitialized = true;
            Canvas = FindObjectOfType<Canvas>();
            AudioManager = FindObjectOfType<AudioManager>();
            Background = FindObjectOfType<Canvas>().transform.GetChild(0).GetComponent<Image>();
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
            for (int i = 0; i < InitialSpawnConfig.NumberOfInitialPopups; i++)
            {
                var prefab = InitialSpawnConfig.InitialSpawnModel.PopupChanceModel[Random.Range(0, InitialSpawnConfig.InitialSpawnModel.PopupChanceModel.Count())].PopupPrefab;
                SpawnPopup(prefab);
                yield return new WaitForSeconds(InitialSpawnConfig.RateOfSpawn);
            }
            StartGame();
        }
    }

    public void StartGame()
    {
        StartCoroutine(SpawnModelSwitchCoroutine());
        StartCoroutine(SpawnCoroutine());
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
        CurrentSpawnModel = pModel;
        var popupChanceModels = pModel.PopupChanceModel.Where(a => a.PopupPrefab != null).OrderBy(a=> a.ChanceOfSpawn);

        List<PopupChanceModel> tmpList = new List<PopupChanceModel>();
        float totalChance = 0;
        foreach (var model in popupChanceModels)
        {
            totalChance += model.ChanceOfSpawn;
            tmpList.Add(new PopupChanceModel { ChanceOfSpawn = totalChance, PopupPrefab = model.PopupPrefab });
        }

        CurrentPopupChanceModels = tmpList;
    }

    private GameObject GetPrefabToSpawn()
    {
        var totalChance = CurrentPopupChanceModels.Last().ChanceOfSpawn;
        var roll = Random.Range(0, totalChance);
        return CurrentPopupChanceModels.FirstOrDefault(a => roll <= a.ChanceOfSpawn).PopupPrefab;
    }

    private void SpawnPopup(GameObject pPopupPrefab)
    {
        Rect size = pPopupPrefab.GetComponent<RectTransform>().rect;
        var popup = Instantiate(pPopupPrefab, PopUpSpawn);
        popup.transform.localPosition = GetSpawnLocation(size);
        GameController.AddPopup(popup);
        AudioManager.PlaySFX(AudioManager.Sounds.BalloonSound);
    }

    private Vector2 GetSpawnLocation(Rect pPrefabRect)
    {
        var backgroundRect = PopUpSpawn.rect;
        Vector2 limits = new Vector2((backgroundRect.width - pPrefabRect.width)/2, (backgroundRect.height - pPrefabRect.height)/2);
        return new Vector2(Random.Range(-limits.x, limits.x), Random.Range(-limits.y, limits.y));
    }
}

[Serializable]
public class InitialSpawnConfig
{
    public SpawnModel InitialSpawnModel;
    public int NumberOfInitialPopups = 75;
    public float RateOfSpawn = 0.1f;
}