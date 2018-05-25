using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    public SpawnModelConfig SpawnModelConfig;
    public SpawnModel CurrentSpawnModel;

    private List<PopupChanceModel> CurrentPopupChanceModels;
    private Queue<SpawnModelTimerModel> SpawnModelQueue;
    private Canvas Canvas;

    private void StartGame()
    {
        SpawnModelQueue = new Queue<SpawnModelTimerModel>(SpawnModelConfig.TimeModels);
        Canvas = FindObjectOfType<Canvas>();

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
    }

    private GameObject GetPrefabToSpawn()
    {
        var totalChance = CurrentPopupChanceModels.Last().ChanceOfSpawn;
        var roll = Random.Range(0, totalChance);
        return CurrentPopupChanceModels.FirstOrDefault(a => roll <= a.ChanceOfSpawn).PopupPrefab;
    }

    private void SpawnPopup(GameObject pPopupPrefab)
    {
        Instantiate(pPopupPrefab, Canvas.transform);
    }
}
