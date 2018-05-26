using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PopupSpawner : MonoBehaviour
{
    public SpawnModelConfig SpawnModelConfig;
    public InitialSpawnConfig InitialSpawnConfig;

    private SpawnModel CurrentSpawnModel;
    private List<PopupChanceModel> CurrentPopupChanceModels;
    private Queue<SpawnModelTimerModel> SpawnModelQueue;
    private Canvas Canvas;

    private void Start()
    {
        Canvas = FindObjectOfType<Canvas>();
        SpawnModelQueue = new Queue<SpawnModelTimerModel>(SpawnModelConfig.TimeModels);

        StartCoroutine(PopulateInitialPopups());
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

    private void StartGame()
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
        var popup = Instantiate(pPopupPrefab, Canvas.transform);
        popup.transform.localPosition = GetSpawnLocation(size);
    }

    private Vector2 GetSpawnLocation(Rect pPrefabRect)
    {
        var canvasRect = Canvas.GetComponent<RectTransform>().rect;
        Vector2 limits = new Vector2((canvasRect.width - pPrefabRect.width)/2, (canvasRect.height - pPrefabRect.height)/2);
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